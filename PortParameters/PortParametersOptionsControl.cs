using System;
using System.Windows.Forms;

namespace DataViewer.PortParameters
{
    public partial class PortParametersOptionsControl : UserControl
    {

        #region - Private Fields -

        private readonly PortParametersViewerOptions _options;

        #endregion

        //********************************************************************************

        #region - Constructor -

        public PortParametersOptionsControl()
        {
            InitializeComponent();
        }

        #endregion

        //********************************************************************************

        #region Implementation of IDataViewer

        public PortParametersOptionsControl(ref IViewerOptions options)
        {
            InitializeComponent();
            _options = (PortParametersViewerOptions)options;
            Text = "Port Parameters";
            EnableViewerCheckBox.Checked = _options.IsAvailable;
            ShowFilepathsCheckBox.Checked = _options.ShowFilePathes;
            ParamTypeComboBox.SelectedItem = _options.ParamType;
            switch (_options.ViewType)
            {
                case ViewType.Ang:
                    radioButtonAng.Checked = true;
                    break;
                case ViewType.Ma:
                    radioButtOnMa.Checked = true;
                    break;
                case ViewType.MaDb:
                    radioButtonMaDb.Checked = true;
                    break;
                default:
                    throw new ArgumentException("Непредусмотренное значение перечисления");
            }
        }

        public PortParametersViewerOptions GetOptions()
        {
            return _options;
        }

        #endregion

        //********************************************************************************

        #region - Event Handler -

        private void ParamTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _options.ParamType = (ParamType)Enum.Parse(typeof(ParamType), ParamTypeComboBox.Text); 
        }
        private void RadioButtonCheckedChanged(object sender, EventArgs e)
        {
            _options.ViewType = (radioButtOnMa.Checked) ? ViewType.Ma : ((radioButtonAng.Checked) ? ViewType.Ang : ViewType.MaDb);
        }

        private void enableCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            _options.IsAvailable = EnableViewerCheckBox.Checked;
        }
        #endregion

    }
}
