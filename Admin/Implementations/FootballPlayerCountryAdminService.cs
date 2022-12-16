namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models.Football;
    using Interfaces;
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class FootballPlayerCountryAdminService : IFootballPlayerCountryAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballPlayerCountryAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public void Create(int playerId, int countryId, bool mainCountry)
        {
            FootballPlayerCountry playerCountry = new FootballPlayerCountry
            {
                PlayerId = playerId,
                CountryId = countryId,
                MainCountry = mainCountry
            };

            db.Add(playerCountry);
            db.SaveChanges();
        }


        //public void Update(int playerId, int countryId, bool mainCountry)
        //{
        //    Create(playerId, countryId, mainCountry);
        //}

        public void Delete(int playerId, int countryId)
        {
            var playerCountry = GetPlayerCountry(playerId, countryId);

            db.Remove(playerCountry);
            db.SaveChanges();
        }

        public FootballPlayerCountry GetPlayerCountry(int playerId, int countryId)
        {
            var playerCountry = db.FootballPlayerCountry
                               .Where(pc => pc.PlayerId == playerId && pc.CountryId == countryId)
                               .FirstOrDefault();
            return playerCountry;
        }

        
    }
}
