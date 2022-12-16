namespace Sportiada.Services.Admin.Implementations
{
    using Models;
    using Data;
    using Data.Models.Football;
    using Interfaces;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    public class FootballGameStatisticAdminService : IFootballGameStatisticAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballGameStatisticAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public void Create(int typeId, int squadId, int gameId, int ballPossession, int corners, int shotsOnTarget, int shotsWide, int fouls, int offsides)
        {
            FootballGameStatistic gameStatistic = new FootballGameStatistic
            {
                TypeId = typeId,
                SquadId = squadId,
                GameId = gameId,
                BallPossession = ballPossession,
                Corners = corners,
                ShotsOnTarget = shotsOnTarget,
                ShotsWide = shotsWide,
                Fouls = fouls,
                Offsides = offsides,
                
            };

            db.Add(gameStatistic);
            db.SaveChanges();
        }

        public void Update(int id, int typeId, int squadId, int gameId, int ballPossession, int corners, int shotsOnTarget, int shotsWide, int fouls, int offsides)
        {
            FootballGameStatistic gameStatistic = GetGameStatistic(id);

            gameStatistic.Id = id;
            gameStatistic.TypeId = typeId;
            gameStatistic.SquadId = squadId;
            gameStatistic.GameId = gameId;
            gameStatistic.BallPossession = ballPossession;
            gameStatistic.Corners = corners;
            gameStatistic.ShotsOnTarget = shotsOnTarget;
            gameStatistic.ShotsWide = shotsWide;
            gameStatistic.Fouls = fouls;
            gameStatistic.Offsides = offsides;

            db.Update(gameStatistic);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballGameStatistic gameStatistic = GetGameStatistic(id);

            db.Remove(gameStatistic);
            db.SaveChanges();
        }

        public FootballGameStatistic GetGameStatistic(int id)
        {
            FootballGameStatistic gameStatistic = db
                .FootballGameStatistics
                .Where(s => s.Id == id)
                .FirstOrDefault();

            return gameStatistic;
        }

        public IEnumerable<FootballGameStatisticTypeAdminModel> GameStatisticTypes()
        {
            var types = db.FootballGameStatisticTypes
                .Select(t => new FootballGameStatisticTypeAdminModel
                {
                    Id = t.id,
                    Name = t.Name               
                });

            return types;
        }
    }
}
