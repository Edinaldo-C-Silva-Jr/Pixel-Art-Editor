namespace PixelArtEditor.Grids
{
    internal class CheckerGrid : IGridGenerator
    {
        Bitmap checkerGridPiece = new(1, 1);
        public void GenerateGrid(Bitmap originalImage, int cellSize, Color gridColor)
        {
            checkerGridPiece = new Bitmap(originalImage.Width, originalImage.Height);
        }

        public Bitmap ApplyGridFullImage(Bitmap originalImage)
        {
            return originalImage;
        }

        public Bitmap ApplyGridSinglePixel(Bitmap originalImage)
        {
            return originalImage;
        }
    }
}
