using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    /// <summary>
    /// A tool used to replace an existing color in the image by a new one.
    /// It changes all pixels of the old color to the new one.
    /// </summary>
    public class ReplaceColorTool : IImageTool, IUndoRedoCreator
    {
        /// <summary>
        /// A copy of the image before it was edited.
        /// </summary>
        private Bitmap? UneditedImage { get; set; }

        /// <summary>
        /// A copy of the image after it was edited.
        /// </summary>
        private Bitmap? EditedImage { get; set; }

        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            if (parameters.OldColor.HasValue && parameters.NewColor.HasValue)
            {
                UneditedImage = new(originalImage);

                // Creates a temporary image, making the old color (to be replaced) transparent.
                using Bitmap temporaryImage = (Bitmap)originalImage.Clone();
                temporaryImage.MakeTransparent(parameters.OldColor.Value);

                // Clears a new image with the new color.
                using Graphics imageGraphics = Graphics.FromImage(originalImage);
                imageGraphics.Clear(parameters.NewColor.Value);

                // Draws the temporary image, with transparent pixels, over the image with the new color, so all transparent pixels show the new color.
                imageGraphics.DrawImage(temporaryImage, 0, 0);

                EditedImage = new(originalImage);
            }
        }

        public IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            if (parameters.OldColor.HasValue && parameters.NewColor.HasValue && UneditedImage is not null && EditedImage is not null)
            {
                ReplaceColorCommand command = new(new(UneditedImage), new(EditedImage), parameters.ChangeCellColor, parameters.OldColor.Value, parameters.NewColor.Value);
                ClearProperties();
                return command;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Disposes of unmanaged resources and clears properties used by the tool.
        /// </summary>
        protected void ClearProperties()
        {
            UneditedImage?.Dispose();
            UneditedImage = null;
            EditedImage?.Dispose();
            EditedImage = null;
        }
    }
}
