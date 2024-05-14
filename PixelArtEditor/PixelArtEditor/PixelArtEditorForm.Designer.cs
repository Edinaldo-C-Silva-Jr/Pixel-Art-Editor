﻿namespace PixelArtEditor
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
            ViewPixelWidthLabel = new Label();
            ViewWidthNumberBox = new Controls.NumberBox();
            ViewHeightNumberBox = new Controls.NumberBox();
            ViewPixelHeightLabel = new Label();
            ViewPixelSizeNumberBox = new Controls.NumberBox();
            ViewPixelSizeLabel = new Label();
            GridTypeLabel = new Label();
            GridTypeComboBox = new ComboBox();
            NewImageButton = new Button();
            SaveImageButton = new Button();
            ColorPickerDialog = new ColorDialog();
            DrawingBackgroundPanel = new Controls.BackgroundPanel();
            DrawingBox = new Controls.DrawBox();
            ColorsBackgroundPanel = new Controls.BackgroundPanel();
            ColorAmountComboBox = new ComboBox();
            PaletteColorTable = new Controls.ColorTable();
            BackgroundColorTable = new Controls.ColorTable();
            BackgroundColorLabel = new Label();
            GridColorTable = new Controls.ColorTable();
            GridColorLabel = new Label();
            ColorAmountLabel = new Label();
            TransparencyCheckBox = new CheckBox();
            ColorChangeCheckBox = new CheckBox();
            LoadImageButton = new Button();
            DialogForLoadingFiles = new OpenFileDialog();
            CopyButton = new Button();
            PasteButton = new Button();
            LoadPaletteButton = new Button();
            SavePaletteButton = new Button();
            DialogForSavingFiles = new SaveFileDialog();
            FullMirrorPenButton = new Controls.ToolButton();
            DrawingToolButtonPanel = new Controls.ToolButtonPanel();
            OutlineRectangleButton = new Controls.ToolButton();
            SolidRectangleButton = new Controls.ToolButton();
            FreeLineButton = new Controls.ToolButton();
            OrdinalLineButton = new Controls.ToolButton();
            CardinalLineButton = new Controls.ToolButton();
            EraserButton = new Controls.ToolButton();
            FourMirrorPenButton = new Controls.ToolButton();
            VerticalMirrorPenButton = new Controls.ToolButton();
            HorizontalMirrorPenButton = new Controls.ToolButton();
            PixelPenButton = new Controls.ToolButton();
            ViewingBackgroundPanel = new Controls.BackgroundPanel();
            ViewingBox = new Controls.ViewBox();
            DrawPixelSizeNumberBox = new Controls.NumberBox();
            DrawPixelSizeLabel = new Label();
            DrawHeightNumberBox = new Controls.NumberBox();
            DrawHeightLabel = new Label();
            DrawWidthNumberBox = new Controls.NumberBox();
            DrawWidthLabel = new Label();
            DrawingBoxSizeButton = new Button();
            ViewingBoxSizeButton = new Button();
            ViewAreaGroupBox = new GroupBox();
            DrawAreaGroupBox = new GroupBox();
            SelectionSizeComboBox = new ComboBox();
            SeelctionSizeLabel = new Label();
            TopBarPanel = new Panel();
            DrawingDivisionPanel = new Panel();
            ViewingDivisionPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)ViewWidthNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ViewHeightNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ViewPixelSizeNumberBox).BeginInit();
            DrawingBackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DrawingBox).BeginInit();
            ColorsBackgroundPanel.SuspendLayout();
            DrawingToolButtonPanel.SuspendLayout();
            ViewingBackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ViewingBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DrawPixelSizeNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DrawHeightNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DrawWidthNumberBox).BeginInit();
            ViewAreaGroupBox.SuspendLayout();
            DrawAreaGroupBox.SuspendLayout();
            TopBarPanel.SuspendLayout();
            DrawingDivisionPanel.SuspendLayout();
            ViewingDivisionPanel.SuspendLayout();
            SuspendLayout();
            // 
            // ViewPixelWidthLabel
            // 
            ViewPixelWidthLabel.Location = new Point(5, 20);
            ViewPixelWidthLabel.Name = "ViewPixelWidthLabel";
            ViewPixelWidthLabel.Size = new Size(50, 20);
            ViewPixelWidthLabel.TabIndex = 0;
            ViewPixelWidthLabel.Text = "Width";
            ViewPixelWidthLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ViewWidthNumberBox
            // 
            ViewWidthNumberBox.Location = new Point(55, 20);
            ViewWidthNumberBox.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            ViewWidthNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            ViewWidthNumberBox.Name = "ViewWidthNumberBox";
            ViewWidthNumberBox.Size = new Size(35, 23);
            ViewWidthNumberBox.TabIndex = 1;
            ViewWidthNumberBox.Value = new decimal(new int[] { 64, 0, 0, 0 });
            // 
            // ViewHeightNumberBox
            // 
            ViewHeightNumberBox.Location = new Point(55, 45);
            ViewHeightNumberBox.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            ViewHeightNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            ViewHeightNumberBox.Name = "ViewHeightNumberBox";
            ViewHeightNumberBox.Size = new Size(35, 23);
            ViewHeightNumberBox.TabIndex = 3;
            ViewHeightNumberBox.Value = new decimal(new int[] { 64, 0, 0, 0 });
            // 
            // ViewPixelHeightLabel
            // 
            ViewPixelHeightLabel.Location = new Point(5, 45);
            ViewPixelHeightLabel.Name = "ViewPixelHeightLabel";
            ViewPixelHeightLabel.Size = new Size(50, 20);
            ViewPixelHeightLabel.TabIndex = 2;
            ViewPixelHeightLabel.Text = "Height";
            ViewPixelHeightLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ViewPixelSizeNumberBox
            // 
            ViewPixelSizeNumberBox.Location = new Point(45, 115);
            ViewPixelSizeNumberBox.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            ViewPixelSizeNumberBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ViewPixelSizeNumberBox.Name = "ViewPixelSizeNumberBox";
            ViewPixelSizeNumberBox.Size = new Size(20, 23);
            ViewPixelSizeNumberBox.TabIndex = 5;
            ViewPixelSizeNumberBox.Value = new decimal(new int[] { 4, 0, 0, 0 });
            ViewPixelSizeNumberBox.ValueChanged += ViewingZoomNumberBox_ValueChanged;
            // 
            // ViewPixelSizeLabel
            // 
            ViewPixelSizeLabel.Location = new Point(5, 115);
            ViewPixelSizeLabel.Name = "ViewPixelSizeLabel";
            ViewPixelSizeLabel.Size = new Size(40, 20);
            ViewPixelSizeLabel.TabIndex = 4;
            ViewPixelSizeLabel.Text = "Zoom";
            ViewPixelSizeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GridTypeLabel
            // 
            GridTypeLabel.Location = new Point(105, 20);
            GridTypeLabel.Name = "GridTypeLabel";
            GridTypeLabel.Size = new Size(40, 20);
            GridTypeLabel.TabIndex = 6;
            GridTypeLabel.Text = "Grid";
            GridTypeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GridTypeComboBox
            // 
            GridTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            GridTypeComboBox.FormattingEnabled = true;
            GridTypeComboBox.Location = new Point(95, 40);
            GridTypeComboBox.Name = "GridTypeComboBox";
            GridTypeComboBox.Size = new Size(60, 23);
            GridTypeComboBox.TabIndex = 7;
            GridTypeComboBox.SelectedIndexChanged += ChangeGrid_GridTypeComboBoxIndexChanged;
            // 
            // NewImageButton
            // 
            NewImageButton.Location = new Point(472, 28);
            NewImageButton.Name = "NewImageButton";
            NewImageButton.Size = new Size(80, 25);
            NewImageButton.TabIndex = 8;
            NewImageButton.Text = "New Image";
            NewImageButton.UseVisualStyleBackColor = true;
            NewImageButton.Click += SetNewImageButton_Click;
            // 
            // SaveImageButton
            // 
            SaveImageButton.Location = new Point(472, 58);
            SaveImageButton.Name = "SaveImageButton";
            SaveImageButton.Size = new Size(80, 25);
            SaveImageButton.TabIndex = 9;
            SaveImageButton.Text = "Save Image";
            SaveImageButton.UseVisualStyleBackColor = true;
            SaveImageButton.Click += SaveImageButton_Click;
            // 
            // DrawingBackgroundPanel
            // 
            DrawingBackgroundPanel.AutoScroll = true;
            DrawingBackgroundPanel.AutoScrollMargin = new Size(1, 1);
            DrawingBackgroundPanel.BackColor = Color.Black;
            DrawingBackgroundPanel.Controls.Add(DrawingBox);
            DrawingBackgroundPanel.Location = new Point(10, 10);
            DrawingBackgroundPanel.MaximumHeight = 514;
            DrawingBackgroundPanel.MaximumWidth = 514;
            DrawingBackgroundPanel.Name = "DrawingBackgroundPanel";
            DrawingBackgroundPanel.Size = new Size(40, 40);
            DrawingBackgroundPanel.TabIndex = 10;
            // 
            // DrawingBox
            // 
            DrawingBox.BackColor = SystemColors.Control;
            DrawingBox.Location = new Point(1, 1);
            DrawingBox.Name = "DrawingBox";
            DrawingBox.Size = new Size(20, 20);
            DrawingBox.TabIndex = 0;
            DrawingBox.TabStop = false;
            DrawingBox.Paint += DrawingBox_Paint;
            DrawingBox.MouseDown += DrawingBox_MouseDown;
            DrawingBox.MouseLeave += DrawingBox_MouseLeave;
            DrawingBox.MouseMove += DrawingBox_MouseMove;
            DrawingBox.MouseUp += DrawingBox_MouseUp;
            // 
            // ColorsBackgroundPanel
            // 
            ColorsBackgroundPanel.BackColor = Color.White;
            ColorsBackgroundPanel.Controls.Add(ColorAmountComboBox);
            ColorsBackgroundPanel.Controls.Add(PaletteColorTable);
            ColorsBackgroundPanel.Controls.Add(BackgroundColorTable);
            ColorsBackgroundPanel.Controls.Add(BackgroundColorLabel);
            ColorsBackgroundPanel.Controls.Add(GridColorTable);
            ColorsBackgroundPanel.Controls.Add(GridColorLabel);
            ColorsBackgroundPanel.Controls.Add(ColorAmountLabel);
            ColorsBackgroundPanel.Location = new Point(354, 12);
            ColorsBackgroundPanel.MaximumHeight = 200;
            ColorsBackgroundPanel.MaximumWidth = 300;
            ColorsBackgroundPanel.Name = "ColorsBackgroundPanel";
            ColorsBackgroundPanel.Size = new Size(120, 120);
            ColorsBackgroundPanel.TabIndex = 11;
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
            PaletteColorTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            PaletteColorTable.CellSize = 16;
            PaletteColorTable.CellVisibleSelection = true;
            PaletteColorTable.ColumnCount = 1;
            PaletteColorTable.ColumnStyles.Add(new ColumnStyle());
            PaletteColorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            PaletteColorTable.Location = new Point(1, 80);
            PaletteColorTable.MaximumCellAmount = 64;
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
            BackgroundColorTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            BackgroundColorTable.CellSize = 30;
            BackgroundColorTable.CellVisibleSelection = false;
            BackgroundColorTable.ColumnCount = 1;
            BackgroundColorTable.ColumnStyles.Add(new ColumnStyle());
            BackgroundColorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            BackgroundColorTable.Location = new Point(90, 40);
            BackgroundColorTable.MaximumCellAmount = 1;
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
            BackgroundColorLabel.Text = "BC";
            // 
            // GridColorTable
            // 
            GridColorTable.BackColor = SystemColors.Control;
            GridColorTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            GridColorTable.CellSize = 30;
            GridColorTable.CellVisibleSelection = false;
            GridColorTable.ColumnCount = 1;
            GridColorTable.ColumnStyles.Add(new ColumnStyle());
            GridColorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            GridColorTable.Location = new Point(30, 40);
            GridColorTable.MaximumCellAmount = 1;
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
            TransparencyCheckBox.Location = new Point(651, 54);
            TransparencyCheckBox.Name = "TransparencyCheckBox";
            TransparencyCheckBox.Size = new Size(90, 20);
            TransparencyCheckBox.TabIndex = 12;
            TransparencyCheckBox.Text = "Transparent";
            TransparencyCheckBox.UseVisualStyleBackColor = true;
            TransparencyCheckBox.CheckedChanged += TransparencyCheckBox_CheckedChanged;
            // 
            // ColorChangeCheckBox
            // 
            ColorChangeCheckBox.Location = new Point(651, 28);
            ColorChangeCheckBox.Name = "ColorChangeCheckBox";
            ColorChangeCheckBox.Size = new Size(150, 20);
            ColorChangeCheckBox.TabIndex = 13;
            ColorChangeCheckBox.Text = "Change Color in Image";
            ColorChangeCheckBox.UseVisualStyleBackColor = true;
            // 
            // LoadImageButton
            // 
            LoadImageButton.Location = new Point(472, 88);
            LoadImageButton.Name = "LoadImageButton";
            LoadImageButton.Size = new Size(80, 25);
            LoadImageButton.TabIndex = 14;
            LoadImageButton.Text = "Load Image";
            LoadImageButton.UseVisualStyleBackColor = true;
            LoadImageButton.Click += LoadImageButton_Click;
            // 
            // CopyButton
            // 
            CopyButton.Location = new Point(557, 88);
            CopyButton.Name = "CopyButton";
            CopyButton.Size = new Size(50, 25);
            CopyButton.TabIndex = 16;
            CopyButton.Text = "Copy";
            CopyButton.UseVisualStyleBackColor = true;
            CopyButton.Click += CopyButton_Click;
            // 
            // PasteButton
            // 
            PasteButton.Location = new Point(612, 88);
            PasteButton.Name = "PasteButton";
            PasteButton.Size = new Size(50, 25);
            PasteButton.TabIndex = 17;
            PasteButton.Text = "Paste";
            PasteButton.UseVisualStyleBackColor = true;
            PasteButton.Click += PasteButton_Click;
            // 
            // LoadPaletteButton
            // 
            LoadPaletteButton.Location = new Point(557, 58);
            LoadPaletteButton.Name = "LoadPaletteButton";
            LoadPaletteButton.Size = new Size(80, 25);
            LoadPaletteButton.TabIndex = 19;
            LoadPaletteButton.Text = "Load Palette";
            LoadPaletteButton.UseVisualStyleBackColor = true;
            LoadPaletteButton.Click += LoadPaletteButton_Click;
            // 
            // SavePaletteButton
            // 
            SavePaletteButton.Location = new Point(557, 28);
            SavePaletteButton.Name = "SavePaletteButton";
            SavePaletteButton.Size = new Size(80, 25);
            SavePaletteButton.TabIndex = 18;
            SavePaletteButton.Text = "Save Palette";
            SavePaletteButton.UseVisualStyleBackColor = true;
            SavePaletteButton.Click += SavePaletteButton_Click;
            // 
            // FullMirrorPenButton
            // 
            FullMirrorPenButton.Location = new Point(120, 0);
            FullMirrorPenButton.Name = "FullMirrorPenButton";
            FullMirrorPenButton.PreviewOnHold = false;
            FullMirrorPenButton.PreviewOnMove = true;
            FullMirrorPenButton.Size = new Size(40, 40);
            FullMirrorPenButton.TabIndex = 25;
            FullMirrorPenButton.Text = "Mir. Pen";
            FullMirrorPenButton.ToolValue = 3;
            FullMirrorPenButton.UseBackgroundColor = false;
            FullMirrorPenButton.UseClickLocation = true;
            FullMirrorPenButton.UseImageSize = true;
            FullMirrorPenButton.UsePixelSize = true;
            FullMirrorPenButton.UseTransparency = false;
            FullMirrorPenButton.UseVisualStyleBackColor = true;
            FullMirrorPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // DrawingToolButtonPanel
            // 
            DrawingToolButtonPanel.BackColor = SystemColors.Control;
            DrawingToolButtonPanel.Controls.Add(OutlineRectangleButton);
            DrawingToolButtonPanel.Controls.Add(SolidRectangleButton);
            DrawingToolButtonPanel.Controls.Add(FreeLineButton);
            DrawingToolButtonPanel.Controls.Add(OrdinalLineButton);
            DrawingToolButtonPanel.Controls.Add(CardinalLineButton);
            DrawingToolButtonPanel.Controls.Add(EraserButton);
            DrawingToolButtonPanel.Controls.Add(FourMirrorPenButton);
            DrawingToolButtonPanel.Controls.Add(VerticalMirrorPenButton);
            DrawingToolButtonPanel.Controls.Add(HorizontalMirrorPenButton);
            DrawingToolButtonPanel.Controls.Add(PixelPenButton);
            DrawingToolButtonPanel.Controls.Add(FullMirrorPenButton);
            DrawingToolButtonPanel.Location = new Point(844, 33);
            DrawingToolButtonPanel.Name = "DrawingToolButtonPanel";
            DrawingToolButtonPanel.Size = new Size(240, 80);
            DrawingToolButtonPanel.TabIndex = 22;
            // 
            // OutlineRectangleButton
            // 
            OutlineRectangleButton.Location = new Point(200, 40);
            OutlineRectangleButton.Name = "OutlineRectangleButton";
            OutlineRectangleButton.PreviewOnHold = true;
            OutlineRectangleButton.PreviewOnMove = false;
            OutlineRectangleButton.Size = new Size(40, 40);
            OutlineRectangleButton.TabIndex = 32;
            OutlineRectangleButton.Text = "Out. Rect.";
            OutlineRectangleButton.ToolValue = 10;
            OutlineRectangleButton.UseBackgroundColor = false;
            OutlineRectangleButton.UseClickLocation = true;
            OutlineRectangleButton.UseImageSize = false;
            OutlineRectangleButton.UsePixelSize = true;
            OutlineRectangleButton.UseTransparency = false;
            OutlineRectangleButton.UseVisualStyleBackColor = true;
            OutlineRectangleButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // SolidRectangleButton
            // 
            SolidRectangleButton.Location = new Point(160, 40);
            SolidRectangleButton.Name = "SolidRectangleButton";
            SolidRectangleButton.PreviewOnHold = true;
            SolidRectangleButton.PreviewOnMove = false;
            SolidRectangleButton.Size = new Size(40, 40);
            SolidRectangleButton.TabIndex = 31;
            SolidRectangleButton.Text = "Rect";
            SolidRectangleButton.ToolValue = 9;
            SolidRectangleButton.UseBackgroundColor = false;
            SolidRectangleButton.UseClickLocation = true;
            SolidRectangleButton.UseImageSize = false;
            SolidRectangleButton.UsePixelSize = true;
            SolidRectangleButton.UseTransparency = false;
            SolidRectangleButton.UseVisualStyleBackColor = true;
            SolidRectangleButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // FreeLineButton
            // 
            FreeLineButton.Location = new Point(120, 40);
            FreeLineButton.Name = "FreeLineButton";
            FreeLineButton.PreviewOnHold = true;
            FreeLineButton.PreviewOnMove = false;
            FreeLineButton.Size = new Size(40, 40);
            FreeLineButton.TabIndex = 30;
            FreeLineButton.Text = "Free Line";
            FreeLineButton.ToolValue = 8;
            FreeLineButton.UseBackgroundColor = false;
            FreeLineButton.UseClickLocation = true;
            FreeLineButton.UseImageSize = false;
            FreeLineButton.UsePixelSize = true;
            FreeLineButton.UseTransparency = false;
            FreeLineButton.UseVisualStyleBackColor = true;
            FreeLineButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // OrdinalLineButton
            // 
            OrdinalLineButton.Location = new Point(80, 40);
            OrdinalLineButton.Name = "OrdinalLineButton";
            OrdinalLineButton.PreviewOnHold = true;
            OrdinalLineButton.PreviewOnMove = false;
            OrdinalLineButton.Size = new Size(40, 40);
            OrdinalLineButton.TabIndex = 29;
            OrdinalLineButton.Text = "8 Dir Line";
            OrdinalLineButton.ToolValue = 7;
            OrdinalLineButton.UseBackgroundColor = false;
            OrdinalLineButton.UseClickLocation = true;
            OrdinalLineButton.UseImageSize = false;
            OrdinalLineButton.UsePixelSize = true;
            OrdinalLineButton.UseTransparency = false;
            OrdinalLineButton.UseVisualStyleBackColor = true;
            OrdinalLineButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // CardinalLineButton
            // 
            CardinalLineButton.Location = new Point(40, 40);
            CardinalLineButton.Name = "CardinalLineButton";
            CardinalLineButton.PreviewOnHold = true;
            CardinalLineButton.PreviewOnMove = false;
            CardinalLineButton.Size = new Size(40, 40);
            CardinalLineButton.TabIndex = 28;
            CardinalLineButton.Text = "L R Line";
            CardinalLineButton.ToolValue = 6;
            CardinalLineButton.UseBackgroundColor = false;
            CardinalLineButton.UseClickLocation = true;
            CardinalLineButton.UseImageSize = false;
            CardinalLineButton.UsePixelSize = true;
            CardinalLineButton.UseTransparency = false;
            CardinalLineButton.UseVisualStyleBackColor = true;
            CardinalLineButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // EraserButton
            // 
            EraserButton.Location = new Point(0, 40);
            EraserButton.Name = "EraserButton";
            EraserButton.PreviewOnHold = false;
            EraserButton.PreviewOnMove = true;
            EraserButton.Size = new Size(40, 40);
            EraserButton.TabIndex = 27;
            EraserButton.Text = "Eraser";
            EraserButton.ToolValue = 5;
            EraserButton.UseBackgroundColor = true;
            EraserButton.UseClickLocation = true;
            EraserButton.UseImageSize = false;
            EraserButton.UsePixelSize = true;
            EraserButton.UseTransparency = true;
            EraserButton.UseVisualStyleBackColor = true;
            EraserButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // FourMirrorPenButton
            // 
            FourMirrorPenButton.Location = new Point(160, 0);
            FourMirrorPenButton.Name = "FourMirrorPenButton";
            FourMirrorPenButton.PreviewOnHold = false;
            FourMirrorPenButton.PreviewOnMove = true;
            FourMirrorPenButton.Size = new Size(40, 40);
            FourMirrorPenButton.TabIndex = 26;
            FourMirrorPenButton.Text = "4x Pen";
            FourMirrorPenButton.ToolValue = 4;
            FourMirrorPenButton.UseBackgroundColor = false;
            FourMirrorPenButton.UseClickLocation = true;
            FourMirrorPenButton.UseImageSize = true;
            FourMirrorPenButton.UsePixelSize = true;
            FourMirrorPenButton.UseTransparency = false;
            FourMirrorPenButton.UseVisualStyleBackColor = true;
            FourMirrorPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // VerticalMirrorPenButton
            // 
            VerticalMirrorPenButton.Location = new Point(80, 0);
            VerticalMirrorPenButton.Name = "VerticalMirrorPenButton";
            VerticalMirrorPenButton.PreviewOnHold = false;
            VerticalMirrorPenButton.PreviewOnMove = true;
            VerticalMirrorPenButton.Size = new Size(40, 40);
            VerticalMirrorPenButton.TabIndex = 24;
            VerticalMirrorPenButton.Text = "U D Pen";
            VerticalMirrorPenButton.ToolValue = 2;
            VerticalMirrorPenButton.UseBackgroundColor = false;
            VerticalMirrorPenButton.UseClickLocation = true;
            VerticalMirrorPenButton.UseImageSize = true;
            VerticalMirrorPenButton.UsePixelSize = true;
            VerticalMirrorPenButton.UseTransparency = false;
            VerticalMirrorPenButton.UseVisualStyleBackColor = true;
            VerticalMirrorPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // HorizontalMirrorPenButton
            // 
            HorizontalMirrorPenButton.Location = new Point(40, 0);
            HorizontalMirrorPenButton.Name = "HorizontalMirrorPenButton";
            HorizontalMirrorPenButton.PreviewOnHold = false;
            HorizontalMirrorPenButton.PreviewOnMove = true;
            HorizontalMirrorPenButton.Size = new Size(40, 40);
            HorizontalMirrorPenButton.TabIndex = 23;
            HorizontalMirrorPenButton.Text = "L R Pen";
            HorizontalMirrorPenButton.ToolValue = 1;
            HorizontalMirrorPenButton.UseBackgroundColor = false;
            HorizontalMirrorPenButton.UseClickLocation = true;
            HorizontalMirrorPenButton.UseImageSize = true;
            HorizontalMirrorPenButton.UsePixelSize = true;
            HorizontalMirrorPenButton.UseTransparency = false;
            HorizontalMirrorPenButton.UseVisualStyleBackColor = true;
            HorizontalMirrorPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // PixelPenButton
            // 
            PixelPenButton.Location = new Point(0, 0);
            PixelPenButton.Name = "PixelPenButton";
            PixelPenButton.PreviewOnHold = false;
            PixelPenButton.PreviewOnMove = true;
            PixelPenButton.Size = new Size(40, 40);
            PixelPenButton.TabIndex = 22;
            PixelPenButton.Text = "Pen";
            PixelPenButton.ToolValue = 0;
            PixelPenButton.UseBackgroundColor = false;
            PixelPenButton.UseClickLocation = true;
            PixelPenButton.UseImageSize = false;
            PixelPenButton.UsePixelSize = true;
            PixelPenButton.UseTransparency = false;
            PixelPenButton.UseVisualStyleBackColor = true;
            PixelPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // ViewingBackgroundPanel
            // 
            ViewingBackgroundPanel.AutoScroll = true;
            ViewingBackgroundPanel.AutoScrollMargin = new Size(1, 1);
            ViewingBackgroundPanel.BackColor = Color.Black;
            ViewingBackgroundPanel.Controls.Add(ViewingBox);
            ViewingBackgroundPanel.Location = new Point(10, 10);
            ViewingBackgroundPanel.MaximumHeight = 514;
            ViewingBackgroundPanel.MaximumWidth = 514;
            ViewingBackgroundPanel.Name = "ViewingBackgroundPanel";
            ViewingBackgroundPanel.Size = new Size(40, 40);
            ViewingBackgroundPanel.TabIndex = 23;
            // 
            // ViewingBox
            // 
            ViewingBox.BackColor = SystemColors.Control;
            ViewingBox.Location = new Point(1, 1);
            ViewingBox.Name = "ViewingBox";
            ViewingBox.Size = new Size(20, 20);
            ViewingBox.TabIndex = 0;
            ViewingBox.TabStop = false;
            ViewingBox.Click += ViewingBox_Click;
            ViewingBox.Paint += ViewingBox_Paint;
            ViewingBox.MouseDown += ViewingBox_MouseDown;
            ViewingBox.MouseMove += ViewingBox_MouseMove;
            // 
            // DrawPixelSizeNumberBox
            // 
            DrawPixelSizeNumberBox.Location = new Point(45, 115);
            DrawPixelSizeNumberBox.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            DrawPixelSizeNumberBox.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            DrawPixelSizeNumberBox.Name = "DrawPixelSizeNumberBox";
            DrawPixelSizeNumberBox.Size = new Size(25, 23);
            DrawPixelSizeNumberBox.TabIndex = 29;
            DrawPixelSizeNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            DrawPixelSizeNumberBox.ValueChanged += DrawingZoomNumberBox_ValueChanged;
            // 
            // DrawPixelSizeLabel
            // 
            DrawPixelSizeLabel.Location = new Point(5, 115);
            DrawPixelSizeLabel.Name = "DrawPixelSizeLabel";
            DrawPixelSizeLabel.Size = new Size(40, 20);
            DrawPixelSizeLabel.TabIndex = 28;
            DrawPixelSizeLabel.Text = "Zoom";
            DrawPixelSizeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DrawHeightNumberBox
            // 
            DrawHeightNumberBox.Location = new Point(55, 45);
            DrawHeightNumberBox.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            DrawHeightNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            DrawHeightNumberBox.Name = "DrawHeightNumberBox";
            DrawHeightNumberBox.Size = new Size(25, 23);
            DrawHeightNumberBox.TabIndex = 27;
            DrawHeightNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // DrawHeightLabel
            // 
            DrawHeightLabel.Location = new Point(5, 45);
            DrawHeightLabel.Name = "DrawHeightLabel";
            DrawHeightLabel.Size = new Size(50, 20);
            DrawHeightLabel.TabIndex = 26;
            DrawHeightLabel.Text = "Height";
            DrawHeightLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DrawWidthNumberBox
            // 
            DrawWidthNumberBox.Location = new Point(55, 20);
            DrawWidthNumberBox.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            DrawWidthNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            DrawWidthNumberBox.Name = "DrawWidthNumberBox";
            DrawWidthNumberBox.Size = new Size(25, 23);
            DrawWidthNumberBox.TabIndex = 25;
            DrawWidthNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // DrawWidthLabel
            // 
            DrawWidthLabel.Location = new Point(5, 20);
            DrawWidthLabel.Name = "DrawWidthLabel";
            DrawWidthLabel.Size = new Size(50, 20);
            DrawWidthLabel.TabIndex = 24;
            DrawWidthLabel.Text = "Width";
            DrawWidthLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DrawingBoxSizeButton
            // 
            DrawingBoxSizeButton.Location = new Point(5, 70);
            DrawingBoxSizeButton.Name = "DrawingBoxSizeButton";
            DrawingBoxSizeButton.Size = new Size(80, 30);
            DrawingBoxSizeButton.TabIndex = 30;
            DrawingBoxSizeButton.Text = "Change Size";
            DrawingBoxSizeButton.UseVisualStyleBackColor = true;
            DrawingBoxSizeButton.Click += DrawingBoxSizeButton_Click;
            // 
            // ViewingBoxSizeButton
            // 
            ViewingBoxSizeButton.Location = new Point(5, 70);
            ViewingBoxSizeButton.Name = "ViewingBoxSizeButton";
            ViewingBoxSizeButton.Size = new Size(85, 30);
            ViewingBoxSizeButton.TabIndex = 31;
            ViewingBoxSizeButton.Text = "Change Size";
            ViewingBoxSizeButton.UseVisualStyleBackColor = true;
            ViewingBoxSizeButton.Click += ViewingBoxSizeButton_Click;
            // 
            // ViewAreaGroupBox
            // 
            ViewAreaGroupBox.Controls.Add(ViewWidthNumberBox);
            ViewAreaGroupBox.Controls.Add(ViewingBoxSizeButton);
            ViewAreaGroupBox.Controls.Add(ViewPixelWidthLabel);
            ViewAreaGroupBox.Controls.Add(ViewHeightNumberBox);
            ViewAreaGroupBox.Controls.Add(ViewPixelHeightLabel);
            ViewAreaGroupBox.Controls.Add(ViewPixelSizeNumberBox);
            ViewAreaGroupBox.Controls.Add(ViewPixelSizeLabel);
            ViewAreaGroupBox.Location = new Point(180, 5);
            ViewAreaGroupBox.Name = "ViewAreaGroupBox";
            ViewAreaGroupBox.Size = new Size(150, 140);
            ViewAreaGroupBox.TabIndex = 32;
            ViewAreaGroupBox.TabStop = false;
            ViewAreaGroupBox.Text = "Viewing Area Options";
            // 
            // DrawAreaGroupBox
            // 
            DrawAreaGroupBox.Controls.Add(DrawWidthNumberBox);
            DrawAreaGroupBox.Controls.Add(DrawWidthLabel);
            DrawAreaGroupBox.Controls.Add(DrawPixelSizeNumberBox);
            DrawAreaGroupBox.Controls.Add(DrawingBoxSizeButton);
            DrawAreaGroupBox.Controls.Add(DrawPixelSizeLabel);
            DrawAreaGroupBox.Controls.Add(DrawHeightNumberBox);
            DrawAreaGroupBox.Controls.Add(DrawHeightLabel);
            DrawAreaGroupBox.Controls.Add(GridTypeLabel);
            DrawAreaGroupBox.Controls.Add(GridTypeComboBox);
            DrawAreaGroupBox.Location = new Point(5, 5);
            DrawAreaGroupBox.Name = "DrawAreaGroupBox";
            DrawAreaGroupBox.Size = new Size(170, 140);
            DrawAreaGroupBox.TabIndex = 33;
            DrawAreaGroupBox.TabStop = false;
            DrawAreaGroupBox.Text = "Drawing Area Options";
            // 
            // SelectionSizeComboBox
            // 
            SelectionSizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            SelectionSizeComboBox.FormattingEnabled = true;
            SelectionSizeComboBox.Items.AddRange(new object[] { "3", "4", "5", "6", "Draw Box" });
            SelectionSizeComboBox.Location = new Point(710, 98);
            SelectionSizeComboBox.Name = "SelectionSizeComboBox";
            SelectionSizeComboBox.Size = new Size(70, 23);
            SelectionSizeComboBox.TabIndex = 34;
            // 
            // SeelctionSizeLabel
            // 
            SeelctionSizeLabel.AutoSize = true;
            SeelctionSizeLabel.Location = new Point(705, 83);
            SeelctionSizeLabel.Name = "SeelctionSizeLabel";
            SeelctionSizeLabel.Size = new Size(78, 15);
            SeelctionSizeLabel.TabIndex = 35;
            SeelctionSizeLabel.Text = "Selection Size";
            // 
            // TopBarPanel
            // 
            TopBarPanel.BackColor = Color.LightGray;
            TopBarPanel.Controls.Add(DrawAreaGroupBox);
            TopBarPanel.Controls.Add(ViewAreaGroupBox);
            TopBarPanel.Controls.Add(SeelctionSizeLabel);
            TopBarPanel.Controls.Add(ColorsBackgroundPanel);
            TopBarPanel.Controls.Add(ColorChangeCheckBox);
            TopBarPanel.Controls.Add(SelectionSizeComboBox);
            TopBarPanel.Controls.Add(NewImageButton);
            TopBarPanel.Controls.Add(SaveImageButton);
            TopBarPanel.Controls.Add(DrawingToolButtonPanel);
            TopBarPanel.Controls.Add(TransparencyCheckBox);
            TopBarPanel.Controls.Add(LoadPaletteButton);
            TopBarPanel.Controls.Add(LoadImageButton);
            TopBarPanel.Controls.Add(SavePaletteButton);
            TopBarPanel.Controls.Add(CopyButton);
            TopBarPanel.Controls.Add(PasteButton);
            TopBarPanel.Location = new Point(0, 0);
            TopBarPanel.Name = "TopBarPanel";
            TopBarPanel.Size = new Size(1110, 150);
            TopBarPanel.TabIndex = 36;
            // 
            // DrawingDivisionPanel
            // 
            DrawingDivisionPanel.BackColor = Color.DarkGray;
            DrawingDivisionPanel.Controls.Add(DrawingBackgroundPanel);
            DrawingDivisionPanel.Location = new Point(0, 150);
            DrawingDivisionPanel.Name = "DrawingDivisionPanel";
            DrawingDivisionPanel.Size = new Size(550, 550);
            DrawingDivisionPanel.TabIndex = 37;
            // 
            // ViewingDivisionPanel
            // 
            ViewingDivisionPanel.BackColor = Color.DarkGray;
            ViewingDivisionPanel.Controls.Add(ViewingBackgroundPanel);
            ViewingDivisionPanel.Location = new Point(560, 150);
            ViewingDivisionPanel.Name = "ViewingDivisionPanel";
            ViewingDivisionPanel.Size = new Size(550, 550);
            ViewingDivisionPanel.TabIndex = 38;
            // 
            // PixelArtEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1184, 761);
            Controls.Add(ViewingDivisionPanel);
            Controls.Add(DrawingDivisionPanel);
            Controls.Add(TopBarPanel);
            KeyPreview = true;
            Name = "PixelArtEditorForm";
            Text = "Pixel Art Editor";
            FormClosing += PixelArtEditorForm_FormClosing;
            Load += PixelArtEditorForm_Load;
            KeyDown += PixelArtEditorForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)ViewWidthNumberBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)ViewHeightNumberBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)ViewPixelSizeNumberBox).EndInit();
            DrawingBackgroundPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DrawingBox).EndInit();
            ColorsBackgroundPanel.ResumeLayout(false);
            DrawingToolButtonPanel.ResumeLayout(false);
            ViewingBackgroundPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ViewingBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)DrawPixelSizeNumberBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)DrawHeightNumberBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)DrawWidthNumberBox).EndInit();
            ViewAreaGroupBox.ResumeLayout(false);
            DrawAreaGroupBox.ResumeLayout(false);
            TopBarPanel.ResumeLayout(false);
            TopBarPanel.PerformLayout();
            DrawingDivisionPanel.ResumeLayout(false);
            ViewingDivisionPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label ViewPixelWidthLabel;
        private Controls.NumberBox ViewWidthNumberBox;
        private Controls.NumberBox ViewHeightNumberBox;
        private Label ViewPixelHeightLabel;
        private Controls.NumberBox ViewPixelSizeNumberBox;
        private Label ViewPixelSizeLabel;
        private Label GridTypeLabel;
        private ComboBox GridTypeComboBox;
        private Button NewImageButton;
        private Button SaveImageButton;
        private ColorDialog ColorPickerDialog;
        private Controls.BackgroundPanel DrawingBackgroundPanel;
        private Controls.DrawBox DrawingBox;
        private Controls.BackgroundPanel ColorsBackgroundPanel;
        private Label GridColorLabel;
        private Label ColorAmountLabel;
        private Controls.ColorTable GridColorTable;
        private Controls.ColorTable BackgroundColorTable;
        private Label BackgroundColorLabel;
        private ComboBox ColorAmountComboBox;
        private Controls.ColorTable PaletteColorTable;
        private CheckBox TransparencyCheckBox;
        private CheckBox ColorChangeCheckBox;
        private Button LoadImageButton;
        private OpenFileDialog DialogForLoadingFiles;
        private Button CopyButton;
        private Button PasteButton;
        private Button LoadPaletteButton;
        private Button SavePaletteButton;
        private SaveFileDialog DialogForSavingFiles;
        private Controls.ToolButton FullMirrorPenButton;
        private Controls.ToolButtonPanel DrawingToolButtonPanel;
        private Controls.ToolButton PixelPenButton;
        private Controls.ToolButton VerticalMirrorPenButton;
        private Controls.ToolButton HorizontalMirrorPenButton;
        private Controls.ToolButton FourMirrorPenButton;
        private Controls.ToolButton EraserButton;
        private Controls.ToolButton CardinalLineButton;
        private Controls.ToolButton OrdinalLineButton;
        private Controls.ToolButton FreeLineButton;
        private Controls.ToolButton SolidRectangleButton;
        private Controls.ToolButton OutlineRectangleButton;
        private Controls.BackgroundPanel ViewingBackgroundPanel;
        private Controls.ViewBox ViewingBox;
        private Controls.NumberBox DrawPixelSizeNumberBox;
        private Label DrawPixelSizeLabel;
        private Controls.NumberBox DrawHeightNumberBox;
        private Label DrawHeightLabel;
        private Controls.NumberBox DrawWidthNumberBox;
        private Label DrawWidthLabel;
        private Button DrawingBoxSizeButton;
        private Button ViewingBoxSizeButton;
        private GroupBox ViewAreaGroupBox;
        private GroupBox DrawAreaGroupBox;
        private ComboBox SelectionSizeComboBox;
        private Label SeelctionSizeLabel;
        private Panel TopBarPanel;
        private Panel DrawingDivisionPanel;
        private Panel ViewingDivisionPanel;
    }
}