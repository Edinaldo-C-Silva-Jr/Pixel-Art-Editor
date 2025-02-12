﻿using PixelArtEditor.Extension_Methods;
using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.RectangleTool
{
    /// <summary>
    /// A class to serve as a base for the Rectangle type tools.
    /// It implements the preview method, all drawing methods and the creation of an undo step.
    /// </summary>
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

        /// <summary>
        /// Draws a rectangle between the StartingPoint and Endpoint.
        /// One point will be the top left vertice while the other will be the bottom right vertice.
        /// </summary>
        /// <param name="drawGraphics">The graphics for the image being drawn.</param>
        /// <param name="drawBrush">The brush with the currently selected color.</param>
        protected abstract void DrawRectangle(Graphics drawGraphics, SolidBrush drawBrush);

        /// <summary>
        /// Draws a rectangle between the StartingPoint and Endpoint while applying zoom.
        /// One point will be the top left vertice while the other will be the bottom right vertice.
        /// </summary>
        /// <param name="drawGraphics">The graphics for the image being drawn.</param>
        /// <param name="drawBrush">The brush with the currently selected color.</param>
        /// <param name="zoom">The size of each pixel in the image.</param>
        protected abstract void DrawRectangle(Graphics drawGraphics, SolidBrush drawBrush, int zoom);

        public override void PreviewTool(Graphics paintGraphics, Color drawingColor, DrawingToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.PixelSize.HasValue)
            {
                using SolidBrush previewBrush = MakePreviewBrush(drawingColor);
                DrawRectangle(paintGraphics, previewBrush, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolClick(Bitmap drawingImage, Color drawingColor, DrawingToolParameters toolParameters)
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

        public override void UseToolHold(DrawingToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue)
            {
                // Preparing drawing properties.
                EndPoint = toolParameters.ClickLocation.Value;
            }
        }

        public override void UseToolRelease(DrawingToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue)
            {
                // Preparing drawing properties.
                EndPoint = toolParameters.ClickLocation.Value;

                DrawRectangle(DrawingCycleGraphics!, DrawingBrush!);
            }
        }

        public override IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            if (parameters.DrawingImageLocation.HasValue && StartingPoint.HasValue && EndPoint.HasValue && UneditedImage is not null && EditedImage is not null)
            {
                // Validates the EndPoint to make sure it's within the image.
                int endPointX = EndPoint.Value.X;
                endPointX = endPointX.ValidateMaximum(UneditedImage.Width - 1).ValidateMinimum(0);
                int endPointY = EndPoint.Value.Y;
                endPointY = endPointY.ValidateMaximum(UneditedImage.Height - 1).ValidateMinimum(0);

                Point firstPoint = StartingPoint.Value;
                Point lastPoint = new(endPointX, endPointY);

                // Makes sure the first point is always smaller than the last point.
                (firstPoint.X, lastPoint.X) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.X, lastPoint.X);
                (firstPoint.Y, lastPoint.Y) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(firstPoint.Y, lastPoint.Y);

                // Getting only the edited portion of the images.
                Rectangle editedArea = new(firstPoint.X, firstPoint.Y, lastPoint.X - firstPoint.X + 1, lastPoint.Y - firstPoint.Y + 1);
                UneditedImage = UneditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);
                EditedImage = EditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);

                // Getting the location where the edits started.
                Point editLocation = new(parameters.DrawingImageLocation.Value.X + firstPoint.X, parameters.DrawingImageLocation.Value.Y + firstPoint.Y);

                RectangleCommand undoStep = new(new Bitmap(UneditedImage), new Bitmap(EditedImage), editLocation);
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
        /// This includes the starting and end points, the edited and unedited images, the graphics and the brush.
        /// </summary>
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
