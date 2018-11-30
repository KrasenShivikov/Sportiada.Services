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
    using Models.Side;
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
                Side = this.db.FootballSides.Where(s => s.SeasonId == seasonId && s.TeamId == teamId).Select(s => new SideGameListModel
                {
                    Id = s.Id,
                    Name = s.Name,
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
                }).First(),
                Cards = this.db.FootballCards.Where(c => c.GameStatistic.Game.Round.Competition.SeasonId == seasonId && c.GameStatistic.Side.TeamId == teamId)
                .Select(c => new CardTeamSeasonStatisticModel
                {
                    Id = c.Id,
                    Player = new PlayerTeamSeasonStatisticModel
                    {
                        Id = c.PlayerId,
                        Name = c.Player.Name
                    },
                    Type = new CardTypeModel
                    {
                        Id = c.TypeId,
                        Name = c.Type.Name,
                        Picture = c.Type.Picture
                    }
                }).ToList(),
                Goals = this.db.FootballGoals.Where(g => g.GameStatistic.Game.Round.Competition.SeasonId == seasonId && g.GameStatistic.Side.TeamId == teamId)
                .Select(g => new GoalTeamSeasonStatisticModel
                {
                    Id = g.Id,
                    Scorer = new PlayerTeamSeasonStatisticModel
                    {
                        Id = g.PlayerId,
                        Name = g.Player.Name
                    }
                }).ToList(),
                GoalAssistances = this.db.FootballGoalAssistances.Where(ga => ga.Goal.GameStatistic.Game.Round.Competition.SeasonId == seasonId
                && ga.Goal.GameStatistic.Side.TeamId == teamId && ga.Player.Name != "Неизвестен")
                .Select(ga => new GoalAssistanceTeamSeasonStatisticModel
                {
                    Id = ga.Id,
                    GoalAssistant = new PlayerTeamSeasonStatisticModel
                    {
                        Id = ga.PlayerId,
                        Name = ga.Player.Name
                    }
                }).ToList(),
                Games = this.db.FootballGameStatistics.Where(gs => gs.Game.Round.Competition.SeasonId == seasonId && gs.Side.TeamId == teamId)
                .Select(gs => new GameModel
                {
                    Date = gs.Game.Date,
                    Id = gs.Game.Id,
                    Season = gs.Game.Round.Competition.Season.Name,
                    Statistics = gs.Game.GameStatistics.Select(g => new GameTeamStattisticListModel
                    {
                        Id = gs.Id,
                        Side = new SideGameListModel
                        {
                            Id = gs.SideId,
                            Name = gs.Side.Name,
                            Season = new SeasonModel
                            {
                                Id = gs.Game.Round.Competition.Season.Id,
                                Name = gs.Game.Round.Competition.Season.Name
                            },
                            Team = new TeamModel
                            {
                                Id = gs.Side.TeamId,
                                Name = gs.Side.Team.Name
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
