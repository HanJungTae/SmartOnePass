using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CST.Utill
{
    class JsonSerializer
    {
        private static JsonSerializer _instance;

        public static JsonSerializer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new JsonSerializer();

                return _instance;
            }
        }

        public string Serialize<T>(T file)
        {
            return JsonConvert.SerializeObject(file);
        }

        public void SerializeAndSave<T>(T file, string fileName)
        {
            string json = Serialize<T>(file);

            if (json == null)
                return;

            string path = Application.StartupPath + "/" + fileName;

            if( !Directory.Exists(path) )
                Directory.CreateDirectory(path);

            StreamWriter streamWriter = new StreamWriter(path + ".json");

            streamWriter.Write(json);

            streamWriter.Close();
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public T LoadAndDeserialize<T>(string Path, string fileName)
        {
            StreamReader streamReader = new StreamReader(Path + "/" +fileName + ".json");

            if (streamReader == null)
                return default(T);

            string json = streamReader.ReadToEnd();

            streamReader.Close();

            return Deserialize<T>(json);
        }
    }
}
