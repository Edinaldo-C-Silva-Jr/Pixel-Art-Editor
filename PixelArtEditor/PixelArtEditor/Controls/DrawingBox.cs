using PixelArtEditor.Drawing_Tools;
using PixelArtEditor.Grids;

namespace PixelArtEditor.Controls
{
    public partial class DrawingBox : PictureBox, IDisposable
    {
        private Bitmap imageWithGrid = new Bitmap(1, 1);
        
        // Disposed in the Designer file
        private Graphics? ImageGraphics;
        private SolidBrush? ColorBrush;

        // Grid stuff
        private Graphics? GridGraphics;

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

        public void ApplySingleGrid(IGridGenerator gridGenerator, OptionalToolParameters toolParameters)
        {
            if (!gridGenerator.BackgroundGrid)
            {
                if (toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
                {
                    int xPos = toolParameters.ClickLocation.Value.X - toolParameters.ClickLocation.Value.X % toolParameters.PixelSize.Value;
                    int yPos = toolParameters.ClickLocation.Value.Y - toolParameters.ClickLocation.Value.Y % toolParameters.PixelSize.Value;
                    gridGenerator.ApplyGridSinglePixel(imageWithGrid, xPos, yPos);
                }
            }
        }

        /// <summary>
        /// Executes the tool's Mouse Click method.
        /// Creates the graphics for the image and the brush to use with the tool.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="gridGenerator">The type of grid generator to use with the tool.</param>
        /// <param name="image">The image that is being drawn on.</param>
        /// <param name="pixelColor">The color to be used by the tool.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawClick(IDrawingTool tool, IGridGenerator gridGenerator, Bitmap image, Color pixelColor, OptionalToolParameters toolParameters)
        {
            ImageGraphics = Graphics.FromImage(image);
            ColorBrush = new(pixelColor);

            GridGraphics = Graphics.FromImage(imageWithGrid);

            tool.UseToolClick(ImageGraphics, ColorBrush, toolParameters);

            // Grid stuff
            tool.UseToolClick(GridGraphics, ColorBrush, toolParameters);
            ApplySingleGrid(gridGenerator, toolParameters);
        }

        /// <summary>
        /// Executes the tool's Mouse Hold method.
        /// Uses the Graphics and Brush already created by the DrawClick method.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="gridGenerator">The type of grid generator to use with the tool.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawHold(IDrawingTool tool, IGridGenerator gridGenerator, OptionalToolParameters toolParameters)
        {
            if (ImageGraphics == null || ColorBrush == null)
            {
                return;
            }

            tool.UseToolHold(ImageGraphics, ColorBrush, toolParameters);

            // Grid stuff
            if (GridGraphics == null)
            {
                return;
            }

            tool.UseToolHold(GridGraphics, ColorBrush, toolParameters);
            ApplySingleGrid(gridGenerator, toolParameters);
        }

        /// <summary>
        /// Executes the tool's Mouse Release method.
        /// Also disposes of the Graphics and Brush used.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="gridGenerator">The type of grid generator to use with the tool.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawRelease(IDrawingTool tool, IGridGenerator gridGenerator, OptionalToolParameters toolParameters)
        {
            if (ImageGraphics == null || ColorBrush == null)
            {
                return;
            }

            tool.UseToolRelease(ImageGraphics, ColorBrush, toolParameters);

            ImageGraphics.Dispose();

            // Grid stuff
            if (GridGraphics == null)
            {
                return;
            }

            tool.UseToolRelease(GridGraphics, ColorBrush, toolParameters);
            ApplySingleGrid(gridGenerator, toolParameters);

            GridGraphics.Dispose();
            // End Grid Stuff

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
        }
    }
}
