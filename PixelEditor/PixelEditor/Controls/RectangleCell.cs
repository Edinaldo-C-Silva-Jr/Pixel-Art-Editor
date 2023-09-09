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
            this.Size = new Size(30, 30);
            this.Dock = DockStyle.Fill;
            this.Margin = new Padding(0, 0, 0, 0);
            this.BackColor = Color.White;

            InitializeComponent();
        }
    }
}
