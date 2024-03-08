
namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class CardinalLineTool : DrawingTool
    {
        private Point? StartingPoint { get; set; }
        
        // Amount of times the drawing has been done, due to grid.
        private int repeats;

        private void DrawCardinalLine(Graphics graphics, SolidBrush brush, OptionalToolParameters parameters)
        {
            int horizontalDifference = Math.Abs(parameters.ClickLocation.Value.X - StartingPoint.Value.X);
            int verticalDifference = Math.Abs(parameters.ClickLocation.Value.Y - StartingPoint.Value.Y);

            Point beginPoint = StartingPoint.Value;
            Point endPoint = parameters.ClickLocation.Value;

            if (horizontalDifference > verticalDifference)
            {
                if (beginPoint.X > endPoint.X)
                {
                    (endPoint.X, beginPoint.X) = (beginPoint.X, endPoint.X);
                }

                int lineLength = GetLineLengthInPixels(beginPoint.X, endPoint.X, parameters.PixelSize.Value);
                beginPoint = SnapPixelTopLeft(beginPoint, parameters.PixelSize.Value);
                DrawRectangle(graphics, brush, beginPoint.X, beginPoint.Y, parameters.PixelSize.Value, lineLength, 1);
            }
            else
            {
                if (beginPoint.Y > endPoint.Y)
                {
                    (endPoint.Y, beginPoint.Y) = (beginPoint.Y, endPoint.Y);
                }

                int lineLength = GetLineLengthInPixels(beginPoint.Y, endPoint.Y, parameters.PixelSize.Value);
                beginPoint = SnapPixelTopLeft(beginPoint, parameters.PixelSize.Value);
                DrawRectangle(graphics, brush, beginPoint.X, beginPoint.Y, parameters.PixelSize.Value, 1, lineLength);
            }
        }

        public override void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                colorBrush = MakePreviewBrush(colorBrush);
                DrawCardinalLine(paintGraphics, colorBrush, toolParameters);
            }
        }

        public override void UseToolClick(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue)
            {
                StartingPoint = toolParameters.ClickLocation.Value;
                repeats = 0;
            }
        }

        public override void UseToolHold(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            return;
        }

        public override void UseToolRelease(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                DrawCardinalLine(imageGraphics, colorBrush, toolParameters);
            }

            // Draw twice. Once on the image and once on the grid image
            repeats++;
            if (repeats == 2)
            {
                StartingPoint = null;
            }
        }
    }
}
