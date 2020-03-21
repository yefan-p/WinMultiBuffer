using System.Collections.Generic;

namespace MultiBuffer.IWebApi
{
    public class WebBufferComparer : IEqualityComparer<WebBuffer>
    {
        public bool Equals(WebBuffer x, WebBuffer y)
        {
            if (x.Key == y.Key) return true;
            return false;
        }

        public int GetHashCode(WebBuffer obj)
        {
            return obj.Key.GetHashCode();
        }
    }
}
