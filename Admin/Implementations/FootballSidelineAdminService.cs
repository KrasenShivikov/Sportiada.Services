namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Interfaces;
    using Sportiada.Data.Models.Football;
    using Sportiada.Services.Admin.Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    public class FootballSidelineAdminService : IFootballSidelineAdminService
    {

        private readonly SportiadaDbContext db;
        public FootballSidelineAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public FootballSidelineAdminModel ById(int id)
        {
            var sideline = db.FootballSidelines
                .Where(s => s.Id == id)
                .Select(s => new FootballSidelineAdminModel
                {
                    Player = new FootballPlayerGameAdminModel
                    {
                        ProfileName = s.Player.ProfileName,
                        Country = s.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath
                    },
                    Reason = new FootballSidelineReasonAdminModel
                    {
                        Name = s.Reason.Name,
                        Picture = s.Reason.Picture
                    }
                }).FirstOrDefault();

            return sideline;
        }

        public void Create(int playerId, int gameStatisticId, int reasonId)
        {
            FootballSideline sideline = new FootballSideline
            {
                PlayerId = playerId,
                GameStatisticId = gameStatisticId,
                ReasonId = reasonId
            };

            db.Add(sideline);
            db.SaveChanges();
        }

        public void Update(int id, int playerId, int gameStatisticId, int reasonId)
        {
            FootballSideline sideline = GetSideline(id);

            sideline.Id = id;
            sideline.PlayerId = playerId;
            sideline.GameStatisticId = gameStatisticId;
            sideline.ReasonId = reasonId;

            db.Update(sideline);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballSideline sideline = GetSideline(id);

            db.Remove(sideline);
            db.SaveChanges();
        }

        public FootballSideline GetSideline(int id)
        {
            FootballSideline sideline = db.FootballSidelines
                .Where(s => s.Id == id)
                .FirstOrDefault();

            return sideline;
        }      
    }
}
