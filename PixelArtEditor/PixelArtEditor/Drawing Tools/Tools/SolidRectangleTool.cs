
namespace PixelArtEditor.Drawing_Tools.Tools
{
    /// <summary>
    /// A tool that draws a completely solid rectangle using a single color by clicking and dragging.
    /// </summary>
    internal class SolidRectangleTool : DrawingTool
    {
        /// <summary>
        /// The point where the rectangle begins, which is where the mouse is first clicked.
        /// </summary>
        private Point? StartingPoint { get; set; }

        // Amount of times the drawing has been done, due to grid.
        private int repeats;

        /// <summary>
        /// Swaps the coordinates if the endpoint has a lower value than the starting point.
        /// This method accepts a single coordinate, which can be the points' X coordinate or Y coordinate.
        /// </summary>
        /// <param name="beginCoodinate">The coordinate from the starting point.</param>
        /// <param name="endCoordinate">The coordinate from the end point.</param>
        /// <returns>A tuple containing both coordinates in the correct order.</returns>
        private static (int, int) SwapCoordinatesOnBackwardsLine(int beginCoodinate, int endCoordinate)
        {
            if (beginCoodinate > endCoordinate)
            {
                return (endCoordinate, beginCoodinate);
            }
            else
            {
                return (beginCoodinate, endCoordinate);
            }
        }

        /// <summary>
        /// Calculates and returns the distance between two points in pixel sizes.
        /// </summary>
        /// <param name="beginCoordinate">The coordinate of the first mouse click.</param>
        /// <param name="finalCoordinate">The coordinate of the current mouse position.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        /// <returns>The distance between the two coordinates, expressed in pixel sizes.</returns>
        private static int GetDistanceInPixelSizes(int beginCoordinate, int finalCoordinate, int pixelSize)
        {
            beginCoordinate -= beginCoordinate % pixelSize; // The starting coordinate is the left or up edge of the pixel.
            finalCoordinate = finalCoordinate - (finalCoordinate % pixelSize) + pixelSize; // The ending coordinate is the left or up edge of the next pixel.

            return Math.Abs((finalCoordinate - beginCoordinate) / pixelSize);
        }

        /// <summary>
        /// Draws a solid rectangle, using a single color.
        /// The click coordinates correspond to the top-left and bottom-right points of the rectangle.
        /// </summary>
        /// <param name="graphics">The graphics to draw the pixel on.</param>
        /// <param name="brush">The brush to use when drawing the pixel.</param>
        /// <param name="location">The location clicked on the image.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        private void DrawSolidRectangle(Graphics graphics, SolidBrush brush, Point location, int pixelSize)
        {
            // The rectangle uses the position of the first mouse click, and the current mouse position.
            // The lowest value is always treated as the start of the rectangle, the top left corner.
            Point beginPoint = StartingPoint!.Value;
            Point finalPoint = location;

            (beginPoint.X, finalPoint.X) = SwapCoordinatesOnBackwardsLine(beginPoint.X, finalPoint.X);
            int rectangleWidth = GetDistanceInPixelSizes(beginPoint.X, finalPoint.X, pixelSize);
            (beginPoint.Y, finalPoint.Y) = SwapCoordinatesOnBackwardsLine(beginPoint.Y, finalPoint.Y);
            int rectangleHeight = GetDistanceInPixelSizes(beginPoint.Y, finalPoint.Y, pixelSize);
            beginPoint = SnapPixelTopLeft(beginPoint, pixelSize);

            DrawRectangle(graphics, brush, beginPoint, pixelSize, rectangleWidth, rectangleHeight);
        }

        public override void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                colorBrush = MakePreviewBrush(colorBrush);
                DrawSolidRectangle(paintGraphics, colorBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolClick(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue)
            {
                StartingPoint = toolParameters.ClickLocation.Value;
                repeats = 0;
            }
        }

        public override void UseToolHold(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            return;
        }

        public override void UseToolRelease(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                DrawSolidRectangle(imageGraphics, colorBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
            }

            // Draw twice. Once on the image and once on the grid image
            repeats++;
            if (repeats == 2)
            {
                StartingPoint = null;
            }
        }
    }
}
