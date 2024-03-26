using PixelArtEditor.Drawing_Tools;
using PixelArtEditor.Grids;

namespace PixelArtEditor.Controls
{
    public partial class DrawingBox : PictureBox, IDisposable
    {
        // Disposed in the Designer file
        private Graphics? ImageGraphics { get; set; }
        private SolidBrush? ColorBrush { get; set; }
        private BackgroundGrid GridGenerator { get; set; }
        private bool MouseReleased { get; set; }

        public DrawingBox()
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

        /// <summary>
        /// Executes the tool's Mouse Click method.
        /// Creates the graphics for the image and the brush to use with the tool.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="image">The image that is being drawn on.</param>
        /// <param name="pixelColor">The color to be used by the tool.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawClick(IDrawingTool tool, Bitmap image, Color pixelColor, OptionalToolParameters toolParameters)
        {
            ImageGraphics = Graphics.FromImage(image);
            ColorBrush = new(pixelColor);
            MouseReleased = false;

            tool.UseToolClick(ImageGraphics, ColorBrush, toolParameters);
        }

        /// <summary>
        /// Executes the tool's Mouse Hold method.
        /// Uses the Graphics and Brush already created by the DrawClick method.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawHold(IDrawingTool tool, OptionalToolParameters toolParameters)
        {
            if (ImageGraphics == null || ColorBrush == null || MouseReleased)
            {
                return;
            }

            tool.UseToolHold(ImageGraphics, ColorBrush, toolParameters);
        }

        /// <summary>
        /// Executes the tool's Mouse Release method.
        /// Also disposes of the Graphics and Brush used.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawRelease(IDrawingTool tool, OptionalToolParameters toolParameters)
        {
            if (ImageGraphics == null || ColorBrush == null || MouseReleased)
            {
                return;
            }

            tool.UseToolRelease(ImageGraphics, ColorBrush, toolParameters);

            ImageGraphics.Dispose();
            ColorBrush.Dispose();
            MouseReleased = true;
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
