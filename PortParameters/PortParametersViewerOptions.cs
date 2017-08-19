using System.Windows.Forms;

namespace DataViewer.PortParameters
{
    public class PortParametersViewerOptions: IViewerOptions
    {
        /// <summary>
        /// Включение/Отключение Вьювера.
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Показывать пути файлов.
        /// </summary>
        public bool ShowFilePathes { get; set; }

        public void DefaultOptions()
        {
            IsAvailable = true;
            ShowFilePathes = false;
            ParamType = ParamType.S;
            ViewType = ViewType.Ma;
        }

        /// <summary>
        /// Тип параметра.
        /// </summary>
        public ParamType @ParamType { get; set; }

        /// <summary>
        /// Тип отображения.
        /// </summary>
        public ViewType @ViewType { get; set; }
    }
}
