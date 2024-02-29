﻿using PixelArtEditor.Drawing_Tools;
using PixelArtEditor.Grids;

namespace PixelArtEditor.Controls
{
    public partial class DrawingBox : PictureBox
    {
        private Bitmap imageWithGrid = new Bitmap(1, 1);

        public DrawingBox()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        public void SetNewSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public void SetNewImage(IGridGenerator gridGenerator, Bitmap originalImage, Color backgroundColor)
        {
            imageWithGrid = new(originalImage);
            gridGenerator.ApplyGridFullImage(imageWithGrid, backgroundColor);
            this.Image = imageWithGrid;
            this.Refresh();
        }

        /// <summary>
        /// Applies a new grid type to the existing image. This method doesn't override the actual image when applying the grid.
        /// </summary>
        /// <param name="gridApply">The IGridGenerator implementation used to apply the grid.</param>
        /// <param name="originalImage">The original image to use when applying the grid.</param>
        public void ApplyNewGrid(IGridGenerator gridApply, Bitmap originalImage, Color backgroundColor)
        {
            imageWithGrid = new(originalImage);

            gridApply.ApplyGridFullImage(imageWithGrid, backgroundColor);
            this.Image = imageWithGrid;
            this.Refresh();
        }

        public void DrawPixelByPosition(IDrawingTool tool, IGridGenerator gridGenerator, Bitmap image, int xPosPixel, int yPosPixel, int pixelSize, Color pixelColor)
        {
            int xPos = pixelSize * xPosPixel;
            int yPos = pixelSize * yPosPixel;

            DrawPixel(tool, gridGenerator, image, xPos, yPos, pixelSize, pixelColor);
        }

        public void DrawPixelByClick(IDrawingTool tool, IGridGenerator gridGenerator, Bitmap image, int xPosMouse, int yPosMouse, int pixelSize, Color pixelColor)
        {
            int xPos = xPosMouse - xPosMouse % pixelSize;
            int yPos = yPosMouse - yPosMouse % pixelSize;

            DrawPixel(tool, gridGenerator, image, xPos, yPos, pixelSize, pixelColor);
        }

        private void DrawPixel(IDrawingTool tool, IGridGenerator gridGenerator, Bitmap image, int xPos, int yPos, int pixelSize, Color pixelColor)
        {
            using Graphics pixelDraw = Graphics.FromImage(image);
            using Graphics gridDraw = Graphics.FromImage(imageWithGrid);
            using Brush pixelBrush = new SolidBrush(pixelColor);

            tool.UseTool(pixelDraw, pixelBrush, xPos, yPos, pixelSize);
            tool.UseTool(gridDraw, pixelBrush, xPos, yPos, pixelSize);

            if (!gridGenerator.BackgroundGrid)
            {
                gridGenerator.ApplyGridSinglePixel(imageWithGrid, xPos, yPos);
            }

            this.Image = imageWithGrid;
        }
    }
}
