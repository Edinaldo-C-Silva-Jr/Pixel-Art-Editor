namespace PixelArtEditor.Drawing_Tools.Tools
{
    /// <summary>
    /// A tool that freely draws a straight line by clicking and dragging.
    /// This tool can draw lines of any size, direction or inclination. 
    /// </summary>
    internal class FreeLineTool : DrawingTool
    {
        /// <summary>
        /// The point where the line begins, which is where the mouse is first clicked.
        /// </summary>
        private Point? StartingPoint { get; set; }

        /// <summary>
        /// Draws the line pixel by pixel.
        /// This is done by calculating when to shift the pixel position horizontally or vertically to draw the next pixel.
        /// </summary>
        /// <param name="graphics">The graphics to draw the pixel on.</param>
        /// <param name="brush">The brush to use when drawing the pixel.</param>
        /// <param name="location">The location clicked on the image.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        /// <param name="lineLength">The length of the line to be drawn, in pixel sizes.</param>
        /// <param name="lineRatio">The ratio between the length of the bigger line and the length of the smaller one.</param>
        /// <param name="horizontalLine">Whether the line will be predominantly horizontal or vertical.</param>
        /// <param name="lineDirection">The direction of the line. This can be 0 = Left Up, 1 = Left Down, 2 = Right Up and 3 = Right Down.</param>
        private static void CalculateAndDrawLine(Graphics graphics, SolidBrush brush, Point location, int pixelSize, int lineLength, decimal lineRatio, bool horizontalLine, int lineDirection)
        {
            // The line's current position inside each pixel. Defines when to shift to the next pixel.
            decimal horizontalSubpixel = 0, verticalSubpixel = 0;
            // Defines the amount of subpixels to increment in each iteration.
            decimal horizontalIncrement, verticalIncrement;

            if (horizontalLine) // If the line is predominantly horizontal...
            {
                horizontalIncrement = pixelSize; // Every iteration will increment one full pixel horizontally.
                verticalIncrement = pixelSize * lineRatio; // Meanwhile the vertical position will increment based on the line ratio.
            }
            else // Otherwise...
            {
                horizontalIncrement = pixelSize * lineRatio; // Every iteration will increment one full pixel vertically.
                verticalIncrement = pixelSize; // Meanwhile the vertical position will increment based on the line ratio.
            }

            for (int i = 0; i < lineLength; i++)
            {
                DrawPixel(graphics, brush, location, pixelSize);

                horizontalSubpixel += horizontalIncrement;
                verticalSubpixel += verticalIncrement;

                if (horizontalSubpixel >= pixelSize) // If the horizontal subpixel moves into or beyond the end of the current pixel...
                {
                    // Shifts the pixel location to the next pixel.
                    if (lineDirection == 0 || lineDirection == 1)
                    {
                        location.X -= pixelSize;
                    }
                    else
                    {
                        location.X += pixelSize;
                    }
                    horizontalSubpixel -= pixelSize; // Then resets the subpixel position to be within the current pixel.
                }

                if (verticalSubpixel >= pixelSize) // If the vertical subpixel moves into or beyond the end of the current pixel...
                {
                    // Shifts the pixel location to the next pixel.
                    if (lineDirection == 0 || lineDirection == 2)
                    {
                        location.Y -= pixelSize;
                    }
                    else
                    {
                        location.Y += pixelSize;
                    }
                    verticalSubpixel -= pixelSize; // Then resets the subpixel position to be within the current pixel.
                }
            }
        }

        /// <summary>
        /// Draws a line freely, the line's inclination will be adjusted based on the initial and final positions.
        /// </summary>
        /// <param name="graphics">The graphics to draw the pixel on.</param>
        /// <param name="brush">The brush to use when drawing the pixel.</param>
        /// <param name="location">The location clicked on the image.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        private void DrawFreeLine(Graphics graphics, SolidBrush brush, Point location, int pixelSize)
        {
            // The line starts at the position the mouse first clicked, and ends where the mouse currently is.
            Point beginPoint = StartingPoint!.Value;
            Point finalPoint = location;

            int horizontalDifference = Math.Abs(finalPoint.X - beginPoint.X);
            int verticalDifference = Math.Abs(finalPoint.Y - beginPoint.Y);

            // Uses the specific coordinate (X for horizontal and Y for vertical) to find the both the horizontal and vertical line lengths.
            int beginCoordinate, finalCoordinate;
            (beginCoordinate, finalCoordinate) = SwapCoordinatesWhenStartIsBigger(beginPoint.X, finalPoint.X);
            int lineLengthX = GetDistanceInPixelSizes(beginCoordinate, finalCoordinate, pixelSize);
            (beginCoordinate, finalCoordinate) = SwapCoordinatesWhenStartIsBigger(beginPoint.Y, finalPoint.Y);
            int lineLengthY = GetDistanceInPixelSizes(beginCoordinate, finalCoordinate, pixelSize);

            // Finds the direction the line is pointing based on the positions.
            bool linePointRight = finalPoint.X > beginPoint.X;
            bool linePointDown = finalPoint.Y > beginPoint.Y;
            int lineDirection = Convert.ToInt32(linePointRight) * 2 + Convert.ToInt32(linePointDown);

            beginPoint = SnapPixelTopLeft(beginPoint, pixelSize);
            decimal ratioBetweenLines; // The ratio between the line lengths. This will always be smaller or equal to 1.

            if (horizontalDifference > verticalDifference)
            {
                ratioBetweenLines = Decimal.Divide(lineLengthY, lineLengthX); // The ratio will be the smaller line divided by the bigger.
                CalculateAndDrawLine(graphics, brush, beginPoint, pixelSize, lineLengthX, ratioBetweenLines, true, lineDirection); // Draws the line based on the bigger length.
            }
            else
            {
                ratioBetweenLines = Decimal.Divide(lineLengthX, lineLengthY);
                CalculateAndDrawLine(graphics, brush, beginPoint, pixelSize, lineLengthY, ratioBetweenLines, false, lineDirection);
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
            StartingPoint = null;
        }
    }
}
