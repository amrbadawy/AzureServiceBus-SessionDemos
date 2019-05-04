using Newtonsoft.Json;
using System.Text;

namespace Extensions
{
    public static class StringExtensions
    {
        public static string Serialize(this object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public static T Deserialize<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
        public static byte[] ToBytes(this string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        public static bool IsNullOrWhiteSpace(this string data)
        {
            return string.IsNullOrWhiteSpace(data);
        }

        public static bool IsNotNullOrWhiteSpace(this string data)
        {
            return !data.IsNullOrWhiteSpace();
        }
    }

}
