using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataViewer.Common;
using Indesys.SDK.Common.Unit;
using Indesys.SDK.DataStorage.DataStorages;
using Indesys.SDK.Graph.Traces;
using Indesys.SDK.Math;
using Indesys.SDK.Math.Matrix;
using Indesys.SDK.Modeling.Circuit;

namespace DataViewer.PortParameters
{
    /// <summary>
    /// Тип параметра.
    /// </summary>
    public enum ParamType
    {
        S,
        Y,
        Z
    }

    /// <summary>
    /// Тип отображения.
    /// </summary>
    public enum ViewType
    {
        Ma,
        MaDb,
        Ang,
        Re,
        Im
    }

    /// <summary>
    /// Необходимость обновления.
    /// </summary>
    public enum UpdateType
    {
        None,
        Smith, //Диаграммы Смита рассчитаны.
        Full
    }

    class PortParametersTraces
    {
        #region - Private Fields -

        private readonly StyleIterator _styleIterator = new StyleIterator();

        private readonly List<string> _filenames = new List<string>();

        private readonly List<Dictionary<KeyValuePair<ParamType, ViewType>, UpdateType>> _updateList =
            new List<Dictionary<KeyValuePair<ParamType, ViewType>, UpdateType>>();

        private KeyValuePair<ParamType, ViewType> _curParamView;
        
        #endregion

        //********************************************************************************

        #region - Public Fields -

        /// <summary>
        /// Список всех трейсов.
        /// </summary>
        public readonly List<Dictionary<KeyValuePair<ParamType, ViewType>, List<Trace>>> TracesList =
            new List<Dictionary<KeyValuePair<ParamType, ViewType>, List<Trace>>>();

        

        public KeyValuePair<ParamType, ViewType> CurrentParamView
        {
            get
            {
                return _curParamView;
            }
            set
            {
                if (_curParamView.Key == value.Key && _curParamView.Value == value.Value)
                    return;
                _curParamView = value;

                for (int index = 0; index < TracesList.Count; index++)
                {
                    if (TracesList[index][CurrentParamView][0].VisualProperties.Visible == false)
                    {
                        continue;
                    }
                    if (CheckForUpdate(index) != UpdateType.Full)
                    {
                        UpdateTraces(index);
                    }
                }
            }
        }

        #endregion

        //********************************************************************************

        #region - Constructor -

        public PortParametersTraces()
        {
            _curParamView = new KeyValuePair<ParamType, ViewType>(ParamType.S, ViewType.MaDb);
        }

        #endregion

        //********************************************************************************

        #region - Public Methods -

        public VisualProperties AddTrace(string filename, VisualProperties traceStyle)
        {
            LoadXnPStorage(filename);
//            var traceStyle = _styleIterator.GetNextStyle();
            var file = new Filename(filename);
            traceStyle.Visible = false;
            var tracesFile = new Dictionary<KeyValuePair<ParamType, ViewType>, List<Trace>>();
            var updatesForFile = new Dictionary<KeyValuePair<ParamType, ViewType>, UpdateType>();
            foreach (var param in Enum.GetValues(typeof(ParamType)))
            {
                foreach (var view in Enum.GetValues(typeof(ViewType)))
                {
                    var traces = new List<Trace>();


                    for (int index = 0; index < 4; index++)
                    {
                        var trace = new Trace(file.NameWithoutExtension) { VisualProperties = traceStyle };
                        trace.VisualProperties.Visible = false;
                        trace.Sectors.Add(new TraceSector());
                        traces.Add(trace);
                    }
                    var paramView = new KeyValuePair<ParamType, ViewType>((ParamType)param, (ViewType)view);
                    tracesFile.Add(paramView, traces);
                    updatesForFile.Add(paramView, UpdateType.None);
                }
            }
            TracesList.Add(tracesFile);
            _updateList.Add(updatesForFile);
            _filenames.Add(filename);

            return traceStyle;
        }

