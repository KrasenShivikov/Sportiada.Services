
namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Interfaces;
    using Models;
    using Sportiada.Data.Models.Football;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;

    public class FootballGoalAdminService : IFootballGoalAdminService
    {
        private readonly SportiadaDbContext db;
        public FootballGoalAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public FootballGoalAdminModel ById(int id)
        {
            var goal = db.FootballGoals
                .Where(g => g.Id == id)
                .Select(g => new FootballGoalAdminModel
                {
                    Minute = g.Minute,
                    FirstHalf = g.FirstHalf,
                    Player = new FootballPlayerGameAdminModel
                    {
                        ProfileName = g.Player.ProfileName,
                        Country = g.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath
                    },
                    Type = new FootballGoalTypeAdminModel
                    {
                        Name = g.Type.Name,
                        Picture = g.Type.picture
                    }
                }).FirstOrDefault();

            return goal;
        }
        public void Create(int playerId, int gameStatisticId, int assistanceId, int typeId, int minute, bool firstHalf)
        {
            FootballGoal goal = new FootballGoal
            {
                PlayerId = playerId,
                GameStatisticId = gameStatisticId,
                AssistanceId = assistanceId,
                TypeId = typeId,
                Minute = minute,
                FirstHalf = firstHalf
            };

            db.Add(goal);
            db.SaveChanges();
        }

        public void Update(int id, int playerId, int gameStatisticId, int assistanceId, int typeId, int minute, bool firstHalf)
        {
            FootballGoal goal = GetGoal(id);

            goal.Id = id;
            goal.PlayerId = playerId;
            goal.GameStatisticId = gameStatisticId;
            goal.AssistanceId = assistanceId;
            goal.TypeId = typeId;
            goal.Minute = minute;
            goal.FirstHalf = firstHalf;

            db.Update(goal);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballGoal goal = GetGoal(id);

            db.Remove(goal);
            db.SaveChanges();
        }

        public FootballGoal GetGoal(int id)
        {
            FootballGoal goal = db.FootballGoals
                .Where(g => g.Id == id)
                .FirstOrDefault();

            return goal;
        }

        
    }
}
