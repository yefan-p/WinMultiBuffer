using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ConsoleAppMultiBuffer
{
    public class TwiceKeyDictionary<TKey, TValue>
    {
        Dictionary<TKey, TKey> pKeyPairs = new Dictionary<TKey, TKey>();
        Dictionary<TKey, TValue> pValuePairs = new Dictionary<TKey, TValue>();

        public void Add(TKey key1, TKey key2, TValue value)
        {
            pKeyPairs.Add(key1, key2);
            pValuePairs.Add(key2, value);
        }

        public TValue this[TKey refKey]
        {
            get
            {
                if (pValuePairs.ContainsKey(refKey))
                {
                    return pValuePairs[refKey];
                }

                if (pKeyPairs.ContainsKey(refKey))
                {
                    TKey valueKey = pKeyPairs[refKey];
                    if (pValuePairs.ContainsKey(valueKey))
                    {
                        return pValuePairs[valueKey];
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
                if (pValuePairs.ContainsKey(refKey))
                {
                    pValuePairs[refKey] = value;
                }

                if (pKeyPairs.ContainsKey(refKey))
                {
                    TKey valueKey = pKeyPairs[refKey];
                    if (pValuePairs.ContainsKey(valueKey))
                    {
                        pValuePairs[valueKey] = value;
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
            return (pKeyPairs.ContainsKey(key) || pValuePairs.ContainsKey(key));
        }
    }
}
