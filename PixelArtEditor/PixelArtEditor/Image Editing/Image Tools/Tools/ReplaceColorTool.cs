using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class ReplaceColorTool : IImageTool, IUndoRedoCreator
    {
        private Bitmap? UneditedImage { get; set; }

        private Bitmap? EditedImage { get; set; }

        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            if (parameters.OldColor.HasValue && parameters.NewColor.HasValue)
            {
                UneditedImage = new(originalImage);

                // Creates a temporary image, making background color (to be replaced) transparent.
                using Bitmap temporaryImage = (Bitmap)originalImage.Clone();
                temporaryImage.MakeTransparent(parameters.OldColor.Value);

                // Clears the original image with the newly desired color.
                using Graphics imageGraphics = Graphics.FromImage(originalImage);
                imageGraphics.Clear(parameters.NewColor.Value);

                // Draws the temporary image, with transparent pixels, over the original image with the new background color.
                imageGraphics.DrawImage(temporaryImage, 0, 0);

                EditedImage = new(originalImage);
            }
        }

        public IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            if (parameters.ChangeCellColor is not null && parameters.OldColor.HasValue && parameters.NewColor.HasValue && UneditedImage is not null && EditedImage is not null)
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

        protected void ClearProperties()
        {
            UneditedImage?.Dispose();
            UneditedImage = null;
            EditedImage?.Dispose();
            EditedImage = null;
        }
    }
}
