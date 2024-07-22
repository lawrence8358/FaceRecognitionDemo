using System.ComponentModel.DataAnnotations;

namespace FaceRecognitionDemo.Models
{
    public class UserFaceInfoModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public List<UserFaceMetaData> Metadatas { get; set; }
    }

    public class UserFaceMetaData
    {
        /// <summary>
        /// Base64 Image
        /// </summary>
        [Required]
        public string Image { get; set; }

        [Required]
        public List<float> Vectors { get; set; }
    }

    public class DetectedFaceDescriptors
    {
        [Required]
        public float[] Vectors { get; set; }
    }
}
