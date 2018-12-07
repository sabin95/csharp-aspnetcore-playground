using FermierExpert.Commands;

namespace FermierExpert.Queries
{
    public class GetAllClientsQuery
    {
        public string[] SortColumns { get; set; }
        public ClientCommand Client { get; set; }
        public int Start { get; set; }
        public int Count { get; set; }
    }
}
