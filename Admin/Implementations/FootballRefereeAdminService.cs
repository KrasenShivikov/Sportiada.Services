namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models;
    using Interfaces;
    using Sportiada.Data.Models.Football;
    using Sportiada.Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class FootballRefereeAdminService : IFootballRefereeAdminService
    {
        private readonly SportiadaDbContext db;
        public FootballRefereeAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<FootballRefereeAdminModel> All()
           => db.FootballReferees
                .Select(r => new FootballRefereeAdminModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    BirthDate = r.BirthDate,
                    Picture = r.Picture,
                    Country = new CountryAdminModel 
                    {
                        Id = r.Country.Id,
                        Name = r.Country.Name,
                        PicturePath = r.Country.PicturePath
                    }
                }).ToList();

        public void Create(string name, DateTime birthdate, int countryId, string pictureId)
        {
            FootballReferee referee = new FootballReferee
            {
                Name = name,
                BirthDate = birthdate,
                CountryId = countryId,
                Picture = pictureId
            };

            db.Add(referee);
            db.SaveChanges();
        }

        public void Update(int id, string name, DateTime birthdate, int countryId, string pictureId)
        {
            FootballReferee referee = GetReferee(id);
            referee.Name = name;
            referee.BirthDate = birthdate;
            referee.CountryId = countryId;
            referee.Picture = pictureId;

            db.Update(referee);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballReferee referee = GetReferee(id);

            db.Remove(referee);
            db.SaveChanges();
        }

        public FootballReferee GetReferee(int id)
        {
            var referee = db.FootballReferees
                            .Where(r => r.Id == id)
                            .FirstOrDefault();

            return referee;
        }

        
    }
}
