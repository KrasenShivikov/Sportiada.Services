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
    public class FootballPlayerOutAdminService : IFootballPlayerOutAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballPlayerOutAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public FootballPlayerOutAdminModel ById(int id)
        {
            var playerOut = db.FootballPlayerOuts
                .Where(p => p.Id == id)
                .Select(p => new FootballPlayerOutAdminModel
                {
                    Player = new FootballPlayerGameAdminModel
                    {
                        ProfileName = p.Player.ProfileName,
                        Country = p.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath
                    },
                    OutIcon = p.OutIcon
                }).FirstOrDefault();

            return playerOut;
        }

        public void Create(int playerId, int substituteId, string outIcon)
        {
            FootballPlayerOut playerOut = new FootballPlayerOut
            {
                PlayerId = playerId,
                SubstituteId = substituteId,
                OutIcon = outIcon
            };

            db.Add(playerOut);
            db.SaveChanges();
        }

        public void Update(int id, int playerId, int substituteId, string outIcon)
        {
            FootballPlayerOut playerOut = GetPlayerOut(id);

            playerOut.Id = id;
            playerOut.PlayerId = playerId;
            playerOut.SubstituteId = substituteId;
            playerOut.OutIcon = outIcon;

            db.Update(playerOut);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballPlayerOut playerOut = GetPlayerOut(id);

            db.Remove(playerOut);
            db.SaveChanges();
        }

        public FootballPlayerOut GetPlayerOut(int id)
        {
            FootballPlayerOut playerOut = db.FootballPlayerOuts
                .Where(p => p.Id == id)
                .FirstOrDefault();

            return playerOut;
        }


    }
}

