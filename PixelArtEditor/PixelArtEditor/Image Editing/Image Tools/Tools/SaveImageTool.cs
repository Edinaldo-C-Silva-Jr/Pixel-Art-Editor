using PixelArtEditor.Files.File_Forms;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class SaveImageTool : IImageTool
    {
        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            if (parameters.OriginalImageSize.HasValue)
            {
                string directory = "C:\\Users\\" + Environment.UserName + "\\Documents\\PixelEditor\\SavedImages\\";
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
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
