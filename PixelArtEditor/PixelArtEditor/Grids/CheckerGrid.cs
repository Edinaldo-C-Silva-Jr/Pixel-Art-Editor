namespace PixelArtEditor.Grids
{
    internal class CheckerGrid : IGridGenerator
    {
        Bitmap? checkerGridPiece;
        Bitmap? checkerGridWhitePixel;
        Bitmap? checkerGridColorPixel;

        public void GenerateGrid(Bitmap originalImage, int cellSize, Color gridColor)
        {
            int gridPieceWidth = DefineGridPieceSize(originalImage.Width / cellSize);
            int gridPieceHeight = DefineGridPieceSize(originalImage.Height / cellSize);

            checkerGridPiece = new Bitmap(gridPieceWidth, gridPieceHeight);

            using SolidBrush gridBrush = new(gridColor);
            using Graphics gridBuilder = Graphics.FromImage(checkerGridPiece);
            gridBuilder.Clear(Color.White);
            for (int y = 0; y < checkerGridPiece.Height; y++)
            {
                for (int x = y % 2; x < checkerGridPiece.Width; x += 2)
                {
                    gridBuilder.FillRectangle(gridBrush, cellSize * x, cellSize * y, cellSize, cellSize);
                }
            }

            checkerGridWhitePixel = new Bitmap(cellSize, cellSize);

            checkerGridColorPixel = new Bitmap(cellSize, cellSize);
            using Graphics gridPixelBuilder = Graphics.FromImage(checkerGridColorPixel);
            gridPixelBuilder.Clear(gridColor);
        }

        /// <summary>
        /// Calculates the size to use when generating the grid piece, based on the size of the full image.
        /// This method returns a size that will make grid generation more efficient, by splitting the full grid into smaller pieces that will be copied to fill the image.
        /// The method returns a single dimension, which should match the parameter passed. (If the method was passed a width value, it returns a width value) 
        /// </summary>
        /// <param name="sizePixelLength">The length of the image side, in pixel cells (not counting the zoom)</param>
        /// <returns>The optimized grid piece length for the image passed.</returns>
        private int DefineGridPieceSize(int sidePixelLength)
        {
            return (int)Math.Sqrt(sidePixelLength);
        }

        public Bitmap ApplyGridFullImage(Bitmap originalImage)
        {
            if (checkerGridPiece == null)
            {
                return originalImage;
            }

            using Graphics gridMerger = Graphics.FromImage(originalImage);

            for (int y = 0; y < originalImage.Height / checkerGridPiece.Height; y++)
            {
                for (int x = 0; x < originalImage.Width / checkerGridPiece.Width; x++)
                {
                    gridMerger.DrawImage(checkerGridPiece, checkerGridPiece.Width * x, checkerGridPiece.Height * y);
                }
            }

            return originalImage;
        }

        public Bitmap ApplyGridSinglePixel(Bitmap originalImage, int xPosition, int yPosition)
        {
            if (checkerGridColorPixel == null || checkerGridWhitePixel == null)
            {
                return originalImage;
            }

            using Graphics gridPixelMerger = Graphics.FromImage(originalImage);

            int positionParity = (xPosition % 2 + yPosition % 2) % 2;
            if (Convert.ToBoolean(positionParity))
            {
                gridPixelMerger.DrawImage(checkerGridColorPixel, xPosition, yPosition);
            }
            else
            {
                gridPixelMerger.DrawImage(checkerGridWhitePixel, xPosition, yPosition);
            }

            return originalImage;
        }
    }
}
