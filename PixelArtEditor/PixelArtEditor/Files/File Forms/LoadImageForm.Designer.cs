namespace PixelArtEditor.Files.File_Forms
{
    partial class LoadImageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LoadImageWidthLabel = new Label();
            LoadImageBackgroundPanel = new Controls.BackgroundPanel();
            LoadImagePictureBox = new PictureBox();
            LoadImageHeightLabel = new Label();
            ConfirmLoadButton = new Button();
            CancelLoadButton = new Button();
            LoadImageZoomedLabel = new Label();
            LoadImageZoomNumberBar = new Controls.NumberBar();
            LoadImageZoomNumberBox = new Controls.NumberBox();
            LoadPixelHeightLabel = new Label();
            LoadPixelWidthLabel = new Label();
            OpenImageButton = new Button();
            LoadImageZoomPanel = new Panel();
            LoadImageCancelZoomButton = new Button();
            LoadImageAcceptZoomButton = new Button();
            LoadImageRemoveZoomButton = new Button();
            LoadImageAddZoomButton = new Button();
            LoadImageBackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LoadImagePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LoadImageZoomNumberBox).BeginInit();
            LoadImageZoomPanel.SuspendLayout();
            SuspendLayout();
            // 
            // LoadImageWidthLabel
            // 
            LoadImageWidthLabel.AutoSize = true;
            LoadImageWidthLabel.Location = new Point(70, 20);
            LoadImageWidthLabel.Name = "LoadImageWidthLabel";
            LoadImageWidthLabel.Size = new Size(45, 15);
            LoadImageWidthLabel.TabIndex = 0;
            LoadImageWidthLabel.Text = "Width: ";
            // 
            // LoadImageBackgroundPanel
            // 
            LoadImageBackgroundPanel.AutoScroll = true;
            LoadImageBackgroundPanel.AutoScrollMargin = new Size(1, 1);
            LoadImageBackgroundPanel.BackColor = Color.Black;
            LoadImageBackgroundPanel.Controls.Add(LoadImagePictureBox);
            LoadImageBackgroundPanel.Location = new Point(4, 79);
            LoadImageBackgroundPanel.MaximumHeight = 514;
            LoadImageBackgroundPanel.MaximumWidth = 514;
            LoadImageBackgroundPanel.Name = "LoadImageBackgroundPanel";
            LoadImageBackgroundPanel.Size = new Size(40, 40);
            LoadImageBackgroundPanel.TabIndex = 1;
            // 
            // LoadImagePictureBox
            // 
            LoadImagePictureBox.BackColor = SystemColors.Control;
            LoadImagePictureBox.Location = new Point(1, 1);
            LoadImagePictureBox.Name = "LoadImagePictureBox";
            LoadImagePictureBox.Size = new Size(20, 20);
            LoadImagePictureBox.TabIndex = 0;
            LoadImagePictureBox.TabStop = false;
            // 
            // LoadImageHeightLabel
            // 
            LoadImageHeightLabel.AutoSize = true;
            LoadImageHeightLabel.Location = new Point(70, 40);
            LoadImageHeightLabel.Name = "LoadImageHeightLabel";
            LoadImageHeightLabel.Size = new Size(49, 15);
            LoadImageHeightLabel.TabIndex = 2;
            LoadImageHeightLabel.Text = "Height: ";
            // 
            // ConfirmLoadButton
            // 
            ConfirmLoadButton.Location = new Point(540, 5);
            ConfirmLoadButton.Name = "ConfirmLoadButton";
            ConfirmLoadButton.Size = new Size(80, 30);
            ConfirmLoadButton.TabIndex = 3;
            ConfirmLoadButton.Text = "Confirm";
            ConfirmLoadButton.UseVisualStyleBackColor = true;
            ConfirmLoadButton.Click += ConfirmLoadButton_Click;
            // 
            // CancelLoadButton
            // 
            CancelLoadButton.Location = new Point(540, 40);
            CancelLoadButton.Name = "CancelLoadButton";
            CancelLoadButton.Size = new Size(80, 30);
            CancelLoadButton.TabIndex = 4;
            CancelLoadButton.Text = "Cancel";
            CancelLoadButton.UseVisualStyleBackColor = true;
            CancelLoadButton.Click += CancelLoadButton_Click;
            // 
            // LoadImageZoomedLabel
            // 
            LoadImageZoomedLabel.AutoSize = true;
            LoadImageZoomedLabel.Location = new Point(5, 5);
            LoadImageZoomedLabel.Name = "LoadImageZoomedLabel";
            LoadImageZoomedLabel.Size = new Size(71, 15);
            LoadImageZoomedLabel.TabIndex = 5;
            LoadImageZoomedLabel.Text = "Zoom Panel";
            // 
            // LoadImageZoomNumberBar
            // 
            LoadImageZoomNumberBar.DefaultWidth = 128;
            LoadImageZoomNumberBar.IncrementAmount = 1;
            LoadImageZoomNumberBar.Location = new Point(40, 30);
            LoadImageZoomNumberBar.MaximumValue = 64;
            LoadImageZoomNumberBar.MinimumValue = 1;
            LoadImageZoomNumberBar.Name = "LoadImageZoomNumberBar";
            LoadImageZoomNumberBar.Size = new Size(128, 30);
            LoadImageZoomNumberBar.TabIndex = 7;
            LoadImageZoomNumberBar.Value = 1;
            LoadImageZoomNumberBar.ValueChanged += LoadImageZoomNumberBar_ValueChanged;
            // 
            // LoadImageZoomNumberBox
            // 
            LoadImageZoomNumberBox.Location = new Point(5, 35);
            LoadImageZoomNumberBox.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            LoadImageZoomNumberBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            LoadImageZoomNumberBox.Name = "LoadImageZoomNumberBox";
            LoadImageZoomNumberBox.Size = new Size(30, 23);
            LoadImageZoomNumberBox.TabIndex = 6;
            LoadImageZoomNumberBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            LoadImageZoomNumberBox.ValueChanged += LoadImageZoomNumberBox_ValueChanged;
            // 
            // LoadPixelHeightLabel
            // 
            LoadPixelHeightLabel.AutoSize = true;
            LoadPixelHeightLabel.Location = new Point(430, 40);
            LoadPixelHeightLabel.Name = "LoadPixelHeightLabel";
            LoadPixelHeightLabel.Size = new Size(77, 15);
            LoadPixelHeightLabel.TabIndex = 9;
            LoadPixelHeightLabel.Text = "Pixel Height: ";
            // 
            // LoadPixelWidthLabel
            // 
            LoadPixelWidthLabel.AutoSize = true;
            LoadPixelWidthLabel.Location = new Point(430, 20);
            LoadPixelWidthLabel.Name = "LoadPixelWidthLabel";
            LoadPixelWidthLabel.Size = new Size(73, 15);
            LoadPixelWidthLabel.TabIndex = 8;
            LoadPixelWidthLabel.Text = "Pixel Width: ";
            // 
            // OpenImageButton
            // 
            OpenImageButton.Location = new Point(5, 10);
            OpenImageButton.Name = "OpenImageButton";
            OpenImageButton.Size = new Size(60, 60);
            OpenImageButton.TabIndex = 10;
            OpenImageButton.Text = "Open Image";
            OpenImageButton.UseVisualStyleBackColor = true;
            OpenImageButton.Click += OpenImageButton_Click;
            // 
            // LoadImageZoomPanel
            // 
            LoadImageZoomPanel.Controls.Add(LoadImageCancelZoomButton);
            LoadImageZoomPanel.Controls.Add(LoadImageAcceptZoomButton);
            LoadImageZoomPanel.Controls.Add(LoadImageZoomNumberBar);
            LoadImageZoomPanel.Controls.Add(LoadImageZoomedLabel);
            LoadImageZoomPanel.Controls.Add(LoadImageZoomNumberBox);
            LoadImageZoomPanel.Location = new Point(170, 100);
            LoadImageZoomPanel.Name = "LoadImageZoomPanel";
            LoadImageZoomPanel.Size = new Size(250, 70);
            LoadImageZoomPanel.TabIndex = 11;
            // 
            // LoadImageCancelZoomButton
            // 
            LoadImageCancelZoomButton.Location = new Point(180, 40);
            LoadImageCancelZoomButton.Name = "LoadImageCancelZoomButton";
            LoadImageCancelZoomButton.Size = new Size(60, 25);
            LoadImageCancelZoomButton.TabIndex = 9;
            LoadImageCancelZoomButton.Text = "Cancel";
            LoadImageCancelZoomButton.UseVisualStyleBackColor = true;
            LoadImageCancelZoomButton.Click += LoadImageCancelZoomButton_Click;
            // 
            // LoadImageAcceptZoomButton
            // 
            LoadImageAcceptZoomButton.Location = new Point(180, 5);
            LoadImageAcceptZoomButton.Name = "LoadImageAcceptZoomButton";
            LoadImageAcceptZoomButton.Size = new Size(60, 25);
            LoadImageAcceptZoomButton.TabIndex = 8;
            LoadImageAcceptZoomButton.Text = "Accept";
            LoadImageAcceptZoomButton.UseVisualStyleBackColor = true;
            LoadImageAcceptZoomButton.Click += LoadImageAcceptZoomButton_Click;
            // 
            // LoadImageRemoveZoomButton
            // 
            LoadImageRemoveZoomButton.Location = new Point(180, 20);
            LoadImageRemoveZoomButton.Name = "LoadImageRemoveZoomButton";
            LoadImageRemoveZoomButton.Size = new Size(60, 40);
            LoadImageRemoveZoomButton.TabIndex = 12;
            LoadImageRemoveZoomButton.Text = "Remove Zoom";
            LoadImageRemoveZoomButton.UseVisualStyleBackColor = true;
            LoadImageRemoveZoomButton.Click += LoadImageRemoveZoomButton_Click;
            // 
            // LoadImageAddZoomButton
            // 
            LoadImageAddZoomButton.Location = new Point(270, 20);
            LoadImageAddZoomButton.Name = "LoadImageAddZoomButton";
            LoadImageAddZoomButton.Size = new Size(60, 40);
            LoadImageAddZoomButton.TabIndex = 13;
            LoadImageAddZoomButton.Text = "Add Zoom";
            LoadImageAddZoomButton.UseVisualStyleBackColor = true;
            LoadImageAddZoomButton.Click += LoadImageAddZoomButton_Click;
            // 
            // LoadImageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(634, 461);
            Controls.Add(LoadImageAddZoomButton);
            Controls.Add(LoadImageRemoveZoomButton);
            Controls.Add(LoadImageZoomPanel);
            Controls.Add(OpenImageButton);
            Controls.Add(LoadPixelHeightLabel);
            Controls.Add(LoadPixelWidthLabel);
            Controls.Add(CancelLoadButton);
            Controls.Add(ConfirmLoadButton);
            Controls.Add(LoadImageHeightLabel);
            Controls.Add(LoadImageBackgroundPanel);
            Controls.Add(LoadImageWidthLabel);
            Name = "LoadImageForm";
            Text = "Load Image";
            LoadImageBackgroundPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)LoadImagePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)LoadImageZoomNumberBox).EndInit();
            LoadImageZoomPanel.ResumeLayout(false);
            LoadImageZoomPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LoadImageWidthLabel;
        private Controls.BackgroundPanel LoadImageBackgroundPanel;
        private PictureBox LoadImagePictureBox;
        private Label LoadImageHeightLabel;
        private Button ConfirmLoadButton;
        private Button CancelLoadButton;
        private Label LoadImageZoomedLabel;
        private Controls.NumberBar LoadImageZoomNumberBar;
        private Controls.NumberBox LoadImageZoomNumberBox;
        private Label LoadPixelHeightLabel;
        private Label LoadPixelWidthLabel;
        private Button OpenImageButton;
        private Panel LoadImageZoomPanel;
        private Button LoadImageCancelZoomButton;
        private Button LoadImageAcceptZoomButton;
        private Button LoadImageRemoveZoomButton;
        private Button LoadImageAddZoomButton;
    }
}