namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models.Football;
    using Interfaces;
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Sportiada.Services.Admin.Models;

    public class FootballSubstituteAdminService : IFootballSubstituteAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballSubstituteAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public FootballSubstituteAdminModel ById(int id)
        {
            var substitute = db
                .FootballSubstitutes
                .Where(s => s.Id == id)
                .Select(s => new FootballSubstituteAdminModel
                {
                    Minute = s.Minute,
                    FirstHalf = s.firstHalf
                }).FirstOrDefault();

            return substitute;
        }

        public void Create(int gameStatisticId, int playerInId, int playerOutId, int minute, bool firstHalf)
        {
            FootballSubstitute substitute = new FootballSubstitute
            {
                GameStatisticId = gameStatisticId,
                PlayerInId = playerInId,
                PlayerOutId = playerOutId,
                Minute = minute,
                firstHalf = firstHalf
            };

            db.Add(substitute);
            db.SaveChanges();
        }

        public void Update(int id, int gameStatisticId, int playerInId, int playerOutId, int minute, bool firstHalf)
        {
            FootballSubstitute substitute = GetSubstitute(id);

            substitute.Id = id;
            substitute.GameStatisticId = gameStatisticId;
            substitute.PlayerInId = playerInId;
            substitute.PlayerOutId = playerOutId;
            substitute.Minute = minute;
            substitute.firstHalf = firstHalf;

            db.Update(substitute);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballSubstitute substitute = GetSubstitute(id);

            db.Remove(substitute);
            db.SaveChanges();
        }

        public FootballSubstitute GetSubstitute(int id)
        {
            FootballSubstitute substitute = db.FootballSubstitutes
                .Where(s => s.Id == id)
                .FirstOrDefault();

            return substitute;
        }


    }
}
