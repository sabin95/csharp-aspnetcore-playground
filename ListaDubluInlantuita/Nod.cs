namespace ListaDubluInlantuita
{
    class Nod<T>
    {
        public T Value;
        public Nod<T> Next;
        public Nod<T> Previous;

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
