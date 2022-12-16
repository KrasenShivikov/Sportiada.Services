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
    public class FootballCardAdminService : IFootballCardAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballCardAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public FootballCardAdminModel ById(int id)
        {
            var card = db.FootballCards
                .Where(c => c.Id == id)
                .Select(c => new FootballCardAdminModel
                {
                    Type = new FootballCardTypeAdminModel
                    {
                        Name = c.Type.Name,
                        Picture = c.Type.Picture
                    },
                    Minute = c.Minute,
                    Player = new FootballPlayerGameAdminModel
                    {
                        ProfileName = c.Player.ProfileName,
                        Country = c.Player.Countries.Where(country => country.MainCountry).FirstOrDefault().Country.PicturePath
                    }
                }).FirstOrDefault();

            return card;
        }

        public void Create(int typeId, int playerId, int gameStatisticId, int minute, bool firstHalf)
        {
            FootballCard card = new FootballCard
            {
                TypeId = typeId,
                PlayerId = playerId,
                GameStatisticId = gameStatisticId,
                Minute = minute,
                FirstHalf = firstHalf
            };

            db.Add(card);
            db.SaveChanges();
        }

        public void Update(int id, int typeId, int playerId, int gameStatisticId, int minute, bool firstHalf)
        {
            FootballCard card = GetCard(id);

            card.Id = id;
            card.TypeId = typeId;
            card.PlayerId = playerId;
            card.GameStatisticId = gameStatisticId;
            card.Minute = minute;
            card.FirstHalf = firstHalf;

            db.Update(card);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballCard card = GetCard(id);

            db.Remove(card);
            db.SaveChanges();
        }

        public FootballCard GetCard(int id)
        {
            FootballCard card = db.FootballCards
                .Where(c => c.Id == id)
                .FirstOrDefault();

            return card;
        }

        
    }
}
