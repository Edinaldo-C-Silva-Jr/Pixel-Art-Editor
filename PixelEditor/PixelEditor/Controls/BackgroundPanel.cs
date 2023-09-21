using System.Drawing;
using System.Windows.Forms;

namespace PixelEditor.Controls
{
    public partial class BackgroundPanel : Panel
    {
        public BackgroundPanel()
        {
            InitializeComponent();
            this.AutoScroll = true;
            this.AutoScrollMargin = new Size(1, 1);
        }

        public void DefineNewSize(int maxWidth, int maxHeight)
        {
            CheckChildControlSize();
            CheckMaximumSize(maxWidth, maxHeight);
        }

        private void CheckChildControlSize()
        {
            int highestWidth = 0, highestHeight = 0;

            foreach (Control c in this.Controls)
            {
                if (c.Width > highestWidth)
                {
                    highestWidth = c.Width;
                }

                if (c.Height > highestHeight)
                {
                    highestHeight = c.Height;
                }
            }

            this.Width = highestWidth + 2;
            this.Height = highestHeight + 2;
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
