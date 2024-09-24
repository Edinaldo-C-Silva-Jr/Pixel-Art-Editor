namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Line
{
    /// <summary>
    /// A tool that draws a straight line by clicking and dragging.
    /// This tool only draws vertical, horizontal or diagonal lines, snapping to the closest side to the mouse cursor.
    /// </summary>
    public class OrdinalLineTool : BaseLineTool
    {
        protected override void DrawLine(Graphics drawGraphics, SolidBrush drawBrush)
        {
            if (StartingPoint.HasValue && EndPoint.HasValue)
            {
                // Calculates how much the mouse moved hozirontally and vertically.
                int horizontalDifference = Math.Abs(EndPoint.Value.X - StartingPoint.Value.X);
                int verticalDifference = Math.Abs(EndPoint.Value.Y - StartingPoint.Value.Y);

                // Checks whether the line extends further horizontally or vertically.
                bool lineIsHorizontal = horizontalDifference > verticalDifference;
                // The line will be diagonal if the bigger difference is smaller than twice the smaller difference.
                bool lineIsDiagonal = (lineIsHorizontal && horizontalDifference < 2 * verticalDifference) || (!lineIsHorizontal && verticalDifference < 2 * horizontalDifference);
                // Defines the directions the line points towards.
                bool linePointsRight = StartingPoint.Value.X < EndPoint.Value.X;
                bool linePointsDown = StartingPoint.Value.Y < EndPoint.Value.Y;

                if (lineIsDiagonal)
                {
                    if ((linePointsRight && linePointsDown) || (!linePointsRight && !linePointsDown))
                    {
                        if (lineIsHorizontal)
                        {
                            EndPoint = new(EndPoint.Value.X, StartingPoint.Value.Y + (EndPoint.Value.X - StartingPoint.Value.X));
                        }
                        else
                        {
                            EndPoint = new(StartingPoint.Value.X + (EndPoint.Value.Y - StartingPoint.Value.Y), EndPoint.Value.Y);
                        }
                    }
                    else
                    {
                        if (lineIsHorizontal)
                        {
                            EndPoint = new(EndPoint.Value.X, StartingPoint.Value.Y - (EndPoint.Value.X - StartingPoint.Value.X));
                        }
                        else
                        {
                            EndPoint = new(StartingPoint.Value.X - (EndPoint.Value.Y - StartingPoint.Value.Y), EndPoint.Value.Y);
                        }
                    }
                    
                    DrawDiagonalLine(drawGraphics, drawBrush, StartingPoint.Value, EndPoint.Value, 1);
                }
                else
                {
                    if (horizontalDifference > verticalDifference) // If the mouse moved further horizontally, draws a horizontal line.
                    {
                        // Gives the EndPoint the same Y position as the StartingPoint, so the line is horizontal.
                        EndPoint = new(EndPoint.Value.X, StartingPoint.Value.Y);

                        // Gets the coordinates so that the firstPointX is always the smaller value.
                        (int firstPointX, int lastPointX) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(StartingPoint.Value.X, EndPoint.Value.X);
                        drawGraphics.FillRectangle(drawBrush, firstPointX, StartingPoint.Value.Y, lastPointX - firstPointX + 1, 1);
                    }
                    else // If it moved further vertically, draw a vertical line.
                    {
                        // Gives the EndPoint the same Y position as the StartingPoint, so the line is horizontal.
                        EndPoint = new(StartingPoint.Value.X, EndPoint.Value.Y);

                        // Gets the coordinates so that the firstPointX is always the smaller value.
                        (int firstPointY, int lastPointY) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(StartingPoint.Value.Y, EndPoint.Value.Y);
                        drawGraphics.FillRectangle(drawBrush, StartingPoint.Value.X, firstPointY, 1, lastPointY - firstPointY + 1);
                    }
                }
            }
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
        private static void DrawDiagonalLine(Graphics graphics, SolidBrush brush, Point beginPoint, Point endPoint, int zoom)
        {
            if (beginPoint.X > endPoint.X)
            {
                (beginPoint, endPoint) = (endPoint, beginPoint);
            }

            if (endPoint.Y > beginPoint.Y)
            {
                for (int i = 0; i < endPoint.Y - beginPoint.Y + zoom; i += zoom)
                {
                    graphics.FillRectangle(brush, beginPoint.X + i, beginPoint.Y + i, zoom, zoom);
                }
            }
            else
            {
                for (int i = 0; i < beginPoint.Y - endPoint.Y + zoom; i += zoom)
                {
                    graphics.FillRectangle(brush, beginPoint.X + i, beginPoint.Y - i, zoom, zoom);
                }
            }
        }

        protected override void DrawLine(Graphics drawGraphics, SolidBrush drawBrush, int zoom)
        {
            if (StartingPoint.HasValue && EndPoint.HasValue)
            {
                // Calculates how much the mouse moved hozirontally and vertically.
                int horizontalDifference = Math.Abs(EndPoint.Value.X - StartingPoint.Value.X);
                int verticalDifference = Math.Abs(EndPoint.Value.Y - StartingPoint.Value.Y);

                // Checks whether the line extends further horizontally or vertically.
                bool lineIsHorizontal = horizontalDifference > verticalDifference;
                // The line will be diagonal if the bigger difference is smaller than twice the smaller difference.
                bool lineIsDiagonal = (lineIsHorizontal && horizontalDifference < 2 * verticalDifference) || (!lineIsHorizontal && verticalDifference < 2 * horizontalDifference);
                // Defines the directions the line points towards.
                bool linePointsRight = StartingPoint.Value.X < EndPoint.Value.X;
                bool linePointsDown = StartingPoint.Value.Y < EndPoint.Value.Y;

                // Creates new points and changes their location to match the zoom.
                Point firstPoint = new(StartingPoint.Value.X * zoom, StartingPoint.Value.Y * zoom);
                Point lastPoint = new(EndPoint.Value.X * zoom, EndPoint.Value.Y * zoom);

                if (lineIsDiagonal)
                {
                    if ((linePointsRight && linePointsDown) || (!linePointsRight && !linePointsDown))
                    {
                        if (lineIsHorizontal)
                        {
                            lastPoint = new(lastPoint.X, firstPoint.Y + (lastPoint.X - firstPoint.X));
                        }
                        else
                        {
                            lastPoint = new(firstPoint.X + (lastPoint.Y - firstPoint.Y), lastPoint.Y);
                        }
                    }
                    else
                    {
                        if (lineIsHorizontal)
                        {
                            lastPoint = new(lastPoint.X, firstPoint.Y - (lastPoint.X - firstPoint.X));
                        }
                        else
                        {
                            lastPoint = new(firstPoint.X - (lastPoint.Y - firstPoint.Y), lastPoint.Y);
                        }
                    }

                    DrawDiagonalLine(drawGraphics, drawBrush, firstPoint, lastPoint, zoom);
                }
                else
                {
                    if (horizontalDifference > verticalDifference) // If the mouse moved further horizontally, draw a horizontal line...
                    {
                        (firstPoint.X, lastPoint.X) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.X, lastPoint.X);
                        drawGraphics.FillRectangle(drawBrush, firstPoint.X, firstPoint.Y, lastPoint.X - firstPoint.X + zoom, zoom);
                    }
                    else // If it moved further vertically, draw a vertical line.
                    {
                        (firstPoint.Y, lastPoint.Y) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.Y, lastPoint.Y);
                        drawGraphics.FillRectangle(drawBrush, firstPoint.X, firstPoint.Y, zoom, lastPoint.Y - firstPoint.Y + zoom);
                    }
                }
            }
        }
    }
}