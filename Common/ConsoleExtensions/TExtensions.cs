using Newtonsoft.Json;
using System.Text;

namespace Extensions
{
    public static class TExtensions
    {
        public static byte[] ToBytes<T>(this T data)
        {
            var dataAsJson = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(dataAsJson);
        }
    }
}
