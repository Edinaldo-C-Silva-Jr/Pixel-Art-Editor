namespace PixelArtEditor.Files.File_Forms
{
    partial class SaveImageForm
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
            SaveImagePictureBox = new PictureBox();
            SaveFullImageButton = new Button();
            CancelSaveButton = new Button();
            SaveImageZoomNumberBox = new Controls.NumberBox();
            SaveImageZoomNumberBar = new Controls.NumberBar();
            SaveImageZoomLabel = new Label();
            SaveImageBackgroundPanel = new Controls.BackgroundPanel();
            SaveImageWidthLabel = new Label();
            SaveImageHeightLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)SaveImagePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SaveImageZoomNumberBox).BeginInit();
            SaveImageBackgroundPanel.SuspendLayout();
            SuspendLayout();
            // 
            // SaveImagePictureBox
            // 
            SaveImagePictureBox.Location = new Point(1, 1);
            SaveImagePictureBox.Name = "SaveImagePictureBox";
            SaveImagePictureBox.Size = new Size(20, 20);
            SaveImagePictureBox.TabIndex = 0;
            SaveImagePictureBox.TabStop = false;
            // 
            // SaveFullImageButton
            // 
            SaveFullImageButton.Location = new Point(130, 10);
            SaveFullImageButton.Name = "SaveFullImageButton";
            SaveFullImageButton.Size = new Size(80, 40);
            SaveFullImageButton.TabIndex = 1;
            SaveFullImageButton.Text = "Save Image";
            SaveFullImageButton.UseVisualStyleBackColor = true;
            SaveFullImageButton.Click += SaveFullImageButton_Click;
            // 
            // CancelSaveButton
            // 
            CancelSaveButton.Location = new Point(220, 10);
            CancelSaveButton.Name = "CancelSaveButton";
            CancelSaveButton.Size = new Size(80, 40);
            CancelSaveButton.TabIndex = 2;
            CancelSaveButton.Text = "Cancel";
            CancelSaveButton.UseVisualStyleBackColor = true;
            CancelSaveButton.Click += CancelSaveButton_Click;
            // 
            // SaveImageZoomNumberBox
            // 
            SaveImageZoomNumberBox.Location = new Point(360, 20);
            SaveImageZoomNumberBox.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            SaveImageZoomNumberBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            SaveImageZoomNumberBox.Name = "SaveImageZoomNumberBox";
            SaveImageZoomNumberBox.Size = new Size(30, 23);
            SaveImageZoomNumberBox.TabIndex = 3;
            SaveImageZoomNumberBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            SaveImageZoomNumberBox.ValueChanged += FullImageZoomNumberBox_ValueChanged;
            // 
            // SaveImageZoomNumberBar
            // 
            SaveImageZoomNumberBar.IncrementAmount = 1;
            SaveImageZoomNumberBar.Location = new Point(395, 15);
            SaveImageZoomNumberBar.MaximumValue = 64;
            SaveImageZoomNumberBar.MinimumValue = 1;
            SaveImageZoomNumberBar.Name = "SaveImageZoomNumberBar";
            SaveImageZoomNumberBar.Size = new Size(128, 30);
            SaveImageZoomNumberBar.TabIndex = 4;
            SaveImageZoomNumberBar.Value = 1;
            SaveImageZoomNumberBar.ValueChanged += FullImageZoomNumberBar_ValueChanged;
            // 
            // SaveImageZoomLabel
            // 
            SaveImageZoomLabel.AutoSize = true;
            SaveImageZoomLabel.Location = new Point(320, 25);
            SaveImageZoomLabel.Name = "SaveImageZoomLabel";
            SaveImageZoomLabel.Size = new Size(39, 15);
            SaveImageZoomLabel.TabIndex = 5;
            SaveImageZoomLabel.Text = "Zoom";
            // 
            // SaveImageBackgroundPanel
            // 
            SaveImageBackgroundPanel.AutoScroll = true;
            SaveImageBackgroundPanel.AutoScrollMargin = new Size(1, 1);
            SaveImageBackgroundPanel.BackColor = Color.Black;
            SaveImageBackgroundPanel.Controls.Add(SaveImagePictureBox);
            SaveImageBackgroundPanel.Location = new Point(4, 59);
            SaveImageBackgroundPanel.MaximumHeight = 514;
            SaveImageBackgroundPanel.MaximumWidth = 514;
            SaveImageBackgroundPanel.Name = "SaveImageBackgroundPanel";
            SaveImageBackgroundPanel.Size = new Size(40, 40);
            SaveImageBackgroundPanel.TabIndex = 6;
            // 
            // SaveImageWidthLabel
            // 
            SaveImageWidthLabel.AutoSize = true;
            SaveImageWidthLabel.Location = new Point(10, 10);
            SaveImageWidthLabel.Name = "SaveImageWidthLabel";
            SaveImageWidthLabel.Size = new Size(45, 15);
            SaveImageWidthLabel.TabIndex = 7;
            SaveImageWidthLabel.Text = "Width: ";
            // 
            // SaveImageHeightLabel
            // 
            SaveImageHeightLabel.AutoSize = true;
            SaveImageHeightLabel.Location = new Point(10, 30);
            SaveImageHeightLabel.Name = "SaveImageHeightLabel";
            SaveImageHeightLabel.Size = new Size(49, 15);
            SaveImageHeightLabel.TabIndex = 8;
            SaveImageHeightLabel.Text = "Height: ";
            // 
            // SaveImageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(584, 461);
            Controls.Add(SaveImageHeightLabel);
            Controls.Add(SaveImageWidthLabel);
            Controls.Add(SaveImageBackgroundPanel);
            Controls.Add(SaveImageZoomLabel);
            Controls.Add(SaveImageZoomNumberBar);
            Controls.Add(SaveImageZoomNumberBox);
            Controls.Add(CancelSaveButton);
            Controls.Add(SaveFullImageButton);
            Name = "SaveImageForm";
            Text = "Save Image";
            ((System.ComponentModel.ISupportInitialize)SaveImagePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)SaveImageZoomNumberBox).EndInit();
            SaveImageBackgroundPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox SaveImagePictureBox;
        private Button SaveFullImageButton;
        private Button CancelSaveButton;
        private Controls.NumberBox SaveImageZoomNumberBox;
        private Controls.NumberBar SaveImageZoomNumberBar;
        private Label SaveImageZoomLabel;
        private Controls.BackgroundPanel SaveImageBackgroundPanel;
        private Label SaveImageWidthLabel;
        private Label SaveImageHeightLabel;
    }
}