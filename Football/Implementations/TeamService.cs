namespace Sportiada.Services.Football.Implementations
{
    using Interfaces;
    using Models.Game;
    using Models.GameStatitistic;
    using Models.GameStatitisticType;
    using Models.Goal;
    using Models.GoalAssistance;
    using Models.Card;
    using Models.CardType;
    using Models.Player;
    using Models.Season;
    using Models.Squad;
    using Models.Team;
    using Sportiada.Data;
    using System.Linq;
    
    public class TeamService : ITeamService
    {
        private readonly SportiadaDbContext db;

        public TeamService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public TeamSeasonStatisticModel TeamSeasonStatistic(int seasonId, int teamId)
        {

            var teamSeasonStat = new TeamSeasonStatisticModel
            {
                Side = this.db.FootballSquads.Where(s => s.SeasonId == seasonId && s.TeamId == teamId).Select(s => new SquadGameListModel
                {
                    Id = s.Id,
                    Name = $"{s.Team.Name} - {s.Season.Name}",
                    Season = new SeasonModel
                    {
                        Id = seasonId,
                        Name = s.Season.Name
                    },
                    Team = new TeamModel
                    {
                        Id = s.TeamId,
                        Name = s.Team.Name
                    }
                }).FirstOrDefault(),
                Cards = this.db.FootballCards.Where(c => c.GameStatistic.Game.Round.Competition.SeasonId == seasonId && c.GameStatistic.Squad.TeamId == teamId)
                .Select(c => new CardTeamSeasonStatisticModel
                {
                    Id = c.Id,
                    Player = new PlayerTeamSeasonStatisticModel
                    {
                        Id = c.PlayerId,
                        Name = c.Player.ProfileName
                    },
                    Type = new CardTypeModel
                    {
                        Id = c.TypeId,
                        Name = c.Type.Name,
                        Picture = c.Type.Picture
                    }
                }).ToList(),
                Goals = this.db.FootballGoals.Where(g => g.GameStatistic.Game.Round.Competition.SeasonId == seasonId && g.GameStatistic.Squad.TeamId == teamId)
                .Select(g => new GoalTeamSeasonStatisticModel
                {
                    Id = g.Id,
                    Scorer = new PlayerTeamSeasonStatisticModel
                    {
                        Id = g.PlayerId,
                        Name = g.Player.ProfileName
                    }
                }).ToList(),
                GoalAssistances = this.db.FootballGoalAssistances.Where(ga => ga.Goal.GameStatistic.Game.Round.Competition.SeasonId == seasonId
                && ga.Goal.GameStatistic.Squad.TeamId == teamId && ga.Player.ProfileName != "Неизвестен")
                .Select(ga => new GoalAssistanceTeamSeasonStatisticModel
                {
                    Id = ga.Id,
                    GoalAssistant = new PlayerTeamSeasonStatisticModel
                    {
                        Id = ga.PlayerId,
                        Name = ga.Player.ProfileName
                    }
                }).ToList(),
                Games = this.db.FootballGameStatistics.Where(gs => gs.Game.Round.Competition.SeasonId == seasonId && gs.Squad.TeamId == teamId)
                .Select(gs => new GameModel
                {
                    Date = gs.Game.Date,
                    Id = gs.Game.Id,
                    Season = gs.Game.Round.Competition.Season.Name,
                    Statistics = gs.Game.GameStatistics.Select(g => new GameTeamStattisticListModel
                    {
                        Id = gs.Id,
                        Squad = new SquadGameListModel
                        {
                            Id = gs.SquadId,
                            Name = $"{gs.Squad.Team.Name} - {gs.Squad.Season.Name}",
                            Season = new SeasonModel
                            {
                                Id = gs.Game.Round.Competition.Season.Id,
                                Name = gs.Game.Round.Competition.Season.Name
                            },
                            Team = new TeamModel
                            {
                                Id = gs.Squad.TeamId,
                                Name = gs.Squad.Team.Name
                            },
                        },
                        Type = new GameStatisticTypeModel
                        {
                            id = gs.TypeId,
                            Name = gs.Type.Name
                        }
                    }).ToList(),
                    Tournament = gs.Game.Round.Competition.Tournament.Name,
                    HostGoals = gs.Game.GameStatistics.Where(gst => gst.TypeId == 1).First().Goals.Count(),
                    GuestGoals = gs.Game.GameStatistics.Where(gst => gst.TypeId == 2).First().Goals.Count()
                }).ToList(),
            };

            return teamSeasonStat;
        }
    }
}
