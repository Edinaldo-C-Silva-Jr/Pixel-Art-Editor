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
            int xPos = absoluteLocation.X - Modulo(absoluteLocation.X, pixelSize);
            int yPos = absoluteLocation.Y - Modulo(absoluteLocation.Y, pixelSize);
            return new(xPos, yPos);
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

        /// <summary>
        /// Receives two coordinates, an initial coordinate and a final one, and compares them.
        /// If the final coordinate is smaller than the initial, they're swapped.
        /// This method accepts a single coordinate, which can be the X or Y coordinate of a Point.
        /// </summary>
        /// <param name="initialCoordinate">The coordinate from the starting point.</param>
        /// <param name="finalCoordinate">The coordinate from the end point.</param>
        /// <returns>A tuple containing both coordinates in the correct order.</returns>
        protected static (int, int) SwapCoordinatesWhenStartIsBigger(int initialCoordinate, int finalCoordinate)
        {
            if (initialCoordinate > finalCoordinate)
            {
                return (finalCoordinate, initialCoordinate);
            }
            else
            {
                return (initialCoordinate, finalCoordinate);
            }
        }

        /// <summary>
        /// Custom modulo function that correctly handles negative values, returning a positive remainder.
        /// </summary>
        /// <param name="value">The value that will have its remainder returned.</param>
        /// <param name="modulo">The number to divide the value and get the remainder.</param>
        /// <returns>The remainder of the division between a value and a number.</returns>
        protected static int Modulo(int value, int modulo)
        {
            return (value % modulo + modulo) % modulo;
        }

        /// <summary>
        /// Calculates and returns the distance between two points in pixel sizes.
        /// This method accepts a single coordinate, which can be the X or Y coordinate of a Point.
        /// </summary>
        /// <param name="initialCoordinate">The coordinate of the first mouse click.</param>
        /// <param name="finalCoordinate">The coordinate of the current mouse position.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        /// <returns>The distance between the two coordinates, expressed in pixel sizes.</returns>
        protected static int GetDistanceInPixelSizes(int initialCoordinate, int finalCoordinate, int pixelSize)
        {
            initialCoordinate -= Modulo(initialCoordinate, pixelSize); // The starting coordinate is the left or up edge of the pixel.
            finalCoordinate = finalCoordinate - (Modulo(finalCoordinate, pixelSize)) + pixelSize; // The ending coordinate is the left or up edge of the next pixel.

            return Math.Abs((finalCoordinate - initialCoordinate) / pixelSize);
        }

        abstract public void UseToolClick(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);

        abstract public void UseToolHold(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);

        abstract public void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);

        abstract public void UseToolRelease(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);
    }
}
