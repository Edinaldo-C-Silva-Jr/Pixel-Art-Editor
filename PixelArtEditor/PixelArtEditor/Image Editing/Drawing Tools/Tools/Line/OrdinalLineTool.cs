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

                if (lineIsDiagonal)
                {
                    if (lineIsHorizontal)
                    {
                        EndPoint = new(EndPoint.Value.X, StartingPoint.Value.Y + (EndPoint.Value.X - StartingPoint.Value.X));
                    }
                    else
                    {
                        EndPoint = new(StartingPoint.Value.X + (EndPoint.Value.Y - StartingPoint.Value.Y), EndPoint.Value.Y);
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
                for (int i = 0; i < endPoint.Y - beginPoint.Y; i++)
                {
                    graphics.FillRectangle(brush, beginPoint.X + i, beginPoint.Y + i, 1, 1);
                }
            }
            else
            {
                for (int i = 0; i < beginPoint.Y - endPoint.Y; i++)
                {
                    graphics.FillRectangle(brush, beginPoint.X + i, beginPoint.Y - i, 1, 1);
                }
            }
        }

        protected override void DrawLine(Graphics drawGraphics, SolidBrush drawBrush, int zoom)
        {
            return;
        }
    }
}