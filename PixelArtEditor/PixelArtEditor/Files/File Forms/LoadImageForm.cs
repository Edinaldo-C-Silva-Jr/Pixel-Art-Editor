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

                LoadImageZoomNumberBox.Maximum = LoadImageZoomNumberBar.MaximumValue = CalculateMaximumZoom();
            }
        }

        private int CalculateMaximumZoom()
        {
            int imageWidth = LoadImagePictureBox.Image.Width;
            int imageHeight = LoadImagePictureBox.Image.Height;

            int smallerDimension = imageWidth < imageHeight ? imageWidth : imageHeight;

            if (smallerDimension >= 320)
            {
                return 64;
            }
            else
            {
                return smallerDimension / 5;
            }
        }

        private void LoadImageZoomNumberBox_ValueChanged(object sender, EventArgs e)
        {
            LoadImageZoomNumberBar.Value = (int)LoadImageZoomNumberBox.Value;
        }

        private void LoadImageZoomNumberBar_ValueChanged(object sender, EventArgs e)
        {
            LoadImageZoomNumberBox.Value = LoadImageZoomNumberBar.Value;
        }
    }
}
