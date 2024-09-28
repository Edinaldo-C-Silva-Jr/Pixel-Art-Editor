﻿namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.RectangleTool
{
    public class SolidRectangleTool : BaseRectangleTool
    {
        protected override void DrawRectangle(Graphics drawGraphics, SolidBrush drawBrush)
        {
            if (StartingPoint.HasValue && EndPoint.HasValue)
            {
                Point firstPoint = StartingPoint.Value;
                Point lastPoint = EndPoint.Value;

                (firstPoint.X, lastPoint.X) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.X, lastPoint.X);
                (firstPoint.Y, lastPoint.Y) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.Y, lastPoint.Y);

                drawGraphics.FillRectangle(drawBrush, firstPoint.X, firstPoint.Y, lastPoint.X - firstPoint.X + 1, lastPoint.Y - firstPoint.Y + 1);
            }
        }

        protected override void DrawRectangle(Graphics drawGraphics, SolidBrush drawBrush, int zoom)
        {
            if (StartingPoint.HasValue && EndPoint.HasValue)
            {
                Point firstPoint = new(StartingPoint.Value.X * zoom, StartingPoint.Value.Y * zoom);
                Point lastPoint = new(EndPoint.Value.X * zoom, EndPoint.Value.Y * zoom);

                (firstPoint.X, lastPoint.X) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.X, lastPoint.X);
                (firstPoint.Y, lastPoint.Y) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.Y, lastPoint.Y);

                drawGraphics.FillRectangle(drawBrush, firstPoint.X, firstPoint.Y, lastPoint.X - firstPoint.X + zoom, lastPoint.Y - firstPoint.Y + zoom);
            }
        }
    }
}