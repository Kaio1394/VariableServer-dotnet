using System.Text.Json;

namespace VariableServer.Helper
{
    public class Helper
    {
        private static readonly string _filePath = "C:\\temp\\data.json";
        public static Dictionary<string, object>? _dictVariables;

        /// <summary>
        /// Save dictionary in a json file.
        /// </summary>
        /// /// <param name="_dictVariables">The key whose value needs to be retrieved.</param>
        /// <returns></returns>
        public static void SaveDictionaryToFile(Dictionary<string, object> _dictVariables)
        {
            var json = JsonSerializer.Serialize(_dictVariables);
            System.IO.File.WriteAllText(_filePath, json);
        }

        /// <summary>
        /// Load file and parse to a dictionary
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> LoadDictionaryFromFile()
        {
            if (System.IO.File.Exists(_filePath))
            {
                if (!File.Exists(_filePath)) return new Dictionary<string, object>();
                var json = System.IO.File.ReadAllText(_filePath);
                _dictVariables = (JsonSerializer.Deserialize<Dictionary<string, object>>(json) == null? 
                    new Dictionary<string, object>()
                    : JsonSerializer.Deserialize<Dictionary<string, object>>(json));
                return _dictVariables;
            }
            else
            {
                return new Dictionary<string, object>();
            }           
        }
    }
}
