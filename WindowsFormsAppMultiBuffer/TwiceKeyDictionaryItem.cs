using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsAppMultiBuffer
{
    public class TwiceKeyDictionaryItem<TKeys, TValue>
    {
        public TKeys FirtsKey { get; set; }
        public TKeys SecondKey { get; set; }
        public TValue Value { get; set; }
    }
}
