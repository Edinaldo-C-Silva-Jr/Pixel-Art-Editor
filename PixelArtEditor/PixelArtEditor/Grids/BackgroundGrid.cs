﻿namespace PixelArtEditor.Grids
{
    /// <summary>
    /// Implements a checkered grid, where the pixels are painted with alternating colors. 
    /// This grid will be used as the background for the Drawing Box.
    /// The colors used are the specified grid color and white.
    /// </summary>
    internal class BackgroundGrid : IGridGenerator
    {
        /// <summary>
        /// A piece of the checkered grid. It is used to fill the entire grid in an optimized way.
        /// </summary>
        private Bitmap? CheckerGridPiece { get; set; }

        /// <summary>
        /// Calculates the size to use when generating the grid piece, based on the size of the full image.
        /// This method returns a size that will make grid generation more efficient, by splitting the full grid into smaller pieces that will be copied to tile the grid.
        /// The method returns a single dimension, which should match the parameter passed. (If the method was passed a width value, it returns a width value.) 
        /// </summary>
        /// <param name="sidePixelLength">The length of the image side, in pixel cells.</param>
        /// <returns>The optimized grid piece length for the image passed.</returns>
        private static int DefineGridPieceSize(int sidePixelLength)
        {
            // The best grid piece size is the square root of the side length, as that evenly divides the grid generating and image filling actions.
            int gridPieceSize = (int)Math.Sqrt(sidePixelLength);

            gridPieceSize += gridPieceSize % 2; // Ensures the grid piece size is an even number, to keep the alternating colors when tiling the grid.

            return gridPieceSize;
        }

        public void GenerateGrid(int imageWidth, int imageHeight, int cellSize, Color gridColor)
        {
            int gridPieceWidth = DefineGridPieceSize(imageWidth / cellSize) * cellSize;
            int gridPieceHeight = DefineGridPieceSize(imageHeight / cellSize) * cellSize;
            CheckerGridPiece = new Bitmap(gridPieceWidth, gridPieceHeight);

            using SolidBrush gridBrush = new(gridColor);
            using Graphics gridBuilder = Graphics.FromImage(CheckerGridPiece);

            gridBuilder.Clear(Color.White); // Makes the grid piece completely white, so only the colored part needs to be drawn.
            for (int y = 0; y < CheckerGridPiece.Height; y++)
            {
                for (int x = y % 2; x < CheckerGridPiece.Width; x += 2) // Iterates on alternating pixels to draw the colored part of the checkered grid.
                {
                    gridBuilder.FillRectangle(gridBrush, cellSize * x, cellSize * y, cellSize, cellSize);
                }
            }
        }

        public void ApplyGrid(Graphics gridGraphics, int imageWidth, int imageHeight)
        {
            if (CheckerGridPiece == null) // Does nothing if the grid wasn't previously generated.
            {
                return;
            }

            for (int y = 0; y < imageHeight / CheckerGridPiece.Height + 1; y++) // Iterates the amount of times needed to cover the full image vertically.
            {
                for (int x = 0; x < imageWidth / CheckerGridPiece.Width + 1; x++) // Iterates the amount of times needed to cover the full image horizontally.
                {
                    gridGraphics.DrawImage(CheckerGridPiece, CheckerGridPiece.Width * x, CheckerGridPiece.Height * y);
                }
            }
        }

        /// <summary>
        /// Disposes of the previously used Grid.
        /// </summary>
        public void Dispose()
        {
            CheckerGridPiece?.Dispose();
        }
    }
}
