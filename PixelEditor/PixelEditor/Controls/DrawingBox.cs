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

        public void GenerateNewImage(int xSize, int ySize, int cellSize, int gridType)
        {
            NewImage(xSize, ySize);

            switch (gridType)
            {
                case 1: // Line Grid
                    {
                        GenerateLineGrid(cellSize);
                        break;
                    }
                case 2: // Checkered Grid
                    {
                        GenerateCheckerGrid(cellSize);
                        break;
                    }
            }
        }

        private void NewImage(int xSize, int ySize)
        {
            this.Width = xSize;
            this.Height = ySize;
            this.Image = new Bitmap(xSize, ySize);
        }

        private void GenerateLineGrid(int cellSize)
        {
            Graphics gridGenerator = Graphics.FromImage(this.Image);
            Pen linePen = new Pen(Color.Gray);

            for (int x = 1; x < this.Image.Height / cellSize; x++)
            {
                gridGenerator.DrawLine(linePen, 0, x * cellSize, this.Width, x * cellSize);
            }

            for (int y = 1; y < this.Image.Width / cellSize; y++)
            {
                gridGenerator.DrawLine(linePen, y * cellSize, 0, y * cellSize, this.Height);
            }
        }

        private void GenerateCheckerGrid(int cellSize)
        {
            Graphics gridGenerator = Graphics.FromImage(this.Image);
            Brush whiteBrush = new SolidBrush(Color.White);
            Brush grayBrush = new SolidBrush(Color.LightGray);

            bool white = true;

            for (int y = 0; y < this.Image.Height / cellSize; y++)
            {
                for (int x = 0; x < this.Image.Width / cellSize; x++)
                {
                    if (white)
                    {
                        gridGenerator.FillRectangle(whiteBrush, cellSize * x, cellSize * y, cellSize, cellSize);
                    }
                    else
                    {
                        gridGenerator.FillRectangle(grayBrush, cellSize * x, cellSize * y, cellSize, cellSize);
                    }

                    white = !white;
                }
                if ((this.Image.Width / cellSize) % 2 == 0 )
                {
                    white = !white;
                }
            }
        }

        public void DrawPixel(int xPos, int yPos, int pixelSize, Color pixelColor, int gridType)
        {
            Graphics pixelDraw = Graphics.FromImage(this.Image);
            Brush pixelBrush = new SolidBrush(pixelColor);

            if (gridType == 1)
            {
                // Gets the correct position of the rectangle from the mouse position, then fills it with a 1 pixel offset in order to not affect the grid
                pixelDraw.FillRectangle(pixelBrush, xPos - xPos % pixelSize + 1, yPos - yPos % pixelSize + 1, pixelSize - 1, pixelSize - 1);
            }
            else
            {
                // Gets the correct position of the rectangle from the mouse position, the 1 pixel offsets aren't needed for a checkers or empty grid
                pixelDraw.FillRectangle(pixelBrush, xPos - xPos % pixelSize, yPos - yPos % pixelSize, pixelSize, pixelSize);
            }
        }
    }
}
