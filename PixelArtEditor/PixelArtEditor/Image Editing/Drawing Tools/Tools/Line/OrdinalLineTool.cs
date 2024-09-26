namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Line
{
    /// <summary>
    /// A tool that draws a straight line by clicking and dragging.
    /// This tool only draws vertical, horizontal or diagonal lines, snapping to the closest side to the mouse cursor.
    /// </summary>
    public class OrdinalLineTool : BaseLineTool
    {
        private bool LineIsDiagonal { get; set; }
        private bool LinePointsRight { get; set; }
        private bool LinePointsDown { get; set; }

        protected override void DrawLine(Graphics drawGraphics, SolidBrush drawBrush)
        {
            if (StartingPoint.HasValue && EndPoint.HasValue && UneditedImage is not null)
            {
                // Calculates how much the mouse moved hozirontally and vertically.
                int horizontalDifference = Math.Abs(EndPoint.Value.X - StartingPoint.Value.X);
                int verticalDifference = Math.Abs(EndPoint.Value.Y - StartingPoint.Value.Y);

                // Checks whether the line extends further horizontally or vertically.
                bool lineIsHorizontal = horizontalDifference > verticalDifference;
                // The line will be diagonal if the bigger difference is smaller than twice the smaller difference.
                LineIsDiagonal = (lineIsHorizontal && horizontalDifference < 2 * verticalDifference) || (!lineIsHorizontal && verticalDifference < 2 * horizontalDifference);
                // Defines the directions the line points towards.
                LinePointsRight = StartingPoint.Value.X < EndPoint.Value.X;
                LinePointsDown = StartingPoint.Value.Y < EndPoint.Value.Y;

                if (LineIsDiagonal)
                {
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
        private void DrawDiagonalLine(Graphics graphics, SolidBrush brush, Point beginPoint, Point endPoint, int zoom)
        {
            if (LinePointsRight && LinePointsDown)
            {
                for (int i = 0; i < endPoint.X - beginPoint.X + zoom; i += zoom)
                {
                    graphics.FillRectangle(brush, beginPoint.X + i, beginPoint.Y + i, zoom, zoom);
                    EndPoint = new(beginPoint.X + i, beginPoint.Y + i);

                    if (beginPoint.X + i == UneditedImage!.Height - 1 || beginPoint.Y + i == UneditedImage!.Width - 1)
                    {
                        break;
                    }
                }
            }

            if (LinePointsRight && !LinePointsDown)
            {
                for (int i = 0; i < endPoint.X - beginPoint.X + zoom; i += zoom)
                {
                    graphics.FillRectangle(brush, beginPoint.X + i, beginPoint.Y - i, zoom, zoom);
                    EndPoint = new(beginPoint.X + i, beginPoint.Y - i);

                    if (beginPoint.X + i == UneditedImage!.Height - 1 || beginPoint.Y - i == 0)
                    {
                        break;
                    }
                }
            }

            if (!LinePointsRight && LinePointsDown)
            {
                for (int i = 0; i < beginPoint.X - endPoint.X + zoom; i += zoom)
                {
                    graphics.FillRectangle(brush, beginPoint.X - i, beginPoint.Y + i, zoom, zoom);
                    EndPoint = new(beginPoint.X - i, beginPoint.Y + i);

                    if (beginPoint.X - i == 0 || beginPoint.Y + i == UneditedImage!.Width - 1)
                    {
                        break;
                    }
                }
            }

            if (!LinePointsRight && !LinePointsDown)
            {
                for (int i = 0; i < beginPoint.X - endPoint.X + zoom; i += zoom)
                {
                    graphics.FillRectangle(brush, beginPoint.X - i, beginPoint.Y - i, zoom, zoom);
                    EndPoint = new(beginPoint.X - i, beginPoint.Y - i);

                    if (beginPoint.X - i == 0 || beginPoint.Y - i == 0)
                    {
                        break;
                    }
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
                LineIsDiagonal = (lineIsHorizontal && horizontalDifference < 2 * verticalDifference) || (!lineIsHorizontal && verticalDifference < 2 * horizontalDifference);
                // Defines the directions the line points towards.
                LinePointsRight = StartingPoint.Value.X < EndPoint.Value.X;
                LinePointsDown = StartingPoint.Value.Y < EndPoint.Value.Y;

                // Creates new points and changes their location to match the zoom.
                Point firstPoint = new(StartingPoint.Value.X * zoom, StartingPoint.Value.Y * zoom);
                Point lastPoint = new(EndPoint.Value.X * zoom, EndPoint.Value.Y * zoom);

                if (LineIsDiagonal)
                {
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