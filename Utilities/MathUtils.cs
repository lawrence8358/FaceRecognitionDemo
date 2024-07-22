namespace FaceRecognitionDemo.Utilities
{
    public static class MathUtils
    {
        /// <summary>
        /// 計算查詢描述符和標記描述符之間的平均距離 (歐式距離)
        /// </summary>
        public static double EuclideanDistance(float[] descriptor1, float[] descriptor2)
        {
            if (descriptor1.Length != descriptor2.Length)
                throw new ArgumentException("EuclideanDistance: arr1.Length != arr2.Length");

            return Math.Sqrt(descriptor1
                .Select((val, i) => val - descriptor2[i])
                .Sum(diff => Math.Pow(diff, 2)));
        }
         
        //private double EuclideanDistance(float[] descriptor1, float[] descriptor2)
        //{
        //    if (descriptor1.Length != descriptor2.Length)
        //    {
        //        throw new ArgumentException("Descriptors must be of the same length");
        //    }

        //    double sum = 0.0;
        //    for (int i = 0; i < descriptor1.Length; i++)
        //    {
        //        double diff = descriptor1[i] - descriptor2[i];
        //        sum += diff * diff;
        //    }

        //    return Math.Sqrt(sum);
        //}
    }
}
