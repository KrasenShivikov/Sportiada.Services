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
    public class FootballGameRefereeAdminService : IFootballGameRefereeAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballGameRefereeAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public FootballGameRefereeAdminModel ById(int id)
        {
            var gameReferee = db.FootballGameReferees
                .Where(r => r.Id == id)
                .Select(r => new FootballGameRefereeAdminModel
                {
                    GameId = r.GameId,
                    Referee = new FootballRefereeAdminModel
                    {
                        Id = r.Referee.Id,
                        BirthDate = r.Referee.BirthDate,
                        Name = r.Referee.Name,
                        Country = new CountryAdminModel
                        { 
                            Name = r.Referee.Name,
                            PicturePath = r.Referee.Country.PicturePath
                        },
                    }
                }).FirstOrDefault();

            return gameReferee;
        }

        public void Create(int gameId, int refereeId, int typeId)
        {
            FootballGameReferee gameReferee = new FootballGameReferee
            {
                GameId = gameId,
                RefereeId = refereeId,
                TypeId = typeId
            };

            db.Add(gameReferee);
            db.SaveChanges();
        }

        public void Update(int id, int gameId, int refereeId, int typeId)
        {
            var gameReferee = GetGameReferee(id);

            gameReferee.Id = id;
            gameReferee.GameId = gameId;
            gameReferee.RefereeId = refereeId;
            gameReferee.TypeId = typeId;

            db.Update(gameReferee);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var gameReferee = GetGameReferee(id);

            db.Remove(gameReferee);
            db.SaveChanges();
        }

        public FootballGameReferee GetGameReferee(int id)
        {
            var gameReferee = db.FootballGameReferees
                .Where(r => r.Id == id)
                .FirstOrDefault();

            return gameReferee;
        }

        
    }
}
