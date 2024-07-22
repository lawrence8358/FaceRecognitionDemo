using FaceRecognitionDemo.Models;

namespace FaceRecognitionDemo.Utilities
{
    public class FaceMatcher
    {
        #region Members

        public List<LabeledFaceDescriptors> LabeledDescriptors { get; set; }

        public double DistanceThreshold { get; set; }

        #endregion

        #region Constructor

        public FaceMatcher(List<LabeledFaceDescriptors> labeledDescriptors, double distanceThreshold = 0.6)
        {
            LabeledDescriptors = labeledDescriptors;
            DistanceThreshold = distanceThreshold;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 找出平均最佳的特徵向量對應的 Lable
        /// </summary>
        public FaceMatch FindBestMatch(float[] queryDescriptor)
        {
            FaceMatch bestMatch = MatchDescriptor(queryDescriptor);

            return bestMatch?.Distance < DistanceThreshold
                ? bestMatch
                : new FaceMatch("unknown", bestMatch?.Distance ?? -1);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 比對特徵向量，找出最佳匹配
        /// </summary>
        private FaceMatch MatchDescriptor(float[] queryDescriptor)
        {
            return LabeledDescriptors
                .Select(ld => new FaceMatch(ld.Label, ComputeMeanDistance(queryDescriptor, ld.Descriptors)))
                .Aggregate((best, curr) => best.Distance < curr.Distance ? best : curr);

            //FaceMatch bestMatch = null;

            //foreach (var labeledDescriptor in LabeledDescriptors)
            //{
            //    double meanDistance = ComputeMeanDistance(queryDescriptor, labeledDescriptor.Descriptors);
            //    var currentMatch = new FaceMatch(labeledDescriptor.Label, meanDistance);

            //    if (bestMatch == null || currentMatch.Distance < bestMatch.Distance)
            //    {
            //        bestMatch = currentMatch;
            //    }
            //}

            //return bestMatch;
        }

        /// <summary>
        /// 計算每個人所有特徵的平均距離
        /// </summary> 
        private double ComputeMeanDistance(float[] queryDescriptor, List<float[]> descriptors)
        {
            //// 最短距離計算
            //return descriptors
            //    .Select(d => MathUtils.EuclideanDistance(d, queryDescriptor))
            //    .Min();

            return descriptors
               .Select(d => MathUtils.EuclideanDistance(d, queryDescriptor))
               .Average();

            //double totalDistance = 0.0;

            //foreach (var descriptor in descriptors)
            //{
            //    totalDistance += EuclideanDistance(descriptor, queryDescriptor);
            //}

            //return totalDistance / (descriptors.Count == 0 ? 1 : descriptors.Count);
        }

        #endregion

    }
}
