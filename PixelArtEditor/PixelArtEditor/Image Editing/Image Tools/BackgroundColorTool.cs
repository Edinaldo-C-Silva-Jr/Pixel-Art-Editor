using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools
{
    public class BackgroundColorTool : BaseImageTool
    {
        private Bitmap? UneditedImage { get; set; }

        private Bitmap? EditedImage { get; set; }

        public override void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            if (parameters.BackgroundColor.HasValue && parameters.NewColor.HasValue)
            {
                UneditedImage = new(originalImage);

                // Creates a temporary image, making background color (to be replaced) transparent.
                using Bitmap temporaryImage = (Bitmap)originalImage.Clone();
                temporaryImage.MakeTransparent(parameters.BackgroundColor.Value);

                // Clears the original image with the newly desired color.
                using Graphics imageGraphics = Graphics.FromImage(originalImage);
                imageGraphics.Clear(parameters.NewColor.Value);

                // Draws the temporary image, with transparent pixels, over the original image with the new background color.
                imageGraphics.DrawImage(temporaryImage, 0, 0);

                EditedImage = new(originalImage);
            }
        }

        public override IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            if (parameters.BackgroundColor.HasValue && UneditedImage is not null && EditedImage is not null)
            {
                BackgroundColorCommand command = new(new(UneditedImage), new(EditedImage));
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
