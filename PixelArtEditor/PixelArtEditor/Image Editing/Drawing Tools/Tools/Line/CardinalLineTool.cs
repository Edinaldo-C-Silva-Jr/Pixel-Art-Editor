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
                // Creates new variables to manipulate the points without affecting the original points.
                Point firstPoint = StartingPoint.Value;
                Point lastPoint = EndPoint.Value;

                // Calculates how much the mouse moved hozirontally and vertically.
                int horizontalDifference = Math.Abs(lastPoint.X - firstPoint.X);
                int verticalDifference = Math.Abs(lastPoint.Y - firstPoint.Y);

                if (horizontalDifference > verticalDifference) // If the mouse moved further horizontally, draw a horizontal line...
                {
                    (firstPoint.X, lastPoint.X) = DrawingCalculations.SwapCoordinatesWhenStartIsBigger(firstPoint.X, lastPoint.X);
                    drawGraphics.FillRectangle(drawBrush, firstPoint.X, firstPoint.Y, lastPoint.X - firstPoint.X + 1, 1);
                }
                else // Otherwise, draw a vertical line.
                {
                    (firstPoint.Y, lastPoint.Y) = DrawingCalculations.SwapCoordinatesWhenStartIsBigger(firstPoint.Y, lastPoint.Y);
                    drawGraphics.FillRectangle(drawBrush, firstPoint.X, firstPoint.Y, 1, lastPoint.Y - firstPoint.Y + 1);
                }
            }
        }

        protected override void DrawLine(Graphics drawGraphics, SolidBrush drawBrush, int zoom)
        {
            if (StartingPoint.HasValue && EndPoint.HasValue)
            {
                // Creates new points and changes their location to match the zoom.
                Point firstPoint = new (StartingPoint.Value.X * zoom, StartingPoint.Value.Y * zoom);
                Point lastPoint = new (EndPoint.Value.X * zoom, EndPoint.Value.Y * zoom);

                // Calculates how much the mouse moved hozirontally and vertically.
                int horizontalDifference = Math.Abs(lastPoint.X - firstPoint.X);
                int verticalDifference = Math.Abs(lastPoint.Y - firstPoint.Y);

                if (horizontalDifference > verticalDifference) // If the mouse moved further horizontally, draw a horizontal line...
                {
                    (firstPoint.X, lastPoint.X) = DrawingCalculations.SwapCoordinatesWhenStartIsBigger(firstPoint.X, lastPoint.X);
                    drawGraphics.FillRectangle(drawBrush, firstPoint.X, firstPoint.Y, lastPoint.X - firstPoint.X + zoom, zoom);
                }
                else // Otherwise, draw a vertical line.
                {
                    (firstPoint.Y, lastPoint.Y) = DrawingCalculations.SwapCoordinatesWhenStartIsBigger(firstPoint.Y, lastPoint.Y);
                    drawGraphics.FillRectangle(drawBrush, firstPoint.X, firstPoint.Y, zoom, lastPoint.Y - firstPoint.Y + zoom);
                }
            }
        }
    }
}
