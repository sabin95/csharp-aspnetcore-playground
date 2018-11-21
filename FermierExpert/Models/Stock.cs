namespace FermierExpert.Models
{
    public class Stock : BaseEntity
    {
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int Ammount { get; set; }
    }
}
