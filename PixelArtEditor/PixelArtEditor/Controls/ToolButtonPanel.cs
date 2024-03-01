namespace PixelArtEditor.Controls
{
    public partial class ToolButtonPanel : Panel
    {
        private int CurrentButton;

        public ToolButtonPanel()
        {
            CurrentButton = 0;
            InitializeComponent();
        }

        public void ChangeCurrentButton(ToolButton button)
        {
            CurrentButton = button.ToolValue;
        }

        public (bool start, bool end, bool size) CheckToolButtonProperties()
        {
            if (Controls[CurrentButton] is ToolButton button)
            {
                bool start = button.UseStartingPoint;
                bool end = button.UseEndPoint;
                bool size = button.UseImageSize;
                return (start, end, size);
            }

            return (false, false, false);
        }
    }
}
