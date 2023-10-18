namespace PixelArtEditor.Grids
{
    internal class CheckerGrid : IGridGenerator
    {
        Bitmap checkerGridPiece = new(1, 1);
        Bitmap checkerGridWhitePixel = new(1, 1);
        Bitmap checkerGridColorPixel = new(1, 1);

        public void GenerateGrid(Bitmap originalImage, int cellSize, Color gridColor)
        {
            int gridPieceWidth = DefineGridPieceSize(originalImage.Width / cellSize);
            int gridPieceHeight = DefineGridPieceSize(originalImage.Height / cellSize);

            checkerGridPiece = new Bitmap(gridPieceWidth, gridPieceHeight);

            using SolidBrush gridBrush = new(gridColor);
            using Graphics gridBuilder = Graphics.FromImage(checkerGridPiece);
            gridBuilder.Clear(Color.White);
            for (int y = 0; y < checkerGridPiece.Height; y++)
            {
                for (int x = y % 2; x < checkerGridPiece.Width; x += 2)
                {
                    gridBuilder.FillRectangle(gridBrush, cellSize*x, cellSize*y, cellSize, cellSize);
                }
            }

            checkerGridWhitePixel = new Bitmap(cellSize, cellSize);

            checkerGridColorPixel = new Bitmap(cellSize, cellSize);
            using Graphics gridPixelBuilder = Graphics.FromImage(checkerGridColorPixel);
            gridPixelBuilder.Clear(gridColor);
        }

        private int DefineGridPieceSize(int sidePixelLength)
        {
            return (int)Math.Sqrt(sidePixelLength);
        }

        public Bitmap ApplyGridFullImage(Bitmap originalImage)
        {
            using Graphics gridMerger = Graphics.FromImage(originalImage);

            for (int y = 0; y < originalImage.Height / checkerGridPiece.Height; y++)
            {
                for (int x = 0; x < originalImage.Width / checkerGridPiece.Width; x++)
                {
                    gridMerger.DrawImage(checkerGridPiece, checkerGridPiece.Width*x, checkerGridPiece.Height*y);
                }
            }

            return originalImage;
        }

        public Bitmap ApplyGridSinglePixel(Bitmap originalImage, int xPosition, int yPosition)
        {
            using Graphics gridPixelMerger = Graphics.FromImage(originalImage);

            int positionParity = (xPosition % 2 + yPosition % 2) % 2;
            if (Convert.ToBoolean(positionParity))
            {
                gridPixelMerger.DrawImage(checkerGridColorPixel, xPosition, yPosition);
            }
            else
            {
                gridPixelMerger.DrawImage(checkerGridWhitePixel, xPosition, yPosition);
            }

            return originalImage;
        }
    }
}
