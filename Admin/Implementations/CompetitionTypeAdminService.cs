namespace Sportiada.Services.Admin.Implementations
{
    using System.Linq;
    using Sportiada.Data;
    using Sportiada.Services.Admin.Interfaces;
    using Sportiada.Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Sportiada.Data.Models;

    public class CompetitionTypeAdminService : ICompetitionTypeAdminService
    {
        private readonly SportiadaDbContext db;
        public CompetitionTypeAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<CompetitionTypeAdminModel> All()
        {
            var types = db.CompetitionTypes
                         .Select(t => new CompetitionTypeAdminModel
                         {
                             Id = t.Id,
                             Name = t.Name
                         }).ToList();

            return types;
        }

        public void Create(string name)
        {
            var type = new CompetitionType();
            type.Name = name;
            db.Add(type);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var type = GetType(id);

            db.Remove(type);
            db.SaveChanges();
        }

        public void Update(int id, string name)
        {
            var type = GetType(id);
            type.Id = id;
            type.Name = name;

            db.Update(type);
            db.SaveChanges();
        }

        private CompetitionType GetType(int id)
        {
            var type = db.CompetitionTypes
                        .Where(t => t.Id == id)
                        .FirstOrDefault();

            return type;
        }
    }
}
