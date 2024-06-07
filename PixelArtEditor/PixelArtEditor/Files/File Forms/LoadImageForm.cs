namespace PixelArtEditor.Files.File_Forms
{
    public partial class LoadImageForm : Form
    {
        private OpenFileDialog DialogForOpeningImages { get; set; }

        private ZoomType TypeOfZoom { get; set; } = ZoomType.None;

        public LoadImageForm(OpenFileDialog dialogForOpeningImages)
        {
            InitializeComponent();

            DialogForOpeningImages = dialogForOpeningImages;

            // Single time control setup for when the form opens.
            LoadImagePictureBox.Size = new(0, 0);
            LoadImageBackgroundPanel.Size = new(0, 0);
            LoadImageZoomPanel.Location = new(170, 0);
            LoadImageZoomPanel.Visible = false;
            LoadImageRemoveZoomButton.Visible = false;
            LoadImageAddZoomButton.Visible = false;
            ConfirmLoadButton.Enabled = false;
        }

        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogForOpeningImages.ShowDialog();
            if (result == DialogResult.OK)
            {
                DisableZoomPanel();

                LoadImagePictureBox.Image = new Bitmap(DialogForOpeningImages.FileName);
                LoadImagePictureBox.Size = LoadImagePictureBox.Image.Size;
                LoadImageBackgroundPanel.ResizePanelToFitControls();

                LoadImageWidthLabel.Text = $"Width: {LoadImagePictureBox.Image.Width}";
                LoadImageHeightLabel.Text = $"Height: {LoadImagePictureBox.Image.Height}";

                SetPixelDimensions();
            }
        }

        private void LoadImageRemoveZoomButton_Click(object sender, EventArgs e)
        {
            EnableZoomPanel("How much zoom to remove?");
            LoadImageZoomNumberBox.Maximum = LoadImageZoomNumberBar.MaximumValue = CalculateMaximumShrink();
            TypeOfZoom = ZoomType.Shrink;
        }

        private int CalculateMaximumShrink()
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

        private void LoadImageAddZoomButton_Click(object sender, EventArgs e)
        {
            EnableZoomPanel("How much zoom to add?");
            LoadImageZoomNumberBox.Maximum = LoadImageZoomNumberBar.MaximumValue = CalculateMaximumEnlarge();
            TypeOfZoom = ZoomType.Enlarge;
        }

        private int CalculateMaximumEnlarge()
        {
            int imageWidth = LoadImagePictureBox.Image.Width;
            int imageHeight = LoadImagePictureBox.Image.Height;

            int biggerDimension = imageWidth > imageHeight ? imageWidth : imageHeight;

            return 1024 / biggerDimension;
        }

        private void EnableZoomPanel(string labelText)
        {
            LoadImageRemoveZoomButton.Visible = false;
            LoadImageAddZoomButton.Visible = false;
            LoadImageZoomPanel.Visible = true;
            LoadImageZoomedLabel.Text = labelText;
        }

        private void DisableZoomPanel()
        {
            LoadImageRemoveZoomButton.Visible = true;
            LoadImageAddZoomButton.Visible = true;
            LoadImageZoomPanel.Visible = false;
            LoadImageZoomNumberBox.Value = 1;
        }

        private void LoadImageAcceptZoomButton_Click(object sender, EventArgs e)
        {
            SetPixelDimensions();
            DisableZoomPanel();
        }

        private void LoadImageCancelZoomButton_Click(object sender, EventArgs e)
        {
            DisableZoomPanel();
        }

        private void LoadImageZoomNumberBox_ValueChanged(object sender, EventArgs e)
        {
            LoadImageZoomNumberBar.Value = (int)LoadImageZoomNumberBox.Value;
            
        }

        private void LoadImageZoomNumberBar_ValueChanged(object sender, EventArgs e)
        {
            LoadImageZoomNumberBox.Value = LoadImageZoomNumberBar.Value;
        }

        private void SetPixelDimensions()
        {
            int pixelWidth = TypeOfZoom switch
            {
                ZoomType.Shrink => LoadImagePictureBox.Image.Width / (int)LoadImageZoomNumberBox.Value,
                ZoomType.Enlarge => LoadImagePictureBox.Image.Width * (int)LoadImageZoomNumberBox.Value,
                _ => LoadImagePictureBox.Image.Width
            };

            int pixelHeight = TypeOfZoom switch
            {
                ZoomType.Shrink => LoadImagePictureBox.Image.Height / (int)LoadImageZoomNumberBox.Value,
                ZoomType.Enlarge => LoadImagePictureBox.Image.Height * (int)LoadImageZoomNumberBox.Value,
                _ => LoadImagePictureBox.Image.Height
            };

            LoadPixelWidthLabel.Text = $"Pixel Width: {pixelWidth}";
            LoadPixelHeightLabel.Text = $"Pixel Height: {pixelHeight}";

            ValidatePixelDimensions(pixelWidth, pixelHeight);
        }

        private void ValidatePixelDimensions(int pixelWidth, int pixelHeight)
        {
            bool valid = true;
            if (pixelWidth > 1024)
            {
                LoadPixelWidthLabel.ForeColor = Color.Red;
                valid = false;
            }
            else
            {
                LoadPixelWidthLabel.ForeColor = Color.Black;
            }

            if (pixelHeight > 1024)
            {
                LoadPixelHeightLabel.ForeColor = Color.Red;
                valid = false;
            }
            else
            {
                LoadPixelHeightLabel.ForeColor = Color.Black;
            }
            ConfirmLoadButton.Enabled = valid;
        }
    }
}
