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
                // Calculates how much the mouse moved hozirontally and vertically.
                int horizontalDifference = Math.Abs(EndPoint.Value.X - StartingPoint.Value.X);
                int verticalDifference = Math.Abs(EndPoint.Value.Y - StartingPoint.Value.Y);

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

        protected override void DrawLine(Graphics drawGraphics, SolidBrush drawBrush, int zoom)
        {
            if (StartingPoint.HasValue && EndPoint.HasValue)
            {
                // Calculates how much the mouse moved hozirontally and vertically.
                int horizontalDifference = Math.Abs(EndPoint.Value.X - StartingPoint.Value.X);
                int verticalDifference = Math.Abs(EndPoint.Value.Y - StartingPoint.Value.Y);

                // Creates new points and changes their location to match the zoom.
                Point firstPoint = new (StartingPoint.Value.X * zoom, StartingPoint.Value.Y * zoom);
                Point lastPoint = new (EndPoint.Value.X * zoom, EndPoint.Value.Y * zoom);

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
