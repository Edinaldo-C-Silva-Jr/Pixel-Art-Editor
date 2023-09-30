using System;
using System.Drawing;
using System.Windows.Forms;

namespace PixelEditor.Controls
{
    public partial class DrawingBox : PictureBox
    {
        private Bitmap imageWithGrid = new Bitmap(1, 1);
        private Bitmap lineGrid = new Bitmap(1, 1);

        public DrawingBox()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
        }

        public void SetNewImage(Bitmap originalImage, int cellSize, int gridType, Color gridColor, bool transparent)
        {
            this.Width = originalImage.Width;
            this.Height = originalImage.Height;

            imageWithGrid = new Bitmap(originalImage);
            this.Image = imageWithGrid;

            switch (gridType)
            {
                case 1: // Line Grid
                    {
                        if (cellSize > 2)
                        {
                            GenerateLineGrid(cellSize, gridColor);
                        }
                        break;
                    }
                case 2: // Checkered Grid
                    {
                        GenerateCheckerGrid(cellSize, gridColor);
                        break;
                    }
            }
        }

        private void GenerateLineGrid(int cellSize, Color gridColor)
        {
            lineGrid = new Bitmap(this.Width, this.Height);
            lineGrid.MakeTransparent();

            Graphics gridGenerator = Graphics.FromImage(lineGrid);
            Pen linePen = new Pen(gridColor);

            for (int x = 1; x < this.Height / cellSize + 1; x++)
            {
                gridGenerator.DrawLine(linePen, 0, x * cellSize - 1, this.Width, x * cellSize - 1);
            }

            for (int y = 1; y < this.Width / cellSize + 1; y++)
            {
                gridGenerator.DrawLine(linePen, y * cellSize - 1, 0, y * cellSize - 1, this.Height);
            }

            Graphics gridMerger = Graphics.FromImage(this.Image);
            gridMerger.DrawImage(lineGrid, 0, 0);
        }

        private void GenerateCheckerGrid(int cellSize, Color gridColor)
        {
            int pieceSize = 32;

            Graphics gridGenerator = Graphics.FromImage(this.Image);
            Brush whiteBrush = new SolidBrush(Color.White);
            Brush gridBrush = new SolidBrush(gridColor);

            Bitmap gridPiece = new Bitmap(pieceSize * cellSize, pieceSize * cellSize);
            Graphics gridPieceBuilder = Graphics.FromImage(gridPiece);

            gridPieceBuilder.FillRectangle(whiteBrush, 0, 0, gridPiece.Width, gridPiece.Height);
            for (int y = 0; y < pieceSize; y++)
            {
                int xStart = y % 2;
                for (int x = xStart; x < pieceSize; x += 2)
                {
                    gridPieceBuilder.FillRectangle(gridBrush, cellSize * x, cellSize * y, cellSize, cellSize);
                }
            }

            for (int y = 0; y < this.Height / cellSize; y += pieceSize)
            {
                for (int x = 0; x < this.Width / cellSize; x += pieceSize)
                {
                    gridGenerator.DrawImage(gridPiece, cellSize * x, cellSize * y);
                }
            }
        }

        public Bitmap DrawPixelByPosition(Bitmap image, int xPosPixel, int yPosPixel, int pixelSize, Color pixelColor, int gridType)
        {
            int xPos = pixelSize * xPosPixel;
            int yPos = pixelSize * yPosPixel;

            return DrawPixel(image, xPos, yPos, pixelSize, pixelColor, gridType);
        }

        public Bitmap DrawPixelByClick(Bitmap image, int xPosMouse, int yPosMouse, int pixelSize, Color pixelColor, int gridType)
        {
            int xPos = xPosMouse - xPosMouse % pixelSize;
            int yPos = yPosMouse - yPosMouse % pixelSize;

            return DrawPixel(image, xPos, yPos, pixelSize, pixelColor, gridType);
        }

        private Bitmap DrawPixel(Bitmap image, int xPos, int yPos, int pixelSize, Color pixelColor, int gridType)
        {
            Graphics pixelDraw = Graphics.FromImage(image);
            Graphics gridDraw = Graphics.FromImage(imageWithGrid);
            Brush pixelBrush = new SolidBrush(pixelColor);

            // Gets the correct position of the rectangle from the mouse position, the 1 pixel offsets aren't needed for a checkers or empty grid
            pixelDraw.FillRectangle(pixelBrush, xPos, yPos, pixelSize, pixelSize);
            gridDraw.FillRectangle(pixelBrush, xPos, yPos, pixelSize, pixelSize);

            if (gridType == 1)
            {
                gridDraw.DrawImage(lineGrid, 0, 0);
            }

            this.Image = imageWithGrid;

            return image;
        }
    }
}
