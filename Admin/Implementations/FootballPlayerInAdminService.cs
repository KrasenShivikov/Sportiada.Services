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
    public class FootballPlayerInAdminService : IFootballPlayerInAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballPlayerInAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public FootballPlayerInAdminModel ById(int id)
        {
            var playerIn = db.FootballPlayerIns
                .Where(p => p.Id == id)
                .Select(p => new FootballPlayerInAdminModel
                {
                    Player = new FootballPlayerGameAdminModel
                    {
                        ProfileName = p.Player.ProfileName,
                        Country = p.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath
                    },
                    InIcon = p.InIcon
                }).FirstOrDefault();

            return playerIn;
        }

        public void Create(int playerId, int substituteId, string inIcon)
        {
            FootballPlayerIn playerIn = new FootballPlayerIn
            {
                PlayerId = playerId,
                SubstituteId = substituteId,
                InIcon = inIcon
            };

            db.Add(playerIn);
            db.SaveChanges();
        }

        public void Update(int id, int playerId, int substituteId, string inIcon)
        {
            FootballPlayerIn playerIn = GetPlayerIn(id);

            playerIn.Id = id;
            playerIn.PlayerId = playerId;
            playerIn.SubstituteId = substituteId;
            playerIn.InIcon = inIcon;

            db.Update(playerIn);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballPlayerIn playerIn = GetPlayerIn(id);

            db.Remove(playerIn);
            db.SaveChanges();
        }

        public FootballPlayerIn GetPlayerIn(int id)
        {
            FootballPlayerIn playerIn = db.FootballPlayerIns
                .Where(p => p.Id == id)
                .FirstOrDefault();

            return playerIn;
        }

        
    }
}
