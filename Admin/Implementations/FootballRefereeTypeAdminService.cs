namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models.Football;
    using Interfaces;
    using Sportiada.Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class FootballRefereeTypeAdminService : IFootballRefereeTypeAdminService
    {
        private readonly SportiadaDbContext db;
        public FootballRefereeTypeAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<FootballRefereeTypeAdminModel> All()
          => db.FootballRefereeTypes
               .Select(t => new FootballRefereeTypeAdminModel
               {
                   Id = t.Id,
                   Name = t.Name
               }).ToList();

        public void Create(string name)
        {
            FootballRefereeType type = new FootballRefereeType
            {
                Name = name
            };

            db.Add(type);
            db.SaveChanges();
        }

        public void Update(int id, string name)
        {
            var type = GetRefereeType(id);
            type.Id = id;
            type.Name = name;

            db.Update(type);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var type = GetRefereeType(id);

            db.Remove(type);
            db.SaveChanges();
        }

        public FootballRefereeType GetRefereeType(int id)
        {
            FootballRefereeType type = db.FootballRefereeTypes
                                        .Where(t => t.Id == id)
                                        .FirstOrDefault();

            return type;
        }    
    }
}
