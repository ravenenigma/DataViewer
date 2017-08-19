using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using DataViewer.Common;
using Indesys.SDK.Common.Unit;
using Indesys.SDK.Graph;
using Indesys.SDK.Graph.Diagrams.GraphicalDiagrams.ConcreteDiagrams.Rectangular;
using Indesys.SDK.Graph.Diagrams.GraphicalDiagrams.ConcreteDiagrams.Smith;
using Indesys.SDK.Graph.Traces;

namespace DataViewer.PortParameters
{
    public partial class PortParametersGraphicsControl : UserControl, IViewerControl
    {
        //********************************************************************************

        #region - Private Fields -

        private PortParametersTraces _traces;

        #endregion

        //********************************************************************************

        #region - Public Fields -

        public bool IsControlPanelVisible { get; set; }
        
        #endregion

        //********************************************************************************

        #region - Public Properties -

        public List<Graph> Graphs
        {
            get { return GraphSetControl.Graphs; }
        }

        private GraphsType _curGraphs;

        /// <summary>
        /// Тип графиков.
        /// </summary>
        public enum GraphsType
        {
            SmithRec,
            Rectangular,
            Smith
        }

        #endregion

        //********************************************************************************

        #region - Constructor -

        public void Clear()
        {
            _traces = new PortParametersTraces();
            GraphSetControl.SuspendDrawing();
            foreach (var t in Graphs)
            {
                t.Traces.Clear();
            }

            GraphSetControl.ResumeDrawing();
        }

        public PortParametersGraphicsControl()
        {
            InitializeComponent();
            GraphSetControl.Visible = false;
            IsControlPanelVisible = true;
            GraphSetInit();
            ResizeAfterChanges();
            GraphSetControl.Visible = true;
        }

        #endregion

        //********************************************************************************

        #region - Public Methods -

        public void ResizeAfterChanges()
        {
            var width = Width - 2 * GraphSetControl.Location.X;
            var height = Height - 2 * GraphSetControl.Location.Y;
            if (IsControlPanelVisible) //Если отображаются настройки.
                height -= groupControlSettings.Height; //Уменьшение ширины справа.
            GraphSetControl.Size = new Size(width, height);
            GraphSetControl.RefreshGraphs();
        }

        /// <summary>
        /// Инициализация графиков на контролле.
        /// </summary>
        public void GraphSetInit()
        {
            _traces = new PortParametersTraces();
            for (var i = 0; i < 4; i++)
            {
                GraphSetControl.Graphs.Add(new Graph());
            }
            Graphs[0].Diagram = new SmithDiagram();
            Graphs[1].Diagram = new SmithDiagram();
            Graphs[2].Diagram = new RectangularDiagram();
            Graphs[3].Diagram = new SmithDiagram();
            GraphSetControl.SuspendDrawing();
            //AddGraph(_curGraphs);
            //Дoбaвляем инициализированные графики в один график
            for (var i = 0; i < 4; i++)
            {
                    if (Graphs[i].Diagram is SmithDiagram)
                    {
                        SmithDiagram(i).Legend.Visible = false;
                    }
                    else
                    {
                        RectangularDiagram(i).Legend.Visible = false;
                        RectangularDiagram(i).Abscissa.Title = "Frequency";
                        RectangularDiagram(i).Abscissa.QuantityType = PhysicalQuantityType.Frequency;
                        RectangularDiagram(i).Abscissa.SignificantDigitsCount = 3;
                        RectangularDiagram(i).Ordinate.SignificantDigitsCount = 3;
                    }
            }
            SetTitles();
            SetOrdinates();
            GraphSetControl.ResumeDrawing();
            GraphSetControl.RefreshGraphs();
            RefreshAllGraphics();
            GraphSetControl.AddMaximize(); //Добавляем Maximize в контекстное меню у графиков.
        }

