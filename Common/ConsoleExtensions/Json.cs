using Newtonsoft.Json;

namespace Extensions
{
    public static class Json
    {
        public static string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
        public static string SerializeD(dynamic data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public static T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
