namespace PixelArtEditor.Controls
{
    /// <summary>
    /// A panel control that is used as a background for other controls.
    /// It has curstom resize methods to adapt to the controls inside of it.
    /// </summary>
    public partial class BackgroundPanel : Panel
    {
        /// <summary>
        /// The maximum allowed width this control can have. 
        /// Once it reaches this maximum size, growing any further will enable scroll bars.
        /// </summary>
        public int MaximumWidth { get; set; }

        /// <summary>
        /// The maximum allowed height this control can have. 
        /// Once it reaches this maximum size, growing any further will enable scroll bars.
        /// </summary>
        public int MaximumHeight { get; set; }

        public BackgroundPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Resizes the panel in order to fit all controls inside it. Then compares that size to the maximum allowed size. 
        /// If the new size is bigger than the allowed size, reduces the control and enables scroll bars.
        /// </summary>
        public void ResizePanelToFitControls()
        {
            SuspendLayout();

            CheckChildControlSize();
            CheckMaximumAllowedSize();

            ResumeLayout();
        }

        /// <summary>
        /// Checks the size of all controls inside the panel and changes its width and height to fit all of them.
        /// </summary>
        private void CheckChildControlSize()
        {
            int highestChildWidth = 0, highestChildHeight = 0;
            int totalChildControlWidth, totalChildControlHeight;

            foreach (Control child in Controls)
            {
                // The "Total Width" represents the size the panel needs to be to contain the child control.
                // This takes in consideration the child control's position and its width.
                totalChildControlWidth = child.Location.X + child.Width;
                if (totalChildControlWidth > highestChildWidth)
                {
                    highestChildWidth = totalChildControlWidth;
                }

                totalChildControlHeight = child.Location.Y + child.Height;
                if (totalChildControlHeight > highestChildHeight)
                {
                    highestChildHeight = totalChildControlHeight;
                }
            }

            // Makes the panel 1 pixel bigger than the size needed to contain all controls inside it.
            Width = highestChildWidth + 1;
            Height = highestChildHeight + 1;
        }

        /// <summary>
        /// Checks the maximum size of the control after being resized.
        /// If the new size is bigger than the maximum size, it gets reduced so the scroll bars are enabled.
        /// </summary>
        public void CheckMaximumAllowedSize()
        {
            // The rectangle that represents the display area of the control.
            // This is used to get the control's size regardless of the position of the scroll bars.
            Rectangle controlSize = DisplayRectangle;

            // If the current width is bigger than the maximum allowed size...
            if (controlSize.Width > MaximumWidth)
            {
                Width = MaximumWidth; // Reduce the width, which will cause scroll bars to appear...
                Height += SystemInformation.VerticalScrollBarWidth; // And increase the height to compensate for the scroll bars being inside the control.
            }
            if (controlSize.Height > MaximumHeight)
            {
                Height = MaximumHeight;
                Width += SystemInformation.HorizontalScrollBarHeight;
            }
        }
    }
}
