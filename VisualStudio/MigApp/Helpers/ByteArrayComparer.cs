namespace MigApp.Helpers
{
    public class ByteArrayComparer : IComparer<byte[]>
    {
        /// <summary>
        /// Сравнивает два массива байтов по длине и элементам.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>0 если массивы равны, 1 если x больше y, -1 если x меньше y </returns>
        public int Compare(byte[]? x, byte[]? y)
        {
            if (x == null || y == null)
            {
                if (x == null && y == null)
                {
                    return 0; // Обра null, значит равны
                }
                return x == null ? -1 : 1; // null меньше любого значения
            }

            if (x.Length != y.Length)
            {
                return x.Length - y.Length; // Сравнение по длине
            }

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return x[i] - y[i]; // Сравнение по элементам
                }
            }

            return 0; // Равны
        }
    }
}
