using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace WpfAppMultiBuffer
{
    public class TwiceKeyDictionary<TKey, TValue> : IEnumerable, INotifyCollectionChanged
    {
        Dictionary<TKey, TKey> _keyPairs = new Dictionary<TKey, TKey>();
        Dictionary<TKey, TValue> _valuePairs = new Dictionary<TKey, TValue>();
        List<TwiceKeyDictionaryItem<TKey, TValue>> _storage = new List<TwiceKeyDictionaryItem<TKey, TValue>>();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void Add(TKey key1, TKey key2, TValue value)
        {
            TwiceKeyDictionaryItem<TKey, TValue> item = new TwiceKeyDictionaryItem<TKey, TValue>()
            {
                FirtsKey = key1,
                SecondKey = key2,
                Value = value,
            };
            _storage.Add(item);
        }

        public void AddRange(TKey[] keyFirst, TKey[] keySecond, TValue value)
        {
            if (keyFirst.Length != keySecond.Length)
                throw new Exception("Count key must be same.");

            for (int i = 0; i < keyFirst.Length; i++)
            {
                Add(keyFirst[i], keySecond[i], value);
            }
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
                else if (_keyPairs.ContainsKey(refKey))
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
                else if(_keyPairs.ContainsKey(refKey))
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

        public void OnPropertyChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(e));
        }
    }
}
