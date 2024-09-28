using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.RectangleTool
{
    public abstract class BaseRectangleTool : DrawingTool
    {
        #region Undo Properties
        /// <summary>
        /// A copy of the Drawing Image before the drawing cycle started.
        /// </summary>
        protected Bitmap? UneditedImage { get; private set; }

        /// <summary>
        /// A copy of the Drawing Image after the drawing cycle finishes.
        /// </summary>
        protected Bitmap? EditedImage { get; private set; }
        #endregion

        #region Drawing Properties
        /// <summary>
        /// The point where the rectangle begins, which is where the mouse is first clicked.
        /// </summary>
        protected Point? StartingPoint { get; set; }

        /// <summary>
        /// The opposite vertice of the starting point of the rectangle, which is where the mouse is last clicked.
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

        protected abstract void DrawRectangle(Graphics drawGraphics, SolidBrush drawBrush);

        protected abstract void DrawRectangle(Graphics drawGraphics, SolidBrush drawBrush, int zoom);

        public override void PreviewTool(Graphics paintGraphics, Color drawingColor, OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                using SolidBrush previewBrush = MakePreviewBrush(drawingColor);
                DrawRectangle(paintGraphics, previewBrush, toolParameters.PixelSize.Value);
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
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue)
            {
                // Preparing drawing properties.
                EndPoint = toolParameters.ClickLocation.Value;
            }
        }

        public override void UseToolRelease(OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue)
            {
                // Preparing drawing properties.
                EndPoint = toolParameters.ClickLocation.Value;

                DrawRectangle(DrawingCycleGraphics!, DrawingBrush!);
            }
        }

        public override IUndoRedoCommand CreateUndoStep(Point drawingImageLocation)
        {
            throw new NotImplementedException();
        }

        protected void ClearProperties()
        {
            UneditedImage?.Dispose();
            UneditedImage = null;
            EditedImage?.Dispose();
            EditedImage = null;

            StartingPoint = EndPoint = null;

            DrawingCycleGraphics?.Dispose();
            DrawingCycleGraphics = null;
            DrawingBrush?.Dispose();
            DrawingBrush = null;
        }
    }
}
