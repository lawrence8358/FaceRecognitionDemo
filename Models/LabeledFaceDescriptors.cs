namespace FaceRecognitionDemo.Models
{
    public class LabeledFaceDescriptors
    {
        public string Label { get; set; }
        public List<float[]> Descriptors { get; set; }

        public LabeledFaceDescriptors(string label, List<float[]> descriptors)
        {
            Label = label;
            Descriptors = descriptors;
        }
    }
}
