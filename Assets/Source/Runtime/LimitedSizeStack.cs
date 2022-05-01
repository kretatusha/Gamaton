using System.Collections.Generic;
using System.Linq;

namespace Source.Runtime.Common
{
    public class LimitedStack<T>
    {
        private readonly LinkedList<T> linkedList;
        private readonly int maxSize;
        public bool IsEmpty => !linkedList.Any();
        
        public LimitedStack(int maxSize)
        {
            this.maxSize = maxSize;
            linkedList = new LinkedList<T>();
        }

        public T Peek() => linkedList.Last.Value;

        public T PeekSecond()
        {
            if (linkedList.Last.Previous != null) return linkedList.Last.Previous.Value;
            return Peek();
        }


        public T Pop()
        {
            var result = Peek();
            if(linkedList.Count > 1)
                linkedList.RemoveLast();
            return result;
        }

        public void Push(T value)
        {
            linkedList.AddLast(value);
            if (linkedList.Count > maxSize)
                linkedList.RemoveFirst();
        }
    }
}