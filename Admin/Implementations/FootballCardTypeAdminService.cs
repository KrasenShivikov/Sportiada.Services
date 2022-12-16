namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Interfaces;
    using Sportiada.Data.Models.Football;
    using Sportiada.Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class FootballCardTypeAdminService : IFootballCardTypeAdminService
    {
        private readonly SportiadaDbContext db;
        public FootballCardTypeAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<FootballCardTypeAdminModel> CardTypes()
          => db.FootballCardTypes
               .Select(c => new FootballCardTypeAdminModel
               {
                   Id = c.Id,
                   Name = c.Name,
                   Picture = c.Picture
               }).ToList();

        public void Create(string name, string picture)
        {
            FootballCardType cardType = new FootballCardType();
            cardType.Name = name;
            cardType.Picture = picture;

            db.Add(cardType);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballCardType cardType = GetCardType(id);

            db.Remove(cardType);
            db.SaveChanges();
        }

       
        public void Update(int id, string name, string picture)
        {
            FootballCardType cardType = GetCardType(id);
            cardType.Name = name;
            cardType.Picture = picture;

            db.Update(cardType);
            db.SaveChanges();
        }

        public FootballCardType GetCardType(int id)
        {
            FootballCardType cardType = db.FootballCardTypes
                                     .Where(c => c.Id == id)
                                     .FirstOrDefault();

            return cardType;
        }
    }
}
