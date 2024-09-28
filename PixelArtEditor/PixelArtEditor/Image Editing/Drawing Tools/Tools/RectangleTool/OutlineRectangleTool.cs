namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.RectangleTool
{
    /// <summary>
    /// A tool that draws an outline of a rectangle with a specified color.
    /// </summary>
    public class OutlineRectangleTool : BaseRectangleTool
    {
        protected override void DrawRectangle(Graphics drawGraphics, SolidBrush drawBrush)
        {
            if (StartingPoint.HasValue && EndPoint.HasValue)
            {
                // Orders the points so the first point is always the top left of the rectangle.
                Point firstPoint = StartingPoint.Value;
                Point lastPoint = EndPoint.Value;

                (firstPoint.X, lastPoint.X) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.X, lastPoint.X);
                (firstPoint.Y, lastPoint.Y) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.Y, lastPoint.Y);

                // Draws 4 rectangles, being each of the sides of the outline.
                drawGraphics.FillRectangle(drawBrush, firstPoint.X, firstPoint.Y, lastPoint.X - firstPoint.X + 1, 1);
                drawGraphics.FillRectangle(drawBrush, firstPoint.X, firstPoint.Y, 1, lastPoint.Y - firstPoint.Y + 1);
                drawGraphics.FillRectangle(drawBrush, firstPoint.X, lastPoint.Y, lastPoint.X - firstPoint.X + 1, 1);
                drawGraphics.FillRectangle(drawBrush, lastPoint.X, firstPoint.Y, 1, lastPoint.Y - firstPoint.Y + 1);
            }
        }

        protected override void DrawRectangle(Graphics drawGraphics, SolidBrush drawBrush, int zoom)
        {
            if (StartingPoint.HasValue && EndPoint.HasValue)
            {
                // Orders the points so the first point is always the top left of the rectangle. Also applies the zoom to the location.
                Point firstPoint = new(StartingPoint.Value.X * zoom, StartingPoint.Value.Y * zoom);
                Point lastPoint = new(EndPoint.Value.X * zoom, EndPoint.Value.Y * zoom);

                (firstPoint.X, lastPoint.X) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.X, lastPoint.X);
                (firstPoint.Y, lastPoint.Y) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.Y, lastPoint.Y);

                // Draws 4 rectangles, being each of the sides of the outline.
                drawGraphics.FillRectangle(drawBrush, firstPoint.X, firstPoint.Y, lastPoint.X - firstPoint.X + zoom, zoom);
                drawGraphics.FillRectangle(drawBrush, firstPoint.X, firstPoint.Y, zoom, lastPoint.Y - firstPoint.Y + zoom);
                drawGraphics.FillRectangle(drawBrush, firstPoint.X, lastPoint.Y, lastPoint.X - firstPoint.X + zoom, zoom);
                drawGraphics.FillRectangle(drawBrush, lastPoint.X, firstPoint.Y, zoom, lastPoint.Y - firstPoint.Y + zoom);
            }
        }
    }
}
