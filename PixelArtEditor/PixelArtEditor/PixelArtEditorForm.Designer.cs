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
            PaletteColorTable = new Controls.ColorTable();
            BackgroundColorTable = new Controls.ColorTable();
            BackgroundColorLabel = new Label();
            GridColorTable = new Controls.ColorTable();
            GridColorLabel = new Label();
            LoadPaletteButton = new Button();
            SavePaletteButton = new Button();
            TransparencyCheckBox = new CheckBox();
            LoadImageButton = new Button();
            CopyButton = new Button();
            PasteButton = new Button();
            FullMirrorPenButton = new Controls.ToolButton();
            DrawingToolButtonPanel = new Controls.ToolButtonPanel();
            DitheringPenButton = new Controls.ToolButton();
            DrawingToolsLabel = new Label();
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
            SeelctionSizeLabel = new Label();
            SelectionSizeComboBox = new ComboBox();
            TopBarPanel = new Panel();
            ReplaceColorPanel = new Panel();
            PickColorToReplaceButton = new Button();
            ReplacementColorLabel = new Label();
            ReplacementColorTable = new Controls.ColorTable();
            ColorToReplaceLabel = new Label();
            ColorToReplaceTable = new Controls.ColorTable();
            ReplaceColorButton = new Button();
            ReplaceColorLabel = new Label();
            ImageOptionsPanel = new Panel();
            RedoButton = new Button();
            UndoButton = new Button();
            ImageOptionsLabel = new Label();
            ColorPalettePanel = new Panel();
            ColorValueLabel = new Label();
            ColorPaletteOptionsLabel = new Label();
            ViewPixelSizeNumberBar = new Controls.NumberBar();
            DrawPixelSizeNumberBar = new Controls.NumberBar();
            DrawingDivisionPanel = new Panel();
            ViewingDivisionPanel = new Panel();
            DrawingBoxSizePanel = new Panel();
            ViewingBoxSizePanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)ViewWidthNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ViewHeightNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ViewPixelSizeNumberBox).BeginInit();
            DrawingBackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DrawingBox).BeginInit();
            DrawingToolButtonPanel.SuspendLayout();
            ViewingBackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ViewingBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DrawPixelSizeNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DrawHeightNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DrawWidthNumberBox).BeginInit();
            TopBarPanel.SuspendLayout();
            ReplaceColorPanel.SuspendLayout();
            ImageOptionsPanel.SuspendLayout();
            ColorPalettePanel.SuspendLayout();
            DrawingDivisionPanel.SuspendLayout();
            ViewingDivisionPanel.SuspendLayout();
            DrawingBoxSizePanel.SuspendLayout();
            ViewingBoxSizePanel.SuspendLayout();
            SuspendLayout();
            // 
            // ViewPixelWidthLabel
            // 
            ViewPixelWidthLabel.Location = new Point(1, 6);
            ViewPixelWidthLabel.Name = "ViewPixelWidthLabel";
            ViewPixelWidthLabel.Size = new Size(45, 20);
            ViewPixelWidthLabel.TabIndex = 0;
            ViewPixelWidthLabel.Text = "Width";
            ViewPixelWidthLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ViewWidthNumberBox
            // 
            ViewWidthNumberBox.Location = new Point(46, 5);
            ViewWidthNumberBox.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            ViewWidthNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            ViewWidthNumberBox.Name = "ViewWidthNumberBox";
            ViewWidthNumberBox.Size = new Size(35, 23);
            ViewWidthNumberBox.TabIndex = 1;
            ViewWidthNumberBox.Value = new decimal(new int[] { 64, 0, 0, 0 });
            // 
            // ViewHeightNumberBox
            // 
            ViewHeightNumberBox.Location = new Point(131, 5);
            ViewHeightNumberBox.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            ViewHeightNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            ViewHeightNumberBox.Name = "ViewHeightNumberBox";
            ViewHeightNumberBox.Size = new Size(35, 23);
            ViewHeightNumberBox.TabIndex = 3;
            ViewHeightNumberBox.Value = new decimal(new int[] { 64, 0, 0, 0 });
            // 
            // ViewPixelHeightLabel
            // 
            ViewPixelHeightLabel.Location = new Point(81, 6);
            ViewPixelHeightLabel.Name = "ViewPixelHeightLabel";
            ViewPixelHeightLabel.Size = new Size(50, 20);
            ViewPixelHeightLabel.TabIndex = 2;
            ViewPixelHeightLabel.Text = "Height";
            ViewPixelHeightLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ViewPixelSizeNumberBox
            // 
            ViewPixelSizeNumberBox.Location = new Point(440, 5);
            ViewPixelSizeNumberBox.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            ViewPixelSizeNumberBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ViewPixelSizeNumberBox.Name = "ViewPixelSizeNumberBox";
            ViewPixelSizeNumberBox.Size = new Size(20, 23);
            ViewPixelSizeNumberBox.TabIndex = 5;
            ViewPixelSizeNumberBox.Value = new decimal(new int[] { 4, 0, 0, 0 });
            ViewPixelSizeNumberBox.ValueChanged += ViewPixelSizeNumberBox_ValueChanged;
            // 
            // ViewPixelSizeLabel
            // 
            ViewPixelSizeLabel.Location = new Point(400, 6);
            ViewPixelSizeLabel.Name = "ViewPixelSizeLabel";
            ViewPixelSizeLabel.Size = new Size(40, 20);
            ViewPixelSizeLabel.TabIndex = 4;
            ViewPixelSizeLabel.Text = "Zoom";
            ViewPixelSizeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GridTypeLabel
            // 
            GridTypeLabel.Location = new Point(280, 6);
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
            GridTypeComboBox.Location = new Point(320, 5);
            GridTypeComboBox.Name = "GridTypeComboBox";
            GridTypeComboBox.Size = new Size(60, 23);
            GridTypeComboBox.TabIndex = 7;
            GridTypeComboBox.SelectedIndexChanged += ChangeGrid_GridTypeComboBoxIndexChanged;
            // 
            // NewImageButton
            // 
            NewImageButton.Location = new Point(5, 15);
            NewImageButton.Name = "NewImageButton";
            NewImageButton.Size = new Size(50, 40);
            NewImageButton.TabIndex = 8;
            NewImageButton.Text = "New Image";
            NewImageButton.UseVisualStyleBackColor = true;
            NewImageButton.Click += SetNewImageButton_Click;
            // 
            // SaveImageButton
            // 
            SaveImageButton.Location = new Point(5, 60);
            SaveImageButton.Name = "SaveImageButton";
            SaveImageButton.Size = new Size(50, 40);
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
            DrawingBackgroundPanel.Location = new Point(5, 5);
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
            // PaletteColorTable
            // 
            PaletteColorTable.BackColor = SystemColors.Control;
            PaletteColorTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            PaletteColorTable.CellSize = 15;
            PaletteColorTable.CellVisibleSelection = true;
            PaletteColorTable.ColumnCount = 1;
            PaletteColorTable.ColumnStyles.Add(new ColumnStyle());
            PaletteColorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            PaletteColorTable.Location = new Point(3, 83);
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
            BackgroundColorTable.CellSize = 16;
            BackgroundColorTable.CellVisibleSelection = false;
            BackgroundColorTable.ColumnCount = 1;
            BackgroundColorTable.ColumnStyles.Add(new ColumnStyle());
            BackgroundColorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            BackgroundColorTable.Location = new Point(150, 60);
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
            BackgroundColorLabel.Location = new Point(70, 60);
            BackgroundColorLabel.Name = "BackgroundColorLabel";
            BackgroundColorLabel.Size = new Size(80, 20);
            BackgroundColorLabel.TabIndex = 15;
            BackgroundColorLabel.Text = "Background";
            BackgroundColorLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // GridColorTable
            // 
            GridColorTable.BackColor = SystemColors.Control;
            GridColorTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            GridColorTable.CellSize = 16;
            GridColorTable.CellVisibleSelection = false;
            GridColorTable.ColumnCount = 1;
            GridColorTable.ColumnStyles.Add(new ColumnStyle());
            GridColorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            GridColorTable.Location = new Point(35, 60);
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
            GridColorLabel.Location = new Point(5, 60);
            GridColorLabel.Name = "GridColorLabel";
            GridColorLabel.Size = new Size(30, 20);
            GridColorLabel.TabIndex = 13;
            GridColorLabel.Text = "Grid";
            GridColorLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // LoadPaletteButton
            // 
            LoadPaletteButton.Location = new Point(56, 15);
            LoadPaletteButton.Name = "LoadPaletteButton";
            LoadPaletteButton.Size = new Size(55, 40);
            LoadPaletteButton.TabIndex = 19;
            LoadPaletteButton.Text = "Load Palette";
            LoadPaletteButton.UseVisualStyleBackColor = true;
            LoadPaletteButton.Click += LoadPaletteButton_Click;
            // 
            // SavePaletteButton
            // 
            SavePaletteButton.Location = new Point(1, 15);
            SavePaletteButton.Name = "SavePaletteButton";
            SavePaletteButton.Size = new Size(55, 40);
            SavePaletteButton.TabIndex = 18;
            SavePaletteButton.Text = "Save Palette";
            SavePaletteButton.UseVisualStyleBackColor = true;
            SavePaletteButton.Click += SavePaletteButton_Click;
            // 
            // TransparencyCheckBox
            // 
            TransparencyCheckBox.Location = new Point(115, 15);
            TransparencyCheckBox.Name = "TransparencyCheckBox";
            TransparencyCheckBox.Size = new Size(160, 20);
            TransparencyCheckBox.TabIndex = 12;
            TransparencyCheckBox.Text = "Transparent Background";
            TransparencyCheckBox.UseVisualStyleBackColor = true;
            TransparencyCheckBox.CheckedChanged += TransparencyCheckBox_CheckedChanged;
            // 
            // LoadImageButton
            // 
            LoadImageButton.Location = new Point(5, 105);
            LoadImageButton.Name = "LoadImageButton";
            LoadImageButton.Size = new Size(50, 40);
            LoadImageButton.TabIndex = 14;
            LoadImageButton.Text = "Load Image";
            LoadImageButton.UseVisualStyleBackColor = true;
            LoadImageButton.Click += LoadImageButton_Click;
            // 
            // CopyButton
            // 
            CopyButton.Location = new Point(60, 15);
            CopyButton.Name = "CopyButton";
            CopyButton.Size = new Size(50, 30);
            CopyButton.TabIndex = 16;
            CopyButton.Text = "Copy";
            CopyButton.UseVisualStyleBackColor = true;
            CopyButton.Click += CopyButton_Click;
            // 
            // PasteButton
            // 
            PasteButton.Location = new Point(60, 45);
            PasteButton.Name = "PasteButton";
            PasteButton.Size = new Size(50, 30);
            PasteButton.TabIndex = 17;
            PasteButton.Text = "Paste";
            PasteButton.UseVisualStyleBackColor = true;
            PasteButton.Click += PasteButton_Click;
            // 
            // FullMirrorPenButton
            // 
            FullMirrorPenButton.Location = new Point(165, 15);
            FullMirrorPenButton.Name = "FullMirrorPenButton";
            FullMirrorPenButton.PreviewOnHold = false;
            FullMirrorPenButton.PreviewOnMove = true;
            FullMirrorPenButton.Size = new Size(40, 40);
            FullMirrorPenButton.TabIndex = 25;
            FullMirrorPenButton.Text = "Mir. Pen";
            FullMirrorPenButton.ToolValue = 9;
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
            DrawingToolButtonPanel.BackColor = Color.Gainsboro;
            DrawingToolButtonPanel.BorderStyle = BorderStyle.FixedSingle;
            DrawingToolButtonPanel.Controls.Add(DitheringPenButton);
            DrawingToolButtonPanel.Controls.Add(DrawingToolsLabel);
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
            DrawingToolButtonPanel.Location = new Point(800, 5);
            DrawingToolButtonPanel.Name = "DrawingToolButtonPanel";
            DrawingToolButtonPanel.Size = new Size(275, 150);
            DrawingToolButtonPanel.TabIndex = 22;
            // 
            // DitheringPenButton
            // 
            DitheringPenButton.Location = new Point(45, 15);
            DitheringPenButton.Name = "DitheringPenButton";
            DitheringPenButton.PreviewOnHold = false;
            DitheringPenButton.PreviewOnMove = true;
            DitheringPenButton.Size = new Size(40, 40);
            DitheringPenButton.TabIndex = 38;
            DitheringPenButton.Text = "Dith. Pen";
            DitheringPenButton.ToolValue = 1;
            DitheringPenButton.UseBackgroundColor = false;
            DitheringPenButton.UseClickLocation = true;
            DitheringPenButton.UseImageSize = false;
            DitheringPenButton.UsePixelSize = true;
            DitheringPenButton.UseTransparency = false;
            DitheringPenButton.UseVisualStyleBackColor = true;
            DitheringPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // DrawingToolsLabel
            // 
            DrawingToolsLabel.Dock = DockStyle.Top;
            DrawingToolsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            DrawingToolsLabel.Location = new Point(0, 0);
            DrawingToolsLabel.Name = "DrawingToolsLabel";
            DrawingToolsLabel.Size = new Size(273, 15);
            DrawingToolsLabel.TabIndex = 37;
            DrawingToolsLabel.Text = "Drawing Tools";
            DrawingToolsLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // OutlineRectangleButton
            // 
            OutlineRectangleButton.Location = new Point(45, 105);
            OutlineRectangleButton.Name = "OutlineRectangleButton";
            OutlineRectangleButton.PreviewOnHold = true;
            OutlineRectangleButton.PreviewOnMove = false;
            OutlineRectangleButton.Size = new Size(40, 40);
            OutlineRectangleButton.TabIndex = 32;
            OutlineRectangleButton.Text = "Out. Rect.";
            OutlineRectangleButton.ToolValue = 6;
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
            SolidRectangleButton.Location = new Point(5, 105);
            SolidRectangleButton.Name = "SolidRectangleButton";
            SolidRectangleButton.PreviewOnHold = true;
            SolidRectangleButton.PreviewOnMove = false;
            SolidRectangleButton.Size = new Size(40, 40);
            SolidRectangleButton.TabIndex = 31;
            SolidRectangleButton.Text = "Rect";
            SolidRectangleButton.ToolValue = 5;
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
            FreeLineButton.Location = new Point(85, 60);
            FreeLineButton.Name = "FreeLineButton";
            FreeLineButton.PreviewOnHold = true;
            FreeLineButton.PreviewOnMove = false;
            FreeLineButton.Size = new Size(40, 40);
            FreeLineButton.TabIndex = 30;
            FreeLineButton.Text = "Free Line";
            FreeLineButton.ToolValue = 4;
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
            OrdinalLineButton.Location = new Point(45, 60);
            OrdinalLineButton.Name = "OrdinalLineButton";
            OrdinalLineButton.PreviewOnHold = true;
            OrdinalLineButton.PreviewOnMove = false;
            OrdinalLineButton.Size = new Size(40, 40);
            OrdinalLineButton.TabIndex = 29;
            OrdinalLineButton.Text = "8 Dir Line";
            OrdinalLineButton.ToolValue = 3;
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
            CardinalLineButton.Location = new Point(5, 60);
            CardinalLineButton.Name = "CardinalLineButton";
            CardinalLineButton.PreviewOnHold = true;
            CardinalLineButton.PreviewOnMove = false;
            CardinalLineButton.Size = new Size(40, 40);
            CardinalLineButton.TabIndex = 28;
            CardinalLineButton.Text = "L R Line";
            CardinalLineButton.ToolValue = 2;
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
            EraserButton.Location = new Point(205, 60);
            EraserButton.Name = "EraserButton";
            EraserButton.PreviewOnHold = false;
            EraserButton.PreviewOnMove = true;
            EraserButton.Size = new Size(40, 40);
            EraserButton.TabIndex = 27;
            EraserButton.Text = "Eraser";
            EraserButton.ToolValue = 11;
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
            FourMirrorPenButton.Location = new Point(205, 15);
            FourMirrorPenButton.Name = "FourMirrorPenButton";
            FourMirrorPenButton.PreviewOnHold = false;
            FourMirrorPenButton.PreviewOnMove = true;
            FourMirrorPenButton.Size = new Size(40, 40);
            FourMirrorPenButton.TabIndex = 26;
            FourMirrorPenButton.Text = "4x Pen";
            FourMirrorPenButton.ToolValue = 10;
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
            VerticalMirrorPenButton.Location = new Point(125, 15);
            VerticalMirrorPenButton.Name = "VerticalMirrorPenButton";
            VerticalMirrorPenButton.PreviewOnHold = false;
            VerticalMirrorPenButton.PreviewOnMove = true;
            VerticalMirrorPenButton.Size = new Size(40, 40);
            VerticalMirrorPenButton.TabIndex = 24;
            VerticalMirrorPenButton.Text = "U D Pen";
            VerticalMirrorPenButton.ToolValue = 8;
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
            HorizontalMirrorPenButton.Location = new Point(85, 15);
            HorizontalMirrorPenButton.Name = "HorizontalMirrorPenButton";
            HorizontalMirrorPenButton.PreviewOnHold = false;
            HorizontalMirrorPenButton.PreviewOnMove = true;
            HorizontalMirrorPenButton.Size = new Size(40, 40);
            HorizontalMirrorPenButton.TabIndex = 23;
            HorizontalMirrorPenButton.Text = "L R Pen";
            HorizontalMirrorPenButton.ToolValue = 7;
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
            PixelPenButton.Location = new Point(5, 15);
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
            ViewingBackgroundPanel.Location = new Point(5, 5);
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
            DrawPixelSizeNumberBox.Increment = new decimal(new int[] { 4, 0, 0, 0 });
            DrawPixelSizeNumberBox.Location = new Point(435, 5);
            DrawPixelSizeNumberBox.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            DrawPixelSizeNumberBox.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            DrawPixelSizeNumberBox.Name = "DrawPixelSizeNumberBox";
            DrawPixelSizeNumberBox.Size = new Size(25, 23);
            DrawPixelSizeNumberBox.TabIndex = 29;
            DrawPixelSizeNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            DrawPixelSizeNumberBox.ValueChanged += DrawPixelSizeNumberBox_ValueChanged;
            // 
            // DrawPixelSizeLabel
            // 
            DrawPixelSizeLabel.Location = new Point(395, 6);
            DrawPixelSizeLabel.Name = "DrawPixelSizeLabel";
            DrawPixelSizeLabel.Size = new Size(40, 20);
            DrawPixelSizeLabel.TabIndex = 28;
            DrawPixelSizeLabel.Text = "Zoom";
            DrawPixelSizeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DrawHeightNumberBox
            // 
            DrawHeightNumberBox.Location = new Point(126, 5);
            DrawHeightNumberBox.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            DrawHeightNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            DrawHeightNumberBox.Name = "DrawHeightNumberBox";
            DrawHeightNumberBox.Size = new Size(25, 23);
            DrawHeightNumberBox.TabIndex = 27;
            DrawHeightNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // DrawHeightLabel
            // 
            DrawHeightLabel.Location = new Point(76, 6);
            DrawHeightLabel.Name = "DrawHeightLabel";
            DrawHeightLabel.Size = new Size(50, 20);
            DrawHeightLabel.TabIndex = 26;
            DrawHeightLabel.Text = "Height";
            DrawHeightLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DrawWidthNumberBox
            // 
            DrawWidthNumberBox.Location = new Point(46, 5);
            DrawWidthNumberBox.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            DrawWidthNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            DrawWidthNumberBox.Name = "DrawWidthNumberBox";
            DrawWidthNumberBox.Size = new Size(25, 23);
            DrawWidthNumberBox.TabIndex = 25;
            DrawWidthNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // DrawWidthLabel
            // 
            DrawWidthLabel.Location = new Point(1, 6);
            DrawWidthLabel.Name = "DrawWidthLabel";
            DrawWidthLabel.Size = new Size(45, 20);
            DrawWidthLabel.TabIndex = 24;
            DrawWidthLabel.Text = "Width";
            DrawWidthLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DrawingBoxSizeButton
            // 
            DrawingBoxSizeButton.Location = new Point(160, 1);
            DrawingBoxSizeButton.Name = "DrawingBoxSizeButton";
            DrawingBoxSizeButton.Size = new Size(80, 30);
            DrawingBoxSizeButton.TabIndex = 30;
            DrawingBoxSizeButton.Text = "Change Size";
            DrawingBoxSizeButton.UseVisualStyleBackColor = true;
            DrawingBoxSizeButton.Click += DrawingBoxSizeButton_Click;
            // 
            // ViewingBoxSizeButton
            // 
            ViewingBoxSizeButton.Location = new Point(170, 1);
            ViewingBoxSizeButton.Name = "ViewingBoxSizeButton";
            ViewingBoxSizeButton.Size = new Size(80, 30);
            ViewingBoxSizeButton.TabIndex = 31;
            ViewingBoxSizeButton.Text = "Change Size";
            ViewingBoxSizeButton.UseVisualStyleBackColor = true;
            ViewingBoxSizeButton.Click += ViewingBoxSizeButton_Click;
            // 
            // SeelctionSizeLabel
            // 
            SeelctionSizeLabel.Location = new Point(270, 1);
            SeelctionSizeLabel.Name = "SeelctionSizeLabel";
            SeelctionSizeLabel.Size = new Size(50, 30);
            SeelctionSizeLabel.TabIndex = 35;
            SeelctionSizeLabel.Text = "Select Size";
            SeelctionSizeLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // SelectionSizeComboBox
            // 
            SelectionSizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            SelectionSizeComboBox.FormattingEnabled = true;
            SelectionSizeComboBox.Items.AddRange(new object[] { "3", "4", "5", "6", "Draw Box" });
            SelectionSizeComboBox.Location = new Point(320, 5);
            SelectionSizeComboBox.Name = "SelectionSizeComboBox";
            SelectionSizeComboBox.Size = new Size(75, 23);
            SelectionSizeComboBox.TabIndex = 34;
            // 
            // TopBarPanel
            // 
            TopBarPanel.BackColor = Color.Silver;
            TopBarPanel.Controls.Add(ReplaceColorPanel);
            TopBarPanel.Controls.Add(ImageOptionsPanel);
            TopBarPanel.Controls.Add(ColorPalettePanel);
            TopBarPanel.Controls.Add(DrawingToolButtonPanel);
            TopBarPanel.Location = new Point(3, 3);
            TopBarPanel.Name = "TopBarPanel";
            TopBarPanel.Size = new Size(1110, 160);
            TopBarPanel.TabIndex = 36;
            // 
            // ReplaceColorPanel
            // 
            ReplaceColorPanel.BackColor = Color.Gainsboro;
            ReplaceColorPanel.BorderStyle = BorderStyle.FixedSingle;
            ReplaceColorPanel.Controls.Add(PickColorToReplaceButton);
            ReplaceColorPanel.Controls.Add(ReplacementColorLabel);
            ReplaceColorPanel.Controls.Add(ReplacementColorTable);
            ReplaceColorPanel.Controls.Add(ColorToReplaceLabel);
            ReplaceColorPanel.Controls.Add(ColorToReplaceTable);
            ReplaceColorPanel.Controls.Add(ReplaceColorButton);
            ReplaceColorPanel.Controls.Add(ReplaceColorLabel);
            ReplaceColorPanel.Location = new Point(400, 5);
            ReplaceColorPanel.Name = "ReplaceColorPanel";
            ReplaceColorPanel.Size = new Size(100, 150);
            ReplaceColorPanel.TabIndex = 35;
            // 
            // PickColorToReplaceButton
            // 
            PickColorToReplaceButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            PickColorToReplaceButton.Location = new Point(5, 50);
            PickColorToReplaceButton.Name = "PickColorToReplaceButton";
            PickColorToReplaceButton.Size = new Size(90, 30);
            PickColorToReplaceButton.TabIndex = 44;
            PickColorToReplaceButton.Text = "Pick old color";
            PickColorToReplaceButton.UseVisualStyleBackColor = true;
            PickColorToReplaceButton.Click += PickColorToReplaceButton_Click;
            // 
            // ReplacementColorLabel
            // 
            ReplacementColorLabel.Location = new Point(5, 90);
            ReplacementColorLabel.Name = "ReplacementColorLabel";
            ReplacementColorLabel.Size = new Size(65, 20);
            ReplacementColorLabel.TabIndex = 43;
            ReplacementColorLabel.Text = "New Color";
            ReplacementColorLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // ReplacementColorTable
            // 
            ReplacementColorTable.BackColor = SystemColors.Control;
            ReplacementColorTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            ReplacementColorTable.CellSize = 16;
            ReplacementColorTable.CellVisibleSelection = false;
            ReplacementColorTable.ColumnCount = 1;
            ReplacementColorTable.ColumnStyles.Add(new ColumnStyle());
            ReplacementColorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            ReplacementColorTable.Location = new Point(70, 90);
            ReplacementColorTable.MaximumCellAmount = 1;
            ReplacementColorTable.Name = "ReplacementColorTable";
            ReplacementColorTable.RowCount = 2;
            ReplacementColorTable.RowStyles.Add(new RowStyle());
            ReplacementColorTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            ReplacementColorTable.Size = new Size(20, 20);
            ReplacementColorTable.TabIndex = 42;
            // 
            // ColorToReplaceLabel
            // 
            ColorToReplaceLabel.Location = new Point(5, 25);
            ColorToReplaceLabel.Name = "ColorToReplaceLabel";
            ColorToReplaceLabel.Size = new Size(60, 20);
            ColorToReplaceLabel.TabIndex = 41;
            ColorToReplaceLabel.Text = "Old Color";
            ColorToReplaceLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // ColorToReplaceTable
            // 
            ColorToReplaceTable.BackColor = SystemColors.Control;
            ColorToReplaceTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            ColorToReplaceTable.CellSize = 16;
            ColorToReplaceTable.CellVisibleSelection = false;
            ColorToReplaceTable.ColumnCount = 1;
            ColorToReplaceTable.ColumnStyles.Add(new ColumnStyle());
            ColorToReplaceTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            ColorToReplaceTable.Location = new Point(70, 25);
            ColorToReplaceTable.MaximumCellAmount = 1;
            ColorToReplaceTable.Name = "ColorToReplaceTable";
            ColorToReplaceTable.RowCount = 2;
            ColorToReplaceTable.RowStyles.Add(new RowStyle());
            ColorToReplaceTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            ColorToReplaceTable.Size = new Size(20, 20);
            ColorToReplaceTable.TabIndex = 40;
            // 
            // ReplaceColorButton
            // 
            ReplaceColorButton.Location = new Point(5, 115);
            ReplaceColorButton.Name = "ReplaceColorButton";
            ReplaceColorButton.Size = new Size(90, 30);
            ReplaceColorButton.TabIndex = 39;
            ReplaceColorButton.Text = "Replace Color";
            ReplaceColorButton.UseVisualStyleBackColor = true;
            ReplaceColorButton.Click += ReplaceColorButton_Click;
            // 
            // ReplaceColorLabel
            // 
            ReplaceColorLabel.Dock = DockStyle.Top;
            ReplaceColorLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ReplaceColorLabel.Location = new Point(0, 0);
            ReplaceColorLabel.Name = "ReplaceColorLabel";
            ReplaceColorLabel.Size = new Size(98, 15);
            ReplaceColorLabel.TabIndex = 38;
            ReplaceColorLabel.Text = "Replace Color";
            ReplaceColorLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // ImageOptionsPanel
            // 
            ImageOptionsPanel.BackColor = Color.Gainsboro;
            ImageOptionsPanel.BorderStyle = BorderStyle.FixedSingle;
            ImageOptionsPanel.Controls.Add(RedoButton);
            ImageOptionsPanel.Controls.Add(UndoButton);
            ImageOptionsPanel.Controls.Add(ImageOptionsLabel);
            ImageOptionsPanel.Controls.Add(NewImageButton);
            ImageOptionsPanel.Controls.Add(PasteButton);
            ImageOptionsPanel.Controls.Add(CopyButton);
            ImageOptionsPanel.Controls.Add(SaveImageButton);
            ImageOptionsPanel.Controls.Add(LoadImageButton);
            ImageOptionsPanel.Location = new Point(10, 5);
            ImageOptionsPanel.Name = "ImageOptionsPanel";
            ImageOptionsPanel.Size = new Size(120, 150);
            ImageOptionsPanel.TabIndex = 34;
            // 
            // RedoButton
            // 
            RedoButton.Location = new Point(60, 115);
            RedoButton.Name = "RedoButton";
            RedoButton.Size = new Size(50, 30);
            RedoButton.TabIndex = 34;
            RedoButton.Text = "Redo";
            RedoButton.UseVisualStyleBackColor = true;
            RedoButton.Click += RedoButton_Click;
            // 
            // UndoButton
            // 
            UndoButton.Location = new Point(60, 85);
            UndoButton.Name = "UndoButton";
            UndoButton.Size = new Size(50, 30);
            UndoButton.TabIndex = 33;
            UndoButton.Text = "Undo";
            UndoButton.UseVisualStyleBackColor = true;
            UndoButton.Click += UndoButton_Click;
            // 
            // ImageOptionsLabel
            // 
            ImageOptionsLabel.BackColor = Color.Gainsboro;
            ImageOptionsLabel.Dock = DockStyle.Top;
            ImageOptionsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ImageOptionsLabel.Location = new Point(0, 0);
            ImageOptionsLabel.Name = "ImageOptionsLabel";
            ImageOptionsLabel.Size = new Size(118, 15);
            ImageOptionsLabel.TabIndex = 32;
            ImageOptionsLabel.Text = "Image Options";
            ImageOptionsLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // ColorPalettePanel
            // 
            ColorPalettePanel.BackColor = Color.Gainsboro;
            ColorPalettePanel.BorderStyle = BorderStyle.FixedSingle;
            ColorPalettePanel.Controls.Add(ColorValueLabel);
            ColorPalettePanel.Controls.Add(ColorPaletteOptionsLabel);
            ColorPalettePanel.Controls.Add(PaletteColorTable);
            ColorPalettePanel.Controls.Add(SavePaletteButton);
            ColorPalettePanel.Controls.Add(BackgroundColorTable);
            ColorPalettePanel.Controls.Add(TransparencyCheckBox);
            ColorPalettePanel.Controls.Add(LoadPaletteButton);
            ColorPalettePanel.Controls.Add(BackgroundColorLabel);
            ColorPalettePanel.Controls.Add(GridColorLabel);
            ColorPalettePanel.Controls.Add(GridColorTable);
            ColorPalettePanel.Location = new Point(510, 5);
            ColorPalettePanel.Name = "ColorPalettePanel";
            ColorPalettePanel.Size = new Size(280, 150);
            ColorPalettePanel.TabIndex = 11;
            // 
            // ColorValueLabel
            // 
            ColorValueLabel.AutoSize = true;
            ColorValueLabel.Location = new Point(180, 60);
            ColorValueLabel.Name = "ColorValueLabel";
            ColorValueLabel.Size = new Size(67, 15);
            ColorValueLabel.TabIndex = 38;
            ColorValueLabel.Text = "Color Value";
            // 
            // ColorPaletteOptionsLabel
            // 
            ColorPaletteOptionsLabel.Dock = DockStyle.Top;
            ColorPaletteOptionsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ColorPaletteOptionsLabel.Location = new Point(0, 0);
            ColorPaletteOptionsLabel.Name = "ColorPaletteOptionsLabel";
            ColorPaletteOptionsLabel.Size = new Size(278, 15);
            ColorPaletteOptionsLabel.TabIndex = 37;
            ColorPaletteOptionsLabel.Text = "Color Palette Options";
            ColorPaletteOptionsLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // ViewPixelSizeNumberBar
            // 
            ViewPixelSizeNumberBar.DefaultWidth = 80;
            ViewPixelSizeNumberBar.IncrementAmount = 1;
            ViewPixelSizeNumberBar.Location = new Point(465, 0);
            ViewPixelSizeNumberBar.MaximumValue = 4;
            ViewPixelSizeNumberBar.MinimumValue = 1;
            ViewPixelSizeNumberBar.Name = "ViewPixelSizeNumberBar";
            ViewPixelSizeNumberBar.Size = new Size(80, 30);
            ViewPixelSizeNumberBar.TabIndex = 37;
            ViewPixelSizeNumberBar.Value = 1;
            ViewPixelSizeNumberBar.ValueChanged += ViewPixelSizeNumberBar_ValueChanged;
            // 
            // DrawPixelSizeNumberBar
            // 
            DrawPixelSizeNumberBar.DefaultWidth = 80;
            DrawPixelSizeNumberBar.IncrementAmount = 4;
            DrawPixelSizeNumberBar.Location = new Point(470, 1);
            DrawPixelSizeNumberBar.MaximumValue = 32;
            DrawPixelSizeNumberBar.MinimumValue = 8;
            DrawPixelSizeNumberBar.Name = "DrawPixelSizeNumberBar";
            DrawPixelSizeNumberBar.Size = new Size(77, 30);
            DrawPixelSizeNumberBar.TabIndex = 32;
            DrawPixelSizeNumberBar.Value = 8;
            DrawPixelSizeNumberBar.ValueChanged += DrawPixelSizeNumberBar_ValueChanged;
            // 
            // DrawingDivisionPanel
            // 
            DrawingDivisionPanel.BackColor = Color.Gray;
            DrawingDivisionPanel.Controls.Add(DrawingBackgroundPanel);
            DrawingDivisionPanel.Location = new Point(3, 197);
            DrawingDivisionPanel.Name = "DrawingDivisionPanel";
            DrawingDivisionPanel.Size = new Size(550, 550);
            DrawingDivisionPanel.TabIndex = 37;
            // 
            // ViewingDivisionPanel
            // 
            ViewingDivisionPanel.BackColor = Color.Gray;
            ViewingDivisionPanel.Controls.Add(ViewingBackgroundPanel);
            ViewingDivisionPanel.Location = new Point(563, 197);
            ViewingDivisionPanel.Name = "ViewingDivisionPanel";
            ViewingDivisionPanel.Size = new Size(550, 550);
            ViewingDivisionPanel.TabIndex = 38;
            // 
            // DrawingBoxSizePanel
            // 
            DrawingBoxSizePanel.BackColor = Color.LightGray;
            DrawingBoxSizePanel.Controls.Add(DrawPixelSizeNumberBar);
            DrawingBoxSizePanel.Controls.Add(DrawingBoxSizeButton);
            DrawingBoxSizePanel.Controls.Add(GridTypeLabel);
            DrawingBoxSizePanel.Controls.Add(GridTypeComboBox);
            DrawingBoxSizePanel.Controls.Add(DrawWidthLabel);
            DrawingBoxSizePanel.Controls.Add(DrawPixelSizeNumberBox);
            DrawingBoxSizePanel.Controls.Add(DrawWidthNumberBox);
            DrawingBoxSizePanel.Controls.Add(DrawHeightLabel);
            DrawingBoxSizePanel.Controls.Add(DrawHeightNumberBox);
            DrawingBoxSizePanel.Controls.Add(DrawPixelSizeLabel);
            DrawingBoxSizePanel.Location = new Point(3, 164);
            DrawingBoxSizePanel.Name = "DrawingBoxSizePanel";
            DrawingBoxSizePanel.Size = new Size(550, 32);
            DrawingBoxSizePanel.TabIndex = 39;
            // 
            // ViewingBoxSizePanel
            // 
            ViewingBoxSizePanel.BackColor = Color.LightGray;
            ViewingBoxSizePanel.Controls.Add(ViewPixelSizeNumberBar);
            ViewingBoxSizePanel.Controls.Add(SeelctionSizeLabel);
            ViewingBoxSizePanel.Controls.Add(ViewingBoxSizeButton);
            ViewingBoxSizePanel.Controls.Add(SelectionSizeComboBox);
            ViewingBoxSizePanel.Controls.Add(ViewPixelWidthLabel);
            ViewingBoxSizePanel.Controls.Add(ViewPixelSizeLabel);
            ViewingBoxSizePanel.Controls.Add(ViewWidthNumberBox);
            ViewingBoxSizePanel.Controls.Add(ViewPixelSizeNumberBox);
            ViewingBoxSizePanel.Controls.Add(ViewPixelHeightLabel);
            ViewingBoxSizePanel.Controls.Add(ViewHeightNumberBox);
            ViewingBoxSizePanel.Location = new Point(563, 164);
            ViewingBoxSizePanel.Name = "ViewingBoxSizePanel";
            ViewingBoxSizePanel.Size = new Size(550, 32);
            ViewingBoxSizePanel.TabIndex = 40;
            // 
            // PixelArtEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(1184, 801);
            Controls.Add(ViewingBoxSizePanel);
            Controls.Add(DrawingBoxSizePanel);
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
            DrawingToolButtonPanel.ResumeLayout(false);
            ViewingBackgroundPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ViewingBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)DrawPixelSizeNumberBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)DrawHeightNumberBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)DrawWidthNumberBox).EndInit();
            TopBarPanel.ResumeLayout(false);
            ReplaceColorPanel.ResumeLayout(false);
            ImageOptionsPanel.ResumeLayout(false);
            ColorPalettePanel.ResumeLayout(false);
            ColorPalettePanel.PerformLayout();
            DrawingDivisionPanel.ResumeLayout(false);
            ViewingDivisionPanel.ResumeLayout(false);
            DrawingBoxSizePanel.ResumeLayout(false);
            ViewingBoxSizePanel.ResumeLayout(false);
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
        private Label GridColorLabel;
        private Controls.ColorTable GridColorTable;
        private Controls.ColorTable BackgroundColorTable;
        private Label BackgroundColorLabel;
        private Controls.ColorTable PaletteColorTable;
        private CheckBox TransparencyCheckBox;
        private Button LoadImageButton;
        private Button CopyButton;
        private Button PasteButton;
        private Button LoadPaletteButton;
        private Button SavePaletteButton;
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
        private ComboBox SelectionSizeComboBox;
        private Label SeelctionSizeLabel;
        private Panel TopBarPanel;
        private Panel DrawingDivisionPanel;
        private Panel ViewingDivisionPanel;
        private Panel ColorPalettePanel;
        private Panel ImageOptionsPanel;
        private Label ImageOptionsLabel;
        private Label ColorPaletteOptionsLabel;
        private Label DrawingToolsLabel;
        private Controls.NumberBar DrawPixelSizeNumberBar;
        private Controls.NumberBar ViewPixelSizeNumberBar;
        private Label ColorValueLabel;
        private Button RedoButton;
        private Button UndoButton;
        private Controls.ToolButton DitheringPenButton;
        private Panel DrawingBoxSizePanel;
        private Panel ViewingBoxSizePanel;
        private Panel ReplaceColorPanel;
        private Label ReplaceColorLabel;
        private Label ColorToReplaceLabel;
        private Controls.ColorTable ColorToReplaceTable;
        private Button ReplaceColorButton;
        private Label ReplacementColorLabel;
        private Controls.ColorTable ReplacementColorTable;
        private Button PickColorToReplaceButton;
    }
}