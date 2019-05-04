using System;

namespace Extensions
{
    public static class Guard
    {
        public static void IsEmpty(string data)
        {
            if(data.IsNullOrWhiteSpace())
                throw new ArgumentNullException(data);
        }
    }

}