        public void UpdateCustomGraph(int index)
        {
            ClearUpdate(index);
            UpdateTraces(index);
        }

        /// <summary>
        /// Удаляет трейсы с графиков поиндексно
        /// </summary>
        /// <param name="selectedIndex">Индекс трейса, для определенного файла.</param>
        public void Delete(int selectedIndex)
        {
            TracesList.RemoveAt(selectedIndex);
            _updateList.RemoveAt(selectedIndex);
            _filenames.RemoveAt(selectedIndex);
            if (_filenames.Count == 0)
            {
                _styleIterator.Reset();
            }
        }


        #endregion

        //********************************************************************************

        #region - Private Methods -

        /// <summary>
        /// Инициализация XnPStorage
        /// </summary>
        private static XnPStorage LoadXnPStorage(string filename)
        {
            var xnPStorage = new XnPStorage();
            try
            {
                xnPStorage.LoadFromFile(filename);
            }
            catch (Exception)
            {
                throw new WrongFileException(Path.GetFileName(filename));
            }
            
            return xnPStorage;
        }

        private void ClearUpdate(int index)
        {
            foreach (var key in _updateList[index].Keys.ToList())
            {
                _updateList[index][key] = UpdateType.None;
            }
        }

        private UpdateType CheckForUpdate(int index)
        {
            foreach (var dict in _updateList[index])
            {
                if (dict.Key.Equals(_curParamView))
                    return dict.Value;
            }
            return UpdateType.None;
        }

        private void UpdateTraces(int index)
        {
            string filename = _filenames[index];
            var frequency = new List<double>(); //Список частот, в который нужно считать частоты из файла.
            var xnPStorage = LoadXnPStorage(filename); //здесь будут загруженные с файла X-параметры
            var signals = xnPStorage.SignalVector;
            var xnPsParameters = new ComplexMatrix2x2[signals.Count];
            for (int i = 0; i < signals.Count; i++)
            {
                xnPsParameters[i] = signals[i].SignalMatrix as ComplexMatrixMxN;
                frequency.Add(signals[i].Frequency);
            }
            var currentMatrixType = (SignalMatrixTypeFlag)Enum.Parse(typeof(SignalMatrixTypeFlag), CurrentParamView.Key.ToString());
            if (xnPStorage.SignalMatrixTypeFlag != currentMatrixType)
            {
                var tempParameters = ParameterConverter.Convert2x2(xnPsParameters, xnPStorage.SignalMatrixTypeFlag,
                    currentMatrixType, 1.0);
                xnPsParameters = tempParameters;
            }
            var smithParam = new KeyValuePair<ParamType, ViewType>(CurrentParamView.Key, ViewType.Ma);
            for (int i = 0; i < TracesList[index][CurrentParamView].Count; i++)
            {
                if (((i == 0) || (i == 3)) && (_updateList[index][CurrentParamView] == UpdateType.None))
                //if (((i == 0) || (i == 3)) && (_updateList[index][smithParam] == UpdateType.None))
                    //TracesList[index][CurrentParamView][i].Sectors[0].Points.Clear();
                    TracesList[index][smithParam][i].Sectors[0].Points.Clear();
                if ((i == 1) || (i == 2))
                {
                    TracesList[index][CurrentParamView][i].Sectors[0].Points.Clear();
                }
            }

            for (int i = 0; i < xnPsParameters.Count(); i++)
            {
                ComplexMatrix2x2 matrix = xnPsParameters[i];
                if (_updateList[index][CurrentParamView] == UpdateType.None)
                {
                    var smithPoint00 = ParameterConverter.Convert1x1(matrix._00, currentMatrixType, SignalMatrixTypeFlag.Z, 1.0);
                    smithPoint00.Im = Math.Sign(smithPoint00.Im)*Math.Max(Math.Abs(smithPoint00.Im), 1e-6);
                    var smithPoint11 = ParameterConverter.Convert1x1(matrix._11, currentMatrixType, SignalMatrixTypeFlag.Z, 1.0);
                    smithPoint11.Im = Math.Sign(smithPoint11.Im) * Math.Max(Math.Abs(smithPoint11.Im), 1e-6);
                    //TracesList[index][CurrentParamView][0].Sectors[0].Points.Add(new TracePoint(frequency[i], smithPoint00));
                    //TracesList[index][CurrentParamView][3].Sectors[0].Points.Add(new TracePoint(frequency[i], smithPoint11)); 
                    TracesList[index][smithParam][0].Sectors[0].Points.Add(new TracePoint(frequency[i], smithPoint00));
                    TracesList[index][smithParam][3].Sectors[0].Points.Add(new TracePoint(frequency[i], smithPoint11));                  
                }
                TracesList[index][CurrentParamView][1].Sectors[0].Points.Add(new TracePoint(frequency[i],
                    CalcDouble(NormalizeCoef(currentMatrixType) * matrix._01, CurrentParamView.Value)));
                TracesList[index][CurrentParamView][2].Sectors[0].Points.Add(new TracePoint(frequency[i],
                    CalcDouble(NormalizeCoef(currentMatrixType) * matrix._10, CurrentParamView.Value)));
            }
            if (xnPsParameters.Count() == 1)
            {
                var point = TracesList[index][smithParam][0].Sectors[0].Points[0];
                TracesList[index][smithParam][0].Sectors[0].Points.Add(new TracePoint(point.Argument * (1.0001), point.Value));
                point = TracesList[index][smithParam][3].Sectors[0].Points[0];
                TracesList[index][smithParam][3].Sectors[0].Points.Add(new TracePoint(point.Argument * (1.0001), point.Value));
            }
            foreach (ViewType view in Enum.GetValues(typeof(ViewType)))
            {
                if (view == CurrentParamView.Value)
                    continue;
                var otherView = new KeyValuePair<ParamType, ViewType>(CurrentParamView.Key, view);
                if (_updateList[index][otherView] == UpdateType.None)
                    _updateList[index][otherView] = UpdateType.Smith;
            }
            _updateList[index][CurrentParamView] = UpdateType.Full;
        }

