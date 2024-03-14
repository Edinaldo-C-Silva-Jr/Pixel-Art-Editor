namespace PixelArtEditor.Drawing_Tools
{
    /// <summary>
    /// A base implementation of a Drawing Tool, that has some of the common methods used by most other tools.
    /// </summary>
    abstract public class DrawingTool : IDrawingTool
    {
        /// <summary>
        /// Draws a single pixel with the current pixel size, filling the pixel where the mouse clicked.
        /// </summary>
        /// <param name="drawGraphics">The graphics for the image being drawn.</param>
        /// <param name="drawBrush">The brush with the currently selected color.</param>
        /// <param name="location">The location of the mouse click inside the Drawing Box.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        protected static void DrawPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, int pixelSize)
        {
            Point pixelLocation = SnapPixelTopLeft(location, pixelSize);
            drawGraphics.FillRectangle(drawBrush, pixelLocation.X, pixelLocation.Y, pixelSize, pixelSize);
        }


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
            Point pixelLocation = SnapPixelTopLeft(location, pixelSize);
            drawGraphics.FillRectangle(drawBrush, pixelLocation.X, pixelLocation.Y, pixelSize * rectangleWidth, pixelSize * rectangleHeight);
        }

        /// <summary>
        /// Receives a location and snaps it to the top left of a pixel. This makes sure the changes are always applied to the entire pixel.
        /// </summary>
        /// <param name="absoluteLocation">The actual location of the mouse click.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        /// <returns>The new location that indicates the top left corner of the current pixel.</returns>
        protected static Point SnapPixelTopLeft(Point absoluteLocation, int pixelSize)
        {
            int xPos = absoluteLocation.X - absoluteLocation.X % pixelSize;
            int yPos = absoluteLocation.Y - absoluteLocation.Y % pixelSize;
            return new(xPos, yPos);
        }

        protected static int GetLineLengthInPixels(int startingPoint, int endPoint, int pixelSize)
        {
            startingPoint -= startingPoint % pixelSize; // The starting point is the first point of the pixel
            endPoint = endPoint - (endPoint % pixelSize) + pixelSize; // The end point is the first point of the next pixel

            return Math.Abs((endPoint - startingPoint) / pixelSize); 
        }

        /// <summary>
        /// Creates a transluscent brush to use in the preview tool methods.
        /// </summary>
        /// <param name="colorBrush">The brush with the current color.</param>
        /// <returns>A new brush with the same color but transluscent.</returns>
        protected static SolidBrush MakePreviewBrush(SolidBrush colorBrush)
        {
            Color transluscentColor = Color.FromArgb(128, colorBrush.Color);
            colorBrush = new(transluscentColor);
            return colorBrush;
        }

        abstract public void UseToolClick(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);

        abstract public void UseToolHold(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);

        abstract public void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);

        abstract public void UseToolRelease(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);
    }
}
