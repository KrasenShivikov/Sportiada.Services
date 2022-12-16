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
    public class FootballGoalAssistanceAdminService : IFootballGoalAssistanceAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballGoalAssistanceAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public FootballGoalAssistanceAdminModel ById(int id)
        {
            var assistance = db.FootballGoalAssistances
                .Where(a => a.Id == id)
                .Select(a => new FootballGoalAssistanceAdminModel
                {
                    Id = id,
                    Player = new FootballPlayerGameAdminModel
                    {
                        ProfileName = a.Player.ProfileName,
                        Country = a.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath
                    },
                    Goal = new FootballGoalAdminModel
                    {
                        Minute = a.Goal.Minute,
                        FirstHalf = a.Goal.FirstHalf,
                        Player = new FootballPlayerGameAdminModel
                        {
                            ProfileName = a.Goal.Player.ProfileName,
                            Country = a.Goal.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath
                        }
                    }
                }).FirstOrDefault();

            return assistance;
        }

        public void Create(int playerId, int goalId)
        {
            FootballGoalAssistance assistance = new FootballGoalAssistance
            {
                PlayerId = playerId,
                GoalId = goalId
            };

            db.Add(assistance);
            db.SaveChanges();
        }

        public void Update(int id, int playerId, int goalId)
        {
            FootballGoalAssistance assistance = GetAssistance(id);

            assistance.Id = id;
            assistance.PlayerId = playerId;
            assistance.GoalId = goalId;

            db.Update(assistance);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballGoalAssistance assistance = GetAssistance(id);

            db.Remove(assistance);
            db.SaveChanges();
        }

        public FootballGoalAssistance GetAssistance(int id)
        {
            FootballGoalAssistance assitance = db.FootballGoalAssistances
                .Where(a => a.Id == id)
                .FirstOrDefault();

            return assitance;
        }
    }
}
