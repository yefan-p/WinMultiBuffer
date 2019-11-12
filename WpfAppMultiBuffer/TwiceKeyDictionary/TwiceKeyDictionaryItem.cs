using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfAppMultiBuffer 
{
    public class TwiceKeyDictionaryItem<TKeys, TValue> : INotifyPropertyChanged
    {
        public TKeys FirtsKey { get; set; }
        public TKeys SecondKey { get; set; }

        TValue _value;
        public TValue Value 
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
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

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName != nameof(IsChanged))
            {
                IsChanged = true;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
