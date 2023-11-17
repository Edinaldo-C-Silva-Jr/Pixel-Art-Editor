namespace PixelArtEditor.Grids
{
    /// <summary>
    /// A class that represents the Grid type "None". Utilized for the Dependency Injection.
    /// </summary>
    internal class NoGrid : IGridGenerator
    {
        /// <summary>
        /// Simply returns the original image, since no grid should be applied.
        /// </summary>
        public void ApplyGridFullImage(Bitmap originalImage)
        {
            return;
        }

        /// <summary>
        /// Simply returns the original image, since no grid should be applied.
        /// </summary>
        public void ApplyGridSinglePixel(Bitmap originalImage, int xPosition, int yPosition)
        {
            return;
        }

        /// <summary>
        /// Does nothing, since the Grid type is set to None.
        /// </summary>
        public void GenerateGrid(Bitmap originalImage, int cellSize, Color gridColor)
        {
            return;
        }
    }
}
