using PixelArtEditor.Grids;

namespace PixelArtEditor.Controls
{
    public partial class DrawingBox : PictureBox
    {
        private Bitmap imageWithGrid = new Bitmap(1, 1);
        private Bitmap pixelLineGrid = new Bitmap(1, 1);

        public DrawingBox()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
        }

        public void SetNewImage(IGridGenerator gridGenerator, Bitmap originalImage, int cellSize, Color gridColor)
        {
            this.Width = originalImage.Width;
            this.Height = originalImage.Height;

            imageWithGrid = new Bitmap(originalImage);

            gridGenerator.GenerateGrid(originalImage, cellSize, gridColor);
            imageWithGrid = gridGenerator.ApplyGridFullImage(originalImage);
            this.Image = imageWithGrid;
            this.Refresh();
        }

        public Bitmap DrawPixelByPosition(Bitmap image, int xPosPixel, int yPosPixel, int pixelSize, Color pixelColor, GridType gridType)
        {
            int xPos = pixelSize * xPosPixel;
            int yPos = pixelSize * yPosPixel;

            return DrawPixel(image, xPos, yPos, pixelSize, pixelColor, gridType);
        }

        public Bitmap DrawPixelByClick(Bitmap image, int xPosMouse, int yPosMouse, int pixelSize, Color pixelColor, GridType gridType)
        {
            int xPos = xPosMouse - xPosMouse % pixelSize;
            int yPos = yPosMouse - yPosMouse % pixelSize;

            return DrawPixel(image, xPos, yPos, pixelSize, pixelColor, gridType);
        }

        private Bitmap DrawPixel(Bitmap image, int xPos, int yPos, int pixelSize, Color pixelColor, GridType gridType)
        {
            Graphics pixelDraw = Graphics.FromImage(image);
            Graphics gridDraw = Graphics.FromImage(imageWithGrid);
            Brush pixelBrush = new SolidBrush(pixelColor);

            // Gets the correct position of the rectangle from the mouse position, the 1 pixel offsets aren't needed for a checkers or empty grid
            pixelDraw.FillRectangle(pixelBrush, xPos, yPos, pixelSize, pixelSize);
            gridDraw.FillRectangle(pixelBrush, xPos, yPos, pixelSize, pixelSize);

            if (gridType == GridType.Line && pixelSize > 2)
            {
                gridDraw.DrawImage(pixelLineGrid, xPos, yPos);
            }

            this.Image = imageWithGrid;

            return image;
        }
    }
}
