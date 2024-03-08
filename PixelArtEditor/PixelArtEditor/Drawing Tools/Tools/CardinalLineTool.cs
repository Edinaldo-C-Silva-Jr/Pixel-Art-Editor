
namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class CardinalLineTool : DrawingTool
    {
        private Point? StartingPoint { get; set; }
        
        // Amount of times the drawing has been done, due to grid.
        private int repeats;

        private void DrawCardinalLine(Graphics graphics, SolidBrush brush, OptionalToolParameters parameters)
        {
            int horizontalDifference = parameters.ClickLocation.Value.X - StartingPoint.Value.X;
            int verticalDifference = parameters.ClickLocation.Value.Y - StartingPoint.Value.Y;

            if (horizontalDifference > verticalDifference)
            {
                int lineLength = GetDifferenceInPixels(parameters.ClickLocation.Value.X, StartingPoint.Value.X, parameters.PixelSize.Value);
                StartingPoint = SnapPixelTopLeft(StartingPoint.Value, parameters.PixelSize.Value);
                DrawRectangle(graphics, brush, StartingPoint.Value.X, StartingPoint.Value.Y, parameters.PixelSize.Value, lineLength, 1);
            }
            else
            {
                int lineLength = GetDifferenceInPixels(parameters.ClickLocation.Value.Y, StartingPoint.Value.Y, parameters.PixelSize.Value);
                StartingPoint = SnapPixelTopLeft(StartingPoint.Value, parameters.PixelSize.Value);
                DrawRectangle(graphics, brush, StartingPoint.Value.X, StartingPoint.Value.Y, parameters.PixelSize.Value, 1, lineLength);
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
