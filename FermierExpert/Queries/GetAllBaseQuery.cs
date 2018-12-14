namespace FermierExpert.Queries
{
    public class GetAllBaseQuery<T>
        where T : class
    {
        public string[] SortColumns { get; set; }
        public T FilterPayload { get; set; }

        public int Start { get; set; }
        public int Count { get; set; }
    }
}
