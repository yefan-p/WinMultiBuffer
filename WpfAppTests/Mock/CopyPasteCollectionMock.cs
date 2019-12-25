using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppMultiBuffer.Models.Interfaces;
using WpfAppMultiBuffer.ViewModels.Services;

namespace WpfAppMultiBufferTests.Mock
{
    class CopyPasteCollectionMock : IList<IBufferItem>
    {
        public IBufferItem this[int index] 
        { 
            get
            {
                BufferItem item = new BufferItem
                {
                    Value = "12345Test",
                };
                return item;
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
            Random rand = new Random(DateTime.Now.Millisecond);
            return rand.Next(-1, 21);
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
