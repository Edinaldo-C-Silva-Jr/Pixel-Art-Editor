using PixelArtEditor.Extension_Methods;

namespace PixelArtEditor.Files
{
    internal class ImageSelection : IDisposable
    {
        #region Properties
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
        public Rectangle SelectedArea { get; private set; }

        public ImageType CurrentImage { get; private set; }
        #endregion

        /// <summary>
        /// Default constructor. Initializes the selection with a light blue color.
        /// </summary>
        public ImageSelection()
        {
            SelectionBrush = new(Color.FromArgb(128, 32, 196, 255));
            CurrentImage = ImageType.None;
        }

        /// <summary>
        /// Defines the start position of the selection.
        /// </summary>
        /// <param name="location">The location of the mouse click, where the selection will start.</param>
        public void DefineStart(Point location, ImageType type)
        {
            SelectionStart = location;
            CurrentImage = type;
        }

        /// <summary>
        /// Clears the current selection, emptying the rectangle area.
        /// </summary>
        public void ClearSelection()
        {
            SelectedArea = Rectangle.Empty;
            CurrentImage = ImageType.None;
        }

        /// <summary>
        /// Receives two coordinates, an initial coordinate and a final one, and compares them.
        /// If the final coordinate is smaller than the initial, they're swapped.
        /// This method accepts a single coordinate, which can be the X or Y coordinate of a Point.
        /// </summary>
        /// <param name="initialCoordinate">The coordinate from the starting point.</param>
        /// <param name="finalCoordinate">The coordinate from the end point.</param>
        /// <returns>A tuple containing both coordinates in the correct order.</returns>
        private static (int, int) SwapCoordinatesWhenStartIsBigger(int initialCoordinate, int finalCoordinate)
        {
            if (initialCoordinate > finalCoordinate)
            {
                return (finalCoordinate, initialCoordinate);
            }
            else
            {
                return (initialCoordinate, finalCoordinate);
            }
        }

        /// <summary>
        /// Changes the selection area based on the initial and final locations, along with an optional size multiplier.
        /// </summary>
        /// <param name="selectionEnd">The current click location, which is where the selecion ends.</param>
        /// <param name="boxSize">The size of the current Image's Box.</param>
        /// <param name="selectionWidth">The width to increase the selection. If no value is given, it defaults to 1 pixel.</param>
        /// <param name="selectionHeight">The height to increase the selection. If no value is given, it defaults to 1 pixel.</param>
        public void ChangeSelectionArea(Point selectionEnd, Size boxSize, int selectionWidth = 1, int selectionHeight = 1)
        {
            Point initialSelecion = SelectionStart;
            Rectangle areaToSelect = new();

            // Makes sure the begin and end coordinates are correctly ordered (the end coordinate has to be bigger than the begin coordinate)
            (initialSelecion.X, selectionEnd.X) = SwapCoordinatesWhenStartIsBigger(initialSelecion.X, selectionEnd.X);
            (initialSelecion.Y, selectionEnd.Y) = SwapCoordinatesWhenStartIsBigger(initialSelecion.Y, selectionEnd.Y);

            // Snaps the initial point of the selection to the top left of the selection unit.
            areaToSelect.X = initialSelecion.X - initialSelecion.X.Modulo(selectionWidth);
            areaToSelect.Y = initialSelecion.Y - initialSelecion.Y.Modulo(selectionHeight);

            // Makes sure the begin coordinates don't go outside the top or left of the box.
            areaToSelect.X.ValidateMinimum(0);
            areaToSelect.Y.ValidateMinimum(0);

            // Snaps the end point of the selection to the bottom right of the selection unit.
            selectionEnd.X = selectionEnd.X - selectionEnd.X.Modulo(selectionWidth) + selectionWidth;
            selectionEnd.Y = selectionEnd.Y - selectionEnd.Y.Modulo(selectionHeight) + selectionHeight;

            // Makes sure the end coordinates don't go outside the bottom or right of the box.
            selectionEnd.X.ValidateMaximum(boxSize.Width - 1);
            selectionEnd.Y.ValidateMaximum(boxSize.Height - 1);

            // Defines the Selection Area size with the coordinates.
            areaToSelect.Width = selectionEnd.X - areaToSelect.X;
            areaToSelect.Height = selectionEnd.Y - areaToSelect.Y;
            SelectedArea = areaToSelect;
        }

        // TODO: Change the drawing method to zoom the rectangle.
        /// <summary>
        /// Draws the selection rectangle in the Image boxes via the Paint Graphics.
        /// </summary>
        /// <param name="paintGraphics">The graphics of the image Box's paint event.</param>
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
