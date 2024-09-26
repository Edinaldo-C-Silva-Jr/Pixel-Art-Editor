namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Line
{
    /// <summary>
    /// A tool that draws a straight line by clicking and dragging.
    /// This tool only draws vertical, horizontal or diagonal lines, snapping to the closest side to the mouse cursor.
    /// </summary>
    public class OrdinalLineTool : CardinalLineTool
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

                CheckLineDirections(horizontalDifference, verticalDifference);

                if (LineIsDiagonal)
                {
                    DrawDiagonalLine(drawGraphics, drawBrush, StartingPoint.Value, EndPoint.Value, 1);
                }
                else
                {
                    DrawCardinalLine(drawGraphics, drawBrush, StartingPoint.Value, EndPoint.Value, 1);
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

                CheckLineDirections(horizontalDifference, verticalDifference);

                // Creates new points and changes their location to match the zoom.
                Point firstPoint = new(StartingPoint.Value.X * zoom, StartingPoint.Value.Y * zoom);
                Point lastPoint = new(EndPoint.Value.X * zoom, EndPoint.Value.Y * zoom);

                if (LineIsDiagonal)
                {
                    DrawDiagonalLine(drawGraphics, drawBrush, firstPoint, lastPoint, zoom);
                }
                else
                {
                    DrawCardinalLine(drawGraphics, drawBrush, firstPoint, lastPoint, zoom);
                }
            }
        }

        private void CheckLineDirections(int horizontalDifference, int verticalDifference)
        {
            // Checks whether the line extends further horizontally or vertically.
            bool lineIsHorizontal = horizontalDifference > verticalDifference;
            // The line will be diagonal if the bigger difference is smaller than twice the smaller difference.
            LineIsDiagonal = (lineIsHorizontal && horizontalDifference < 2 * verticalDifference) || (!lineIsHorizontal && verticalDifference < 2 * horizontalDifference);
            // Defines the directions the line points towards.
            LinePointsRight = StartingPoint!.Value.X < EndPoint!.Value.X;
            LinePointsDown = StartingPoint!.Value.Y < EndPoint!.Value.Y;
        }


        /// <summary>
        /// Draws a diagonal line. The direction and size of the line are defined by the parameters.
        /// </summary>
        /// <param name="graphics">The graphics to draw the pixel on.</param>
        /// <param name="brush">The brush to use when drawing the pixel.</param>
        /// <param name="firstPoint">The starting point of the line.</param>
        /// <param name="pixelSize">The size of a pixel in the image.</param>
        /// <param name="lineLength">The length of the line in pixel sizes.</param>
        /// <param name="lineDirection">The direction of the line. This can be 0 = Left Up, 1 = Left Down, 2 = Right Up and 3 = Right Down.</param>
        private void DrawDiagonalLine(Graphics graphics, SolidBrush brush, Point firstPoint, Point lastPoint, int zoom)
        {
            if (LinePointsRight && LinePointsDown)
            {
                for (int i = 0; i < lastPoint.X - firstPoint.X + zoom; i += zoom)
                {
                    graphics.FillRectangle(brush, firstPoint.X + i, firstPoint.Y + i, zoom, zoom);
                    EndPoint = new(firstPoint.X + i, firstPoint.Y + i);

                    if (firstPoint.X + i == UneditedImage!.Height - 1 || firstPoint.Y + i == UneditedImage!.Width - 1)
                    {
                        break;
                    }
                }
            }

            if (LinePointsRight && !LinePointsDown)
            {
                for (int i = 0; i < lastPoint.X - firstPoint.X + zoom; i += zoom)
                {
                    graphics.FillRectangle(brush, firstPoint.X + i, firstPoint.Y - i, zoom, zoom);
                    EndPoint = new(firstPoint.X + i, firstPoint.Y - i);

                    if (firstPoint.X + i == UneditedImage!.Height - 1 || firstPoint.Y - i == 0)
                    {
                        break;
                    }
                }
            }

            if (!LinePointsRight && LinePointsDown)
            {
                for (int i = 0; i < firstPoint.X - lastPoint.X + zoom; i += zoom)
                {
                    graphics.FillRectangle(brush, firstPoint.X - i, firstPoint.Y + i, zoom, zoom);
                    EndPoint = new(firstPoint.X - i, firstPoint.Y + i);

                    if (firstPoint.X - i == 0 || firstPoint.Y + i == UneditedImage!.Width - 1)
                    {
                        break;
                    }
                }
            }

            if (!LinePointsRight && !LinePointsDown)
            {
                for (int i = 0; i < firstPoint.X - lastPoint.X + zoom; i += zoom)
                {
                    graphics.FillRectangle(brush, firstPoint.X - i, firstPoint.Y - i, zoom, zoom);
                    EndPoint = new(firstPoint.X - i, firstPoint.Y - i);

                    if (firstPoint.X - i == 0 || firstPoint.Y - i == 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}