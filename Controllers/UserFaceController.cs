using FaceRecognitionDemo.Models;
using FaceRecognitionDemo.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FaceRecognitionDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFaceController : ControllerBase
    {
        #region Member 

        private readonly DataSource _dataSouce;

        #endregion

        #region Constructor

        public UserFaceController()
        {
            _dataSouce = new DataSource();
        }

        #endregion

        #region Public Methods

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello Face Recognition Demo");
        }

        [HttpPost("Add")]
        public IActionResult AddUserMetadata([FromBody] UserFaceInfoModel data)
        {
            _dataSouce.AddUserMetadata(data);

            return Ok();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllUserVectors()
        {
            var userVectors = _dataSouce.GetAllUserVectors();

            return Ok(userVectors);
        }

        [HttpPost("Match")]
        public IActionResult MatchUser([FromBody] DetectedFaceDescriptors queryDescriptor)
        {
            var userVectors = _dataSouce.GetAllUserVectors();

            var matcher = new FaceMatcher(userVectors, 0.6);

            FaceMatch bestMatch = matcher.FindBestMatch(queryDescriptor.Vectors);
            Console.WriteLine($"Best Match: {bestMatch.Label} with Distance: {bestMatch.Distance}");

            return Ok(bestMatch);
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            var labeledDescriptors = new List<LabeledFaceDescriptors>
            {
                new LabeledFaceDescriptors("Person1", new List<float[]>
                {
                    new float[] { 0.1f, 0.2f, 0.3f }, // 示例數據
                    new float[] { 0.2f, 0.3f, 0.4f }  // 示例數據
                }),
                new LabeledFaceDescriptors("Person2", new List<float[]>
                {
                    new float[] { 0.5f, 0.6f, 0.7f }, // 示例數據
                    new float[] { 0.6f, 0.7f, 0.8f }  // 示例數據
                })
            };

            var matcher = new FaceMatcher(labeledDescriptors, 0.6);
            float[] queryDescriptor = new float[] { 0.1f, 0.2f, 0.3f }; // 示例數據

            FaceMatch bestMatch = matcher.FindBestMatch(queryDescriptor); 
            return Ok(bestMatch);
        }

        #endregion 
    }
}
