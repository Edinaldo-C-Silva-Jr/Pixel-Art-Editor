using PixelArtEditor.Files.File_Forms;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    /// <summary>
    /// A tool used to save the Original Image to a file.
    /// It creates and opens an instance of a SaveImageForm to handle the saving, which allows zooming the image and choosing the location and filename.
    /// </summary>
    public class SaveImageTool : IImageTool
    {
        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            if (parameters.OriginalImageSize.HasValue)
            {
                // Defines the default directory to save the images to.
                string directory = "C:\\Users\\" + Environment.UserName + "\\Documents\\PixelEditor\\SavedImages\\";
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                // Instances a SaveFileDialog and a SaveImageForm to handle saving the image into a file.
                using SaveFileDialog saveDialog = new()
                {
                    AddExtension = true,
                    InitialDirectory = directory,
                    Filter = "PNG Image|*.png",
                    DefaultExt = "png",
                    Title = "Save the current image"
                };
                using SaveImageForm saveImageForm = new(originalImage, parameters.OriginalImageSize.Value, saveDialog);
                saveImageForm.ShowDialog();
            }
        }
    }
}
