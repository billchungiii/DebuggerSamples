using System.Diagnostics;

namespace DebuggerSample001
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>() { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
            Console.WriteLine(list.Count);
            var collection = new MyCollection<int>();

            for (int i = 0; i < 10; i++)
            {
                collection.Add(i);

            }
            Console.WriteLine(collection.Count);
        }
    }
    public class MyCollectionDebugView<T>
    {
        private MyCollection<T> _collection;

        public MyCollectionDebugView(MyCollection<T> collection)
        {
            _collection = collection;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                T[] items = new T[_collection.Count];
                for (int i = 0; i < _collection.Count; i++)
                {
                    items[i] = _collection[i];
                }
                return items;
            }
        }
    }
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(MyCollectionDebugView<>))]
    public class MyCollection<T>
    {
        T[] _value;

        public int Count { get; set; }

        public int Capacity
        {
            get => _value.Length;
        }
        public MyCollection()
        {
            _value = new T[0];
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return _value[index];
            }

            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                _value[index] = value;
            }
        }

        public void Add(T item)
        {
            if (Count == Capacity)
            {
                var newArray = new T[Capacity == 0 ? 4 : 2 * Capacity];
                for (int i = 0; i < Count; i++)
                {
                    newArray[i] = _value[i];
                }
                _value = newArray;
            }
            _value[Count] = item;
            Count++;

        }
    }

}
