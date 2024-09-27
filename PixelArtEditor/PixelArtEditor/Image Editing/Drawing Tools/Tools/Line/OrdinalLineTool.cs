namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Line
{
    /// <summary>
    /// A tool that draws a straight line by clicking and dragging.
    /// This tool only draws vertical, horizontal or diagonal lines, snapping to the closest side to the mouse cursor.
    /// </summary>
    public class OrdinalLineTool : CardinalLineTool
    {
        #region Properties
        /// <summary>
        /// Indicates whether the line will be diagonal or cardinal.
        /// </summary>
        private bool LineIsDiagonal { get; set; }

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

        /// <summary>
        /// Checks the direction that the line is pointing.
        /// This checks if the line is diagonal, if it's pointing right and if it's pointing down.
        /// </summary>
        /// <param name="horizontalDifference">The absolute distance between the X position of the starting and ending points.</param>
        /// <param name="verticalDifference">The absolute distance between the Y position of the starting and ending points.</param>
        private void CheckLineDirections(int horizontalDifference, int verticalDifference)
        {
            // Checks whether the line extends further horizontally or vertically.
            bool lineGoesFurtherHorizontally = horizontalDifference > verticalDifference;
            // The line will be diagonal if the bigger difference is smaller than twice the smaller difference.
            LineIsDiagonal = (lineGoesFurtherHorizontally && horizontalDifference < 2 * verticalDifference) 
                || (!lineGoesFurtherHorizontally && verticalDifference < 2 * horizontalDifference);
            // Defines the directions the line points towards.
            LinePointsRight = StartingPoint!.Value.X < EndPoint!.Value.X;
            LinePointsDown = StartingPoint!.Value.Y < EndPoint!.Value.Y;
        }

        /// <summary>
        /// Draws a diagonal line. The direction of the line is defined by the LinePointsRight and LinePointsDown properties.
        /// The line is drawn one pixel at a time, shifting the position by 1 pixel horizontally and vertically to make it diagonal.
        /// </summary>
        /// <param name="graphics">The graphics for the image being drawn.</param>
        /// <param name="brush">The brush with the currently selected color.</param>
        /// <param name="firstPoint">The starting point of the line.</param>
        /// <param name="lastPoint">The ending point of the line.</param>
        /// <param name="zoom">The size of each pixel in the image.</param>
        private void DrawDiagonalLine(Graphics graphics, SolidBrush brush, Point firstPoint, Point lastPoint, int zoom)
        {
            // Line points right/down.
            if (LinePointsRight && LinePointsDown)
            {
                for (int i = 0; i < lastPoint.X - firstPoint.X + zoom; i += zoom)
                {
                    graphics.FillRectangle(brush, firstPoint.X + i, firstPoint.Y + i, zoom, zoom);
                    EndPoint = new(firstPoint.X + i, firstPoint.Y + i);

                    // If the last point reaches the right or bottom of the image, stop drawing the line.
                    if (firstPoint.X + i == UneditedImage!.Width - 1 || firstPoint.Y + i == UneditedImage!.Height - 1)
                    {
                        break;
                    }
                }
            }

            // Line points right/up.
            if (LinePointsRight && !LinePointsDown)
            {
                for (int i = 0; i < lastPoint.X - firstPoint.X + zoom; i += zoom)
                {
                    graphics.FillRectangle(brush, firstPoint.X + i, firstPoint.Y - i, zoom, zoom);
                    EndPoint = new(firstPoint.X + i, firstPoint.Y - i);

                    // If the last point reaches the right or top of the image, stop drawing the line.
                    if (firstPoint.X + i == UneditedImage!.Width - 1 || firstPoint.Y - i == 0)
                    {
                        break;
                    }
                }
            }

            // Line points left/down.
            if (!LinePointsRight && LinePointsDown)
            {
                for (int i = 0; i < firstPoint.X - lastPoint.X + zoom; i += zoom)
                {
                    graphics.FillRectangle(brush, firstPoint.X - i, firstPoint.Y + i, zoom, zoom);
                    EndPoint = new(firstPoint.X - i, firstPoint.Y + i);

                    // If the last point reaches the left or bottom of the image, stop drawing the line.
                    if (firstPoint.X - i == 0 || firstPoint.Y + i == UneditedImage!.Height - 1)
                    {
                        break;
                    }
                }
            }

            // Line points left/up.
            if (!LinePointsRight && !LinePointsDown)
            {
                for (int i = 0; i < firstPoint.X - lastPoint.X + zoom; i += zoom)
                {
                    graphics.FillRectangle(brush, firstPoint.X - i, firstPoint.Y - i, zoom, zoom);
                    EndPoint = new(firstPoint.X - i, firstPoint.Y - i);

                    // If the last point reaches the left or top of the image, stop drawing the line.
                    if (firstPoint.X - i == 0 || firstPoint.Y - i == 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}