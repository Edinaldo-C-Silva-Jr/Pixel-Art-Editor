using PixelArtEditor.Extension_Methods;
using PixelArtEditor.Image_Editing.Drawing_Tools.Tools.PenTool;
using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.OtherTools
{
    public class EraserTool : DrawingTool
    {
        #region Undo Properties
        /// <summary>
        /// A copy of the Drawing Image before the drawing cycle started.
        /// </summary>
        private Bitmap? UneditedImage { get; set; }

        /// <summary>
        /// A copy of the Drawing Image after the drawing cycle finishes.
        /// </summary>
        private Bitmap? EditedImage { get; set; }

        /// <summary>
        /// The left boundary of the mouse movement, which is the furthest pixel on the left that was drawn on.
        /// </summary>
        private int LeftBoundary { get; set; } = 0;
        /// <summary>
        /// The right boundary of the mouse movement, which is the furthest pixel on the right that was drawn on.
        /// </summary>
        private int RightBoundary { get; set; } = 0;
        /// <summary>
        /// The upper boundary of the mouse movement, which is the furthest pixel on the top that was drawn on.
        /// </summary>
        private int UpperBoundary { get; set; } = 0;
        /// <summary>
        /// The lower boundary of the mouse movement, which is the furthest pixel on the bottom that was drawn on.
        /// </summary>
        private int LowerBoundary { get; set; } = 0;
        #endregion

        #region Drawing Properties
        /// <summary>
        /// The location of the previous pixel clicked.
        /// </summary>
        Point? PreviousPoint { get; set; }

        /// <summary>
        /// The Graphics object used for a drawing cycle.
        /// </summary>
        private Graphics? DrawingCycleGraphics { get; set; }
        #endregion

        private void ErasePixel(Graphics graphics, Color backgroundColor, Point location)
        {
            // Defines the area that will be erased...
            Rectangle eraseArea = new(location, new Size(1, 1));
            // ...And uses it as the clip area for the graphics.
            graphics.SetClip(eraseArea);

            // Then clears the area with the background color or transparency.
            graphics.Clear(backgroundColor);
        }

        protected void EraseLineBetweenPixels(Graphics drawGraphics, Color backgroundColor, Point location)
        {
            if (PreviousPoint.HasValue && PreviousPoint != location)
            {
                int horizontalDistance = Math.Abs(PreviousPoint.Value.X - location.X);
                int verticalDistance = Math.Abs(PreviousPoint.Value.Y - location.Y);

                // Defines the directions the line points towards.
                bool linePointsRight = PreviousPoint.Value.X < location.X;
                bool linePointsDown = PreviousPoint.Value.Y < location.Y;

                // The ratio between the horizontal and vertical distance of the starting and end point.
                decimal lineDistanceRatio;

                if (horizontalDistance > verticalDistance)
                {
                    lineDistanceRatio = DrawingCalculations.GetRatioBetweenDistances(verticalDistance, horizontalDistance);
                    CalculateAndEraseLine(drawGraphics, backgroundColor, horizontalDistance, lineDistanceRatio, true, linePointsRight, linePointsDown);
                }
                else
                {
                    lineDistanceRatio = DrawingCalculations.GetRatioBetweenDistances(horizontalDistance, verticalDistance);
                    CalculateAndEraseLine(drawGraphics, backgroundColor, verticalDistance, lineDistanceRatio, false, linePointsRight, linePointsDown);
                }

            }
        }

        private void CalculateAndEraseLine(Graphics graphics, Color bgColor, int lineLength, decimal lineRatio, bool horizontalLine, bool rightLine, bool downLine)
        {
            // The line's current position inside each pixel. Defines when to shift to the next pixel.
            decimal horizontalSubpixel = 0, verticalSubpixel = 0;

            // Defines the amount of subpixels to increment in each iteration.
            // The longer direction of the line will increment a full pixel, while the shorter direction will be the line ratio.
            decimal horizontalIncrement = horizontalLine ? 1 : lineRatio;
            decimal verticalIncrement = horizontalLine ? lineRatio : 1;

            // Defines whether each iteration will increase (+1) or decrease (-1) the point position, based on the line direction.
            int xPixelIncrease = rightLine ? 1 : -1;
            int yPixelIncrease = downLine ? 1 : -1;

            Point drawPoint = PreviousPoint!.Value;

            for (int i = 0; i < lineLength + 1; i++)
            {
                ErasePixel(graphics, bgColor, drawPoint);

                horizontalSubpixel += horizontalIncrement;
                verticalSubpixel += verticalIncrement;

                if (horizontalSubpixel >= 1) // If the horizontal subpixel moves into or beyond the end of the current pixel...
                {
                    // Increases the pixel location to the next one and removes it from the subpixel.
                    drawPoint.X += xPixelIncrease;
                    horizontalSubpixel -= 1;
                }

                if (verticalSubpixel >= 1) // If the vertical subpixel moves into or beyond the end of the current pixel...
                {
                    // Increases the pixel location to the next one and removes it from the subpixel.
                    drawPoint.Y += yPixelIncrease;
                    verticalSubpixel -= 1;
                }
            }
        }

        public override void PreviewTool(Graphics paintGraphics, Color drawingColor, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                // Preparing drawing properties.
                Point location = toolParameters.ClickLocation.Value;
                int zoom = toolParameters.PixelSize.Value;

                // Makes the eraser a white square with a black outline.
                paintGraphics.FillRectangle(Brushes.Black, location.X * zoom, location.Y * zoom, zoom, zoom);
                paintGraphics.FillRectangle(Brushes.White, location.X * zoom + 1, location.Y * zoom + 1, zoom - 2, zoom - 2);
            }
        }

        public override void UseToolClick(Bitmap drawingImage, Color drawingColor, OptionalToolParameters toolParameters)
        {
            if (toolParameters.BackgroundColor.HasValue && toolParameters.ClickLocation.HasValue)
            {
                // Preparing undo properties.
                UneditedImage = new(drawingImage);
                EditedImage = drawingImage;
                LeftBoundary = RightBoundary = toolParameters.ClickLocation.Value.X;
                UpperBoundary = LowerBoundary = toolParameters.ClickLocation.Value.Y;

                // Preparing drawing properties.
                DrawingCycleGraphics = Graphics.FromImage(drawingImage);

                ErasePixel(DrawingCycleGraphics, toolParameters.BackgroundColor.Value, toolParameters.ClickLocation.Value);

                PreviousPoint = toolParameters.ClickLocation.Value;
            }
        }

        public override void UseToolHold(OptionalToolParameters toolParameters)
        {
            if (toolParameters.BackgroundColor.HasValue && toolParameters.ClickLocation.HasValue)
            {
                // Preparing location data.
                int pixelClickedX = toolParameters.ClickLocation.Value.X;
                int pixelClickedY = toolParameters.ClickLocation.Value.Y;

                // Validating undo location properties.
                LeftBoundary = LeftBoundary.ValidateMaximum(pixelClickedX).ValidateMinimum(0);
                RightBoundary = RightBoundary.ValidateMinimum(pixelClickedX).ValidateMaximum(UneditedImage!.Width - 1);
                UpperBoundary = UpperBoundary.ValidateMaximum(pixelClickedY).ValidateMinimum(0);
                LowerBoundary = LowerBoundary.ValidateMinimum(pixelClickedY).ValidateMaximum(UneditedImage!.Height - 1);

                EraseLineBetweenPixels(DrawingCycleGraphics!, toolParameters.BackgroundColor.Value, toolParameters.ClickLocation.Value);

                PreviousPoint = toolParameters.ClickLocation.Value;
            }
        }

        public override void UseToolRelease(OptionalToolParameters toolParameters)
        {
            return;
        }

        public override IUndoRedoCommand CreateUndoStep(Point drawingImageLocation)
        {
            // Getting only the edited portion of the images.
            Rectangle editedArea = new(LeftBoundary, UpperBoundary, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
            UneditedImage = UneditedImage!.Clone(editedArea, PixelFormat.Format32bppArgb);
            EditedImage = EditedImage!.Clone(editedArea, PixelFormat.Format32bppArgb);

            // Getting the location where the edits started.
            Point editLocation = new(drawingImageLocation.X + LeftBoundary, drawingImageLocation.Y + UpperBoundary);

            PenCommand undoStep = new(new Bitmap(UneditedImage), new Bitmap(EditedImage), editLocation);
            ClearProperties();
            return undoStep;
        }

        /// <summary>
        /// Clears the properties used after a drawing cycle.
        /// This includes the boundaries, the edited and unedited images, the graphics and the brush.
        /// </summary>
        protected void ClearProperties()
        {
            LeftBoundary = RightBoundary = UpperBoundary = LowerBoundary = 0;
            UneditedImage?.Dispose();
            UneditedImage = null;
            EditedImage?.Dispose();
            EditedImage = null;

            PreviousPoint = null;
            DrawingCycleGraphics?.Dispose();
            DrawingCycleGraphics = null;
        }
    }
}
