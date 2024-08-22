namespace PixelArtEditor.Image_Editing
{
    public static class DrawingCalculations
    {
        /// <summary>
        /// Custom modulo function that correctly handles negative values, returning a positive remainder.
        /// </summary>
        /// <param name="value">The value that will have its remainder returned.</param>
        /// <param name="modulo">The number to divide the value and get the remainder.</param>
        /// <returns>The remainder of the division between a value and a number.</returns>
        public static int Modulo(int value, int modulo)
        {
            return (value % modulo + modulo) % modulo;
        }

        /// <summary>
        /// Receives a location and snaps it to the top left of a pixel. This makes sure the changes are always applied to the entire pixel.
        /// </summary>
        /// <param name="absoluteLocation">The actual location of the mouse click.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        /// <returns>The new location that indicates the top left corner of the current pixel.</returns>
        public static Point SnapPixelTopLeft(Point absoluteLocation, int pixelSize)
        {
            int xPos = absoluteLocation.X - Modulo(absoluteLocation.X, pixelSize);
            int yPos = absoluteLocation.Y - Modulo(absoluteLocation.Y, pixelSize);
            return new(xPos, yPos);
        }

        /// <summary>
        /// Receives two coordinates, an initial coordinate and a final one, and compares them.
        /// If the final coordinate is smaller than the initial, they're swapped.
        /// This method accepts a single coordinate, which can be the X or Y coordinate of a Point.
        /// </summary>
        /// <param name="initialCoordinate">The coordinate from the starting point.</param>
        /// <param name="finalCoordinate">The coordinate from the end point.</param>
        /// <returns>A tuple containing both coordinates in the correct order.</returns>
        public static (int, int) SwapCoordinatesWhenStartIsBigger(int initialCoordinate, int finalCoordinate)
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
        /// Calculates and returns the distance between two points in pixel sizes.
        /// This method accepts a single coordinate, which can be the X or Y coordinate of a Point.
        /// </summary>
        /// <param name="initialCoordinate">The coordinate of the first mouse click.</param>
        /// <param name="finalCoordinate">The coordinate of the current mouse position.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        /// <returns>The distance between the two coordinates, expressed in pixel sizes.</returns>
        public static int GetDistanceInPixelSizes(int initialCoordinate, int finalCoordinate, int pixelSize)
        {
            initialCoordinate -= Modulo(initialCoordinate, pixelSize); // The starting coordinate is the left or up edge of the pixel.
            finalCoordinate = finalCoordinate - (Modulo(finalCoordinate, pixelSize)) + pixelSize; // The ending coordinate is the left or up edge of the next pixel.

            return Math.Abs((finalCoordinate - initialCoordinate) / pixelSize);
        }
    }
}
