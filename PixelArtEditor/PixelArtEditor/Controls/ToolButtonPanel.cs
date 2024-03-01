﻿namespace PixelArtEditor.Controls
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

        public (bool start, bool end, bool size) CheckToolButtonProperties()
        {
            if (Controls[CurrentButton] is ToolButton button)
            {
                bool start = button.UseBeginPoint;
                bool end = button.UseEndPoint;
                bool size = button.UseImageSize;
                return (start, end, size);
            }

            return (false, false, false);
        }
    }
}
