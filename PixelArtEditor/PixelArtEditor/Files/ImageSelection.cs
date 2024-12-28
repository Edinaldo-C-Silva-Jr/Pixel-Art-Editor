using PixelArtEditor.Extension_Methods;
using PixelArtEditor.Image_Editing;

namespace PixelArtEditor.Files
{
    /// <summary>
    /// A class that handles selecting a portion of the image.
    /// </summary>
    public class ImageSelection : IDisposable
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

        /// <summary>
        /// The type of image currently being used for the selection.
        /// </summary>
        public ImageType CurrentImageType { get; private set; }
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
        /// <param name="location">The point to start the selection in the image (No zoom).</param>
        /// <param name="type">The type of the image that will be selected.</param>
        public void DefineStart(Point location, ImageType type)
        {
            SelectionStart = location;
            CurrentImageType = type;
        }

        /// <summary>
        /// Clears the current selection if the calling box's image type matches the current selection's image type.
        /// </summary>
        /// <param name="callingBoxImageType">The image type of the calling image box.</param>
        public void ClearSelection(ImageType callingBoxImageType)
        {
            if (CurrentImageType == callingBoxImageType)
            {
                SelectedArea = Rectangle.Empty;
            }
        }

        /// <summary>
        /// Changes the selection area based on the initial and final locations, along with an optional size multiplier.
        /// </summary>
        /// <param name="selectionEnd">The current click location, which is where the selecion ends.</param>
        /// <param name="imageSize">The size of the current Image.</param>
        /// <param name="selectionWidth">The width to increase the selection. If no value is given, it defaults to 1 pixel.</param>
        /// <param name="selectionHeight">The height to increase the selection. If no value is given, it defaults to 1 pixel.</param>
        public void ChangeSelectionArea(Point selectionEnd, Size imageSize, int selectionWidth = 1, int selectionHeight = 1)
        {
            Point initialSelecion = SelectionStart;
            Rectangle areaToSelect = new();

            // Makes sure the initial and end coordinates are correctly ordered (the end coordinate has to be bigger than the initial coordinate)
            (initialSelecion.X, selectionEnd.X) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(initialSelecion.X, selectionEnd.X);
            (initialSelecion.Y, selectionEnd.Y) = DrawingCalculations.OrderCoordinatesWithSmallerFirst(initialSelecion.Y, selectionEnd.Y);

            // Snaps the initial point of the selection to the top left of the selection unit
            // The selection unit is the block made up by the width and height multipliers.
            areaToSelect.X = initialSelecion.X - initialSelecion.X.Modulo(selectionWidth);
            areaToSelect.Y = initialSelecion.Y - initialSelecion.Y.Modulo(selectionHeight);

            // Makes sure the begin coordinates don't go outside the top or left of the box.
            areaToSelect.X = areaToSelect.X.ValidateMinimum(0);
            areaToSelect.Y = areaToSelect.Y.ValidateMinimum(0);

            // Snaps the end point of the selection to the bottom right of the selection unit.
            selectionEnd.X = selectionEnd.X - selectionEnd.X.Modulo(selectionWidth) + selectionWidth;
            selectionEnd.Y = selectionEnd.Y - selectionEnd.Y.Modulo(selectionHeight) + selectionHeight;

            // Makes sure the end coordinates don't go outside the bottom or right of the box.
            selectionEnd.X = selectionEnd.X.ValidateMaximum(imageSize.Width);
            selectionEnd.Y = selectionEnd.Y.ValidateMaximum(imageSize.Height);

            // Defines the Selection Area size with the coordinates.
            areaToSelect.Width = selectionEnd.X - areaToSelect.X;
            areaToSelect.Height = selectionEnd.Y - areaToSelect.Y;
            SelectedArea = areaToSelect;
        }

        /// <summary>
        /// Draws the selection rectangle in the Image boxes via the Paint Graphics.
        /// Will only draw the rectangle if the calling box's image type matches the current selection's image type.
        /// </summary>
        /// <param name="paintGraphics">The graphics of the image Box's paint event.</param>
        /// <param name="callingBoxImageType">The image type of the calling image box.</param>
        /// <param name="zoom">The amount of zoom to apply to the rectangle before drawing it.</param>
        public void DrawSelection(Graphics paintGraphics, ImageType callingBoxImageType, int zoom)
        {
            if (SelectedArea != Rectangle.Empty && CurrentImageType == callingBoxImageType)
            {
                Rectangle zoomedSelectionArea = new(SelectedArea.Left * zoom, SelectedArea.Top * zoom, SelectedArea.Width * zoom, SelectedArea.Height * zoom);

                paintGraphics.FillRectangle(SelectionBrush, zoomedSelectionArea);
            }
        }

        /// <summary>
        /// Disposes of unmanaged resources in the class.
        /// </summary>
        public void Dispose()
        {
            SelectionBrush?.Dispose();
        }
    }
}
