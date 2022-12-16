namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models;
    using Interfaces;
    using Sportiada.Services.Admin.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class CityAdminService : ICityAdminService
    {
        private readonly SportiadaDbContext db;
        public CityAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CityAdminModel> All()
           => this.db.Cities
                    .Select(c => new CityAdminModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Country = new CountryAdminModel
                        {
                            Name = c.Country.Name,
                            PicturePath = c.Country.PicturePath
                        }
                    });

        public CityAdminModel ById(int id)
          => this.db.Cities
                .Where(c => c.Id == id)
                .Select(c => new CityAdminModel 
                {
                    Id = c.Id,
                    Name = c.Name,
                    Country = new CountryAdminModel
                    {
                        Id = c.CountryId,
                        Name = c.Country.Name,
                        PicturePath = c.Country.PicturePath
                    }
                }).FirstOrDefault();

        public void Create(string name, int countryId)
        {
            City city = new City { Name = name, CountryId = countryId };
            this.db.Add(city);
            this.db.SaveChanges();
        }

        public void Update(int id, string name, int countryId)
        {
            City city = this.db.Cities.Where(c => c.Id == id).FirstOrDefault();
            city.Name = name;
            city.CountryId = countryId;
        }

        public void Delete(int id)
        {
            City city = this.db.Cities.Where(c => c.Id == id).FirstOrDefault();
            this.db.Remove(city);
            this.db.SaveChanges();
        }

      
    }
}
