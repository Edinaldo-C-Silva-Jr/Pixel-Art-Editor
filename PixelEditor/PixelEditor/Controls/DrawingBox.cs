using System.Drawing;
using System.Windows.Forms;

namespace PixelEditor.Controls
{
    public partial class DrawingBox : PictureBox
    {
        public DrawingBox()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
        }

        public void NewImage(int xSize, int ySize)
        {
            // The size is set to 1 pixel larger in order to fit the grid outline
            this.Width = xSize + 1;
            this.Height = ySize + 1;

            this.Image = new Bitmap(this.Width, this.Height);
        }

        public void GenerateGrid(int cellSize)
        {
            Graphics gridGenerator = Graphics.FromImage(this.Image);
            Pen linePen = new Pen(Color.Gray);

            for (int x = 0; x < this.Width / cellSize + 1; x++)
            {
                gridGenerator.DrawLine(linePen, 0, x * cellSize, this.Width, x * cellSize);
            }

            for (int y = 0; y < this.Height / cellSize + 1; y++)
            {
                gridGenerator.DrawLine(linePen, y * cellSize, 0, y * cellSize, this.Height);
            }
        }
    }
}
