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

        public void ReorganizeButtons()
        {
            foreach(ToolButton c in Controls)
            {
                Controls.SetChildIndex(c, c.ToolValue);
            }
        }

        public void ChangeCurrentButton(ToolButton button)
        {
            CurrentButton = button.ToolValue;
        }

        public Dictionary<string, bool> CheckToolButtonProperties()
        {
            Dictionary<string, bool> properties = new();

            if (Controls[CurrentButton] is ToolButton button)
            {
                properties.Add("BeginPoint", button.UseBeginPoint);
                properties.Add("EndPoint", button.UseEndPoint);
                properties.Add("ImageSize", button.UseImageSize);
                properties.Add("Transparency", button.UseTransparency);
                properties.Add("BackgroundColor", button.UseBackgroundColor);
                properties.Add("PixelSize", button.UsePixelSize);
            }

            return properties;
        }
    }
}