        public void AddTraceOnGraph(string filename, VisualProperties style)
        {
            var extension = Convert.ToInt32(Path.GetExtension(filename).ToLower().Substring(2,1));
            if (extension == 1 || extension == 3 || extension == 4)
            {
                throw new DemoVersionException
                { 
                     Text= "Port Parameters Viewer supports 2 port measurements only in demo-version.\n" +
                        "Purchase full version of Ellics DataViewer for 1-, 3- and 4-port measurements and other functions."
                };
            }
            _traces.AddTrace(filename, style);
            GraphSetControl.SuspendDrawing();
            foreach (var graph in GraphSetControl.Graphs)
            {
                if (graph.Diagram is SmithDiagram)
                {
                    //OLD_VARIANT:var pair = new KeyValuePair<ParamType, ViewType>(_traces.CurrentParamView.Key, _traces.CurrentParamView.Value);
                    var pair = new KeyValuePair<ParamType, ViewType>(_traces.CurrentParamView.Key, ViewType.Ma);
                    graph.Traces.Add(_traces.TracesList[_traces.TracesList.Count-1][pair][graph.TabIndex]);
                }
                else
                {
                    graph.Traces.Add(_traces.TracesList[_traces.TracesList.Count-1][_traces.CurrentParamView][graph.TabIndex]);
                }
                GraphTools.SetGraphSize(graph, _traces.CurrentParamView.Value);
            }
            GraphSetControl.ResumeDrawing();
        }


        public void UpdateTraceOnGraph(string filename, int index)
        {
            GraphSetControl.SuspendDrawing();
            _traces.UpdateCustomGraph(index);
            GraphSetControl.ResumeDrawing();
            RefreshGraphData();
        }

        /// <summary>
        /// Метод отображения трейсов на графиках.
        /// </summary>
        /// <param name="index">Индекс принимаемого файла.</param>
        /// <param name="visibility">Принимает значение, видимый ли трейс на графике.</param>
        public void ChangeFileVisibility(int index, bool visibility)
        {
            GraphSetControl.SuspendDrawing();
            _traces.ChangeTraceVisibility(index, visibility);
            GraphSetControl.ResumeDrawing();
            RefreshAllGraphics();
        }

        /// <summary>
        /// Удаляет трейсы с графиков поиндексно
        /// </summary>
        /// <param name="selectedIndex">Индекс трейса, для определенного файла.</param>
        public void DeleteTrace(int selectedIndex)
        {
            GraphSetControl.SuspendDrawing();
            _traces.Delete(selectedIndex);
            foreach (var i in GraphSetControl.Graphs)
            {
                i.Traces.RemoveAt(selectedIndex);
                GraphTools.SetGraphSize(i, _traces.CurrentParamView.Value);
            }
            GraphSetControl.ResumeDrawing();
        }

        public Metafile GetMetafileImage()
        {
            return GraphSetControl.GetMetafileImage();
        }

        public void SetTracesSettings(KeyValuePair<ParamType, ViewType> currentParamView)
        {
            _traces.CurrentParamView = currentParamView;
            ParamTypeComboBox.SelectedItem = currentParamView.Key.ToString();
            switch (currentParamView.Value)
            {
                case ViewType.Ma:   radioButtOnMa.Checked = true;
                    break;
                case ViewType.Ang:  radioButtonAng.Checked = true;
                    break;
                case ViewType.MaDb: radioButtonMaDb.Checked = true;
                    break;
                case ViewType.Re:   radioButtonRe.Checked = true;
                    break;
                case ViewType.Im:   radioButtonIm.Checked = true;
                    break;
            }

            RefreshGraphData();
            SetTitles();
            SetOrdinates();
        }

        public KeyValuePair<ParamType, ViewType> GetValuePair()
        {
            return _traces.CurrentParamView;
        }

        public void DisableGraph()
        {
            GraphSetControl.DisableOpenGlGraph();
        }

        /// <summary>
        /// Обновить отрисовку графиков.
        /// </summary>
        public void RefreshAllGraphics()
        {
            foreach (var graph in Graphs)
            {
                GraphTools.SetGraphSize(graph, _traces.CurrentParamView.Value);
            }
            GraphSetControl.Display();
        }

        #endregion

        //********************************************************************************

        #region - Private Methods -

        private RectangularDiagram RectangularDiagram(int graphIndex)
        {
            return (RectangularDiagram) Graphs[graphIndex].Diagram;
        }

        private SmithDiagram SmithDiagram(int graphIndex)
        {
            return (SmithDiagram)Graphs[graphIndex].Diagram;
        }
        
