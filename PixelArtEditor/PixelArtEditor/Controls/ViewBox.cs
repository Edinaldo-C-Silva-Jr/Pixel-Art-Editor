using PixelArtEditor.Grids;

namespace PixelArtEditor.Controls
{
    /// <summary>
    /// A Picture Box for viewing the full image.
    /// </summary>
    public partial class ViewBox : PictureBox
    {
        // Disposed in the Designer file.
        /// <summary>
        /// The grid generator used to create the background grid of the Viewing Box.
        /// </summary>
        private BackgroundGrid GridGenerator { get; set; }

        public ViewBox()
        {
            GridGenerator = new BackgroundGrid();
            DoubleBuffered = true;
            InitializeComponent();
        }

        /// <summary>
        /// Sets a new size for the Viewing Box.
        /// </summary>
        /// <param name="width">The width for the Viewing Box to have.</param>
        /// <param name="height">The height for the Viewing Box to have.</param>
        public void SetNewSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Sets a new image into the Viewing Box. This image is the full image being drawn on in the editor.
        /// </summary>
        /// <param name="originalImage">The image that will be set into the Viewing Box.</param>
        public void SetNewImage(Bitmap originalImage)
        {
            Image = new Bitmap(originalImage);
            Invalidate();
        }

        /// <summary>
        /// Generates and applies a Background Grid for the Viewing Box, to be shown behind the image.
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

        /// <summary>
        /// Draws an overlay of the Drawing Image position into the Viewing Box, to show the area that is currently being drawn on.
        /// </summary>
        /// <param name="paintGraphics">The graphics of the Viewing Box's Paint event.</param>
        /// <param name="location">The location of the Drawing Image in the full image.</param>
        /// <param name="boxSize">The size of the Drawing Box, with the pixel size equivalent to that of the Viewing Box.</param>
        public void DrawDrawingBoxOverlay(Graphics paintGraphics, Point location, Size boxSize)
        {
            using Pen blackPen = new(Color.Black);
            using Pen whitePen = new(Color.White);

            // Draws two different color rectangles so they're visible regardless of the image's current colors.
            paintGraphics.DrawRectangle(blackPen, location.X, location.Y, boxSize.Width, boxSize.Height);
            paintGraphics.DrawRectangle(whitePen, location.X + 1, location.Y + 1, boxSize.Width - 2, boxSize.Height - 2);
        }
    }
}
