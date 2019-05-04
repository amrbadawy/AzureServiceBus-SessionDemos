using System.Text;

namespace Extensions
{
    public static class ByteExtensions
    {
        public static string AsString(this byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
    }
}
