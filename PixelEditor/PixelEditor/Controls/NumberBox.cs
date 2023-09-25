using System;
using System.Windows.Forms;

namespace PixelEditor.Controls
{
    public partial class NumberBox : NumericUpDown
    {
        public NumberBox()
        {
            InitializeComponent();
            this.Controls[0].Enabled = false;
            this.Controls[0].Visible = false;
        }

        protected override void OnTextBoxResize(object source, EventArgs e)
        {
            Controls[1].Width = Width - 4;
        }
    }
}
