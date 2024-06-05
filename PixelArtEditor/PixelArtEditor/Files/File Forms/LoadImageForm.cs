namespace PixelArtEditor.Files.File_Forms
{
    public partial class LoadImageForm : Form
    {
        private OpenFileDialog DialogForOpeningImages { get; set; }

        public LoadImageForm(OpenFileDialog dialogForOpeningImages)
        {
            InitializeComponent();

            DialogForOpeningImages = dialogForOpeningImages;
        }

        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogForOpeningImages.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadImagePictureBox.Image = new Bitmap(DialogForOpeningImages.FileName);
                LoadImagePictureBox.Size = LoadImagePictureBox.Image.Size;
                LoadImageBackgroundPanel.ResizePanelToFitControls();

                LoadImageWidthLabel.Text = $"Width: {LoadImagePictureBox.Image.Width}";
                LoadImageHeightLabel.Text = $"Height: {LoadImagePictureBox.Image.Height}";
                LoadPixelWidthLabel.Text = $"Pixel Width: {LoadImagePictureBox.Image.Width}";
                LoadPixelHeightLabel.Text = $"Pixel Height: {LoadImagePictureBox.Image.Height}";
            }
        }
    }
}
