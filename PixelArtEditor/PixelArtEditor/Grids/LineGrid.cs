namespace PixelArtEditor.Grids
{
    /// <summary>
    /// Implements a line based grid, where every cell is separated by a single pixel thick line of a specified color.
    /// </summary>
    internal class LineGrid : IGridGenerator
    {
        Bitmap? lineGridPiece;
        Bitmap? lineGridSinglePixel;

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

        /// <summary>
        /// Calculates the size to use when generating the grid piece, based on the size of the full image.
        /// This method returns a size that will make grid generation more efficient, by splitting the full grid into smaller pieces that will be copied to fill the image.
        /// The method returns a single dimension, which should match the parameter passed. (If the method was passed a width value, it returns a width value) 
        /// </summary>
        /// <param name="sizePixelLength">The length of the image side, in pixel cells (not counting the zoom)</param>
        /// <returns>The optimized grid piece length for the image passed.</returns>
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
            if (lineGridPiece == null)
            {
                return originalImage;
            }

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
            if (lineGridSinglePixel == null)
            {
                return originalImage;
            }

            using Graphics lineGridPixelMerger = Graphics.FromImage(originalImage);
            lineGridPixelMerger.DrawImage(lineGridSinglePixel, xPosition, yPosition);
            return originalImage;
        }
    }
}
