﻿namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.LineTool
{
    /// <summary>
    /// A tool that freely draws a straight line by clicking and dragging.
    /// This tool can draw lines of any size, direction or inclination. 
    /// </summary>
    public class FreeLineTool : BaseLineTool
    {
        #region Properties
        /// <summary>
        /// Indicates if the line is pointing to the right or to the left.
        /// </summary>
        private bool LinePointsRight { get; set; }

        /// <summary>
        /// Indicates if the line is pointing down or up.
        /// </summary>
        private bool LinePointsDown { get; set; }
        #endregion

        protected override void DrawLine(Graphics drawGraphics, SolidBrush drawBrush)
        {
            if (StartingPoint.HasValue && EndPoint.HasValue && UneditedImage is not null)
            {
                int horizontalDistance = Math.Abs(StartingPoint.Value.X - EndPoint.Value.X);
                int verticalDistance = Math.Abs(StartingPoint.Value.Y - EndPoint.Value.Y);

                // Defines the directions the line points towards.
                LinePointsRight = StartingPoint!.Value.X < EndPoint!.Value.X;
                LinePointsDown = StartingPoint!.Value.Y < EndPoint!.Value.Y;

                // The ratio between the horizontal and vertical distance of the starting and end point.
                decimal lineDistanceRatio;

                if (horizontalDistance > verticalDistance)
                {
                    lineDistanceRatio = DrawingCalculations.GetRatioBetweenDistances(verticalDistance, horizontalDistance);
                    CalculateAndDrawLine(drawGraphics, drawBrush, StartingPoint.Value, 1, horizontalDistance, lineDistanceRatio, true);
                }
                else
                {
                    lineDistanceRatio = DrawingCalculations.GetRatioBetweenDistances(horizontalDistance, verticalDistance);
                    CalculateAndDrawLine(drawGraphics, drawBrush, StartingPoint.Value, 1, verticalDistance, lineDistanceRatio, false);
                }
            }
        }

        protected override void DrawLine(Graphics drawGraphics, SolidBrush drawBrush, int zoom)
        {
            if (StartingPoint.HasValue && EndPoint.HasValue && UneditedImage is not null)
            {
                int horizontalDistance = Math.Abs(StartingPoint.Value.X - EndPoint.Value.X);
                int verticalDistance = Math.Abs(StartingPoint.Value.Y - EndPoint.Value.Y);

                // Defines the directions the line points towards.
                LinePointsRight = StartingPoint!.Value.X < EndPoint!.Value.X;
                LinePointsDown = StartingPoint!.Value.Y < EndPoint!.Value.Y;

                // Creates a new point with a location that matches the zoom.
                Point firstPoint = new(StartingPoint.Value.X * zoom, StartingPoint.Value.Y * zoom);

                // The ratio between the horizontal and vertical distance of the starting and end point.
                decimal lineDistanceRatio;

                if (horizontalDistance > verticalDistance)
                {
                    lineDistanceRatio = DrawingCalculations.GetRatioBetweenDistances(verticalDistance, horizontalDistance);
                    CalculateAndDrawLine(drawGraphics, drawBrush, firstPoint, zoom, horizontalDistance, lineDistanceRatio, true);
                }
                else
                {
                    lineDistanceRatio = DrawingCalculations.GetRatioBetweenDistances(horizontalDistance, verticalDistance);
                    CalculateAndDrawLine(drawGraphics, drawBrush, firstPoint, zoom, verticalDistance, lineDistanceRatio, false);
                }
            }
        }

        /// <summary>
        /// Draws the line pixel by pixel.
        /// This is done by calculating when to shift the pixel position horizontally or vertically to draw the next pixel.
        /// </summary>
        /// <param name="graphics">The graphics to draw the pixel on.</param>
        /// <param name="brush">The brush to use when drawing the pixel.</param>
        /// <param name="drawPoint">The point where the next pixel will be drawn.</param>
        /// <param name="zoom">The size of each pixel in the image.</param>
        /// <param name="lineLength">The length of the line to be drawn. This should be the bigger distance between horizontal or vertical.</param>
        /// <param name="lineRatio">The ratio between the horizontal and vertical distances of the line.</param>
        /// <param name="horizontalLine">Whether the line will be predominantly horizontal or vertical.</param>
        private void CalculateAndDrawLine(Graphics graphics, SolidBrush brush, Point drawPoint, int zoom, int lineLength, decimal lineRatio, bool horizontalLine)
        {
            // The line's current position inside each pixel. Defines when to shift to the next pixel.
            decimal horizontalSubpixel = 0, verticalSubpixel = 0;

            // Defines the amount of subpixels to increment in each iteration.
            // The longer direction of the line will increment a full pixel, while the shorter direction will be the line ratio.
            decimal horizontalIncrement = horizontalLine ? zoom : lineRatio * zoom;
            decimal verticalIncrement = horizontalLine ? lineRatio * zoom : zoom;

            // Defines whether each iteration will increase (+1) or decrease (-1) the point position, based on the line direction.
            int xPixelIncrease = LinePointsRight ? 1 : -1;
            int yPixelIncrease = LinePointsDown ? 1 : -1;

            for (int i = 0; i < lineLength + 1; i++)
            {
                graphics.FillRectangle(brush, drawPoint.X, drawPoint.Y, zoom, zoom);
                EndPoint = drawPoint;

                horizontalSubpixel += horizontalIncrement;
                verticalSubpixel += verticalIncrement;

                if (horizontalSubpixel >= zoom) // If the horizontal subpixel moves into or beyond the end of the current pixel...
                {
                    // Increases the pixel location to the next one and removes it from the subpixel.
                    drawPoint.X += xPixelIncrease * zoom;
                    horizontalSubpixel -= zoom;
                }

                if (verticalSubpixel >= zoom) // If the vertical subpixel moves into or beyond the end of the current pixel...
                {
                    // Increases the pixel location to the next one and removes it from the subpixel.
                    drawPoint.Y += yPixelIncrease * zoom;
                    verticalSubpixel -= zoom;
                }

                // If the draw point goes past any of the image's borders, stop drawing the line.
                if (drawPoint.X > UneditedImage!.Width * zoom - 1 || drawPoint.X < 0 || drawPoint.Y > UneditedImage!.Height * zoom - 1 || drawPoint.Y < 0)
                {
                    break;
                }
            }
        }
    }
}
