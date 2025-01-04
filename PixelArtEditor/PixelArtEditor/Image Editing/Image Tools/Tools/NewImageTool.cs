using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    /// <summary>
    /// A tool used to create a new blank image in the editor.
    /// It clears the Original image with the current background color.
    /// </summary>
    public class NewImageTool : IImageTool, IUndoRedoCreator
    {
        /// <summary>
        /// The old image, before being replaced by a blank one.
        /// </summary>
        private Bitmap? UneditedImage { get; set; }

        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            if (parameters.BackgroundColor.HasValue)
            {
                UneditedImage = new(originalImage);

                // Clears the entire image with the background color to create a new blank image.
                using Graphics imageGraphics = Graphics.FromImage(originalImage);
                imageGraphics.Clear(parameters.BackgroundColor.Value);
            }
        }

        public IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            if (parameters.BackgroundColor.HasValue && UneditedImage is not null)
            {
                NewImageCommand command = new(new(UneditedImage), parameters.BackgroundColor.Value);
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
        }
    }
}
