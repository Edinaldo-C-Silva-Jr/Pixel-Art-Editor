using System;
using System.Windows.Forms;

namespace PixelEditor
{
    public partial class frm_PixelEditor : Form
    {
        public frm_PixelEditor()
        {
            InitializeComponent();
        }

        private void frm_PixelEditor_Load(object sender, EventArgs e)
        {
            dbx_ViewingArea.NewImage(256, 256);
            dbx_ViewingArea.GenerateGrid(16);
        }
    }
}
