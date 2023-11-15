using PixelArtEditor.Grids;

namespace PixelArtEditor.Controls
{
    public partial class DrawingBox : PictureBox
    {
        private Bitmap imageWithGrid = new Bitmap(1, 1);

        public DrawingBox()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        public void SetNewImage(IGridGenerator gridGenerator, Bitmap originalImage, int cellSize, Color gridColor)
        {
            this.Width = originalImage.Width;
            this.Height = originalImage.Height;

            imageWithGrid = new Bitmap(originalImage);
            imageWithGrid = gridGenerator.ApplyGridFullImage(originalImage);
            this.Image = imageWithGrid;
            this.Refresh();
        }

        /// <summary>
        /// Applies a new grid type to the existing image. This method doesn't override the actual image when applying the grid.
        /// </summary>
        /// <param name="gridApply">The IGridGenerator implementation used to apply the grid.</param>
        /// <param name="originalImage">The original image to use when applying the grid.</param>
        public void ApplyNewGrid(IGridGenerator gridApply, Bitmap originalImage)
        {
            Bitmap imageToApplyGrid = new(originalImage);

            imageWithGrid = gridApply.ApplyGridFullImage(imageToApplyGrid);
            this.Image = imageWithGrid;
            this.Refresh();
        }

        public Bitmap DrawPixelByPosition(IGridGenerator gridGenerator, Bitmap image, int xPosPixel, int yPosPixel, int pixelSize, Color pixelColor, GridType gridType)
        {
            int xPos = pixelSize * xPosPixel;
            int yPos = pixelSize * yPosPixel;

            return DrawPixel(gridGenerator, image, xPos, yPos, pixelSize, pixelColor);
        }

        public Bitmap DrawPixelByClick(IGridGenerator gridGenerator, Bitmap image, int xPosMouse, int yPosMouse, int pixelSize, Color pixelColor, GridType gridType)
        {
            int xPos = xPosMouse - xPosMouse % pixelSize;
            int yPos = yPosMouse - yPosMouse % pixelSize;

            return DrawPixel(gridGenerator, image, xPos, yPos, pixelSize, pixelColor);
        }

        private Bitmap DrawPixel(IGridGenerator gridGenerator, Bitmap image, int xPos, int yPos, int pixelSize, Color pixelColor)
        {
            Bitmap imageToDraw = new(image);

            Graphics pixelDraw = Graphics.FromImage(imageToDraw);
            Graphics gridDraw = Graphics.FromImage(imageWithGrid);
            Brush pixelBrush = new SolidBrush(pixelColor);

            // Gets the correct position of the rectangle from the mouse position
            pixelDraw.FillRectangle(pixelBrush, xPos, yPos, pixelSize, pixelSize);
            gridDraw.FillRectangle(pixelBrush, xPos, yPos, pixelSize, pixelSize);

            imageWithGrid = gridGenerator.ApplyGridSinglePixel(image, xPos, yPos);

            this.Image = imageWithGrid;

            return image;
        }
    }
}
