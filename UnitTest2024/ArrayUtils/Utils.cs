namespace ArrayUtils
{
    public static class Utils
    {
        public static MaxMinResult ArgMaxMin(double[] arr)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            if(arr.Length == 0) throw new ArgumentException(nameof(arr));

            int minIndex = 0, maxIndex = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < arr[minIndex])
                {
                    minIndex = i;
                }

                if (arr[i] > arr[maxIndex])
                {
                    maxIndex = i;
                }
            }

            return new MaxMinResult(minIndex, maxIndex);
        }
    }

    public record MaxMinResult(int Min, int Max);
}