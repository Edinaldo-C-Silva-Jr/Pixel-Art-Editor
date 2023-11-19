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
            if (cellSize < 4) // Only generates grid if the zoom is at least 4, as to prevent the grid from covering too much of the image
            {
                LineGridPiece = null;
                LineGridSinglePixel = null;
                return;
            }

            int gridPieceWidth = DefineGridPieceSize(originalImage.Width / cellSize) * cellSize;
            int gridPieceHeight = DefineGridPieceSize(originalImage.Height / cellSize) * cellSize;

            LineGridPiece = new Bitmap(gridPieceWidth, gridPieceHeight);
            LineGridPiece.MakeTransparent(); // Makes the grid piece transparent in order to apply only the grid to an existing image.

            using Pen gridPen = new(gridColor);
            using Graphics gridBuilder = Graphics.FromImage(LineGridPiece);

            for (int x = 1; x < gridPieceWidth / cellSize + 1; x++) // Draws the vertical lines of the grid.
            {
                gridBuilder.DrawLine(gridPen, cellSize * x - 1, 0, cellSize * x - 1, gridPieceHeight);
            }
            for (int y = 1; y < gridPieceHeight / cellSize + 1; y++) // Draws the horizontal lines of the grid.
            {
                gridBuilder.DrawLine(gridPen, 0, cellSize * y - 1, gridPieceWidth, cellSize * y - 1);
            }

            // Generates the single pixel grid piece.
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
            int leastAmountOActions = 10000, gridPieceSize, sizeWithLeastActions = 0, amountOfGridPieces;

            // Finds the best size to use for the current side length.
            // This is done by testing how many actions are needed to fill the full image when using different grid piece sizes.
            // The tests are based on two values:
            // 1 - The amount of lines needed to build the grid piece, which is the side length divided by a given arbitrary value.
            // 2 - The amount of times the grid piece with the above size will be applied to cover the full image, which is the side length divided by the grid piece size, squared.
            // These amounts are then added together to see the total amount of actions needed to build the grid and cover the image with it.
            // For the sake of simplicity, the tests assume the image is a square.
            for (int i = 1; i < Math.Sqrt(sidePixelLength) + 1; i++)
            {
                gridPieceSize = sidePixelLength / i; // The arbitrary value used to test the grid piece is i.
                if (sidePixelLength % i != 0) // If the above division has a remainder, the grid would end up too short. If that's the case, increase grid size by 1.
                {
                    gridPieceSize++;
                }

                if (sidePixelLength % gridPieceSize != 0) // Checks if the side length is a multiple of the grid piece size.
                {
                    amountOfGridPieces = (int)Math.Pow(sidePixelLength / gridPieceSize + 1, 2); // If it isn't, one extra grid piece will be needed in each line to cover the image, so the division needs to be added 1 before being squared.
                }
                else
                {
                    amountOfGridPieces = (int)Math.Pow(sidePixelLength / gridPieceSize, 2); // If it is, then just square the division.
                }

                int totalActions = amountOfGridPieces + gridPieceSize * 2;
                if (totalActions < leastAmountOActions) // If the amount of actions on this test is lower than the current lower amount...
                {
                    leastAmountOActions = totalActions;
                    sizeWithLeastActions = gridPieceSize; // Define this grid piece size as the most efficient.
                }
            }
            return sizeWithLeastActions;
        }

        public void ApplyGridFullImage(Bitmap imageWithGrid)
        {
            if (LineGridPiece == null) // Does nothing if the grid wasn't previously generated.
            {
                return;
            }

            using Graphics gridMerger = Graphics.FromImage(imageWithGrid);

            for (int y = 0; y < imageWithGrid.Height / LineGridPiece.Height + 1; y++) // Iterates the amount of times needed to cover the full image vertically.
            {
                for (int x = 0; x < imageWithGrid.Width / LineGridPiece.Width + 1; x++) // Iterates the amount of times needed to cover the full image horizontally.
                {
                    gridMerger.DrawImage(LineGridPiece, LineGridPiece.Width * x, LineGridPiece.Height * y);
                }
            }
        }

        public void ApplyGridSinglePixel(Bitmap imageWithGrid, int xPosition, int yPosition)
        {
            if (LineGridSinglePixel == null) // Does nothing if the grid wasn't previously generated.
            {
                return;
            }

            using Graphics lineGridPixelMerger = Graphics.FromImage(imageWithGrid);
            lineGridPixelMerger.DrawImage(LineGridSinglePixel, xPosition, yPosition);
        }
    }
}