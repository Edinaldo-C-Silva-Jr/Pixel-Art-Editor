namespace PixelArtEditor.Drawing_Tools.Tools
{
    /// <summary>
    /// A tool that draws a straight line by clicking and dragging.
    /// This tool only draws vertical or horizontal lines, snapping to the closest side to the mouse cursor.
    /// </summary>
    internal class CardinalLineTool : DrawingTool
    {
        /// <summary>
        /// The point where the line begins, which is where the mouse is first clicked.
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
        /// Draws a line that can be either horizontal or vertical, depending on which point is closer to the mouse cursor.
        /// </summary>
        /// <param name="graphics">The graphics to draw the pixel on.</param>
        /// <param name="brush">The brush to use when drawing the pixel.</param>
        /// <param name="location">The location clicked on the image.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        private void DrawCardinalLine(Graphics graphics, SolidBrush brush, Point location, int pixelSize)
        {
            // The line starts at the position the mouse first clicked, and ends where the mouse currently is.
            Point beginPoint = StartingPoint!.Value;
            Point finalPoint = location;

            int horizontalDifference = Math.Abs(finalPoint.X - beginPoint.X);
            int verticalDifference = Math.Abs(finalPoint.Y - beginPoint.Y);

            if (horizontalDifference > verticalDifference) // If the mouse moved further horizontally, draw a horizontal line...
            {
                (beginPoint.X, finalPoint.X) = SwapCoordinatesOnBackwardsLine(beginPoint.X, finalPoint.X);
                int lineLength = GetLineLengthInPixels(beginPoint.X, finalPoint.X, pixelSize);
                DrawRectangle(graphics, brush, beginPoint, pixelSize, lineLength, 1);
            }
            else // Otherwise, draw a vertical line.
            {
                (beginPoint.Y, finalPoint.Y) = SwapCoordinatesOnBackwardsLine(beginPoint.Y, finalPoint.Y);
                int lineLength = GetLineLengthInPixels(beginPoint.Y, finalPoint.Y, pixelSize);
                DrawRectangle(graphics, brush, beginPoint, pixelSize, 1, lineLength);
            }
        }

        public override void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                colorBrush = MakePreviewBrush(colorBrush);
                DrawCardinalLine(paintGraphics, colorBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
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
                DrawCardinalLine(imageGraphics, colorBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
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
