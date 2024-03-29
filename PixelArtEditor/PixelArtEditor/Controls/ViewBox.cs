using PixelArtEditor.Grids;

namespace PixelArtEditor.Controls
{
    public partial class ViewBox : PictureBox
    {
        private BackgroundGrid GridGenerator { get; set; }

        public ViewBox()
        {
            GridGenerator = new BackgroundGrid();
            DoubleBuffered = true;
            InitializeComponent();
        }

        public void SetNewSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void SetNewImage(Bitmap originalImage)
        {
            Image = new Bitmap(originalImage);
            Invalidate();
        }

        /// <summary>
        /// Generates and applies a Background Grid for the Drawing Box, to be shown behind the image.
        /// This grid will appear when the image has a transparent background.
        /// </summary>
        /// <param name="imageWidth">The width of the image, to base the grid size.</param>
        /// <param name="imageHeight">The height of the image, to base the grid size.</param>
        /// <param name="cellSize">The size of each cell in the grid.</param>
        public void SetBackgroundGrid(int imageWidth, int imageHeight, int cellSize)
        {
            BackgroundImageLayout = ImageLayout.Stretch; // Changes the layout mode for better performance.
            BackgroundImage = new Bitmap(imageWidth, imageHeight);

            // Generates and applies background grid for the Drawing Box, with the size of the image and the color Light Gray.
            GridGenerator.GenerateGrid(imageWidth, imageHeight, cellSize, Color.LightGray);
            using Graphics gridGraphics = Graphics.FromImage(BackgroundImage);
            GridGenerator.ApplyGrid(gridGraphics, imageWidth, imageHeight);
        }
    }
}
