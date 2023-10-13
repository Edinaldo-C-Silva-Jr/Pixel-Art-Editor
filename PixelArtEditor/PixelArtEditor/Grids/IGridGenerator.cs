namespace PixelArtEditor.Grids
{
    internal interface IGridGenerator
    {
        public void GenerateGrid(Bitmap originalImage, int cellSize, Color gridColor);

        public Bitmap ApplyGridFullImage(Bitmap originalImage);

        public Bitmap ApplyGridSinglePixel(Bitmap originalImage);
    }
}
