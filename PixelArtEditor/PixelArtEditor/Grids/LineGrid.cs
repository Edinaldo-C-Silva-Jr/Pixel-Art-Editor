namespace PixelArtEditor.Grids
{
    internal class LineGrid : IGridGenerator
    {
        Bitmap lineGridPiece = new(1, 1);
        Bitmap lineGridSinglePixel = new(1, 1);

        public void GenerateGrid(Bitmap originalImage, int cellSize, Color gridColor)
        {
            int gridPieceWidth = DefineGridPieceSize(originalImage.Width / cellSize);
            int gridPieceHeight = DefineGridPieceSize(originalImage.Height / cellSize);

            lineGridPiece = new Bitmap(gridPieceWidth, gridPieceHeight);
            lineGridPiece.MakeTransparent();

            using Pen gridPen = new(gridColor);
            using Graphics gridBuilder = Graphics.FromImage(lineGridPiece);

            for (int x = 1; x < gridPieceHeight / cellSize + 1; x++)
            {
                gridBuilder.DrawLine(gridPen, 0, cellSize * x - 1, gridPieceHeight, cellSize * x - 1);
            }
            for (int y = 1; y < gridPieceWidth / cellSize + 1; y++)
            {
                gridBuilder.DrawLine(gridPen, cellSize * y - 1, 0, cellSize * y - 1, gridPieceWidth);
            }

            lineGridSinglePixel = new Bitmap(cellSize, cellSize);
            using Graphics gridPixelBuilder = Graphics.FromImage(lineGridSinglePixel);
            gridPixelBuilder.DrawLine(gridPen, 0, cellSize - 1, cellSize - 1, cellSize - 1);
            gridPixelBuilder.DrawLine(gridPen, cellSize - 1, 0, cellSize - 1, cellSize - 1);
        }

        private int DefineGridPieceSize(int sizePixelLength)
        {
            int amountOfIterations = 10000, gridPieceSize, sizeWithLeastIterations = 0, amountOfGridPieces;

            for (int i = 1; i < Math.Sqrt(sizePixelLength) + 1; i++)
            {
                gridPieceSize = sizePixelLength / i;
                if (sizePixelLength % i != 0)
                {
                    gridPieceSize++;
                }

                if (sizePixelLength % gridPieceSize != 0)
                {
                    amountOfGridPieces = (int)Math.Pow(sizePixelLength / gridPieceSize + 1, 2);
                }
                else
                {
                    amountOfGridPieces = (int)Math.Pow(sizePixelLength / gridPieceSize, 2);
                }

                int totalIterations = amountOfGridPieces + gridPieceSize * 2;
                if (totalIterations < amountOfIterations)
                {
                    amountOfIterations = totalIterations;
                    sizeWithLeastIterations = gridPieceSize;
                }
            }

            return sizeWithLeastIterations;
        }

        public Bitmap ApplyGridFullImage(Bitmap originalImage)
        {
            using Graphics gridMerger = Graphics.FromImage(originalImage);
            for (int y = 0; y < originalImage.Height / lineGridPiece.Height; y++)
            {
                for (int x = 0; x < originalImage.Width / lineGridPiece.Width; x++)
                {
                    gridMerger.DrawImage(lineGridPiece, lineGridPiece.Width * x, lineGridPiece.Height * y);
                }
            }
            return originalImage;
        }

        public Bitmap ApplyGridSinglePixel(Bitmap originalImage, int xPosition, int yPosition)
        {
            using Graphics lineGridPixelMerger = Graphics.FromImage(originalImage);
            lineGridPixelMerger.DrawImage(lineGridSinglePixel, xPosition, yPosition);
            return originalImage;
        }
    }
}
