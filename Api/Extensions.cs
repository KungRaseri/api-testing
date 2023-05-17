
namespace Api;

public static class Extensions
{
    /// <summary>
    /// Splits an array into several smaller arrays.
    /// </summary>
    /// <typeparam name="T">The type of the array.</typeparam>
    /// <param name="array">The array to split.</param>
    /// <param name="size">The size of the smaller arrays.</param>
    /// <returns>An array containing smaller arrays.</returns>
    public static IEnumerable<IEnumerable<T>> Split<T>(this T[] array, int size)
    {
        for (var i = 0; i < (float)array.Length / size; i++)
        {
            yield return array.Skip(i * size).Take(size);
        }
    }

    /// <summary>
    /// Splits a given array into a two dimensional arrays of a given size.
    /// The given size must be a divisor of the initial array, otherwise the returned value is <c>null</c>,
    /// because not all the values will fit into the resulting array.
    /// </summary>
    /// <param name="array">The array to split.</param>
    /// <param name="size">The size to split the array into. The size must be a divisor of the length of the array.</param>
    /// <returns>
    /// A two dimensional array if the size is a divisor of the length of the initial array, otherwise <c>null</c>.
    /// </returns>
    public static T[,]? ToSquare2D<T>(this T[] array, int size)
    {
        if (array.Length % size != 0) return null;

        var firstDimensionLength = array.Length / size;
        var buffer = new T[firstDimensionLength, size];

        for (var i = 0; i < firstDimensionLength; i++)
        {
            for (var j = 0; j < size; j++)
            {
                buffer[i, j] = array[i * size + j];
            }
        }

        return buffer;
    }
}