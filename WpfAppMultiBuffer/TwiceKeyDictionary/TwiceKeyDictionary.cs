using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfAppMultiBuffer
{
    public class TwiceKeyDictionary<TKey, TValue> : IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
    {
        Dictionary<TKey, TKey> _keyPairs = new Dictionary<TKey, TKey>();
        Dictionary<TKey, TValue> _valuePairs = new Dictionary<TKey, TValue>();

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        bool _isChanged;
        public bool IsChanged
        {
            get => _isChanged;
            set
            {
                if (value == _isChanged) return;
                {
                    _isChanged = value;
                    OnPropertyChanged();
                }
            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName != nameof(IsChanged))
            {
                IsChanged = true;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Add(TKey key1, TKey key2, TValue value)
        {
            _keyPairs.Add(key1, key2);
            _valuePairs.Add(key2, value);
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
                    TValue tmp = _valuePairs[refKey];
                    _valuePairs[refKey] = value;
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, tmp));
                    OnPropertyChanged($"Storage[{refKey}]");
                }

                if (_keyPairs.ContainsKey(refKey))
                {
                    TKey valueKey = _keyPairs[refKey];
                    if (_valuePairs.ContainsKey(valueKey))
                    {
                        TValue tmp = _valuePairs[valueKey];
                        _valuePairs[valueKey] = value;
                        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, tmp));
                        OnPropertyChanged($"Storage[{valueKey}]");
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
