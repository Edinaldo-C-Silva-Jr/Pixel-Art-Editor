
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
                case 0: // Up Right
                    for (int i = 0; i < lineLength; i++)
                    {
                        DrawPixel(graphics, brush, beginPoint.X + i * pixelSize, beginPoint.Y - i * pixelSize, pixelSize);
                    }
                    break;
                case 1: // Down Right
                    for (int i = 0; i < lineLength; i++)
                    {
                        DrawPixel(graphics, brush, beginPoint.X + i * pixelSize, beginPoint.Y + i * pixelSize, pixelSize);
                    }
                    break;
                case 2: // Down Left
                    for (int i = 0; i < lineLength; i++)
                    {
                        DrawPixel(graphics, brush, beginPoint.X - i * pixelSize, beginPoint.Y + i * pixelSize, pixelSize);
                    }
                    break;
                case 3: // Up Left
                    for (int i = 0; i < lineLength; i++)
                    {
                        DrawPixel(graphics, brush, beginPoint.X - i * pixelSize, beginPoint.Y - i * pixelSize, pixelSize);
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

            if (horizontalDifference > verticalDifference)
            {
                int lineLength = GetLineLengthInPixels(beginPoint.X, endPoint.X, parameters.PixelSize.Value);
                beginPoint = SnapPixelTopLeft(beginPoint, parameters.PixelSize.Value);

                if (horizontalDifference - verticalDifference < verticalDifference)
                {
                    if (endPoint.X > beginPoint.X)
                    {
                        if (endPoint.Y > beginPoint.Y)
                        {
                            DrawDiagonalLine(graphics, brush, beginPoint, parameters.PixelSize.Value, lineLength, 1);
                        }
                        else
                        {
                            DrawDiagonalLine(graphics, brush, beginPoint, parameters.PixelSize.Value, lineLength, 0);
                        }
                    }
                    else
                    {
                        if (endPoint.Y > beginPoint.Y)
                        {
                            DrawDiagonalLine(graphics, brush, beginPoint, parameters.PixelSize.Value, lineLength, 2);
                        }
                        else
                        {
                            DrawDiagonalLine(graphics, brush, beginPoint, parameters.PixelSize.Value, lineLength, 3);
                        }
                    }
                }
                else
                {
                    if (beginPoint.X > endPoint.X)
                    {
                        (endPoint.X, beginPoint.X) = (beginPoint.X, endPoint.X);
                        beginPoint = SnapPixelTopLeft(beginPoint, parameters.PixelSize.Value);
                    }
                    DrawRectangle(graphics, brush, beginPoint.X, beginPoint.Y, parameters.PixelSize.Value, lineLength, 1);
                }
            }
            else
            {
                int lineLength = GetLineLengthInPixels(beginPoint.Y, endPoint.Y, parameters.PixelSize.Value);
                beginPoint = SnapPixelTopLeft(beginPoint, parameters.PixelSize.Value);

                if (verticalDifference - horizontalDifference < horizontalDifference)
                {
                    if (endPoint.X > beginPoint.X)
                    {
                        if (endPoint.Y > beginPoint.Y)
                        {
                            DrawDiagonalLine(graphics, brush, beginPoint, parameters.PixelSize.Value, lineLength, 1);
                        }
                        else
                        {
                            DrawDiagonalLine(graphics, brush, beginPoint, parameters.PixelSize.Value, lineLength, 0);
                        }
                    }
                    else
                    {
                        if (endPoint.Y > beginPoint.Y)
                        {
                            DrawDiagonalLine(graphics, brush, beginPoint, parameters.PixelSize.Value, lineLength, 2);
                        }
                        else
                        {
                            DrawDiagonalLine(graphics, brush, beginPoint, parameters.PixelSize.Value, lineLength, 3);
                        }
                    }
                }
                else
                {
                    if (beginPoint.Y > endPoint.Y)
                    {
                        (endPoint.Y, beginPoint.Y) = (beginPoint.Y, endPoint.Y);
                        beginPoint = SnapPixelTopLeft(beginPoint, parameters.PixelSize.Value);
                    }
                    DrawRectangle(graphics, brush, beginPoint.X, beginPoint.Y, parameters.PixelSize.Value, 1, lineLength);
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
