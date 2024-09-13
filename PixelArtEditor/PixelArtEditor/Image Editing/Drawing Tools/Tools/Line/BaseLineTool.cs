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
        private Point? StartingPoint { get; set; }

        /// <summary>
        /// The point where the line ends, which is where the mouse is last clicked.
        /// </summary>
        private Point? EndPoint { get; set; }

        /// <summary>
        /// The Graphics object used for a drawing cycle.
        /// </summary>
        private Graphics? DrawingCycleGraphics { get; set; }

        /// <summary>
        /// The brush object used for a drawing cycle.
        /// </summary>
        private SolidBrush? DrawingBrush { get; set; }
        #endregion

        protected abstract void DrawLine(Graphics drawGraphics, SolidBrush drawBrush, Point location);

        protected abstract void DrawLine(Graphics drawGraphics, SolidBrush drawBrush, Point location, int pixelSize);

        public override void PreviewTool(Graphics paintGraphics, Color drawingColor, OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                using SolidBrush previewBrush = MakePreviewBrush(drawingColor);
                DrawLine(paintGraphics, previewBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
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

                DrawLine(DrawingCycleGraphics!, DrawingBrush!, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
            }
        }

        public override IUndoRedoCommand CreateUndoStep(Point drawingImageLocation)
        {
            // Validating the line points.
            Point firstPoint = StartingPoint!.Value;
            Point lastPoint = EndPoint!.Value;
            (firstPoint.X, lastPoint.X) = DrawingCalculations.SwapCoordinatesWhenStartIsBigger(firstPoint.X, lastPoint.X);
            (firstPoint.Y, lastPoint.Y) = DrawingCalculations.SwapCoordinatesWhenStartIsBigger(firstPoint.Y, lastPoint.Y);

            // Getting only the edited portion of the images.
            Rectangle editedArea = new(firstPoint.X, firstPoint.Y, lastPoint.X - firstPoint.X + 1, lastPoint.X - firstPoint.X + 1);
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
