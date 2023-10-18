namespace PixelArtEditor.Grids
{
    /// <summary>
    /// Interface for generating and applying a grid to images shown in the Drawing Box
    /// </summary>
    internal interface IGridGenerator
    {
        /// <summary>
        /// Generates a grid, saving it into a property, so it can be applied to an image with the ApplyGridFullImage method.
        /// Also generates a single pixel of the grid, which can be applied with ApplyGridSinglePixel method.
        /// </summary>
        /// <param name="originalImage">The image used to define the dimensions of the grid.</param>
        /// <param name="cellSize">The size of each pixel cell in the grid, defined by the zoom parameter in the editor.</param>
        /// <param name="gridColor">The color currently set for the grid.</param>
        public void GenerateGrid(Bitmap originalImage, int cellSize, Color gridColor);

        /// <summary>
        /// Applies a previously generated grid to the specified image, filling the entire image with it.
        /// If the grid has not been generated, this simply returns the image.
        /// </summary>
        /// <param name="originalImage">The image where the grid will be applied.</param>
        /// <returns>The image with the grid applied in its entirety.</returns>
        public Bitmap ApplyGridFullImage(Bitmap originalImage);

        /// <summary>
        /// Applies a single pixel of the grid to a specific place in the image.
        /// If the grid has not been generated, thsi simply returns the image.
        /// </summary>
        /// <param name="originalImage">The image where the grid pixel will be applied.</param>
        /// <param name="xPosition">The horizontal position where the pixel will be applied.</param>
        /// <param name="yPosition">The vertical position where the pixel will be applied.</param>
        /// <returns>The image with the grid pixel applied in the specified position.</returns>
        public Bitmap ApplyGridSinglePixel(Bitmap originalImage, int xPosition, int yPosition);
    }
}
