namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    /// <summary>
    /// A tool used to copy a portion of an image, which can be the DrawingImage or the OriginalImage,
    /// It uses an existing method provided by the Image Handler to copy a portion of the image into the corresponding Clipboard Image.
    /// </summary>
    public class CopyImageTool : IImageTool
    {
        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            if (parameters.SelectedArea.HasValue && parameters.SelectedArea.Value != Rectangle.Empty && parameters.CopyImage is not null)
            {
                parameters.CopyImage(parameters.SelectedArea.Value);
            }
        }
    }
}
