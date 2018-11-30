namespace Sportiada.Services.Football.Implementations
{
    using Models.LineUp;
    using Models.PlayerIn;
    using Models.Reserve;
    using Models.PlayerOut;
    using Models.GoalAssistance;
    using Models.Goal;
    using Models.Card;
    using Models.GameStatitistic;
    using Models.Player;
    using Models.PlayerType;
    using Models.Team;
    using Models.Season;
    using Models.Side;
    using Models.Substitute;
    using Interfaces;
    using Sportiada.Data;
    using System.Linq;
    using System.Collections.Generic;
    using Sportiada.Web.Infrastructure.Extensions;
    using Services.Models;
    

    public class PlayerService : IPlayerService
    {
        private readonly SportiadaDbContext db;

        public PlayerService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public PlayerProfileModel ProfileById(int id)
          => this.db
            .FootballPlayers
            .Where(p => p.Id == id)
            .Select(p => new PlayerProfileModel
            {
                Id = p.Id,
                Name = p.Name,
                Type = new PlayerTypeModel
                {
                    Id = p.TypeId,
                    Name = p.Type.Name
                },
                Country = new CountryModel
                {
                    Name = p.Countries.Where(c => c.MainCountry == true).First().Country.Name,
                    ShortName = p.Countries.Where(c => c.MainCountry == true).First().Country.ShortName,
                    PicturePath = p.Countries.Where(c => c.MainCountry == true).First().Country.LargePicturePath
                },
                Picture = p.Sides.Where(s => s.UntilDate == null).First().PlayerPicture,
                PlayerNumber = p.Sides.Where(s => s.UntilDate == null).First().PlayerNumber,
                Seasons = this.db.Seasons.Select(s => new SeasonModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .OrderByDescending(o => o.Name)
                .ToList()
            }).First();

        public PlayerSeasonStatisticModel PlayerSeasonStatistic(int id, int seasonId)
        {

            var playerSides = this.db.FootballSidePlayers
                .Where(s => s.PlayerId == id)
                .Select(s => new SidePlayerGameStatitisticModel
                {
                    SideId = s.SideId,
                    PlayerId = s.PlayerId,
                    SeasonId = s.Side.SeasonId,
                    FromDate = s.FromDate,
                    UntilDate = s.UntilDate,
                    Team = new TeamModel
                    {
                        Id = s.Side.TeamId,
                        Name = s.Side.Team.Name
                    }
                });


            List<PlayerGameStatisticModel> games = new List<PlayerGameStatisticModel>();

            foreach (var side in playerSides)
            {
                var gamesQuery = this.db
                    .FootballGames
                    .Where(g => g.Round.Competition.SeasonId == seasonId)
                    .Select(g => g.GameStatistics
                                  .Where(gs => gs.SideId == side.SideId
                                && (gs.Game.Date <= side.UntilDate
                                || gs.Game.Date >= side.FromDate
                                || gs.Game.Date >= side.FromDate && gs.Game.Date <= side.UntilDate
                                || side.FromDate == null && side.UntilDate == null)));

                foreach (var gameStatistics in gamesQuery)
                {
                    foreach (var item in gameStatistics)
                    {
                        var stat = new GameSideStatisticModel
                        {
                            SideId = item.SideId,
                            Season = new SeasonModel
                            {
                                Id = side.SeasonId,
                                Name = this.db.Seasons.Where(s => s.Id == side.SeasonId).First().Name
                            },
                            GameId = item.GameId,
                            Team = side.Team.Name,
                            RivalTeam = this.db.FootballGameStatistics.Where(gs => gs.GameId == item.GameId && gs.SideId != item.SideId)
                            .Select(gs => new TeamModel
                            {
                                Id = gs.Side.TeamId,
                                Name = gs.Side.Team.Name
                            }).First(),
                            RivalTeamGoals = this.db.FootballGoals.Where(g => g.GameStatistic.GameId == item.GameId && g.GameStatistic.SideId != item.SideId && g.TypeId != 4)
                            .Select(g => new RivalGoalModel
                            {
                                Id = g.Id
                            }).ToList(),

                            LineUps = this.db.FootballLineUps.Where(l => l.GameStattisticId == item.Id).Select(l => new LineUpBaseModel
                            {
                                Id = l.Id,
                                PlayerId = l.PlayerId
                            }).ToList(),
                            Cards = this.db.FootballCards.Where(c => c.GameStatisticId == item.Id).Select(c => new CardBaseModel
                            {
                                PlayerId = c.PlayerId,
                                CardIcon = c.Type.Picture,
                                TypeId = c.TypeId
                            }).ToList(),

                            Goals = this.db.FootballGoals.Where(g => g.GameStatisticId == item.Id && g.TypeId != 4).Select(gl => new GoalBaseModel
                            {
                                PlayerId = gl.PlayerId,
                                TypeId = gl.TypeId,
                                Assistance = new GoalAssistanceBaseModel
                                {
                                    PlayerId = gl.Assistance.PlayerId
                                }
                            }).ToList(),
                            Substitutes = this.db.FootballSubstitutes.Where(s => s.GameStatisticId == item.Id).Select(s => new SubstituteBaseModel
                            {
                                PlayerIn = new PlayerInBaseModel
                                {
                                    PlayerId = s.PlayerIn.PlayerId,
                                    FirstHalf = s.PlayerIn.FirstHalf,
                                    Minute = s.PlayerIn.Minute,
                                    InIcon = s.PlayerIn.InIcon

                                },
                                PlayerOut = new PlayerOutBaseModel
                                {
                                    PlayerId = s.PlayerOut.PlayerId,
                                    Minute = s.PlayerOut.Minute,
                                    FirstHalf = s.PlayerOut.FirstHalf,
                                    OutIcon = s.PlayerOut.OutIcon
                                }
                            }).ToList(),
                            Reserves = this.db.FootballReserves.Where(r => r.GameStattisticId == item.Id).Select(r => new ResBaseModel
                            {
                                PlayerId = r.PlayerId
                            }).ToList()

                        };

                        var substituteIn = stat.Substitutes.Where(s => s.PlayerIn.PlayerId == side.PlayerId);
                        var substituteOut = stat.Substitutes.Where(s => s.PlayerOut.PlayerId == side.PlayerId);

                        var sideGame = new PlayerGameStatisticModel
                        {
                            GameId = stat.GameId,
                            PlayerId = id,
                            SeasonName = stat.Season.Name,
                            PlayerTeam = stat.Team,
                            RivalTeam = stat.RivalTeam.Name,
                            InLineUp = stat.LineUps.Where(l => l.PlayerId == side.PlayerId).Any() ? true : false,
                            IsReserve = stat.Reserves.Where(r => r.PlayerId == side.PlayerId).Any() 
                                        || stat.Substitutes.Where(s => s.PlayerIn.PlayerId == side.PlayerId).Any() ? true : false,
                            SubstituteIn = new SubstituteInModel
                            {
                                Minute = substituteIn.Any() ?
                                         substituteIn.First().PlayerIn.Minute.ToString()
                                        .MinutesAfterHalftime(substituteIn.First().PlayerIn.FirstHalf) : string.Empty,
                                InIcon = substituteIn.Any() ?
                                         substituteIn.First().PlayerIn.InIcon : string.Empty
                            },
                            substituteOut = new SubstituteOutModel
                            {
                                Minute = substituteOut.Any() ?
                                         substituteOut.First().PlayerOut.Minute.ToString()
                                        .MinutesAfterHalftime(substituteOut.First().PlayerOut.FirstHalf) : string.Empty,
                                InIcon = substituteOut.Any() ?
                                         substituteOut.First().PlayerOut.OutIcon : string.Empty
                            },
                            Sideline = !stat.LineUps.Where(l => l.PlayerId == side.PlayerId).Any()
                                   && !stat.Reserves.Where(r => r.PlayerId == side.PlayerId).Any()
                                   && !stat.Substitutes.Where(s => s.PlayerIn.PlayerId == side.PlayerId).Any()? true : false,
                            YellowCards = stat.Cards.Where(c => c.PlayerId == side.PlayerId && c.TypeId == 1).Count(),
                            RedCard = stat.Cards.Where(c => c.PlayerId == side.PlayerId && (c.TypeId == 2 || c.TypeId == 3)).Count(),
                            ScoredGoals = stat.Goals.Where(g => g.PlayerId == side.PlayerId && g.TypeId != 3).Count(),
                            GoalAssistances = stat.Goals.Where(g => g.Assistance.PlayerId == side.PlayerId).Count(),
                            ScoredPenalties = stat.Goals.Where(g => g.PlayerId == side.PlayerId && g.TypeId == 2).Count(),
                            OwnGoals = stat.Goals.Where(g => g.PlayerId == side.PlayerId && g.TypeId == 3).Count(),
                            AllowedGaols = stat.RivalTeamGoals.Count()
                        };

                        games.Add(sideGame);
                    }
                }
            };

            var playerProfile = this.ProfileById(games.First().PlayerId);

            var playerSeasonStat = new PlayerSeasonStatisticModel
            {
                Games = games,
                Id = playerProfile.Id,
                Country = playerProfile.Country,
                Name = playerProfile.Name,
                Picture = playerProfile.Picture,
                PlayerNumber = playerProfile.PlayerNumber,
                Seasons = playerProfile.Seasons,
                Type = playerProfile.Type
            };

            return playerSeasonStat;
        }
    }
}
