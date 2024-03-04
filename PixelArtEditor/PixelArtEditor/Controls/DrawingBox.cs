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
                if (toolParameters.BeginPoint.HasValue && toolParameters.PixelSize.HasValue)
                {
                    int xPos = toolParameters.BeginPoint.Value.X - toolParameters.BeginPoint.Value.X % toolParameters.PixelSize.Value;
                    int yPos = toolParameters.BeginPoint.Value.Y - toolParameters.BeginPoint.Value.Y % toolParameters.PixelSize.Value;
                    gridGenerator.ApplyGridSinglePixel(imageWithGrid, xPos, yPos);
                }
            }
        }

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

        public void DrawRelease(IDrawingTool tool, IGridGenerator gridGenerator, OptionalToolParameters toolParameters)
        {
            if (ImageGraphics == null || ColorBrush == null)
            {
                return;
            }

            tool.UseToolRelease(ImageGraphics, ColorBrush, toolParameters);

            ImageGraphics.Dispose();
            ColorBrush.Dispose();

            // Grid stuff
            if (GridGraphics == null)
            {
                return;
            }

            tool.UseToolRelease(GridGraphics, ColorBrush, toolParameters);
            ApplySingleGrid(gridGenerator, toolParameters);

            GridGraphics.Dispose();
        }

        public void PreviewTool(IDrawingTool tool, Graphics paintGraphics, Color pixelColor, OptionalToolParameters toolParameters)
        {
            ColorBrush = new(pixelColor);

            tool.PreviewTool(paintGraphics, ColorBrush, toolParameters);
        }
    }
}
