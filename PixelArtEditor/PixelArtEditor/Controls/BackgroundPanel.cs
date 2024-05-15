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
                // This takes in consideration the child control's X position and its width.
                totalChildControlWidth = child.Location.X + child.Width;
                if (totalChildControlWidth > highestChildWidth)
                {
                    highestChildWidth = totalChildControlWidth;
                }

                // The "Total Height" works like the Total Width, but with the child control's Y position and its height.
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
        /// If the new size exceeds the maximum size and the scroll bar space, it's reduced so scroll bars are enabled.
        /// The size is also adjusted to compensate for the space taken by the scroll bars.
        /// </summary>
        public void CheckMaximumAllowedSize()
        {
            // The rectangle that represents the display area of the control.
            // This is used to get the control's size regardless of the position of the scroll bars.
            Rectangle controlSize = DisplayRectangle;

            // Boolean values to store whether the scroll bars are enabled or not.
            bool xScroll = false, yScroll = false;

            // Checks if the current width and height are bigger than the maximum allowed, including the size of the scroll bars.
            if (controlSize.Width > MaximumWidth + SystemInformation.VerticalScrollBarWidth)
            {
                xScroll = true;
                Width = MaximumWidth;
            }
            if (controlSize.Height > MaximumHeight + SystemInformation.HorizontalScrollBarHeight)
            {
                yScroll = true;
                Height = MaximumHeight;
            }
            
            // Increases the height and width of the panel to compensate for the space taken by scroll bars.
            if (xScroll) 
            { 
                Height += SystemInformation.VerticalScrollBarWidth; 
            }
            if (yScroll)
            {
                Width += SystemInformation.HorizontalScrollBarHeight;
            }
        }
    }
}
