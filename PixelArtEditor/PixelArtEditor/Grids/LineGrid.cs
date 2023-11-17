namespace PixelArtEditor.Grids
{
    /// <summary>
    /// Implements a line based grid, where every cell is separated by a single pixel thick line of a specified color.
    /// </summary>
    internal class LineGrid : IGridGenerator
    {
        /// <summary>
        /// A piece of a line based grid. It is used to fill the entire image with the grid.
        /// </summary>
        private Bitmap? LineGridPiece { get; set; }

        /// <summary>
        /// A single pixel of a line based grid.
        /// It is used to restore the grid on a single pixel of the image, to avoid having to reapply the entire grid.
        /// </summary>
        private Bitmap? LineGridSinglePixel { get; set; }

        public void GenerateGrid(Bitmap originalImage, int cellSize, Color gridColor)
        {
            int gridPieceWidth = DefineGridPieceSize(originalImage.Width / cellSize) * cellSize;
            int gridPieceHeight = DefineGridPieceSize(originalImage.Height / cellSize) * cellSize;

            LineGridPiece = new Bitmap(gridPieceWidth, gridPieceHeight);
            LineGridPiece.MakeTransparent(); // Makes the grid piece transparent in order to apply only the grid to an existing image.

            using Pen gridPen = new(gridColor);
            using Graphics gridBuilder = Graphics.FromImage(LineGridPiece);

            for (int x = 1; x < gridPieceHeight / cellSize + 1; x++) // Draws the horizontal lines of the grid
            {
                gridBuilder.DrawLine(gridPen, 0, cellSize * x - 1, gridPieceHeight, cellSize * x - 1);
            }
            for (int y = 1; y < gridPieceWidth / cellSize + 1; y++) // Draws the vertical lines of the grid
            {
                gridBuilder.DrawLine(gridPen, cellSize * y - 1, 0, cellSize * y - 1, gridPieceWidth);
            }

            // Generates the single pixel grid piece
            LineGridSinglePixel = new Bitmap(cellSize, cellSize);
            LineGridSinglePixel.MakeTransparent();

            using Graphics gridPixelBuilder = Graphics.FromImage(LineGridSinglePixel);
            
            gridPixelBuilder.DrawLine(gridPen, 0, cellSize - 1, cellSize - 1, cellSize - 1);
            gridPixelBuilder.DrawLine(gridPen, cellSize - 1, 0, cellSize - 1, cellSize - 1);
        }

        /// <summary>
        /// Calculates the size to use when generating the grid piece, based on the size of the full image.
        /// This method returns a size that will make grid generation more efficient, by splitting the full grid into smaller pieces that will be copied to fill the image.
        /// The method returns a single dimension, which should match the parameter passed. (If the method was passed a width value, it returns a width value) 
        /// </summary>
        /// <param name="sidePixelLength">The length of the image side, in pixel cells (not counting the zoom)</param>
        /// <returns>The optimized grid piece length for the image passed.</returns>
        private int DefineGridPieceSize(int sidePixelLength)
        {
            // TODO: Revise the size calculations

            int amountOfIterations = 10000, gridPieceSize, sizeWithLeastIterations = 0, amountOfGridPieces;

            for (int i = 1; i < Math.Sqrt(sidePixelLength) + 1; i++)
            {
                gridPieceSize = sidePixelLength / i;
                if (sidePixelLength % i != 0)
                {
                    gridPieceSize++;
                }

                if (sidePixelLength % gridPieceSize != 0)
                {
                    amountOfGridPieces = (int)Math.Pow(sidePixelLength / gridPieceSize + 1, 2);
                }
                else
                {
                    amountOfGridPieces = (int)Math.Pow(sidePixelLength / gridPieceSize, 2);
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

        public void ApplyGridFullImage(Bitmap imageWithGrid)
        {
            if (LineGridPiece == null) // Does nothing if the grid wasn't previously generated
            {
                return;
            }

            using Graphics gridMerger = Graphics.FromImage(imageWithGrid);

            for (int y = 0; y < imageWithGrid.Height / LineGridPiece.Height + 1; y++) // Iterates the amount of times needed to cover the full image horizontally
            {
                for (int x = 0; x < imageWithGrid.Width / LineGridPiece.Width + 1; x++) // Iterates the amount of times needed to cover the full image vertically
                {
                    gridMerger.DrawImage(LineGridPiece, LineGridPiece.Width * x, LineGridPiece.Height * y);
                }
            }
        }

        public void ApplyGridSinglePixel(Bitmap imageWithGrid, int xPosition, int yPosition)
        {
            if (LineGridSinglePixel == null) // Does nothing if the grid wasn't previously generated
            {
                return;
            }

            using Graphics lineGridPixelMerger = Graphics.FromImage(imageWithGrid);
            
            lineGridPixelMerger.DrawImage(LineGridSinglePixel, xPosition, yPosition);
        }
    }
}