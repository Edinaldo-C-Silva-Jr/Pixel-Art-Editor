namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Line
{
    /// <summary>
    /// A tool that draws a straight line by clicking and dragging.
    /// This tool only draws vertical or horizontal lines, snapping to the closest side to the mouse cursor.
    /// </summary>
    public class CardinalLineTool : BaseLineTool
    {
        protected override void DrawLine(Graphics drawGraphics, SolidBrush drawBrush)
        {
            if (StartingPoint.HasValue && EndPoint.HasValue)
            {
                DrawCardinalLine(drawGraphics, drawBrush, StartingPoint.Value, EndPoint.Value, 1);
            }
        }

        protected override void DrawLine(Graphics drawGraphics, SolidBrush drawBrush, int zoom)
        {
            if (StartingPoint.HasValue && EndPoint.HasValue)
            {
                // Creates new points and changes their location to match the zoom.
                Point firstPoint = new (StartingPoint.Value.X * zoom, StartingPoint.Value.Y * zoom);
                Point lastPoint = new (EndPoint.Value.X * zoom, EndPoint.Value.Y * zoom);

                DrawCardinalLine(drawGraphics, drawBrush, firstPoint, lastPoint, zoom);
            }
        }

        /// <summary>
        /// Draws a cardinal line, that can be either horizontal or vertical.
        /// </summary>
        /// <param name="graphics">The graphics for the image being drawn.</param>
        /// <param name="brush">The brush with the currently selected color.</param>
        /// <param name="firstPoint">The starting point of the line.</param>
        /// <param name="lastPoint">The ending point of the line.</param>
        /// <param name="zoom">The size of each pixel in the image.</param>
        protected void DrawCardinalLine(Graphics graphics, SolidBrush brush, Point firstPoint, Point lastPoint, int zoom)
        {
            // Calculates how much the mouse moved hozirontally and vertically.
            int horizontalDifference = Math.Abs(lastPoint.X - firstPoint.X);
            int verticalDifference = Math.Abs(lastPoint.Y - firstPoint.Y);

            if (horizontalDifference > verticalDifference) // If the mouse moved further horizontally, draws a horizontal line.
            {
                // Gives the EndPoint the same Y position as the StartingPoint, so the line is horizontal.
                EndPoint = new(EndPoint!.Value.X, StartingPoint!.Value.Y);

                // Gets the coordinates so that the firstPointX is always the smaller value.
                (firstPoint.X, lastPoint.X) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.X, lastPoint.X);
                graphics.FillRectangle(brush, firstPoint.X, firstPoint.Y, lastPoint.X - firstPoint.X + zoom, zoom);
            }
            else // If it moved further vertically, draw a vertical line.
            {
                // Gives the EndPoint the same Y position as the StartingPoint, so the line is horizontal.
                EndPoint = new(StartingPoint!.Value.X, EndPoint!.Value.Y);

                // Gets the coordinates so that the firstPointX is always the smaller value.
                (firstPoint.Y, lastPoint.Y) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.Y, lastPoint.Y);
                graphics.FillRectangle(brush, firstPoint.X, firstPoint.Y, zoom, lastPoint.Y - firstPoint.Y + zoom);
            }
        }
    }
}
