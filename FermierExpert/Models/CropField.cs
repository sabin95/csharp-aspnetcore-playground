namespace FermierExpert.Models
{
    public class CropField : BaseEntity
    {
        public string Name { get; set; }
        public uint Size { get; set; }
        public int CropId { get; set; }
        public int ClientId { get; set; }
    }
}
