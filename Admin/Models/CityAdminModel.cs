namespace Sportiada.Services.Admin.Models
{

    public class CityAdminModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CountryAdminModel Country { get; set; }
    }
}
