namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class FreeLineTool : DrawingTool
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
        /// Calculates and returns the length of the line to be drawn, in pixel sizes.
        /// </summary>
        /// <param name="beginCoordinate">The coordinate where the line begins, which is on the first mouse click.</param>
        /// <param name="finalCoordinate">The coordinate where the line ends, which is on the current mouse position.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        /// <returns>The length of the line, expressed in pixel sizes.</returns>
        private static int GetLineLengthInPixels(int beginCoordinate, int finalCoordinate, int pixelSize)
        {
            beginCoordinate -= beginCoordinate % pixelSize; // The starting coordinate is the left or up edge of the pixel.
            finalCoordinate = finalCoordinate - (finalCoordinate % pixelSize) + pixelSize; // The ending coordinate is the left or up edge of the next pixel.

            return Math.Abs((finalCoordinate - beginCoordinate) / pixelSize);
        }

        private static void CalculateAndDrawLine(Graphics graphics, SolidBrush brush, Point location, int pixelSize, int lineLength, decimal lineRatio, bool horizontalLine, int lineDirection)
        {
            decimal xProgress = 0, yProgress = 0;
            decimal xStep, yStep;

            if (horizontalLine)
            {
                xStep = pixelSize;
                yStep = pixelSize * lineRatio;
            }
            else
            {
                xStep = pixelSize * lineRatio;
                yStep = pixelSize;
            }

            for (int i = 0; i < lineLength; i++)
            {
                DrawPixel(graphics, brush, location, pixelSize);

                xProgress += xStep;
                yProgress += yStep;

                if (xProgress >= pixelSize)
                {
                    xProgress -= pixelSize;
                    if (lineDirection == 0 || lineDirection == 1)
                    {
                        location.X -= pixelSize;
                    }
                    else
                    {
                        location.X += pixelSize;
                    }
                }

                if (yProgress >= pixelSize)
                {
                    yProgress -= pixelSize;
                    if (lineDirection == 0 || lineDirection == 2)
                    {
                        location.Y -= pixelSize;
                    }
                    else
                    {
                        location.Y += pixelSize;
                    }
                }
            }
        }

        private void DrawFreeLine(Graphics graphics, SolidBrush brush, Point location, int pixelSize)
        {
            // The line starts at the position the mouse first clicked, and ends where the mouse currently is.
            Point beginPoint = StartingPoint!.Value;
            Point finalPoint = location;

            int horizontalDifference = Math.Abs(finalPoint.X - beginPoint.X);
            int verticalDifference = Math.Abs(finalPoint.Y - beginPoint.Y);

            int beginCoordinate, finalCoordinate;
            (beginCoordinate, finalCoordinate) = SwapCoordinatesOnBackwardsLine(beginPoint.X, finalPoint.X);
            int lineLengthX = GetLineLengthInPixels(beginCoordinate, finalCoordinate, pixelSize);
            (beginCoordinate, finalCoordinate) = SwapCoordinatesOnBackwardsLine(beginPoint.Y, finalPoint.Y);
            int lineLengthY = GetLineLengthInPixels(beginCoordinate, finalCoordinate, pixelSize);

            bool linePointRight = finalPoint.X > beginPoint.X;
            bool linePointDown = finalPoint.Y > beginPoint.Y;
            int lineDirection = Convert.ToInt32(linePointRight) * 2 + Convert.ToInt32(linePointDown);

            beginPoint = SnapPixelTopLeft(beginPoint, pixelSize);
            decimal lineRatio;

            if (horizontalDifference > verticalDifference)
            {
                lineRatio = Decimal.Divide(lineLengthY, lineLengthX);
                CalculateAndDrawLine(graphics, brush, beginPoint, pixelSize, lineLengthX, lineRatio, true, lineDirection);
            }
            else
            {
                lineRatio = Decimal.Divide(lineLengthX, lineLengthY);
                CalculateAndDrawLine(graphics, brush, beginPoint, pixelSize, lineLengthY, lineRatio, false, lineDirection);
            }
        }

        public override void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                colorBrush = MakePreviewBrush(colorBrush);
                DrawFreeLine(paintGraphics, colorBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
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
                DrawFreeLine(imageGraphics, colorBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
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
