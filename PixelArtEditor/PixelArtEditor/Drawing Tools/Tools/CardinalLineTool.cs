
namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class CardinalLineTool : DrawingTool
    {
        public Point? StartingPoint { get; set; }

        public override void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            // TODO
        }

        public override void UseToolClick(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue)
            {
                StartingPoint = toolParameters.ClickLocation.Value;
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
                int horizontalDifference = toolParameters.ClickLocation.Value.X - StartingPoint.Value.X;
                int verticalDifference = toolParameters.ClickLocation.Value.Y - StartingPoint.Value.Y;

                if (horizontalDifference > verticalDifference)
                {
                    int lineLength = GetDifferenceInPixels(toolParameters.ClickLocation.Value.X, StartingPoint.Value.X, toolParameters.PixelSize.Value);
                    StartingPoint = SnapPixelTopLeft(StartingPoint.Value, toolParameters.PixelSize.Value);
                    DrawRectangle(imageGraphics, colorBrush, StartingPoint.Value.X, StartingPoint.Value.Y, toolParameters.PixelSize.Value, lineLength, 1);
                }
                else
                {
                    int lineLength = GetDifferenceInPixels(toolParameters.ClickLocation.Value.Y, StartingPoint.Value.Y, toolParameters.PixelSize.Value);
                    StartingPoint = SnapPixelTopLeft(StartingPoint.Value, toolParameters.PixelSize.Value);
                    DrawRectangle(imageGraphics, colorBrush, StartingPoint.Value.X, StartingPoint.Value.Y, toolParameters.PixelSize.Value, 1, lineLength);
                }
            }

            StartingPoint = null;
        }
    }
}
