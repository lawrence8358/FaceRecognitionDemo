using FaceRecognitionDemo.Models;
using Newtonsoft.Json;

namespace FaceRecognitionDemo.Utilities
{
    public class DataSource
    {
        #region Members

        private readonly string UserFaceJsonFolder = Path.Combine("AppData/UserFace/_JsonData");
        private readonly string UserFaceImageFolder = Path.Combine("AppData/UserFace");

        #endregion

        #region Public Methods

        /// <summary>
        /// 新增使用者資料
        /// </summary> 
        public void AddUserMetadata(UserFaceInfoModel data)
        {
            // 如果 JSON 向量 資料夾不存在，則建立資料夾
            if (!Directory.Exists(UserFaceJsonFolder))
                Directory.CreateDirectory(UserFaceJsonFolder);

            var userImageFolder = Path.Combine(UserFaceImageFolder, data.Name);

            // 如果 使用者圖片 資料夾存在，刪除舊有的圖片
            if (Directory.Exists(userImageFolder))
                Directory.Delete(userImageFolder, true);

            Directory.CreateDirectory(userImageFolder);

            // 儲存圖片和向量資料
            for (int i = 0; i < data.Metadatas.Count; i++)
            {
                var metaData = data.Metadatas[i];

                // 將 Base64 字串轉換成圖片並儲存
                byte[] imageBytes = Convert.FromBase64String(metaData.Image);
                string imagePath = Path.Combine(userImageFolder, $"{data.Name}{i + 1}.png");
                File.WriteAllBytes(imagePath, imageBytes);
            }

            // 儲存向量到 JSON 文件
            string jsonFilePath = Path.Combine(UserFaceJsonFolder, $"{data.Name}.json");

            var vectors = data.Metadatas.Select(x => x.Vectors).ToList();
            SaveVectorsToJson(jsonFilePath, data.Name, vectors);
        }

        /// <summary>
        /// 取得所有使用者向量資料
        /// </summary> 
        public List<LabeledFaceDescriptors> GetAllUserVectors()
        {
            List<LabeledFaceDescriptors> userVectors = new List<LabeledFaceDescriptors>();

            if (!Directory.Exists(UserFaceJsonFolder))
                return userVectors;

            string[] jsonFiles = Directory.GetFiles(UserFaceJsonFolder, "*.json");

            foreach (var jsonFile in jsonFiles)
            {
                string json = File.ReadAllText(jsonFile);
                var userVector = JsonConvert.DeserializeObject<LabeledFaceDescriptors>(json);
                userVectors.Add(userVector!);
            }

            return userVectors;
        }

        #endregion

        #region Private Methods

        private void SaveVectorsToJson(string filePath, string name, List<List<float>> vectors)
        {
            Dictionary<string, object> vectorData = new Dictionary<string, object>
            {
                { nameof(LabeledFaceDescriptors.Label), name },
                { nameof(LabeledFaceDescriptors.Descriptors), vectors }
            };

            string json = JsonConvert.SerializeObject(vectorData);
            File.WriteAllText(filePath, json);
        }

        #endregion
    }
}
