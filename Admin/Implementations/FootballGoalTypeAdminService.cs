namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models.Football;
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class FootballGoalTypeAdminService : IFootballGoalTypeAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballGoalTypeAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<FootballGoalTypeAdminModel> All()
           => db.FootballGoalTypes
                .Select(g => new FootballGoalTypeAdminModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    Picture = g.picture
                }).ToList();

        public void Create(string name, string picture)
        {
            FootballGoalType type = new FootballGoalType();
            type.Name = name;
            type.picture = picture;

            db.Add(type);
            db.SaveChanges();
        }

        public void Update(int id, string name, string picture)
        {
            FootballGoalType type = GetGoalType(id);
            type.Name = name;
            type.picture = picture;

            db.Update(type);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballGoalType type = GetGoalType(id);

            db.Remove(type);
            db.SaveChanges();
        }


        public FootballGoalType GetGoalType(int id)
        {
            FootballGoalType type = db.FootballGoalTypes
                        .Where(g => g.Id == id)
                        .FirstOrDefault();

            return type;
        }
    }
}
