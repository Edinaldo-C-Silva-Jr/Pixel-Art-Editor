﻿using PixelArtEditor.Extension_Methods;

namespace PixelArtEditor.Image_Editing
{
    /// <summary>
    /// A class with calculation methods used in the drawing methods.
    /// </summary>
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
        /// Gets the ratio between the horizontal and vertical distance of the two points in the line.
        /// The ratio is the smaller distance divided by the bigger, so the result is always between 0 and 1.
        /// </summary>
        /// <param name="smallerDistance">The smaller of the two distances.</param>
        /// <param name="biggerDistance">The bigger of the two distances.</param>
        /// <returns></returns>
        public static decimal GetRatioBetweenDistances(int smallerDistance, int biggerDistance)
        {
            if (smallerDistance == 0 || biggerDistance == 0)
            {
                return 0;
            }

            return Decimal.Divide(smallerDistance + 1, biggerDistance + 1);
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
