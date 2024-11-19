namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    internal class CopyImageTool : IImageTool
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
