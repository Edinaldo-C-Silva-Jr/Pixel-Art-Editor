using PixelArtEditor.Drawing_Tools;
using PixelArtEditor.Grids;

namespace PixelArtEditor.Controls
{
    public partial class DrawingBox : PictureBox, IDisposable
    {
        // Disposed in the Designer file
        private Graphics? ImageGraphics;
        private SolidBrush? ColorBrush;

        public DrawingBox()
        {
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
        /// Applies a new grid type to the existing image. This method doesn't override the actual image when applying the grid.
        /// </summary>
        /// <param name="gridApply">The IGridGenerator implementation used to apply the grid.</param>
        /// <param name="paintGraphics">The graphics used to apply the grid.</param>
        /// <param name="imageWidth">The width of the image that will receive the grid.</param>
        /// <param name="imageHeight">The height of the image that will receive the grid.</param>
        public void ApplyNewGrid(IGridGenerator gridApply, Graphics paintGraphics, int imageWidth, int imageHeight)
        {
            gridApply.ApplyGrid(paintGraphics, imageWidth, imageHeight);
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
            if (ImageGraphics == null || ColorBrush == null)
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
            if (ImageGraphics == null || ColorBrush == null)
            {
                return;
            }

            tool.UseToolRelease(ImageGraphics, ColorBrush, toolParameters);

            ImageGraphics.Dispose();
            ColorBrush.Dispose();
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
