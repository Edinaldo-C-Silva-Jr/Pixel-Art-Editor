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

        public ImageType CurrentImage { get; set; }
        #endregion

        /// <summary>
        /// Default constructor. Initializes the selection with a light blue color.
        /// </summary>
        public ImageSelection()
        {
            SelectionBrush = new(Color.FromArgb(128, 32, 196, 255));
        }

        /// <summary>
        /// Defines the start position of the selection.
        /// </summary>
        /// <param name="location">The location of the mouse click, where the selection will start.</param>
        public void DefineStart(Point location)
        {
            SelectionStart = location;
        }

        /// <summary>
        /// Clears the current selection, emptying the rectangle area.
        /// </summary>
        public void ClearSelection()
        {
            SelectedArea = Rectangle.Empty;
        }

        /// <summary>
        /// Ensures the value passed isn't higher than the maximum allowed value. If it is, reduces it to the maximum value.
        /// </summary>
        /// <param name="value">The value to compare to the maximum.</param>
        /// <param name="maximumValue">The maximum allowed value.</param>
        /// <returns>The value itself, if it's lower than the maximum. Otherwise returns the maximum value.</returns>
        private static int KeepValueBelowMaximum(int value, int maximumValue)
        {
            if (value > maximumValue)
            {
                value = maximumValue;
            }
            return value;
        }

        /// <summary>
        /// Ensures the value passed isn't lower than the minimum allowed value. If it is, increases it to the minimum value.
        /// </summary>
        /// <param name="value">The value to compare to the minimum.</param>
        /// <param name="minimumValue">The minimum allowed value.</param>
        /// <returns>The value itself, if it's higher than the minimum. Otherwise returns the minimum value.</returns>
        private static int KeepValueAboveMinimum(int value, int minimumValue)
        {
            if (value < minimumValue)
            {
                value = minimumValue;
            }
            return value;
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
        /// Changes the selection area based on the initial and final locations.
        /// Utilizes the image's pixel size along with a multiplier to define the base size of the selection.
        /// </summary>
        /// <param name="selectionEnd">The current click location, which is where the selecion ends.</param>
        /// <param name="boxSize">The size of the current Image's Box.</param>
        /// <param name="pixelSize">The size of an individual pixel in the image being currently selected.</param>
        /// <param name="selectionMultiplier">The multiplier to use along with the pixel size to define the selection.</param>
        public void ChangeSelectionArea(Point selectionEnd, Size boxSize, int pixelSize, Size selectionMultiplier)
        {
            // Defines the selection size to use for the width and height based on the pixel size and the multiplier.
            int xSelectionSize = pixelSize * selectionMultiplier.Width;
            int ySelectionSize = pixelSize * selectionMultiplier.Height;
            ChangeSelectArea(selectionEnd, boxSize, xSelectionSize, ySelectionSize);
        }

        /// <summary>
        /// Changes the selection area based on the initial and final locations.
        /// Utilizes the image's pixel size as the base size for the selection.
        /// </summary>
        /// <param name="selectionEnd">The current click location, which is where the selecion ends.</param>
        /// <param name="boxSize">The size of the current Image's Box.</param>
        /// <param name="pixelSize">The size of an individual pixel in the image being currently selected.</param>
        public void ChangeSelectionArea(Point selectionEnd, Size boxSize, int pixelSize)
        {
            ChangeSelectArea(selectionEnd, boxSize, pixelSize, pixelSize);
        }

        /// <summary>
        /// Changes the selection area based on the initial and final locations.
        /// Utilizes a separate value for width and height to allow for rectangular selection areas.
        /// </summary>
        /// <param name="selectionEnd">The current click location, which is where the selecion ends.</param>
        /// <param name="boxSize">The size of the current Image's Box.</param>
        /// <param name="selectionWidth">The width to use for each increment of the selection.</param>
        /// <param name="selectionHeight">The height to use for each increment of the selection.</param>
        private void ChangeSelectArea(Point selectionEnd, Size boxSize, int selectionWidth, int selectionHeight)
        {
            Point beginSelecion = SelectionStart;
            Rectangle areaToSelect = new();

            // Makes sure the begin and end coordinates are correctly ordered (the end coordinate has to be bigger than the begin coordinate)
            (beginSelecion.X, selectionEnd.X) = SwapCoordinatesWhenStartIsBigger(beginSelecion.X, selectionEnd.X);
            (beginSelecion.Y, selectionEnd.Y) = SwapCoordinatesWhenStartIsBigger(beginSelecion.Y, selectionEnd.Y);

            // Snaps the begin point to the top left of its equivalent selection square.
            areaToSelect.X = beginSelecion.X - beginSelecion.X % selectionWidth;
            areaToSelect.Y = beginSelecion.Y - beginSelecion.Y % selectionHeight;

            // Makes sure the begin coordinates don't go outside the top or left of the box.
            areaToSelect.X = KeepValueAboveMinimum(areaToSelect.X, 0);
            areaToSelect.Y = KeepValueAboveMinimum(areaToSelect.Y, 0);

            // Snaps the end point to the bottom right of its equivalent selection square.
            selectionEnd.X = selectionEnd.X - selectionEnd.X % selectionWidth + selectionWidth;
            selectionEnd.Y = selectionEnd.Y - selectionEnd.Y % selectionHeight + selectionHeight;

            // Makes sure the end coordinates don't go outside the bottom or right of the box.
            selectionEnd.X = KeepValueBelowMaximum(selectionEnd.X, boxSize.Width - 1);
            selectionEnd.Y = KeepValueBelowMaximum(selectionEnd.Y, boxSize.Height - 1);

            // Defines the Selection Area size with the coordinates.
            areaToSelect.Width = selectionEnd.X - areaToSelect.X;
            areaToSelect.Height = selectionEnd.Y - areaToSelect.Y;
            SelectedArea = areaToSelect;
        }

        /// <summary>
        /// Draws the selection rectangle in the ViewingBox via the Paint Graphics.
        /// </summary>
        /// <param name="paintGraphics">The graphics of the Viewing Box's paint event.</param>
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
