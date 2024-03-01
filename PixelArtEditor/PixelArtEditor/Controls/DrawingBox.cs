using PixelArtEditor.Drawing_Tools;
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

        public void SetNewSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public void SetNewImage(IGridGenerator gridGenerator, Bitmap originalImage, Color backgroundColor)
        {
            imageWithGrid = new(originalImage);
            gridGenerator.ApplyGridFullImage(imageWithGrid, backgroundColor);
            this.Image = imageWithGrid;
            this.Refresh();
        }

        /// <summary>
        /// Applies a new grid type to the existing image. This method doesn't override the actual image when applying the grid.
        /// </summary>
        /// <param name="gridApply">The IGridGenerator implementation used to apply the grid.</param>
        /// <param name="originalImage">The original image to use when applying the grid.</param>
        public void ApplyNewGrid(IGridGenerator gridApply, Bitmap originalImage, Color backgroundColor)
        {
            imageWithGrid = new(originalImage);

            gridApply.ApplyGridFullImage(imageWithGrid, backgroundColor);
            this.Image = imageWithGrid;
            this.Refresh();
        }

        public void DrawPixelByPosition(IDrawingTool tool, IGridGenerator gridGenerator, Bitmap image, int xPosPixel, int yPosPixel, int pixelSize, Color pixelColor)
        {
            int xPos = pixelSize * xPosPixel;
            int yPos = pixelSize * yPosPixel;
            Point startingPoint = new(xPos, yPos);

            Draw(tool, gridGenerator, image, pixelSize, pixelColor, startingPoint);
        }

        public void DrawPixelByClick(IDrawingTool tool, IGridGenerator gridGenerator, Bitmap image, int pixelSize, Color pixelColor, Point? beginPoint = null, Point? endPoint = null, Size? imageSize = null)
        {
            Draw(tool, gridGenerator, image, pixelSize, pixelColor, beginPoint, imageSize: imageSize);
        }

        private void Draw(IDrawingTool tool, IGridGenerator gridGenerator, Bitmap image, int pixelSize, Color pixelColor, Point? beginPoint = null, Point? endPoint = null, Size? imageSize = null)
        {
            using Graphics pixelDraw = Graphics.FromImage(image);
            using Graphics gridDraw = Graphics.FromImage(imageWithGrid);
            using Brush pixelBrush = new SolidBrush(pixelColor);

            tool.UseTool(pixelDraw, pixelBrush, pixelSize, beginPoint, endPoint, imageSize);
            tool.UseTool(gridDraw, pixelBrush, pixelSize, beginPoint, endPoint, imageSize);

            if (!gridGenerator.BackgroundGrid)
            {
                int xPos = beginPoint.Value.X - beginPoint.Value.X % pixelSize;
                int yPos = beginPoint.Value.Y - beginPoint.Value.Y % pixelSize;
                gridGenerator.ApplyGridSinglePixel(imageWithGrid, xPos, yPos);
            }

            this.Image = imageWithGrid;
        }
    }
}
