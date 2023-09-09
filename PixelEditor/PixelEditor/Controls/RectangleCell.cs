using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelEditor.Controls
{
    public partial class RectangleCell : UserControl
    {
        public RectangleCell()
        {
            this.Dock = DockStyle.Fill;
            this.Size = new Size(30, 30);
            this.Margin = new Padding(0, 0, 0, 0);
            this.BackColor = Color.White;

            InitializeComponent();
        }

        public void SelectCell()
        {
            this.Size = new Size(26, 26);
            this.Margin = new Padding(2, 2, 2, 2);
        }

        public void DeselectCell()
        {
            this.Size = new Size(30, 30);
            this.Margin = new Padding(0, 0, 0, 0);
        }
    }
}
