using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Drawing_Tools
{
    /// <summary>
    /// A base implementation of a Drawing Tool, that has some of the common methods used by most other tools.
    /// </summary>
    abstract public class DrawingTool : IDrawingTool, IUndoRedoCreator
    {
        /// <summary>
        /// Draws a single pixel with the current pixel size, at the exact location of the mouse click.
        /// </summary>
        /// <param name="drawGraphics">The graphics for the image being drawn.</param>
        /// <param name="drawBrush">The brush with the currently selected color.</param>
        /// <param name="location">The location of the mouse click inside the Drawing Box.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        protected static void DrawPixelAbsolute(Graphics drawGraphics, SolidBrush drawBrush, Point location, int pixelSize)
        {
            drawGraphics.FillRectangle(drawBrush, location.X, location.Y, pixelSize, pixelSize);
        }

        /// <summary>
        /// Draws a rectangle based on the current pixel size and starting at the pixel where the mouse clicked. 
        /// The size of the rectangle can be chosen with the length parameters.
        /// </summary>
        /// <param name="drawGraphics">The graphics for the image being drawn.</param>
        /// <param name="drawBrush">The brush with the currently selected color.</param>
        /// <param name="location">The location of the mouse click inside the Drawing Box.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        /// <param name="rectangleWidth">The width of the rectangle, in pixel sizes.</param>
        /// <param name="rectangleHeight">The height of the rectangle, in pixel sizes.</param>
        protected static void DrawRectangle(Graphics drawGraphics, SolidBrush drawBrush, Point location, int pixelSize, int rectangleWidth, int rectangleHeight)
        {
            Point pixelLocation = DrawingCalculations.SnapPixelTopLeft(location, pixelSize);
            drawGraphics.FillRectangle(drawBrush, pixelLocation.X, pixelLocation.Y, pixelSize * rectangleWidth, pixelSize * rectangleHeight);
        }

        /// <summary>
        /// Creates a brush to use in the preview tool methods. This brush will have a darker or lighter version of the color.
        /// </summary>
        /// <param name="brushColor">The color to use in the brush.</param>
        /// <returns>A new brush with a lighter or darker version of the original color.</returns>
        protected static SolidBrush MakePreviewBrush(Color brushColor)
        {
            // Formula to get the brightness of a color. Values range from 0 to 255.
            // The green component has the most influence in the color's brightness, and the blue component has the least influence.
            decimal brightness = 0.2126M * brushColor.R + 0.7152M * brushColor.G + 0.0722M * brushColor.B;

            // Make the color lighter if it's dark, and darker if it's light.
            Color previewColor;
            if (brightness < 64)
            {
                previewColor = ControlPaint.Light(brushColor, 1.0f);
            } 
            else if (brightness < 128)
            {
                previewColor = ControlPaint.Light(brushColor, 0.4f);
            } 
            else if(brightness < 196)
            {
                previewColor = ControlPaint.Dark(brushColor, 0.05f);
            }
            else
            {
                previewColor = ControlPaint.Dark(brushColor, 0.1f);
            }
            
            return new SolidBrush(previewColor);
        }

        abstract public void PreviewTool(Graphics paintGraphics, Color drawingColor, OptionalToolParameters toolParameters);

        abstract public void UseToolClick(Bitmap drawingImage, Color drawingColor, OptionalToolParameters toolParameters);

        abstract public void UseToolHold(OptionalToolParameters toolParameters);

        abstract public void UseToolRelease(OptionalToolParameters toolParameters);

        abstract public IUndoRedoCommand CreateUndoStep(Point drawingImageLocation);
    }
}
