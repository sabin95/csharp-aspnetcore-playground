namespace FermierExpert.Queries
{
    public class GetAllClientsQuery
    {
        public string[] SortColumns { get; set; }
        public int Start { get; set; }
        public int Count { get; set; }
    }
}
