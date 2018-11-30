namespace Sportiada.Services.AlpineSki.Models.Skier
{
    using Services.Models;

    public class SkierModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PicturePath { get; set; }

        public CountryModel Country { get; set; }
    }
}
