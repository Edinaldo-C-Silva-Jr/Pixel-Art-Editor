using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class NewImageTool : IImageTool, IUndoRedoCreator
    {
        private Bitmap? UneditedImage { get; set; }

        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            if (parameters.BackgroundColor.HasValue)
            {
                UneditedImage = new(originalImage);

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

        protected void ClearProperties()
        {
            UneditedImage?.Dispose();
            UneditedImage = null;
        }
    }
}
