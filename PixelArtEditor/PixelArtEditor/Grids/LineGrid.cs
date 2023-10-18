namespace PixelArtEditor.Grids
{
    internal class LineGrid : IGridGenerator
    {
        Bitmap lineGridPiece = new(1, 1);
        Bitmap lineGridPixel = new(1, 1);

        public void GenerateGrid(Bitmap originalImage, int cellSize, Color gridColor)
        {
            int gridPieceWidth = DefineGridPieceSize(originalImage.Width / cellSize);
            int gridPieceHeight = DefineGridPieceSize(originalImage.Height / cellSize);

            lineGridPiece = new Bitmap(originalImage.Width, originalImage.Height);
        }

        private int DefineGridPieceSize(int sizePixelLength)
        {
            int amountOfIterations = 10000, gridPieceSize, sizeWithLeastIterations = 0, amountOfGridPieces;

            for (int i = 1; i < Math.Sqrt(sizePixelLength) + 1; i++)
            {
                gridPieceSize = sizePixelLength / i;

                if (sizePixelLength % gridPieceSize != 0)
                {
                    amountOfGridPieces = (int)Math.Pow(sizePixelLength / gridPieceSize + 1, 2);
                }
                else
                {
                    amountOfGridPieces = (int)Math.Pow(sizePixelLength / gridPieceSize, 2);
                }

                int totalIterations = amountOfGridPieces + gridPieceSize*2;
                if (totalIterations < amountOfIterations)
                {
                    amountOfIterations = totalIterations;
                    sizeWithLeastIterations = gridPieceSize;
                }
            }

            return sizeWithLeastIterations;
        }

        public Bitmap ApplyGridFullImage(Bitmap originalImage)
        {

            return originalImage;
        }

        public Bitmap ApplyGridSinglePixel(Bitmap originalImage, int xPosition, int yPosition)
        {
            return originalImage;
        }
    }
}
