namespace PixelArtEditor.Files.File_Forms
{
    public partial class LoadImageForm : Form
    {
        private OpenFileDialog DialogForOpeningImages { get; set; }

        private ZoomType TypeOfZoom { get; set; } = ZoomType.None;

        private bool ShowZoom { get; set; }

        private Size InitialImageSize { get; set; }

        public Size ImageLoadedSize { get; set; }

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

                ImageLoadedSize = InitialImageSize = LoadImagePictureBox.Size;
                LoadImageWidthLabel.Text = $"Width: {InitialImageSize.Width}";
                LoadImageHeightLabel.Text = $"Height: {InitialImageSize.Height}";

                AcceptPixelDimensions();
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
            int imageWidth = InitialImageSize.Width;
            int imageHeight = InitialImageSize.Height;

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
            int imageWidth = InitialImageSize.Width;
            int imageHeight = InitialImageSize.Height;

            int biggerDimension = imageWidth > imageHeight ? imageWidth : imageHeight;

            if (biggerDimension > 1024)
            {
                return 1;
            }
            else
            {
                return 1024 / biggerDimension;
            }
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
            ShowZoom = true;
        }

        private void LoadImageZoomNumberBox_ValueChanged(object sender, EventArgs e)
        {
            LoadImageZoomNumberBar.Value = (int)LoadImageZoomNumberBox.Value;
            if (ShowZoom)
            {
                ShowPixelDimensions();
            }
        }

        private void LoadImageZoomNumberBar_ValueChanged(object sender, EventArgs e)
        {
            LoadImageZoomNumberBox.Value = LoadImageZoomNumberBar.Value;
        }

        private void LoadImageAcceptZoomButton_Click(object sender, EventArgs e)
        {
            AcceptPixelDimensions();
            ShowZoom = false;
            DisableZoomPanel();
        }

        private void LoadImageCancelZoomButton_Click(object sender, EventArgs e)
        {
            DisableZoomPanel();
        }

        private Size CalculatePixelDimensions()
        {
            int pixelWidth = TypeOfZoom switch
            {
                ZoomType.Shrink => InitialImageSize.Width / (int)LoadImageZoomNumberBox.Value,
                ZoomType.Enlarge => InitialImageSize.Width * (int)LoadImageZoomNumberBox.Value,
                _ => InitialImageSize.Width
            };

            int pixelHeight = TypeOfZoom switch
            {
                ZoomType.Shrink => InitialImageSize.Height / (int)LoadImageZoomNumberBox.Value,
                ZoomType.Enlarge => InitialImageSize.Height * (int)LoadImageZoomNumberBox.Value,
                _ => InitialImageSize.Height
            };

            return new Size(pixelWidth, pixelHeight);
        }

        private void ShowPixelDimensions()
        {
            Size pixelDimensions = CalculatePixelDimensions();

            LoadPixelWidthLabel.ForeColor = Color.Black;
            LoadPixelHeightLabel.ForeColor = Color.Black;
            LoadPixelWidthLabel.Text = $"Pixel Width: {pixelDimensions.Width}";
            LoadPixelHeightLabel.Text = $"Pixel Height: {pixelDimensions.Height}";
        }

        private void AcceptPixelDimensions()
        {
            Size pixelDimensions = CalculatePixelDimensions();
            ImageLoadedSize = pixelDimensions;

            ValidatePixelDimensions(pixelDimensions);
            LoadPixelWidthLabel.Text = $"Pixel Width: {pixelDimensions.Width}";
            LoadPixelHeightLabel.Text = $"Pixel Height: {pixelDimensions.Height}";
        }

        private void ValidatePixelDimensions(Size pixelDimensions)
        {
            bool valid = true;
            if (pixelDimensions.Width > 1024)
            {
                LoadPixelWidthLabel.ForeColor = Color.Red;
                valid = false;
            }
            else
            {
                LoadPixelWidthLabel.ForeColor = Color.Green;
            }

            if (pixelDimensions.Height > 1024)
            {
                LoadPixelHeightLabel.ForeColor = Color.Red;
                valid = false;
            }
            else
            {
                LoadPixelHeightLabel.ForeColor = Color.Green;
            }

            ConfirmLoadButton.Enabled = valid;
        }
    }
}
