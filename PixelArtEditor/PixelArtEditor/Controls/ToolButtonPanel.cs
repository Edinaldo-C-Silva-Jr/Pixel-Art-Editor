﻿namespace PixelArtEditor.Controls
{
    public partial class ToolButtonPanel : Panel
    {
        public int CurrentButton { get; private set; }

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

        public Dictionary<string, bool> CheckToolPreviewProperties()
        {
            Dictionary<string, bool> properties = new();

            if (Controls[CurrentButton] is ToolButton button)
            {
                properties.Add("PreviewMove", button.PreviewOnMove);
                properties.Add("PreviewHold", button.PreviewOnHold);
            }

            return properties;
        }

        public Dictionary<string, bool> CheckToolDrawProperties()
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
