using PixelArtEditor.Files.File_Forms;
using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class LoadImageTool : IImageTool, IUndoRedoCreator
    {
        private Bitmap? UneditedImage { get; set; }
        private Bitmap? EditedImage { get; set; }

        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            string directory = "C:\\Users\\" + Environment.UserName + "\\Documents\\PixelEditor\\SavedImages\\";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using OpenFileDialog openDialog = new()
            {
                InitialDirectory = directory,
                Title = "Load an image into the editor"
            };

            using LoadImageForm loadImageForm = new(openDialog);
            DialogResult result = loadImageForm.ShowDialog();

            if (result == DialogResult.OK && loadImageForm.ImageLoaded is not null)
            {
                UneditedImage = new(originalImage);

                if (loadImageForm.ResizeAfterLoad 
                    && parameters.UseImageTool is not null && parameters.GetImageReference is not null 
                    && parameters.ChangeOriginalImageSize is not null && parameters.ChangeViewNumberBoxes is not null)
                {
                    parameters.ChangeOriginalImageSize(loadImageForm.ImageLoaded.Size.Width, loadImageForm.ImageLoaded.Size.Height);
                    parameters.ChangeViewNumberBoxes();

                    ImageToolParameters imageParameters = new() { OriginalImageSize = loadImageForm.ImageLoaded.Size };

                    string[] imageProperties = { "BackgroundColor", "UpdateOriginalImage" };
                    string[] undoProperties = { "UpdateOriginalImage", "ChangeOriginalImageSize", "ChangeViewNumberBoxes" };
                    parameters.UseImageTool(4, imageProperties, undoProperties, imageParameters, null, null);

                    originalImage = parameters.GetImageReference();
                }

                using Bitmap imageLoaded = new(loadImageForm.ImageLoaded);
                using Graphics originalImageGraphics = Graphics.FromImage(originalImage);
                originalImageGraphics.DrawImage(imageLoaded, 0, 0);
                EditedImage = new(originalImage);
            }
        }

        public IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            if (UneditedImage is not null && EditedImage is not null && parameters.BackgroundColor.HasValue)
            {
                LoadImageCommand command = new(new(UneditedImage), new(EditedImage), parameters.BackgroundColor.Value);
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
