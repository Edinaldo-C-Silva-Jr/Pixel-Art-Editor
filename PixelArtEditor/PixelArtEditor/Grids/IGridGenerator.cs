namespace PixelArtEditor.Grids
{
    /// <summary>
    /// Interface for generating and applying a grid to images shown in the Drawing Box
    /// </summary>
    public interface IGridGenerator
    {
        /// <summary>
        /// Generates a grid, saving it into a property, so it can be applied to an image with the ApplyGrid method.
        /// </summary>
        /// <param name="imageWidth">The width of the image that will receive the grid.</param>
        /// <param name="imageHeight">The height of the image that will receive the grid.</param>
        /// <param name="cellSize">The size of each pixel cell in the grid, defined by the zoom parameter in the editor.</param>
        /// <param name="gridColor">The color currently set for the grid.</param>
        public void GenerateGrid(int imageWidth, int imageHeight, int cellSize, Color gridColor);

        /// <summary>
        /// Applies a previously generated grid to the specified image, filling the entire image with it.
        /// If the grid has not been generated, it does nothing.
        /// </summary>
        /// <param name="gridGraphics">The graphics that will be used to draw the grid.</param>
        /// <param name="backgroundColor">The color used for the image's background.</param>
        public void ApplyGrid(Graphics gridGraphics, int imageWidth, int imageHeight);
    }
}
