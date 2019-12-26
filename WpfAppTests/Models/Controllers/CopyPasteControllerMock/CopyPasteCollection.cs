using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.Models.Interfaces;
using MultiBuffer.WpfApp.ViewModels.Services;

namespace MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerMock
{
    class CopyPasteCollection : IList<IBufferItem>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexOfResult">Значение, которое будет возвращать метод IndexOf</param>
        public CopyPasteCollection(int indexOfResult, string bufferItemValue)
        {
            _indexOfResult = indexOfResult;
            _bufferItemValue = bufferItemValue;
        }

        /// <summary>
        /// Значение, которое будет возвращать метод IndexOf
        /// </summary>
        private int _indexOfResult;

        /// <summary>
        /// Значение буфера, которое будет возвращать коллекция
        /// </summary>
        private string _bufferItemValue;

        public IBufferItem this[int index] 
        { 
            get
            {
                return new WpfApp.ViewModels.Services.BufferItem
                            {
                                Value = _bufferItemValue,
                            };
            }
            set
            {

            }
        }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(IBufferItem item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IBufferItem item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IBufferItem[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IBufferItem> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(IBufferItem item)
        {
            return _indexOfResult;
        }

        public void Insert(int index, IBufferItem item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IBufferItem item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
