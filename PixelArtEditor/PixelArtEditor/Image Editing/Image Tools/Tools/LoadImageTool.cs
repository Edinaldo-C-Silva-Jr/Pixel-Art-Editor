using PixelArtEditor.Files.File_Forms;
using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    /// <summary>
    /// A tool used to load an image from a file into the editor.
    /// It creates and opens an instance of a LoadImageForm, which allows loading and resizing the image, as well as resizing the Viewing Box to fit the image size.
    /// </summary>
    public class LoadImageTool : IImageTool, IUndoRedoCreator
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
            // Defines the default directory to load the images from.
            string directory = "C:\\Users\\" + Environment.UserName + "\\Documents\\PixelEditor\\SavedImages\\";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Instances an OpenFileDialog and a LoadImageForm to handle the loading.
            using OpenFileDialog openDialog = new()
            {
                InitialDirectory = directory,
                Title = "Load an image into the editor"
            };

            using LoadImageForm loadImageForm = new(openDialog);
            DialogResult result = loadImageForm.ShowDialog();

            if (result == DialogResult.OK && loadImageForm.ImageLoaded is not null)
            {
                // Undo Properties.
                UneditedImage = new(originalImage);

                // If the "Resize after load" property was set, change the size of the original image using the ResizeImageTool.
                // Resizes before loading to fit the entire loaded image into the Original Image.
                if (loadImageForm.ResizeAfterLoad 
                    && parameters.UseImageTool is not null && parameters.GetImageReference is not null 
                    && parameters.ChangeOriginalImageSize is not null && parameters.ChangeViewNumberBoxes is not null)
                {
                    parameters.ChangeOriginalImageSize(loadImageForm.ImageLoaded.Size.Width, loadImageForm.ImageLoaded.Size.Height);
                    parameters.ChangeViewNumberBoxes();

                    // Use the size of the loaded image to resize the Original Image.
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
