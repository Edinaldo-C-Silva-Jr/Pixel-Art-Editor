using PixelArtEditor.Extension_Methods;

namespace PixelArtEditor.Image_Editing
{
    public static class DrawingCalculations
    {
        /// <summary>
        /// Receives a location and snaps it to the top left of a pixel. This makes sure the changes are always applied to the entire pixel.
        /// </summary>
        /// <param name="absoluteLocation">The actual location of the mouse click.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        /// <returns>The new location that indicates the top left corner of the current pixel.</returns>
        public static Point SnapPixelTopLeft(Point absoluteLocation, int pixelSize)
        {
            int xPos = absoluteLocation.X - absoluteLocation.X.Modulo(pixelSize);
            int yPos = absoluteLocation.Y - absoluteLocation.Y.Modulo(pixelSize);
            return new(xPos, yPos);
        }

        /// <summary>
        /// Receives two coordinates to compare, and returns them ordered so that the first is the smaller and the second is the bigger.
        /// This method is intended to compare a coordinate (either X or Y) from two different points.
        /// </summary>
        /// <param name="firstCoordinate">The coordinate from the first point.</param>
        /// <param name="lastCoordinate">The coordinate from the last point.</param>
        /// <returns>A tuple containing both coordinates ordered so that the first is smaller.</returns>
        public static (int, int) OrderCoordinatesWithSmallerFirst(int firstCoordinate, int lastCoordinate)
        {
            if (firstCoordinate > lastCoordinate)
            {
                return (lastCoordinate, firstCoordinate);
            }
            else
            {
                return (firstCoordinate, lastCoordinate);
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
            initialCoordinate -= initialCoordinate.Modulo(pixelSize); // The starting coordinate is the left or up edge of the pixel.
            finalCoordinate = finalCoordinate - (finalCoordinate.Modulo(pixelSize)) + pixelSize; // The ending coordinate is the left or up edge of the next pixel.

            return Math.Abs((finalCoordinate - initialCoordinate) / pixelSize);
        }
    }
}
