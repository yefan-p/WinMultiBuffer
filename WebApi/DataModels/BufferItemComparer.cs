using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MultiBuffer.WebApi.DataModels
{
    public class BufferItemComparer : IEqualityComparer<BufferItem>
    {
        public bool Equals([AllowNull] BufferItem x, [AllowNull] BufferItem y)
        {
            if (x.Key == y.Key && x.UserId == y.UserId) return true;
            return false;
        }

        public int GetHashCode([DisallowNull] BufferItem obj)
        {
            return (obj.Key + obj.UserId).GetHashCode();
        }
    }
}
