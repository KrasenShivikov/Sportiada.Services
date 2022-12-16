namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models;
    using Interfaces;
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Sportiada.Services.Admin.Models;

    public class CountryAdminService : ICountryAdminService
    {
        private readonly SportiadaDbContext db;
        public CountryAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public void Create(string name, string shortName, string picturePath)
        {
            Country country = new Country();
            country.Name = name;
            country.ShortName = shortName;
            country.PicturePath = picturePath;

            this.db.Add(country);
            this.db.SaveChanges();
        }

        public IEnumerable<CountryAdminModel> All()
           => this.db.Countries.Select(c => new CountryAdminModel
           {
               Id = c.Id,
               Name = c.Name,
               ShortName = c.ShortName,
               PicturePath = c.PicturePath
           }).ToList();

        public CountryAdminModel ById(int Id)
          => this.db.Countries
                  .Where(c => c.Id == Id)
                  .Select(c => new CountryAdminModel
                  {
                      Id = c.Id,
                      Name = c.Name,
                      ShortName = c.ShortName,
                      PicturePath = c.PicturePath
                  }).FirstOrDefault();

        public void Change(int id, string name, string shortName, string picturePath)
        {
            Country country = this.db.Countries.Where(c => c.Id == id).FirstOrDefault();
            country.Name = name;
            country.ShortName = shortName;
            country.PicturePath = picturePath;

            this.db.Update(country);
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            Country country = this.db.Countries.Where(c => c.Id == id).FirstOrDefault();
            this.db.Remove(country);
            this.db.SaveChanges();
        }
    }

}
