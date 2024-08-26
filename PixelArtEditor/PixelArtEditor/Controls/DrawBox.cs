using PixelArtEditor.Grids;

namespace PixelArtEditor.Controls
{
    /// <summary>
    /// The Picture Box used for drawing into the image.
    /// </summary>
    public partial class DrawBox : PictureBox, IDisposable
    {
        /// <summary>
        /// The grid generator used to create the background grid of the Drawing Box.
        /// </summary>
        private BackgroundGrid GridGenerator { get; set; }

        /// <summary>
        /// Default constructor. Initiates the grid generator and sets double buffering.
        /// </summary>
        public DrawBox()
        {
            GridGenerator = new BackgroundGrid();
            DoubleBuffered = true;
            InitializeComponent();
            Disposed += OnDispose;
        }

        /// <summary>
        /// Sets a new size for the Drawing Box.
        /// </summary>
        /// <param name="width">The width for the Drawing Box to have.</param>
        /// <param name="height">The height for the Drawing Box to have.</param>
        public void SetNewSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Sets a new image into the Drawing Box. This image is a portion of the full image that is currently being drawn on.
        /// </summary>
        /// <param name="drawingImage">The image that will be set into the Drawing Box, to be drawn into.</param>
        public void SetNewImage(Bitmap drawingImage)
        {
            Image = new Bitmap(drawingImage);
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

            // Generates and applies a checkered background grid for the Drawing Box, with the size of the image and the color Light Gray.
            GridGenerator.GenerateGrid(imageWidth, imageHeight, cellSize, Color.LightGray);
            using Graphics gridGraphics = Graphics.FromImage(BackgroundImage);
            GridGenerator.ApplyGrid(gridGraphics, imageWidth, imageHeight);
        }

        /// <summary>
        /// Disposes of the grid generator when the control is disposed.
        /// </summary>
        private void OnDispose(object? sender, EventArgs e)
        {
            GridGenerator?.Dispose();
        }
    }
}
