namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Interfaces;
    using Sportiada.Data.Models.Football;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using Sportiada.Services.Admin.Models;

    public class FootballLineUpAdminService : IFootballLineUpAdminService
    {
        private readonly SportiadaDbContext db;
        public FootballLineUpAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public FootballLineUpAdminModel LineUpById(int id)
        {
            var lineUp = db.FootballLineUps
                .Where(l => l.Id == id)
                .Select(l => new FootballLineUpAdminModel
                {
                    Id = l.Id,
                    GameStattisticId = l.GameStattisticId,
                    PlayerId = l.PlayerId,
                    Player = new FootballPlayerGameAdminModel
                    {
                        Country = l.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath,
                        ProfileName = l.Player.ProfileName
                    }
                    
                }).FirstOrDefault();

            return lineUp;
        }
        public void Create(int playerId, int gameStatisticId)
        {
            FootballLineUp lineUp = new FootballLineUp
            {
                GameStattisticId = gameStatisticId,
                PlayerId = playerId
            };

            db.Add(lineUp);
            db.SaveChanges();
        }


        public void Update(int id, int playerId, int gameStatisticId)
        {
            FootballLineUp lineUp = GetLineUp(id);

            lineUp.Id = id;
            lineUp.PlayerId = playerId;
            lineUp.GameStattisticId = gameStatisticId;

            db.Update(lineUp);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballLineUp lineUp = GetLineUp(id);

            db.Remove(lineUp);
            db.SaveChanges();
        }

       
        public FootballLineUp GetLineUp(int id)
        {
            FootballLineUp lineUp = db.FootballLineUps
                .Where(l => l.Id == id)
                .FirstOrDefault();

            return lineUp;
        }

        
    }
}
