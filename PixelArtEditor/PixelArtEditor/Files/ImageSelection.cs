namespace PixelArtEditor.Files
{
    internal class ImageSelection : IDisposable
    {
        /// <summary>
        /// The brush used to draw the selection box in the image when making a selection.
        /// </summary>
        private SolidBrush SelectionBrush { get; }

        /// <summary>
        /// A point that holds the position where the current selection started.
        /// </summary>
        private Point SelectionStart { get; set; }

        /// <summary>
        /// The rectangle currently used as the selection area in the image.
        /// </summary>
        public Rectangle SelectedArea;

        public ImageSelection()
        {
            SelectionBrush = new(Color.FromArgb(128, 32, 196, 255));
        }

        public void DefineStart(Point location)
        {
            SelectionStart = location;
        }

        public void ClearSelection()
        {
            SelectedArea = Rectangle.Empty;
        }

        public void ChangeSelectionArea(Point selectionEnd, int boxWidth, int boxHeight, int zoom)
        {
            // Makes sure the selection can't go outside the boundaries of the drawing box.
            if (selectionEnd.X >= boxWidth)
            {
                selectionEnd.X = boxWidth - 1;
            }
            if (selectionEnd.Y >= boxHeight)
            {
                selectionEnd.Y = boxHeight - 1;
            }
            if (selectionEnd.X < 0)
            {
                selectionEnd.X = 0;
            }
            if (selectionEnd.Y < 0)
            {
                selectionEnd.Y = 0;
            }

            // Defines the selection
            if (selectionEnd.X < SelectionStart.X)
            {
                SelectedArea.X = selectionEnd.X - selectionEnd.X % zoom;
                selectionEnd.X = SelectionStart.X - SelectionStart.X % zoom + zoom;
            }
            else
            {
                SelectedArea.X = SelectionStart.X - SelectionStart.X % zoom;
                selectionEnd.X = selectionEnd.X - selectionEnd.X % zoom + zoom;
            }

            SelectedArea.Y = SelectionStart.Y;
            if (selectionEnd.Y < SelectionStart.Y)
            {
                SelectedArea.Y = selectionEnd.Y - selectionEnd.Y % zoom;
                selectionEnd.Y = SelectionStart.Y - SelectionStart.Y % zoom + zoom;
            }
            else
            {
                SelectedArea.Y = SelectionStart.Y - SelectionStart.Y % zoom;
                selectionEnd.Y = selectionEnd.Y - selectionEnd.Y % zoom + zoom;
            }

            SelectedArea.Width = selectionEnd.X - SelectedArea.X;
            SelectedArea.Height = selectionEnd.Y - SelectedArea.Y;
        }

        public void DrawSelection(Graphics paintGraphics)
        {
            if (SelectedArea != Rectangle.Empty)
            {
                paintGraphics.FillRectangle(SelectionBrush, SelectedArea);
            }
        }

        public void Dispose()
        {
            SelectionBrush?.Dispose();
        }
    }
}