        /// <summary>
        /// Обновить данные на графиках.
        /// </summary>
        private void RefreshGraphData()
        {
            GraphSetControl.SuspendDrawing();
            ClearGraphs();
            foreach (var graph in GraphSetControl.Graphs)
            {
                if ((graph.Diagram is SmithDiagram))
                {
                    //OLD_VARIANT:var pair = new KeyValuePair<ParamType, ViewType>(_traces.CurrentParamView.Key, _traces.CurrentParamView.Value);
                    var pair = new KeyValuePair<ParamType, ViewType>(_traces.CurrentParamView.Key, ViewType.Ma);
                    foreach (var trace in _traces.TracesList)
                    {
                        graph.Traces.Add(trace[pair][graph.TabIndex]);
                    }
                }
                else
                {
                    foreach (var trace in _traces.TracesList)
                    {
                        graph.Traces.Add(trace[_traces.CurrentParamView][graph.TabIndex]);
                    }
                }
                GraphTools.SetGraphSize(graph, _traces.CurrentParamView.Value);
            }
            GraphSetControl.ResumeDrawing();
        }

        /// <summary>
        /// Отображение осей ординат на графиках. 
        /// </summary>
        private void SetOrdinates()
        {
            string innerScope = String.Empty;
            string outerScope = String.Empty;
            var quantity = PhysicalQuantityType.Scalar;
            switch (_traces.CurrentParamView.Value)
            {
                case ViewType.Ma:       innerScope = "|"; outerScope = "|"; break;
                case ViewType.Ang:      innerScope = "Ang("; outerScope = ")"; quantity = PhysicalQuantityType.Angle; break;
                case ViewType.MaDb:     innerScope = "|"; outerScope = "|"; quantity = PhysicalQuantityType.Gain; break;
                case ViewType.Re:       innerScope = "Re ("; outerScope = ")"; break;
                case ViewType.Im:       innerScope = "Im ("; outerScope = ")"; break;
            }
            var parameter = _traces.CurrentParamView.Key.ToString();
            //RectangularDiagram(1).Ordinate.Title = innerScope  + parameter + "12" + outerScope;
            RectangularDiagram(2).Ordinate.Title = innerScope  + parameter + "21" + outerScope;
            //RectangularDiagram(1).Ordinate.QuantityType = RectangularDiagram(2).Ordinate.QuantityType = quantity;
        }

        private void SetTitles()
        {
            SmithDiagram(0).Title = _traces.CurrentParamView.Key + "11";
            //RectangularDiagram(1).Title = string.Empty;
            RectangularDiagram(2).Title = string.Empty;
            SmithDiagram(3).Title = _traces.CurrentParamView.Key + "22";
        }
        
        private void ClearGraphs()
        {
            foreach (var graph in Graphs)
            {
                graph.Traces.Clear();
            }
        }

        #endregion

        //********************************************************************************

        #region - EventArgs -

        private void PortParametrGraphicsControlSizeChanged(object sender, EventArgs e)
        {
            ResizeAfterChanges();
        }


        private void ParamTypeComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            var paramType = (ParamType)Enum.Parse(typeof(ParamType), ParamTypeComboBox.Text);
            _traces.CurrentParamView = new KeyValuePair<ParamType, ViewType>(paramType, _traces.CurrentParamView.Value);
            GraphSetControl.SuspendDrawing();
            RefreshGraphData();
            SetTitles();
            SetOrdinates();
            GraphSetControl.ResumeDrawing();
        }

        private void RadioButtonCheckedChanged(object sender, EventArgs e)
        {
            var viewType = (radioButtOnMa.Checked) ? ViewType.Ma : ((radioButtonAng.Checked) ? ViewType.Ang :
                ((radioButtonMaDb.Checked) ? ViewType.MaDb : ((radioButtonRe.Checked) ? ViewType.Re : ViewType.Im)));
            _traces.CurrentParamView = new KeyValuePair<ParamType, ViewType>(_traces.CurrentParamView.Key, viewType);
            GraphSetControl.SuspendDrawing();
            RefreshGraphData();
            SetOrdinates();
            GraphSetControl.ResumeDrawing();
        }

        private void ViewTipeComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            GraphSetControl.RefreshGraphs();
        }

        #endregion

        private void AddGraph(GraphsType graphsType){}
    }
}