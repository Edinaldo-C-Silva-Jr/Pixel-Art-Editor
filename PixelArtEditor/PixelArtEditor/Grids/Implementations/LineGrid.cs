namespace PixelArtEditor.Grids.Implementations
{
    /// <summary>
    /// Implements a line based grid, where every cell is separated by a single pixel thick line of a specified color.
    /// </summary>
    internal class LineGrid : IGridGenerator
    {
        /// <summary>
        /// A piece of a line based grid. It is used to tile the entire grid.
        /// </summary>
        private Bitmap? LineGridPiece { get; set; }

        /// <summary>
        /// Calculates the size to use when generating the grid piece, based on the size of the full image.
        /// This method returns a size that will make grid generation more efficient, by splitting the full grid into smaller pieces that will be copied to tile the grid.
        /// The method returns a single dimension, which should match the parameter passed. (If the method was passed a width value, it returns a width value) 
        /// </summary>
        /// <param name="sidePixelLength">The length of the image side, in pixel cells.</param>
        /// <returns>The optimized grid piece length for the image passed.</returns>
        private static int DefineGridPieceSize(int sidePixelLength)
        {
            int leastAmountOActions = 10000, gridPieceSize, sizeWithLeastActions = 0, amountOfGridPieces;

            // Finds the best size to use for the current side length.
            // This is done by testing how many actions are needed to fill the full image when using different grid piece sizes.
            // The tests are based on two values:
            // 1 - The amount of lines needed to build the grid piece, which is the side length divided by a given arbitrary value.
            // 2 - The amount of times the grid piece with the above required to tile the full grid, which is the side length divided by the grid piece size, squared.
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
                    // If it isn't, one extra grid piece will be needed in each line to cover the image, so the division needs to be added 1 before being squared.
                    amountOfGridPieces = (int)Math.Pow(sidePixelLength / gridPieceSize + 1, 2);
                }
                else
                {
                    // If it is, then just square the division.
                    amountOfGridPieces = (int)Math.Pow(sidePixelLength / gridPieceSize, 2); 
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

        public void GenerateGrid(int imageWidth, int imageHeight, int cellSize, Color gridColor)
        {
            if (cellSize < 4) // Only generates grid if the zoom is at least 4, to prevent the grid from covering too much of the image.
            {
                LineGridPiece = null;
                return;
            }

            int gridPieceWidth = DefineGridPieceSize(imageWidth / cellSize) * cellSize;
            int gridPieceHeight = DefineGridPieceSize(imageHeight / cellSize) * cellSize;

            LineGridPiece = new Bitmap(gridPieceWidth, gridPieceHeight);
            LineGridPiece.MakeTransparent(); // Makes the grid piece transparent so the image appears below it.
            
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
        }

        public void ApplyGrid(Graphics gridGraphics, int imageWidth, int imageHeight)
        {
            if (LineGridPiece == null) // Does nothing if the grid wasn't previously generated.
            {
                return;
            }

            for (int y = 0; y < imageHeight / LineGridPiece.Height + 1; y++) // Iterates the amount of times needed to cover the full image vertically.
            {
                for (int x = 0; x < imageWidth / LineGridPiece.Width + 1; x++) // Iterates the amount of times needed to cover the full image horizontally.
                {
                    gridGraphics.DrawImage(LineGridPiece, LineGridPiece.Width * x, LineGridPiece.Height * y);
                }
            }
        }

        /// <summary>
        /// Disposes of the previously used Grid.
        /// </summary>
        public void Dispose()
        {
            LineGridPiece?.Dispose();
        }
    }
}