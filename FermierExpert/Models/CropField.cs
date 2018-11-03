namespace FermierExpert.Models
{
    public class CropField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public uint Size { get; set; }
        public Crop CultivatedCrop { get; set; }
        public Client Client { get; set; }
    }
}
