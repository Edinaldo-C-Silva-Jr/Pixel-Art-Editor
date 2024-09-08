/*namespace PixelArtEditor.Drawing_Tools.Tools
{
    /// <summary>
    /// A tool that draws a rectangle outline using a single color by clicking and dragging.
    /// </summary>
    internal class OutlineRectangleTool : DrawingTool
    {
        /// <summary>
        /// The point where the rectangle begins, which is where the mouse is first clicked.
        /// </summary>
        private Point? StartingPoint { get; set; }

        /// <summary>
        /// Draws a rectangle outline, using a single color. The inner part of the rectangle is not affected.
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

            (beginPoint.X, finalPoint.X) = SwapCoordinatesWhenStartIsBigger(beginPoint.X, finalPoint.X);
            int rectangleWidth = GetDistanceInPixelSizes(beginPoint.X, finalPoint.X, pixelSize);
            (beginPoint.Y, finalPoint.Y) = SwapCoordinatesWhenStartIsBigger(beginPoint.Y, finalPoint.Y);
            int rectangleHeight = GetDistanceInPixelSizes(beginPoint.Y, finalPoint.Y, pixelSize);
            beginPoint = SnapPixelTopLeft(beginPoint, pixelSize);

            DrawRectangle(graphics, brush, beginPoint, pixelSize, rectangleWidth, 1);
            DrawRectangle(graphics, brush, beginPoint, pixelSize, 1, rectangleHeight);
            DrawRectangle(graphics, brush, new(beginPoint.X, finalPoint.Y), pixelSize, rectangleWidth, 1);
            DrawRectangle(graphics, brush, new(finalPoint.X, beginPoint.Y), pixelSize, 1, rectangleHeight);
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
            StartingPoint = null;
        }
    }
}
*/