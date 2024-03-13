namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class OrdinalLineTool : DrawingTool
    {
        private Point? StartingPoint { get; set; }

        // Amount of times the drawing has been done, due to grid.
        private int repeats;

        private void DrawDiagonalLine(Graphics graphics, SolidBrush brush, Point beginPoint, int pixelSize, int lineLength, int lineDirection)
        {
            switch (lineDirection)
            {
                case 0: // Left Up (LinePointRight is 0 and LinePointDown is 0)
                    for (int i = 0; i < lineLength; i++)
                    {
                        Point pixelPoint = new(beginPoint.X - i * pixelSize, beginPoint.Y - i * pixelSize);
                        DrawPixel(graphics, brush, pixelPoint, pixelSize);
                    }
                    break;
                case 1: // Left Down (LinePointRight is 0 and LinePointDown is 1)
                    for (int i = 0; i < lineLength; i++)
                    {
                        Point pixelPoint = new(beginPoint.X - i * pixelSize, beginPoint.Y + i * pixelSize);
                        DrawPixel(graphics, brush, pixelPoint, pixelSize);
                    }
                    break;
                case 2: // Right Up (LinePointRight is 1 and LinePointDown is 0)
                    for (int i = 0; i < lineLength; i++)
                    {
                        Point pixelPoint = new(beginPoint.X + i * pixelSize, beginPoint.Y - i * pixelSize);
                        DrawPixel(graphics, brush, pixelPoint, pixelSize);
                    }
                    break;
                case 3: // Right Down (LinePointRight is 1 and LinePointDown is 1)
                    for (int i = 0; i < lineLength; i++)
                    {
                        Point pixelPoint = new(beginPoint.X + i * pixelSize, beginPoint.Y + i * pixelSize);
                        DrawPixel(graphics, brush, pixelPoint, pixelSize);
                    }
                    break;
            }
        }

        private void DrawOrdinalLine(Graphics graphics, SolidBrush brush, OptionalToolParameters parameters)
        {
            Point beginPoint = StartingPoint.Value;
            Point endPoint = parameters.ClickLocation.Value;

            int horizontalDifference = Math.Abs(endPoint.X - beginPoint.X);
            int verticalDifference = Math.Abs(endPoint.Y - beginPoint.Y);

            bool lineDirectionHorizontal = horizontalDifference > verticalDifference;
            bool linePointRight = endPoint.X > beginPoint.X;
            bool linePointDown = endPoint.Y > beginPoint.Y;
            bool diagonalLine = (lineDirectionHorizontal && horizontalDifference < 2 * verticalDifference)
                || (!lineDirectionHorizontal && verticalDifference < 2 * horizontalDifference);

            if (diagonalLine)
            {
                int beginCoordinate, endCoordinate; // These coordinates will either be the X or Y coordinate of the begin and end point, depending on which of them is bigger
                if (lineDirectionHorizontal)
                {
                    beginCoordinate = beginPoint.X;
                    endCoordinate = endPoint.X;
                    if (!linePointRight) // Swaps the begin and end if the line is being drawn backwards.
                    {
                        (beginCoordinate, endCoordinate) = (endCoordinate, beginCoordinate);
                    }
                }
                else
                {
                    beginCoordinate = beginPoint.Y;
                    endCoordinate = endPoint.Y;
                    if (!linePointDown) // Swaps the begin and end if the line is being drawn backwards.
                    {
                        (beginCoordinate, endCoordinate) = (endCoordinate, beginCoordinate);
                    }
                }

                int lineLength = GetLineLengthInPixels(beginCoordinate, endCoordinate, parameters.PixelSize.Value);
                beginPoint = SnapPixelTopLeft(beginPoint, parameters.PixelSize.Value);
                int lineDirection = Convert.ToInt32(linePointRight) * 2 + Convert.ToInt32(linePointDown);
                DrawDiagonalLine(graphics, brush, beginPoint, parameters.PixelSize.Value, lineLength, lineDirection);
            }
            else
            {
                if (lineDirectionHorizontal)
                {
                    if (!linePointRight)
                    {
                        (endPoint.X, beginPoint.X) = (beginPoint.X, endPoint.X);
                    }
                    int lineLength = GetLineLengthInPixels(beginPoint.X, endPoint.X, parameters.PixelSize.Value);
                    beginPoint = SnapPixelTopLeft(beginPoint, parameters.PixelSize.Value);
                    DrawRectangle(graphics, brush, beginPoint, parameters.PixelSize.Value, lineLength, 1);
                }
                else
                {
                    if (!linePointDown)
                    {
                        (endPoint.Y, beginPoint.Y) = (beginPoint.Y, endPoint.Y);
                    }
                    int lineLength = GetLineLengthInPixels(beginPoint.Y, endPoint.Y, parameters.PixelSize.Value);
                    beginPoint = SnapPixelTopLeft(beginPoint, parameters.PixelSize.Value);
                    DrawRectangle(graphics, brush, beginPoint, parameters.PixelSize.Value, 1, lineLength);
                }
            }
        }

        public override void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                colorBrush = MakePreviewBrush(colorBrush);
                DrawOrdinalLine(paintGraphics, colorBrush, toolParameters);
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
                DrawOrdinalLine(imageGraphics, colorBrush, toolParameters);
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
