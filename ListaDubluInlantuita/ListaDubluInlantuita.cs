using System;
using System.Collections;
using System.Collections.Generic;

namespace ListaDubluInlantuita
{
    public class ListaDubluInlantuita<T> : IList<T>
    {
        private const string EMPTY_SPACE = " ";

        Nod<T> cap { get; set; }
        Nod<T> coada { get; set; }

        public int Count { get; private set; } = 0;

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get
            {
                if (!int.TryParse(index.ToString(),out int something))
                {
                    throw new ArgumentException();
                }
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                var tempNod = cap;
                for (var iterator = 0; iterator < index; iterator++)
                {
                    tempNod = tempNod.Next;
                }
                return tempNod.Value;
            }
            set
            {
                if (!int.TryParse(index.ToString(), out int something))
                {
                    throw new ArgumentException();
                }
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                var tempNod = cap;
                for (var iterator = 0; iterator < index; iterator++)
                {
                    tempNod = tempNod.Next;
                }
                tempNod.Value = value;
            }
        }

        public void Add(T element)
        {
            var x = new Nod<T>
            {
                Value = element
            };
            if (cap == null)
            {
                cap = coada = x;
            }
            else
            {
                x.Previous = coada;
                coada.Next = x;
            }

            coada = x;
            Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (cap == null)
            {
                yield break;
            }
            for (var iterator = cap; iterator != coada.Next; iterator = iterator.Next)
            {
                yield return iterator.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int IndexOf(T item)
        {
            int pozitie = 0;
            for (var i = cap; i != coada.Next; i = i.Next)
            {
                if (i.Value.Equals(item))
                {
                    return pozitie;
                }
                pozitie++;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            var x = new Nod<T>
            {
                Value = item
            };

            var iterator = cap;
            for (var s = 0; s < index; s++)
            {
                iterator = iterator.Next;
            }
            if (index != 0)
            {
                x.Previous = iterator.Previous;
                iterator.Previous.Next = x;
            }
            else
            {
                cap = x;
            }
            iterator.Previous = x;
            x.Next = iterator;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (cap == null || index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            var iterator = cap;
            for (var i = 0; i < index; i++)
            {
                iterator = iterator.Next;
            }
            if (iterator.Previous != null)
            {
                iterator.Previous.Next = iterator.Next;
                if (iterator.Next != null)
                {
                    iterator.Next.Previous = iterator.Previous;
                }
            }
            else
            {
                cap = iterator.Next;
                if (cap != null)
                {
                    cap.Previous = null;
                }
            }
            Count--;
        }

        public void Clear()
        {
            cap = coada = null;
        }

        public bool Contains(T item) => IndexOf(item) != -1;

        public void CopyTo(T[] array, int arrayIndex)
        {
            for(var i = 0; i < Count; i++)
            {
                array[i] = this[i];
            }
        }

        public bool Remove(T item)
        {
            if (cap == null)
            {
                return false;
            }
            for (var i = cap; i != coada.Next; i = i.Next)
            {
                if (!i.Value.Equals(item))
                {
                    continue;
                }
                if (i.Previous != null)
                {
                    i.Previous.Next = i.Next;
                    if (i.Next != null)
                    {
                        i.Next.Previous = i.Previous;
                    }
                }
                else
                {
                    cap = i.Next;
                    if (cap != null)
                    {
                        cap.Previous = null;
                    }
                }
                Count--;
                return true;
            }
            return false;
        }

        public void Dispose()
        {
        }
    }
}
