using PixelArtEditor.Extension_Methods;
using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Pen
{
    /// <summary>
    /// A class to serve as a base for the Pen type tools.
    /// It implements the preview method, all drawing methods and the creation of an undo step.
    /// </summary>
    public abstract class BasePenTool : DrawingTool
    {
        #region Properties
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
        /// Draws a single pixel.
        /// </summary>
        /// <param name="drawGraphics">The graphics for the image being drawn.</param>
        /// <param name="drawBrush">The brush with the currently selected color.</param>
        /// <param name="location">The location of the mouse click inside the Drawing Box.</param>
        protected abstract void DrawPenPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location);

        /// <summary>
        /// Draws a single pixel while applying zoom, filling the pixel where the mouse clicked.
        /// </summary>
        /// <param name="drawGraphics">The graphics for the image being drawn.</param>
        /// <param name="drawBrush">The brush with the currently selected color.</param>
        /// <param name="location">The location of the mouse click inside the Drawing Box.</param>
        /// <param name="zoom">The size of each pixel in the image.</param>
        protected abstract void DrawPenPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, int zoom);

        public override void PreviewTool(Graphics paintGraphics, Color pixelColor, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                using SolidBrush previewBrush = MakePreviewBrush(pixelColor);
                DrawPenPixel(paintGraphics, previewBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolClick(Bitmap drawingImage, Color drawingColor, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                UneditedImage = new(drawingImage);
                EditedImage = drawingImage;
                LeftBoundary = RightBoundary = toolParameters.ClickLocation.Value.X;
                UpperBoundary = LowerBoundary = toolParameters.ClickLocation.Value.Y;

                DrawingCycleGraphics = Graphics.FromImage(drawingImage);
                DrawingBrush = new(drawingColor);
                DrawPenPixel(DrawingCycleGraphics, DrawingBrush, toolParameters.ClickLocation.Value);
            }
        }

        public override void UseToolHold(OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                int pixelClickedX = toolParameters.ClickLocation.Value.X;
                int pixelClickedY = toolParameters.ClickLocation.Value.Y;

                LeftBoundary = LeftBoundary.ValidateMaximum(pixelClickedX);
                LeftBoundary = LeftBoundary.ValidateMinimum(0);

                RightBoundary = RightBoundary.ValidateMinimum(pixelClickedX);
                RightBoundary = RightBoundary.ValidateMaximum(UneditedImage!.Width - 1);

                UpperBoundary = UpperBoundary.ValidateMaximum(pixelClickedY);
                UpperBoundary = UpperBoundary.ValidateMinimum(0);

                LowerBoundary = LowerBoundary.ValidateMinimum(pixelClickedY);
                LowerBoundary = LowerBoundary.ValidateMaximum(UneditedImage!.Height - 1);

                DrawPenPixel(DrawingCycleGraphics!, DrawingBrush!, toolParameters.ClickLocation.Value);
            }
        }

        public override void UseToolRelease(OptionalToolParameters toolParameters)
        {
            return;
        }

        public override IUndoRedoCommand CreateUndoStep(Point drawingImageLocation)
        {
            Rectangle editedArea = new(LeftBoundary, UpperBoundary, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
            UneditedImage = UneditedImage!.Clone(editedArea, PixelFormat.Format32bppArgb);

            EditedImage = EditedImage!.Clone(editedArea, PixelFormat.Format32bppArgb);

            Point editLocation = new(drawingImageLocation.X + LeftBoundary, drawingImageLocation.Y + UpperBoundary);

            PenCommand UndoStep = new(new Bitmap(UneditedImage), new Bitmap(EditedImage), editLocation);

            ClearProperties();

            return UndoStep;
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
            DrawingCycleGraphics?.Dispose();
            DrawingCycleGraphics = null;
            DrawingBrush?.Dispose();
            DrawingBrush = null;
        }
    }
}
