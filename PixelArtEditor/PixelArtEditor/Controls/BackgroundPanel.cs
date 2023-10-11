namespace PixelArtEditor.Controls
{
    public partial class BackgroundPanel : Panel
    {
        public BackgroundPanel()
        {
            InitializeComponent();
        }

        public void DefineNewSize(int maxWidth, int maxHeight)
        {
            this.SuspendLayout();
            CheckChildControlSize();
            CheckMaximumSize(maxWidth, maxHeight);
            this.ResumeLayout();
        }

        private void CheckChildControlSize()
        {
            int highestWidth = 0, highestHeight = 0;
            int totalChildControlWidth, totalChildControlHeight;

            foreach (Control c in this.Controls)
            {
                totalChildControlWidth = c.Location.X + c.Width;
                if (totalChildControlWidth > highestWidth)
                {
                    highestWidth = totalChildControlWidth;
                }

                totalChildControlHeight = c.Location.Y + c.Height;
                if (totalChildControlHeight > highestHeight)
                {
                    highestHeight = totalChildControlHeight;
                }
            }

            this.Width = highestWidth + 1;
            this.Height = highestHeight + 1;
        }

        public void CheckMaximumSize(int maxWidth, int maxHeight)
        {
            if (this.Width > maxWidth)
            {
                this.Width = maxWidth;
                this.Height += SystemInformation.VerticalScrollBarWidth;
            }
            if (this.Height > maxHeight)
            {
                this.Height = maxHeight;
                this.Width += SystemInformation.HorizontalScrollBarHeight;
            }
        }
    }
}
