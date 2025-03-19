using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_3.Models
{
    public class Stack<T>
    {
        private List<T> items;

        
        public T CurrentItem => IsEmpty ? default : items[items.Count - 1];
        public int Count => items.Count;
        public bool IsEmpty => items.Count == 0;

        
        public Stack()
        {
            items = new List<T>();
        }

        
        public void Push(T item)
        {
            items.Add(item);
        }

        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Stack is empty");

            T item = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return item;
        }

        public void Clear()
        {
            items.Clear();
        }
    }
}