using PixelArtEditor.Drawing_Tools;
using PixelArtEditor.Grids;

namespace PixelArtEditor.Controls
{
    /// <summary>
    /// The Picture Box used for drawing into the image.
    /// </summary>
    public partial class DrawBox : PictureBox, IDisposable
    {
        // Disposed in the Designer file.
        /// <summary>
        /// The graphics used to draw into the image.
        /// </summary>
        private Graphics? ImageGraphics { get; set; }

        // Disposed in the Designer file.
        /// <summary>
        /// The brush used to draw into the image.
        /// </summary>
        private SolidBrush? ColorBrush { get; set; }

        // Disposed in the Designer file.
        /// <summary>
        /// The grid generator used to create the background grid of the Drawing Box.
        /// </summary>
        private BackgroundGrid GridGenerator { get; set; }

        /// <summary>
        /// Keeps track of whether the mouse has been clicked in the current drawing cycle or not.
        /// </summary>
        private bool MouseClicked { get; set; }

        public DrawBox()
        {
            GridGenerator = new BackgroundGrid();
            MouseClicked = false;
            DoubleBuffered = true;
            InitializeComponent();
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
        /// Executes the drawing tool's Mouse Click method.
        /// Marks the beginning of a drawing cycle, and creates the Graphics and Brush objects to be used in it.
        /// The drawing cycle represents the string of actions between clicking, holding and releasing the mouse while drawing.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="image">The image that is being drawn on.</param>
        /// <param name="pixelColor">The color to be used by the tool.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawClick(IDrawingTool tool, Bitmap image, Color pixelColor, OptionalToolParameters toolParameters)
        {
            ImageGraphics = Graphics.FromImage(image);
            ColorBrush = new(pixelColor);
            MouseClicked = true;

            tool.UseToolClick(ImageGraphics, ColorBrush, toolParameters);
        }

        /// <summary>
        /// Executes the tool's Mouse Hold method.
        /// This event continues the drawing cycle started by DrawClick, utilizing the same Graphics and Brush already created by it.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawHold(IDrawingTool tool, OptionalToolParameters toolParameters)
        {
            // Only executes the drawing tool if the mouse has been clicked in the control before, to ensure the graphics and brush objects are valid.
            if (!MouseClicked)
            {
                return;
            }

            tool.UseToolHold(ImageGraphics!, ColorBrush!, toolParameters);
        }

        /// <summary>
        /// Executes the tool's Mouse Release method.
        /// This concludes the current drawing cycle, by disposing of the Graphics and Brush used.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawRelease(IDrawingTool tool, OptionalToolParameters toolParameters)
        {
            // Only executes the drawing tool if the mouse has been clicked in the control before, to ensure the graphics and brush objects are valid.
            if (!MouseClicked)
            {
                return;
            }

            tool.UseToolRelease(ImageGraphics!, ColorBrush!, toolParameters);

            // Disposes of the graphics and brush used in the current drawing cycle, and sets MouseClicked to false to finish this cycle.
            ImageGraphics?.Dispose();
            ColorBrush?.Dispose();
            MouseClicked = false;
        }

        /// <summary>
        /// Executes the tool's Preview method.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="paintGraphics">The graphics from the Paint event in the DrawingBox.</param>
        /// <param name="pixelColor">The color to be used by the tool.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void PreviewTool(IDrawingTool tool, Graphics paintGraphics, Color pixelColor, OptionalToolParameters toolParameters)
        {
            paintGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            ColorBrush = new(pixelColor);

            tool.PreviewTool(paintGraphics, ColorBrush, toolParameters);

            paintGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
        }
    }
}
