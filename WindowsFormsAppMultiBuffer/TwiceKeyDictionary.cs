using System;
using System.Collections;
using System.Collections.Generic;

namespace WindowsFormsAppMultiBuffer
{
    public class TwiceKeyDictionary<TKey, TValue> : IEnumerable
    {
        Dictionary<TKey, TKey> _keyPairs = new Dictionary<TKey, TKey>();
        Dictionary<TKey, TValue> _valuePairs = new Dictionary<TKey, TValue>();

        public void Add(TKey key1, TKey key2, TValue value)
        {
            _keyPairs.Add(key1, key2);
            _valuePairs.Add(key2, value);
        }

        public int Length 
        { 
            get { return _valuePairs.Count; }
        }

        public TValue this[TKey refKey]
        {
            get
            {
                if (_valuePairs.ContainsKey(refKey))
                {
                    return _valuePairs[refKey];
                }

                if (_keyPairs.ContainsKey(refKey))
                {
                    TKey valueKey = _keyPairs[refKey];
                    if (_valuePairs.ContainsKey(valueKey))
                    {
                        return _valuePairs[valueKey];
                    }
                    else
                    {
                        Exception exc = new Exception("TwiceKeyDictionary: Value key is not exist.");
                        throw exc;
                    }
                }
                else
                {
                    Exception exc = new Exception("TwiceKeyDictionary: Reference key is not exist.");
                    throw exc;
                }
            }
            set
            {
                if (_valuePairs.ContainsKey(refKey))
                {
                    _valuePairs[refKey] = value;
                }

                if (_keyPairs.ContainsKey(refKey))
                {
                    TKey valueKey = _keyPairs[refKey];
                    if (_valuePairs.ContainsKey(valueKey))
                    {
                        _valuePairs[valueKey] = value;
                    }
                    else
                    {
                        Exception exc = new Exception("TwiceKeyDictionary: Value key is not exist.");
                        throw exc;
                    }
                }
                else
                {
                    Exception exc = new Exception("TwiceKeyDictionary: Reference key is not exist.");
                    throw exc;
                }
            }
        }

        public bool ContainsKey(TKey key)
        {
            return (_keyPairs.ContainsKey(key) || _valuePairs.ContainsKey(key));
        }

        public IEnumerator GetEnumerator()
        {
            return new TwiceKeyDictionaryEnumerator<TKey, TValue>(_keyPairs, _valuePairs);
        }
    }
}
