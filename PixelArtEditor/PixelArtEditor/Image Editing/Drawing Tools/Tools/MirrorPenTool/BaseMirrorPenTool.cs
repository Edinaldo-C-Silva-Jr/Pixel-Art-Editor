using PixelArtEditor.Extension_Methods;
using PixelArtEditor.Image_Editing.Drawing_Tools.Tools.PenTool;
using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.MirrorPenTool
{
    /// <summary>
    /// A base class to implement the tools of type MirrorPen.
    /// It implements the preview method, all drawing methods and the creation of an undo step.
    /// </summary>
    public abstract class BaseMirrorPenTool : DrawingTool
    {
        #region Undo Properties
        /// <summary>
        /// A copy of the Drawing Image before the drawing cycle started.
        /// </summary>
        protected Bitmap? UneditedImage { get; set; }

        /// <summary>
        /// A copy of the Drawing Image after the drawing cycle finishes.
        /// </summary>
        protected Bitmap? EditedImage { get; set; }

        /// <summary>
        /// The left boundary of the mouse movement, which is the furthest pixel on the left that was drawn on.
        /// </summary>
        protected int LeftBoundary { get; set; } = 0;
        /// <summary>
        /// The right boundary of the mouse movement, which is the furthest pixel on the right that was drawn on.
        /// </summary>
        protected int RightBoundary { get; set; } = 0;
        /// <summary>
        /// The upper boundary of the mouse movement, which is the furthest pixel on the top that was drawn on.
        /// </summary>
        protected int UpperBoundary { get; set; } = 0;
        /// <summary>
        /// The lower boundary of the mouse movement, which is the furthest pixel on the bottom that was drawn on.
        /// </summary>
        protected int LowerBoundary { get; set; } = 0;
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

        /// <summary>
        /// The brush object used for a drawing cycle.
        /// </summary>
        private SolidBrush? DrawingBrush { get; set; }
        #endregion

        /// <summary>
        /// Draws a mirrored pixel.
        /// This will be a pixel in the click location, as well as another pixel mirrored in the image.
        /// </summary>
        /// <param name="drawGraphics">The graphics for the image being drawn.</param>
        /// <param name="drawBrush">The brush with the currently selected color.</param>
        /// <param name="location">The location of the mouse click inside the Drawing Box.</param>
        /// <param name="imageSize">The size of the image being drawn on.</param>
        protected abstract void DrawMirrorPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, Size imageSize);

        /// <summary>
        /// Draws a mirrored pixel while applying zoom, filling the pixel where the mouse clicked.
        /// This will be a pixel in the click location, as well as another pixel mirrored in the image.
        /// </summary>
        /// <param name="drawGraphics">The graphics for the image being drawn.</param>
        /// <param name="drawBrush">The brush with the currently selected color.</param>
        /// <param name="location">The location of the mouse click inside the Drawing Box.</param>
        /// <param name="imageSize">The size of the image being drawn on.</param>
        /// <param name="zoom">The size of each pixel in the image.</param>
        protected abstract void DrawMirrorPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, Size imageSize, int zoom);

        /// <summary>
        /// Draws a line between the previously clicked point and the currently clicked point.
        /// </summary>
        /// <param name="drawGraphics">The graphics for the image being drawn.</param>
        /// <param name="drawBrush">The brush with the currently selected color.</param>
        /// <param name="location">The location of the mouse click inside the Drawing Box.</param>
        /// <param name="imageSize">The size of the image being drawn on.</param>
        protected void DrawLineBetweenPixels(Graphics drawGraphics, SolidBrush drawBrush, Point location, Size imageSize)
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
                    CalculateAndDrawLine(drawGraphics, drawBrush, imageSize, horizontalDistance, lineDistanceRatio, true, linePointsRight, linePointsDown);
                }
                else
                {
                    lineDistanceRatio = DrawingCalculations.GetRatioBetweenDistances(horizontalDistance, verticalDistance);
                    CalculateAndDrawLine(drawGraphics, drawBrush, imageSize, verticalDistance, lineDistanceRatio, false, linePointsRight, linePointsDown);
                }

            }
        }

        /// <summary>
        /// Draws the line pixel by pixel.
        /// This is done by calculating when to shift the pixel position horizontally or vertically to draw the next pixel.
        /// </summary>
        /// <param name="graphics">The graphics to draw the pixel on.</param>
        /// <param name="brush">The brush to use when drawing the pixel.</param>
        /// <param name="imageSize">The size of the image being drawn on.</param>
        /// <param name="lineLength">The length of the line to be drawn. This should be the bigger distance between horizontal or vertical.</param>
        /// <param name="lineRatio">The ratio between the horizontal and vertical distances of the line.</param>
        /// <param name="horizontalLine">Whether the line will be predominantly horizontal or vertical.</param>
        /// <param name="rightLine">Defines if the line is pointing to the right or left.</param>
        /// <param name="downLine">Defines if the line is pointing down or up.</param>
        private void CalculateAndDrawLine(Graphics graphics, SolidBrush brush, Size imageSize, int lineLength, decimal lineRatio, bool horizontalLine, bool rightLine, bool downLine)
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
                DrawMirrorPixel(graphics, brush, drawPoint, imageSize);

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

        /// <summary>
        /// Sets the initial boundaries of the mouse movement.
        /// In the Base Mirror Pen Tool, it's the location of the mouse click when the drawing cycle begins.
        /// </summary>
        /// <param name="clickX">X position of the mouse click.</param>
        /// <param name="clickY">Y position of the mouse click.</param>
        /// <param name="imageSize">The size of the Drawing Image.</param>
        protected virtual void ValidateInitialBoundaries(int clickX, int clickY, Size imageSize)
        {
            LeftBoundary = RightBoundary = clickX;
            UpperBoundary = LowerBoundary = clickY;
        }

        /// <summary>
        /// Validates the boundaries during each drawing action to account for the mirrored positions.
        /// In the Base Mirror Pen Tool, the validation simply ensures the pixel is within the image.
        /// </summary>
        /// <param name="clickX">X position of the mouse click.</param>
        /// <param name="clickY">Y position of the mouse click.</param>
        /// <param name="imageSize">The size of the Drawing Image.</param>
        protected virtual void ValidateMirrorBoundaries(int clickX, int clickY, Size imageSize)
        {
            LeftBoundary = LeftBoundary.ValidateMaximum(clickX).ValidateMinimum(0);
            RightBoundary = RightBoundary.ValidateMinimum(clickX).ValidateMaximum(imageSize.Width - 1);
            UpperBoundary = UpperBoundary.ValidateMaximum(clickY).ValidateMinimum(0);
            LowerBoundary = LowerBoundary.ValidateMinimum(clickY).ValidateMaximum(imageSize.Height - 1);
        }

        public override void PreviewTool(Graphics paintGraphics, Color pixelColor, DrawingToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.ImageSize.HasValue && toolParameters.PixelSize.HasValue)
            {
                using SolidBrush previewBrush = MakePreviewBrush(pixelColor);
                DrawMirrorPixel(paintGraphics, previewBrush, toolParameters.ClickLocation.Value, toolParameters.ImageSize.Value, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolClick(Bitmap drawingImage, Color drawingColor, DrawingToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.ImageSize.HasValue)
            {
                // Preparing undo properties.
                UneditedImage = new(drawingImage);
                EditedImage = drawingImage;
                ValidateInitialBoundaries(toolParameters.ClickLocation.Value.X, toolParameters.ClickLocation.Value.Y, toolParameters.ImageSize.Value);

                // Preparing drawing properties.
                DrawingCycleGraphics = Graphics.FromImage(drawingImage);
                DrawingBrush = new(drawingColor);

                DrawMirrorPixel(DrawingCycleGraphics, DrawingBrush, toolParameters.ClickLocation.Value, toolParameters.ImageSize.Value);

                PreviousPoint = toolParameters.ClickLocation.Value;
            }
        }

        public override void UseToolHold(DrawingToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.ImageSize.HasValue)
            {
                // Preparing location data.
                int pixelClickedX = toolParameters.ClickLocation.Value.X;
                int pixelClickedY = toolParameters.ClickLocation.Value.Y;

                ValidateMirrorBoundaries(pixelClickedX, pixelClickedY, toolParameters.ImageSize.Value);

                DrawLineBetweenPixels(DrawingCycleGraphics!, DrawingBrush!, toolParameters.ClickLocation.Value, toolParameters.ImageSize.Value);

                PreviousPoint = toolParameters.ClickLocation.Value;
            }
        }

        public override void UseToolRelease(DrawingToolParameters toolParameters)
        {
            return;
        }

        public override IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            if (parameters.DrawingImageLocation.HasValue && UneditedImage is not null && EditedImage is not null)
            {
                // Getting only the edited portion of the images.
                Rectangle editedArea = new(LeftBoundary, UpperBoundary, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
                UneditedImage = UneditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);
                EditedImage = EditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);

                // Getting the location where the edits started.
                Point editLocation = new(parameters.DrawingImageLocation.Value.X + LeftBoundary, parameters.DrawingImageLocation.Value.Y + UpperBoundary);

                PenCommand undoStep = new(new Bitmap(UneditedImage), new Bitmap(EditedImage), editLocation);
                ClearProperties();
                return undoStep;
            }
            else
            {
                return null;
            }
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
            DrawingBrush?.Dispose();
            DrawingBrush = null;
        }
    }
}
