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
    public class FootballSidelineReasonAdminService : IFootballSidelineReasonAdminService
    {
        private readonly SportiadaDbContext db;
        public FootballSidelineReasonAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<FootballSidelineReasonAdminModel> All()
          => db.FootballSidelineReasons
              .Select(r => new FootballSidelineReasonAdminModel
              {
                  Id = r.Id,
                  Name = r.Name,
                  Picture = r.Picture
              }).ToList();

        public void Create(string name, string picture)
        {
            FootballSidelineReason reason = new FootballSidelineReason();
            reason.Name = name;
            reason.Picture = picture;

            db.Add(reason);
            db.SaveChanges();
        }

        public void Update(int id, string name, string picture)
        {
            FootballSidelineReason reason = GetReason(id);
            reason.Name = name;
            reason.Picture = picture;

            db.Update(reason);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballSidelineReason reason = GetReason(id);

            db.Remove(reason);
            db.SaveChanges();
        }

        public FootballSidelineReason GetReason(int id)
           => db.FootballSidelineReasons
               .Where(r => r.Id == id)
               .FirstOrDefault();
    }
}
