using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DataViewer.Common;

namespace DataViewer.PortParameters
{
    class PortParametersViewer: MultipleFilesViewerBase
    {
        //********************************************************************************

        #region - Private Fields -

        private readonly string[] _supportedFileTypes = { ".s2p", ".z2p", ".y2p", ".s1p", ".z1p", ".y1p", ".s3p", ".z3p", ".y3p", ".s4p", ".z4p", ".y4p" };
        private readonly string _caption = @"Port Parameters";
        private readonly string _fileFilter = @"1-, 2-, 3-, 4-Port Parameters (*.s?p, *.z?p, *.y?p)|*.s2p; *.z2p; *.y2p; *.s1p; *.z1p; *.y1p; *.s3p; *.z3p; *.y3p; *.s4p; *.z4p; *.y4p";
        private PortParametersGraphicsControl _viewerControl = new PortParametersGraphicsControl();

        #endregion

        //********************************************************************************

        #region Implementation of IDataViewer

        /// <summary>
        /// Заголовок просмотрщика данных.
        /// </summary>
        public override string Caption{get { return _caption; }}

        /// <summary>
        /// Фильтр расширений файлов, отображаемых на просмотрщике
        /// </summary>
        public override string FileFilter{get { return _fileFilter; }}

        protected override string[] SupportedFileTypes { get { return _supportedFileTypes; } }

        protected override IViewerControl ViewerControl { get { return _viewerControl; } }

        /// <summary>
        /// Опции вьювера.
        /// </summary>
        public override IViewerOptions Options
        {
            get
            {
                return new PortParametersViewerOptions
                {
                    ShowFilePathes = CheckedFileGridView.ShowFilePaths,
                    IsAvailable = IsAvailable,
                    ParamType = _viewerControl.GetValuePair().Key,
                    ViewType = _viewerControl.GetValuePair().Value,
                };
            }
            set
            {
                var options = (PortParametersViewerOptions)value;
                CheckedFileGridView.ShowFilePaths = options.ShowFilePathes;
                _viewerControl.SetTracesSettings(
                    new KeyValuePair<ParamType, ViewType>(options.ParamType, options.ViewType));
                IsAvailable = options.IsAvailable;
            } 
        }

        #endregion

        //********************************************************************************

        #region - Private fields -

        protected override void InitViewMenuBarItems()
        {
            ViewMenuBarItems = new List<ToolStripItem>();
            var showSettingPanel = new ToolStripMenuItem { Text = @"Show Settings Panel", Checked = true, CheckOnClick = true };

            ViewMenuBarItems.Add(showSettingPanel);
            showSettingPanel.CheckedChanged += ShowSettingPanelBarItemClick;
        }

        /// <summary>
        /// Показывание/скрытие панели настроек.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowSettingPanelBarItemClick(object sender, EventArgs e)
        {
            var transferBarItem = (ToolStripMenuItem)sender;
            _viewerControl.groupControlSettings.Visible = transferBarItem.Checked;
            _viewerControl.IsControlPanelVisible = transferBarItem.Checked;
            _viewerControl.ResizeAfterChanges();
        }

        #endregion
    }
}
