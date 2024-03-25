﻿namespace PixelArtEditor.Grids.Implementations
{
    /// <summary>
    /// A class that represents the Grid type "None". Utilized for the Dependency Injection.
    /// </summary>
    internal class NoGrid : IGridGenerator
    {
        /// <summary>
        /// Does nothing, since the Grid type is set to None.
        /// </summary>
        public void GenerateGrid(int imageWidth, int imageHeight, int cellSize, Color gridColor)
        {
            return;
        }

        /// <summary>
        /// Simply returns the original image, since no grid should be applied.
        /// </summary>
        public void ApplyGrid(Graphics gridGraphics, int imageWidth, int imageHeight, Color backgroundColor)
        {
            return;
        }
    }
}
