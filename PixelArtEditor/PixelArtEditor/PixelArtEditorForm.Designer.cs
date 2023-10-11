namespace PixelArtEditor
{
    partial class PixelArtEditorForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PixelWidthLabel = new Label();
            PixelWidthNumberBox = new Controls.NumberBox();
            PixelHeightNumberBox = new Controls.NumberBox();
            PixelHeightLabel = new Label();
            ViewingZoomNumberBox = new Controls.NumberBox();
            ViewingZoomLabel = new Label();
            GridTypeLabel = new Label();
            GridTypeComboBox = new ComboBox();
            SetNewImageSizeButton = new Button();
            SaveImageButton = new Button();
            ColorPickerDialog = new ColorDialog();
            ViewingAreaBackgroundPanel = new Controls.BackgroundPanel();
            ViewingAreaDrawingBox = new Controls.DrawingBox();
            ColorAreaBackgroundPanel = new Controls.BackgroundPanel();
            ColorAmountComboBox = new ComboBox();
            PaletteColorTable = new Controls.ColorTable();
            BackgroundColorTable = new Controls.ColorTable();
            BackgroundColorLabel = new Label();
            GridColorTable = new Controls.ColorTable();
            GridColorLabel = new Label();
            ColorAmountLabel = new Label();
            TransparencyCheckBox = new CheckBox();
            ColorChangeCheckBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)PixelWidthNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PixelHeightNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ViewingZoomNumberBox).BeginInit();
            ViewingAreaBackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ViewingAreaDrawingBox).BeginInit();
            ColorAreaBackgroundPanel.SuspendLayout();
            SuspendLayout();
            // 
            // PixelWidthLabel
            // 
            PixelWidthLabel.Location = new Point(10, 10);
            PixelWidthLabel.Name = "PixelWidthLabel";
            PixelWidthLabel.Size = new Size(50, 20);
            PixelWidthLabel.TabIndex = 0;
            PixelWidthLabel.Text = "Width";
            PixelWidthLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PixelWidthNumberBox
            // 
            PixelWidthNumberBox.Location = new Point(55, 9);
            PixelWidthNumberBox.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            PixelWidthNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            PixelWidthNumberBox.Name = "PixelWidthNumberBox";
            PixelWidthNumberBox.Size = new Size(50, 23);
            PixelWidthNumberBox.TabIndex = 1;
            PixelWidthNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            PixelWidthNumberBox.ValueChanged += SizeValuesChanged;
            // 
            // PixelHeightNumberBox
            // 
            PixelHeightNumberBox.Location = new Point(55, 39);
            PixelHeightNumberBox.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            PixelHeightNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            PixelHeightNumberBox.Name = "PixelHeightNumberBox";
            PixelHeightNumberBox.Size = new Size(50, 23);
            PixelHeightNumberBox.TabIndex = 3;
            PixelHeightNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            PixelHeightNumberBox.ValueChanged += SizeValuesChanged;
            // 
            // PixelHeightLabel
            // 
            PixelHeightLabel.Location = new Point(10, 40);
            PixelHeightLabel.Name = "PixelHeightLabel";
            PixelHeightLabel.Size = new Size(50, 20);
            PixelHeightLabel.TabIndex = 2;
            PixelHeightLabel.Text = "Height";
            PixelHeightLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ViewingZoomNumberBox
            // 
            ViewingZoomNumberBox.Location = new Point(165, 9);
            ViewingZoomNumberBox.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            ViewingZoomNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            ViewingZoomNumberBox.Name = "ViewingZoomNumberBox";
            ViewingZoomNumberBox.Size = new Size(50, 23);
            ViewingZoomNumberBox.TabIndex = 5;
            ViewingZoomNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // ViewingZoomLabel
            // 
            ViewingZoomLabel.Location = new Point(120, 10);
            ViewingZoomLabel.Name = "ViewingZoomLabel";
            ViewingZoomLabel.Size = new Size(50, 20);
            ViewingZoomLabel.TabIndex = 4;
            ViewingZoomLabel.Text = "Zoom";
            ViewingZoomLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GridTypeLabel
            // 
            GridTypeLabel.Location = new Point(105, 40);
            GridTypeLabel.Name = "GridTypeLabel";
            GridTypeLabel.Size = new Size(60, 20);
            GridTypeLabel.TabIndex = 6;
            GridTypeLabel.Text = "Grid Type";
            GridTypeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GridTypeComboBox
            // 
            GridTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            GridTypeComboBox.FormattingEnabled = true;
            GridTypeComboBox.Items.AddRange(new object[] { "None", "Line", "Checker" });
            GridTypeComboBox.Location = new Point(165, 39);
            GridTypeComboBox.Name = "GridTypeComboBox";
            GridTypeComboBox.Size = new Size(70, 23);
            GridTypeComboBox.TabIndex = 7;
            // 
            // SetNewImageSizeButton
            // 
            SetNewImageSizeButton.Location = new Point(15, 70);
            SetNewImageSizeButton.Name = "SetNewImageSizeButton";
            SetNewImageSizeButton.Size = new Size(90, 30);
            SetNewImageSizeButton.TabIndex = 8;
            SetNewImageSizeButton.Text = "Set New Size";
            SetNewImageSizeButton.UseVisualStyleBackColor = true;
            SetNewImageSizeButton.Click += SetNewImageSizeButton_Click;
            // 
            // SaveImageButton
            // 
            SaveImageButton.Location = new Point(130, 70);
            SaveImageButton.Name = "SaveImageButton";
            SaveImageButton.Size = new Size(80, 30);
            SaveImageButton.TabIndex = 9;
            SaveImageButton.Text = "Save Image";
            SaveImageButton.UseVisualStyleBackColor = true;
            SaveImageButton.Click += SaveImageButton_Click;
            // 
            // ViewingAreaBackgroundPanel
            // 
            ViewingAreaBackgroundPanel.AutoScroll = true;
            ViewingAreaBackgroundPanel.AutoScrollMargin = new Size(1, 1);
            ViewingAreaBackgroundPanel.BackColor = Color.Black;
            ViewingAreaBackgroundPanel.Controls.Add(ViewingAreaDrawingBox);
            ViewingAreaBackgroundPanel.Location = new Point(10, 120);
            ViewingAreaBackgroundPanel.Name = "ViewingAreaBackgroundPanel";
            ViewingAreaBackgroundPanel.Size = new Size(40, 40);
            ViewingAreaBackgroundPanel.TabIndex = 10;
            // 
            // ViewingAreaDrawingBox
            // 
            ViewingAreaDrawingBox.BackColor = SystemColors.Control;
            ViewingAreaDrawingBox.Location = new Point(1, 1);
            ViewingAreaDrawingBox.Name = "ViewingAreaDrawingBox";
            ViewingAreaDrawingBox.Size = new Size(20, 20);
            ViewingAreaDrawingBox.TabIndex = 0;
            ViewingAreaDrawingBox.TabStop = false;
            ViewingAreaDrawingBox.Click += ViewingAreaDrawingBox_Click;
            ViewingAreaDrawingBox.MouseMove += ViewingAreaDrawingBox_MouseMove;
            // 
            // ColorAreaBackgroundPanel
            // 
            ColorAreaBackgroundPanel.BackColor = Color.White;
            ColorAreaBackgroundPanel.Controls.Add(ColorAmountComboBox);
            ColorAreaBackgroundPanel.Controls.Add(PaletteColorTable);
            ColorAreaBackgroundPanel.Controls.Add(BackgroundColorTable);
            ColorAreaBackgroundPanel.Controls.Add(BackgroundColorLabel);
            ColorAreaBackgroundPanel.Controls.Add(GridColorTable);
            ColorAreaBackgroundPanel.Controls.Add(GridColorLabel);
            ColorAreaBackgroundPanel.Controls.Add(ColorAmountLabel);
            ColorAreaBackgroundPanel.Location = new Point(10, 170);
            ColorAreaBackgroundPanel.Name = "ColorAreaBackgroundPanel";
            ColorAreaBackgroundPanel.Size = new Size(120, 120);
            ColorAreaBackgroundPanel.TabIndex = 11;
            // 
            // ColorAmountComboBox
            // 
            ColorAmountComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ColorAmountComboBox.FormattingEnabled = true;
            ColorAmountComboBox.Items.AddRange(new object[] { "2", "4", "8", "16", "32", "64" });
            ColorAmountComboBox.Location = new Point(25, 3);
            ColorAmountComboBox.Name = "ColorAmountComboBox";
            ColorAmountComboBox.Size = new Size(50, 23);
            ColorAmountComboBox.TabIndex = 17;
            ColorAmountComboBox.SelectedIndexChanged += ColorAmountComboBox_SelectedIndexChanged;
            // 
            // PaletteColorTable
            // 
            PaletteColorTable.BackColor = SystemColors.Control;
            PaletteColorTable.ColumnCount = 1;
            PaletteColorTable.ColumnStyles.Add(new ColumnStyle());
            PaletteColorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            PaletteColorTable.Location = new Point(1, 80);
            PaletteColorTable.Name = "PaletteColorTable";
            PaletteColorTable.RowCount = 2;
            PaletteColorTable.RowStyles.Add(new RowStyle());
            PaletteColorTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            PaletteColorTable.Size = new Size(20, 20);
            PaletteColorTable.TabIndex = 16;
            // 
            // BackgroundColorTable
            // 
            BackgroundColorTable.BackColor = SystemColors.Control;
            BackgroundColorTable.ColumnCount = 1;
            BackgroundColorTable.ColumnStyles.Add(new ColumnStyle());
            BackgroundColorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            BackgroundColorTable.Location = new Point(90, 40);
            BackgroundColorTable.Name = "BackgroundColorTable";
            BackgroundColorTable.RowCount = 1;
            BackgroundColorTable.RowStyles.Add(new RowStyle());
            BackgroundColorTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            BackgroundColorTable.Size = new Size(20, 20);
            BackgroundColorTable.TabIndex = 14;
            // 
            // BackgroundColorLabel
            // 
            BackgroundColorLabel.Location = new Point(60, 45);
            BackgroundColorLabel.Name = "BackgroundColorLabel";
            BackgroundColorLabel.Size = new Size(30, 20);
            BackgroundColorLabel.TabIndex = 15;
            BackgroundColorLabel.Text = "GC";
            // 
            // GridColorTable
            // 
            GridColorTable.BackColor = SystemColors.Control;
            GridColorTable.ColumnCount = 1;
            GridColorTable.ColumnStyles.Add(new ColumnStyle());
            GridColorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            GridColorTable.Location = new Point(30, 40);
            GridColorTable.Name = "GridColorTable";
            GridColorTable.RowCount = 2;
            GridColorTable.RowStyles.Add(new RowStyle());
            GridColorTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            GridColorTable.Size = new Size(20, 20);
            GridColorTable.TabIndex = 12;
            // 
            // GridColorLabel
            // 
            GridColorLabel.Location = new Point(0, 45);
            GridColorLabel.Name = "GridColorLabel";
            GridColorLabel.Size = new Size(30, 20);
            GridColorLabel.TabIndex = 13;
            GridColorLabel.Text = "GC";
            // 
            // ColorAmountLabel
            // 
            ColorAmountLabel.Location = new Point(0, 5);
            ColorAmountLabel.Name = "ColorAmountLabel";
            ColorAmountLabel.Size = new Size(30, 20);
            ColorAmountLabel.TabIndex = 12;
            ColorAmountLabel.Text = "CA";
            // 
            // TransparencyCheckBox
            // 
            TransparencyCheckBox.Location = new Point(240, 10);
            TransparencyCheckBox.Name = "TransparencyCheckBox";
            TransparencyCheckBox.Size = new Size(120, 20);
            TransparencyCheckBox.TabIndex = 12;
            TransparencyCheckBox.Text = "Use Transparency";
            TransparencyCheckBox.UseVisualStyleBackColor = true;
            TransparencyCheckBox.CheckedChanged += TransparencyCheckBox_CheckedChanged;
            // 
            // ColorChangeCheckBox
            // 
            ColorChangeCheckBox.Location = new Point(240, 35);
            ColorChangeCheckBox.Name = "ColorChangeCheckBox";
            ColorChangeCheckBox.Size = new Size(150, 20);
            ColorChangeCheckBox.TabIndex = 13;
            ColorChangeCheckBox.Text = "Change Color in Image";
            ColorChangeCheckBox.UseVisualStyleBackColor = true;
            // 
            // PixelArtEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(800, 450);
            Controls.Add(ColorChangeCheckBox);
            Controls.Add(TransparencyCheckBox);
            Controls.Add(ColorAreaBackgroundPanel);
            Controls.Add(ViewingAreaBackgroundPanel);
            Controls.Add(SaveImageButton);
            Controls.Add(SetNewImageSizeButton);
            Controls.Add(GridTypeComboBox);
            Controls.Add(GridTypeLabel);
            Controls.Add(ViewingZoomNumberBox);
            Controls.Add(ViewingZoomLabel);
            Controls.Add(PixelHeightNumberBox);
            Controls.Add(PixelHeightLabel);
            Controls.Add(PixelWidthNumberBox);
            Controls.Add(PixelWidthLabel);
            Name = "PixelArtEditorForm";
            Text = "Pixel Art Editor";
            Load += PixelArtEditorForm_Load;
            ((System.ComponentModel.ISupportInitialize)PixelWidthNumberBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)PixelHeightNumberBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)ViewingZoomNumberBox).EndInit();
            ViewingAreaBackgroundPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ViewingAreaDrawingBox).EndInit();
            ColorAreaBackgroundPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label PixelWidthLabel;
        private Controls.NumberBox PixelWidthNumberBox;
        private Controls.NumberBox PixelHeightNumberBox;
        private Label PixelHeightLabel;
        private Controls.NumberBox ViewingZoomNumberBox;
        private Label ViewingZoomLabel;
        private Label GridTypeLabel;
        private ComboBox GridTypeComboBox;
        private Button SetNewImageSizeButton;
        private Button SaveImageButton;
        private ColorDialog ColorPickerDialog;
        private Controls.BackgroundPanel ViewingAreaBackgroundPanel;
        private Controls.DrawingBox ViewingAreaDrawingBox;
        private Controls.BackgroundPanel ColorAreaBackgroundPanel;
        private Label GridColorLabel;
        private Label ColorAmountLabel;
        private Controls.ColorTable GridColorTable;
        private Controls.ColorTable BackgroundColorTable;
        private Label BackgroundColorLabel;
        private ComboBox ColorAmountComboBox;
        private Controls.ColorTable PaletteColorTable;
        private CheckBox TransparencyCheckBox;
        private CheckBox ColorChangeCheckBox;
    }
}