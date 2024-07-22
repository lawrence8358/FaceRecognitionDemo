namespace FaceRecognitionDemo.Models
{
    public class FaceMatch
    {
        public string Label { get; set; }

        public double Distance { get; set; }

        public FaceMatch(string label, double distance)
        {
            Label = label;
            Distance = distance;
        }
    }
}
