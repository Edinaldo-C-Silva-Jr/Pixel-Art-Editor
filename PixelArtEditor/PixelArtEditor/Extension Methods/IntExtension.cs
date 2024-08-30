namespace PixelArtEditor.Extension_Methods
{
    /// <summary>
    /// A class that implements extensions methods for the type Int.
    /// </summary>
    public static class IntExtension
    {
        /// <summary>
        /// Custom modulo function that correctly handles negative values, returning a positive remainder.
        /// </summary>
        /// <param name="value">The value that will have its remainder returned.</param>
        /// <param name="modulo">The number to divide the value and get the remainder.</param>
        /// <returns>The remainder of the division between a value and a number.</returns>
        public static int Modulo(this int value, int modulo)
        {
            return (value % modulo + modulo) % modulo;
        }

        /// <summary>
        /// Validates whether a value is higher than a specified maximum value.
        /// If it is, the maximum value is returned.
        /// </summary>
        /// <param name="value">The value to verify.</param>
        /// <param name="maximum">The maximum value allowed.</param>
        /// <returns>The value itself if it's within the allowed maximum, otherwise returns the maximum.</returns>
        public static int ValidateMaximum(this int value, int maximum)
        {
            if (value > maximum)
            {
                return maximum;
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Validates whether a value is lower than a specified minimum value.
        /// If it is, the minimum value is returned.
        /// </summary>
        /// <param name="value">The value to verify.</param>
        /// <param name="minimum">The minimum value allowed.</param>
        /// <returns>The value itself if it's within the allowed minimum, otherwise returns the minimum.</returns>
        public static int ValidateMinimum(this int value, int minimum)
        {
            if (value < minimum)
            {
                return minimum;
            }
            else
            {
                return value;
            }
        }
    }
}
