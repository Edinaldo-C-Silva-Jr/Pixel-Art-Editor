using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class NewImageTool : BaseImageTool
    {
        private Bitmap? UneditedImage { get; set; }

        public override void UseTool(Bitmap OriginalImage, ImageToolParameters parameters)
        {
            if (parameters.BackgroundColor.HasValue)
            {
                UneditedImage = new(OriginalImage);

                using Graphics imageGraphics = Graphics.FromImage(OriginalImage);
                imageGraphics.Clear(parameters.BackgroundColor.Value);
            }
        }

        public override IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
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
