using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Utilities
{
    class IteratorList<T>
    {
        protected List<T> itemList;
        protected int counter;
        protected int startIdx;
        protected int endIdx;

        public IteratorList()
        {
            itemList = new List<T>();
            counter = 0;
            startIdx = 0;
            endIdx = 0;
        }

        public void addItem(T item)
        {
            itemList.Add(item);
        }

        public T getItemAtIndex(int i)
        {
            if (i >= itemList.Count)
                return default(T);

            return itemList[i];
        }

        public void removeItemAtIndex(int i)
        {
            if (i >= itemList.Count)
                return;

            itemList.RemoveAt(i);
            counter -= 1;
            endIdx = itemList.Count;
        }

        public void removeItem(T item)
        {
            itemList.Remove(item);
            counter -= 1;
            endIdx = itemList.Count;
        }

        public bool Contains(T item)
        {
            return itemList.Contains(item);
        }

        public int Size()
        {
            return itemList.Count; ;
        }

        public int begin()
        {
            counter = startIdx;
            endIdx = itemList.Count;
            return startIdx;
        }

        public int end()
        {
            return endIdx;
        }

        public int next()
        {
            counter += 1;
            if (counter < 0)
                counter = 0;
            return counter;
        }

        public void copy(List<T> list)
        {
            itemList = list;
        }

        public void clear()
        {
            itemList.Clear();
            counter = 0;
            startIdx = 0;
            endIdx = 0;
        }

        public T this[int index]
        {
            get
            {
                if (index >= itemList.Count)
                    return default(T);
                return itemList[index];
            }

            set{
                if (index >= itemList.Count)
                    return;
                itemList[index] = value;
            }
        }
    }
}
