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
    public class FootballReserveAdminService : IFootballReserveAdminService
    {
        private readonly SportiadaDbContext db;
        public FootballReserveAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public FootballReserveAdminModel ById(int id)
        {
            var reserve = db.FootballReserves
                .Where(r => r.Id == id)
                .Select(r => new FootballReserveAdminModel
                {
                    Id = r.Id,
                    PlayerId = r.PlayerId,
                    GameStattisticId = r.GameStattisticId,
                    Player = new FootballPlayerGameAdminModel
                    {
                        ProfileName = r.Player.ProfileName,
                        Country = r.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath
                    }
                }).FirstOrDefault();

            return reserve;
        }

        public void Create(int playerId, int gameStatisticId)
        {
            FootballReserve reserve = new FootballReserve
            {
                GameStattisticId = gameStatisticId,
                PlayerId = playerId
            };

            db.Add(reserve);
            db.SaveChanges();
        }

        public void Update(int id, int playerId, int gameStatisticId)
        {
            var reserve = GetReserve(id);

            reserve.Id = id;
            reserve.PlayerId = playerId;
            reserve.GameStattisticId = gameStatisticId;

            db.Update(reserve);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var reserve = GetReserve(id);

            db.Remove(reserve);
            db.SaveChanges();
        }

        public FootballReserve GetReserve(int id)
        {
            var reserve = db.FootballReserves
                .Where(r => r.Id == id)
                .FirstOrDefault();

            return reserve;
        }

        
    }
}
