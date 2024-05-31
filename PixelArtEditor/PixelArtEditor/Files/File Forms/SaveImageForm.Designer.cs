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
            FullImagePictureBox = new PictureBox();
            SaveFullImageButton = new Button();
            CancelSaveButton = new Button();
            FullImageZoomNumberBox = new Controls.NumberBox();
            FullImageZoomNumberBar = new Controls.NumberBar();
            FullImageZoomLabel = new Label();
            FullImageBackgroundPanel = new Controls.BackgroundPanel();
            FullImageWidthLabel = new Label();
            FullImageHeightLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)FullImagePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FullImageZoomNumberBox).BeginInit();
            FullImageBackgroundPanel.SuspendLayout();
            SuspendLayout();
            // 
            // FullImagePictureBox
            // 
            FullImagePictureBox.Location = new Point(1, 1);
            FullImagePictureBox.Name = "FullImagePictureBox";
            FullImagePictureBox.Size = new Size(20, 20);
            FullImagePictureBox.TabIndex = 0;
            FullImagePictureBox.TabStop = false;
            // 
            // SaveFullImageButton
            // 
            SaveFullImageButton.Location = new Point(130, 10);
            SaveFullImageButton.Name = "SaveFullImageButton";
            SaveFullImageButton.Size = new Size(80, 40);
            SaveFullImageButton.TabIndex = 1;
            SaveFullImageButton.Text = "Save Image";
            SaveFullImageButton.UseVisualStyleBackColor = true;
            // 
            // CancelSaveButton
            // 
            CancelSaveButton.Location = new Point(220, 10);
            CancelSaveButton.Name = "CancelSaveButton";
            CancelSaveButton.Size = new Size(80, 40);
            CancelSaveButton.TabIndex = 2;
            CancelSaveButton.Text = "Cancel";
            CancelSaveButton.UseVisualStyleBackColor = true;
            // 
            // FullImageZoomNumberBox
            // 
            FullImageZoomNumberBox.Location = new Point(360, 20);
            FullImageZoomNumberBox.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            FullImageZoomNumberBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            FullImageZoomNumberBox.Name = "FullImageZoomNumberBox";
            FullImageZoomNumberBox.Size = new Size(30, 23);
            FullImageZoomNumberBox.TabIndex = 3;
            FullImageZoomNumberBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            FullImageZoomNumberBox.ValueChanged += FullImageZoomNumberBox_ValueChanged;
            // 
            // FullImageZoomNumberBar
            // 
            FullImageZoomNumberBar.IncrementAmount = 1;
            FullImageZoomNumberBar.Location = new Point(395, 15);
            FullImageZoomNumberBar.MaximumValue = 64;
            FullImageZoomNumberBar.MinimumValue = 1;
            FullImageZoomNumberBar.Name = "FullImageZoomNumberBar";
            FullImageZoomNumberBar.Size = new Size(128, 30);
            FullImageZoomNumberBar.TabIndex = 4;
            FullImageZoomNumberBar.Value = 1;
            FullImageZoomNumberBar.ValueChanged += FullImageZoomNumberBar_ValueChanged;
            // 
            // FullImageZoomLabel
            // 
            FullImageZoomLabel.AutoSize = true;
            FullImageZoomLabel.Location = new Point(320, 25);
            FullImageZoomLabel.Name = "FullImageZoomLabel";
            FullImageZoomLabel.Size = new Size(39, 15);
            FullImageZoomLabel.TabIndex = 5;
            FullImageZoomLabel.Text = "Zoom";
            // 
            // FullImageBackgroundPanel
            // 
            FullImageBackgroundPanel.AutoScroll = true;
            FullImageBackgroundPanel.AutoScrollMargin = new Size(1, 1);
            FullImageBackgroundPanel.BackColor = Color.Black;
            FullImageBackgroundPanel.Controls.Add(FullImagePictureBox);
            FullImageBackgroundPanel.Location = new Point(4, 59);
            FullImageBackgroundPanel.MaximumHeight = 514;
            FullImageBackgroundPanel.MaximumWidth = 514;
            FullImageBackgroundPanel.Name = "FullImageBackgroundPanel";
            FullImageBackgroundPanel.Size = new Size(40, 40);
            FullImageBackgroundPanel.TabIndex = 6;
            // 
            // FullImageWidthLabel
            // 
            FullImageWidthLabel.AutoSize = true;
            FullImageWidthLabel.Location = new Point(10, 10);
            FullImageWidthLabel.Name = "FullImageWidthLabel";
            FullImageWidthLabel.Size = new Size(45, 15);
            FullImageWidthLabel.TabIndex = 7;
            FullImageWidthLabel.Text = "Width: ";
            // 
            // FullImageHeightLabel
            // 
            FullImageHeightLabel.AutoSize = true;
            FullImageHeightLabel.Location = new Point(10, 30);
            FullImageHeightLabel.Name = "FullImageHeightLabel";
            FullImageHeightLabel.Size = new Size(49, 15);
            FullImageHeightLabel.TabIndex = 8;
            FullImageHeightLabel.Text = "Height: ";
            // 
            // SaveImageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(584, 461);
            Controls.Add(FullImageHeightLabel);
            Controls.Add(FullImageWidthLabel);
            Controls.Add(FullImageBackgroundPanel);
            Controls.Add(FullImageZoomLabel);
            Controls.Add(FullImageZoomNumberBar);
            Controls.Add(FullImageZoomNumberBox);
            Controls.Add(CancelSaveButton);
            Controls.Add(SaveFullImageButton);
            Name = "SaveImageForm";
            Text = "Save Image";
            ((System.ComponentModel.ISupportInitialize)FullImagePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)FullImageZoomNumberBox).EndInit();
            FullImageBackgroundPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox FullImagePictureBox;
        private Button SaveFullImageButton;
        private Button CancelSaveButton;
        private Controls.NumberBox FullImageZoomNumberBox;
        private Controls.NumberBar FullImageZoomNumberBar;
        private Label FullImageZoomLabel;
        private Controls.BackgroundPanel FullImageBackgroundPanel;
        private Label FullImageWidthLabel;
        private Label FullImageHeightLabel;
    }
}