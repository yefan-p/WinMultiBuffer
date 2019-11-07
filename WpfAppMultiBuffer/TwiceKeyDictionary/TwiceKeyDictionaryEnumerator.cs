using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfAppMultiBuffer
{
    public class TwiceKeyDictionaryEnumerator<TKeys, TValue> : IEnumerator<TwiceKeyDictionaryItem<TKeys, TValue>>
    {
        int _postion = -1;
        Dictionary<TKeys, TKeys> _keyPairs;
        Dictionary<TKeys, TValue> _valuePairs;

        public TwiceKeyDictionaryEnumerator(Dictionary<TKeys, TKeys> keyPairs, Dictionary<TKeys, TValue> valuePairs)
        {
            _keyPairs = keyPairs;
            _valuePairs = valuePairs;
        }

        public TwiceKeyDictionaryItem<TKeys, TValue> Current
        {
            get
            {
                TwiceKeyDictionaryItem<TKeys, TValue> item = new TwiceKeyDictionaryItem<TKeys, TValue>();

                item.FirtsKey = _keyPairs.Keys.ElementAt(_postion);
                item.SecondKey = _keyPairs[item.FirtsKey];
                item.Value = _valuePairs[item.SecondKey];

                return item;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            _postion++;
            if (_postion < _keyPairs.Count)
                return true;
            else
                return false;

        }

        public void Reset()
        {
            _postion = -1;
        }
    }
}