        private double NormalizeCoef(SignalMatrixTypeFlag type)
        {
            switch (type)
            {
                case SignalMatrixTypeFlag.S :
                    return 1.0;
                case SignalMatrixTypeFlag.Y:
                    return 0.02;
                case SignalMatrixTypeFlag.Z:
                    return 50.0;
                default:
                    throw new ArgumentException("Impossible type of signal");
            }
        }

        private Double CalcDouble(Complex cell, ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Ma:
                    return cell.Magnitude;
                case ViewType.MaDb:
                    return 2 * Math.Log10(Math.Max(cell.Magnitude, 1e-15));
                case ViewType.Ang:
                    return cell.AngleRadian;
                case ViewType.Re:
                    return cell.Re;
                case ViewType.Im:
                    return cell.Im;
            }
            return -1;
        }

        /// <summary>
        /// Смена видимости файла.
        /// </summary>
        /// <param name="index">Индекс принимаемого файла.</param>
        /// <param name="visibility">Принимает значение, видимый ли трейс на графике.</param>
        public void ChangeTraceVisibility(int index, bool visibility)
        {
            if (visibility)
            {
                if (CheckForUpdate(index) != UpdateType.Full)
                {
                    UpdateTraces(index);
                }
            }

            var smithParam = new KeyValuePair<ParamType, ViewType>(CurrentParamView.Key, ViewType.Ma);
            for (int i = 0; i < TracesList[index][CurrentParamView].Count; i++)
            {
                if ((i == 0) || (i == 3))
                    TracesList[index][smithParam][i].VisualProperties.Visible = visibility;
                else
                {
                    TracesList[index][CurrentParamView][i].VisualProperties.Visible = visibility;
                }
            }
        }

        #endregion
    }
}
