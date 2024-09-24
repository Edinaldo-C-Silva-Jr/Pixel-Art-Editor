using PixelArtEditor.Extension_Methods;
using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Line
{
    public abstract class BaseLineTool : DrawingTool
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
        #endregion

        #region Drawing Properties
        /// <summary>
        /// The point where the line begins, which is where the mouse is first clicked.
        /// </summary>
        protected Point? StartingPoint { get; set; }

        /// <summary>
        /// The point where the line ends, which is where the mouse is last clicked.
        /// </summary>
        protected Point? EndPoint { get; set; }

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
        /// Draws a line between the StartingPoint and Endpoint.
        /// </summary>
        /// <param name="drawGraphics">The graphics for the image being drawn.</param>
        /// <param name="drawBrush">The brush with the currently selected color.</param>
        protected abstract void DrawLine(Graphics drawGraphics, SolidBrush drawBrush);

        /// <summary>
        /// Draws a line between the StartingPoint and Endpoint while applying zoom.
        /// </summary>
        /// <param name="drawGraphics">The graphics for the image being drawn.</param>
        /// <param name="drawBrush">The brush with the currently selected color.</param>
        /// <param name="zoom">The size of each pixel in the image.</param>
        protected abstract void DrawLine(Graphics drawGraphics, SolidBrush drawBrush, int zoom);

        public override void PreviewTool(Graphics paintGraphics, Color drawingColor, OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                using SolidBrush previewBrush = MakePreviewBrush(drawingColor);
                DrawLine(paintGraphics, previewBrush, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolClick(Bitmap drawingImage, Color drawingColor, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue)
            {
                // Preparing undo properties.
                UneditedImage = new(drawingImage);
                EditedImage = drawingImage;

                // Preparing drawing properties.
                DrawingCycleGraphics = Graphics.FromImage(drawingImage);
                DrawingBrush = new(drawingColor);
                StartingPoint = toolParameters.ClickLocation.Value;
                EndPoint = toolParameters.ClickLocation.Value;
            }
        }

        public override void UseToolHold(OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                // Preparing drawing properties.
                EndPoint = toolParameters.ClickLocation.Value;
            }
            return;
        }

        public override void UseToolRelease(OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                // Preparing drawing properties.
                EndPoint = toolParameters.ClickLocation.Value;

                DrawLine(DrawingCycleGraphics!, DrawingBrush!);
            }
        }

        public override IUndoRedoCommand CreateUndoStep(Point drawingImageLocation)
        {
            // Validates the EndPoint to make sure it's within the image.
            int endPointX = EndPoint!.Value.X;
            endPointX = endPointX.ValidateMaximum(UneditedImage!.Width - 1);
            endPointX = endPointX.ValidateMinimum(0);
            int endPointY = EndPoint!.Value.Y;
            endPointY = endPointY.ValidateMaximum(UneditedImage!.Height - 1);
            endPointY = endPointY.ValidateMinimum(0);

            Point firstPoint = StartingPoint!.Value;
            Point lastPoint = new(endPointX, endPointY);

            // Makes sure the first point is always smaller than the last point.
            (firstPoint.X, lastPoint.X) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.X, lastPoint.X);
            (firstPoint.Y, lastPoint.Y) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.Y, lastPoint.Y);

            // Getting only the edited portion of the images.
            Rectangle editedArea = new(firstPoint.X, firstPoint.Y, lastPoint.X - firstPoint.X + 1, lastPoint.Y - firstPoint.Y + 1);
            UneditedImage = UneditedImage!.Clone(editedArea, PixelFormat.Format32bppArgb);
            EditedImage = EditedImage!.Clone(editedArea, PixelFormat.Format32bppArgb);

            // Getting the location where the edits started.
            Point editLocation = new(drawingImageLocation.X + firstPoint.X, drawingImageLocation.Y + firstPoint.Y);

            LineCommand undoStep = new(new Bitmap(UneditedImage), new Bitmap(EditedImage), editLocation);
            ClearProperties();
            return undoStep;
        }

        protected void ClearProperties()
        {
            StartingPoint = EndPoint = null;

            DrawingCycleGraphics?.Dispose();
            DrawingCycleGraphics = null;
            DrawingBrush?.Dispose();
            DrawingBrush = null;
        }
    }
}
