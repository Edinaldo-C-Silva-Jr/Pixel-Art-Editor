﻿namespace PixelArtEditor.Drawing_Tools.Tools
{
    /// <summary>
    /// A tool that draws a straight line by clicking and dragging.
    /// This tool only draws vertical, horizontal or diagonal lines, snapping to the closest side to the mouse cursor.
    /// </summary>
    internal class OrdinalLineTool : DrawingTool
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
        /// <param name="startingPoint">The point where the line begins, which is the first mouse click.</param>
        /// <param name="finalPoint">The point where the line ends, which is the current mouse position.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        /// <returns>The length of the line, expressed in pixel sizes.</returns>
        private static int GetLineLengthInPixels(int startingPoint, int finalPoint, int pixelSize)
        {
            startingPoint -= startingPoint % pixelSize; // The starting point is the top left of the pixel.
            finalPoint = finalPoint - (finalPoint % pixelSize) + pixelSize; // The end point is the top left of the next pixel.

            return Math.Abs((finalPoint - startingPoint) / pixelSize);
        }

        /// <summary>
        /// Draws a diagonal line. The direction and size of the line are defined by the parameters.
        /// </summary>
        /// <param name="graphics">The graphics to draw the pixel on.</param>
        /// <param name="brush">The brush to use when drawing the pixel.</param>
        /// <param name="beginPoint">The starting point of the line.</param>
        /// <param name="pixelSize">The size of a pixel in the image.</param>
        /// <param name="lineLength">The length of the line in pixel sizes.</param>
        /// <param name="lineDirection">The direction of the line. This can be 0 = Left Up, 1 = Left Down, 2 = Right Up and 3 = Right Down.</param>
        private static void DrawDiagonalLine(Graphics graphics, SolidBrush brush, Point beginPoint, int pixelSize, int lineLength, int lineDirection)
        {
            switch (lineDirection)
            {
                case 0: // Left Up (LinePointRight is 0 and LinePointDown is 0)
                    for (int i = 0; i < lineLength; i++)
                    {
                        Point pixelPoint = new(beginPoint.X - i * pixelSize, beginPoint.Y - i * pixelSize);
                        DrawPixel(graphics, brush, pixelPoint, pixelSize);
                    }
                    break;
                case 1: // Left Down (LinePointRight is 0 and LinePointDown is 1)
                    for (int i = 0; i < lineLength; i++)
                    {
                        Point pixelPoint = new(beginPoint.X - i * pixelSize, beginPoint.Y + i * pixelSize);
                        DrawPixel(graphics, brush, pixelPoint, pixelSize);
                    }
                    break;
                case 2: // Right Up (LinePointRight is 1 and LinePointDown is 0)
                    for (int i = 0; i < lineLength; i++)
                    {
                        Point pixelPoint = new(beginPoint.X + i * pixelSize, beginPoint.Y - i * pixelSize);
                        DrawPixel(graphics, brush, pixelPoint, pixelSize);
                    }
                    break;
                case 3: // Right Down (LinePointRight is 1 and LinePointDown is 1)
                    for (int i = 0; i < lineLength; i++)
                    {
                        Point pixelPoint = new(beginPoint.X + i * pixelSize, beginPoint.Y + i * pixelSize);
                        DrawPixel(graphics, brush, pixelPoint, pixelSize);
                    }
                    break;
            }
        }

        /// <summary>
        /// Draws a line that can be either horizontal, vertical or diagonal, depending on which point is closer to the mouse cursor.
        /// </summary>
        /// <param name="graphics">The graphics to draw the pixel on.</param>
        /// <param name="brush">The brush to use when drawing the pixel.</param>
        /// <param name="location">The location clicked on the image.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        private void DrawOrdinalLine(Graphics graphics, SolidBrush brush, Point location, int pixelSize)
        {
            // The line starts at the position the mouse first clicked, and ends where the mouse currently is.
            Point beginPoint = StartingPoint!.Value;
            Point finalPoint = location;

            int horizontalDifference = Math.Abs(finalPoint.X - beginPoint.X);
            int verticalDifference = Math.Abs(finalPoint.Y - beginPoint.Y);

            // The line will be horizontal if the mouse moves first horizontally, otherwise it's vertical.
            bool lineDirectionHorizontal = horizontalDifference > verticalDifference;
            // If the line end has a higher X value than the line start, then the line points towards the right, otherwise it's the left.
            bool linePointRight = finalPoint.X > beginPoint.X;
            // If the line end has a higher Y value than the line start, then the line points downwards, otherwise it's upwards.
            bool linePointDown = finalPoint.Y > beginPoint.Y;
            // The line will be diagonal if:
            // 1 - The line is horizontal and the horizontal difference is less than twice the vertical difference.
            // 2 - The line is vertical and the vertical difference is less than twice the horizontal difference.
            bool diagonalLine = (lineDirectionHorizontal && horizontalDifference < 2 * verticalDifference)
                || (!lineDirectionHorizontal && verticalDifference < 2 * horizontalDifference);

            if (diagonalLine)
            {
                // These will either be the X or Y coordinate of the line start and line end, depending on whether the line goes further horizontally or vertically. 
                int beginCoordinate, endCoordinate;

                if (lineDirectionHorizontal)
                {
                    (beginCoordinate, endCoordinate) = SwapCoordinatesOnBackwardsLine(beginPoint.X, finalPoint.X);
                }
                else
                {
                    (beginCoordinate, endCoordinate) = SwapCoordinatesOnBackwardsLine(beginPoint.Y, finalPoint.Y);
                }

                // Defines the value of the diagonal line direction by using 2 bits, one for right/left and one for up/down.
                int lineDirection = Convert.ToInt32(linePointRight) * 2 + Convert.ToInt32(linePointDown);
                int lineLength = GetLineLengthInPixels(beginCoordinate, endCoordinate, pixelSize);
                DrawDiagonalLine(graphics, brush, beginPoint, pixelSize, lineLength, lineDirection);
            }
            else
            {
                if (lineDirectionHorizontal)
                {
                    (beginPoint.X, finalPoint.X) = SwapCoordinatesOnBackwardsLine(beginPoint.X, finalPoint.X);
                    int lineLength = GetLineLengthInPixels(beginPoint.X, finalPoint.X, pixelSize);
                    DrawRectangle(graphics, brush, beginPoint, pixelSize, lineLength, 1);
                }
                else
                {
                    (beginPoint.Y, finalPoint.Y) = SwapCoordinatesOnBackwardsLine(beginPoint.Y, finalPoint.Y);
                    int lineLength = GetLineLengthInPixels(beginPoint.Y, finalPoint.Y, pixelSize);
                    DrawRectangle(graphics, brush, beginPoint, pixelSize, 1, lineLength);
                }
            }
        }

        public override void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                colorBrush = MakePreviewBrush(colorBrush);
                DrawOrdinalLine(paintGraphics, colorBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
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
                DrawOrdinalLine(imageGraphics, colorBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
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
