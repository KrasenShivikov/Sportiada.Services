namespace Sportiada.Services.Football.Implementations
{
    using Infrastructure.Constants;
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
    using Models.Country;
    using Models.Team;
    using Models.Season;
    using Models.Squad;
    using Models.Substitute;
    using Interfaces;
    using Sportiada.Data;
    using System.Linq;
    using System.Collections.Generic;
    using Infrastructure.Extensions;
    using Services.Admin.Models;
    using Services.Models;


    public class PlayerService : IPlayerService
    {
        private readonly SportiadaDbContext db;

        public PlayerService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PlayerProfileModel> PlayersBySquad(int squadId)
        {

            var players = db.SquadPlayers
                .Where(sp => sp.SquadId == squadId)
            .Select(p => new PlayerProfileModel
            {
                Id = p.PlayerId,
                Name = p.Player.FirstName + p.Player.LastName,
                Countries = p.Player.Countries
                            .Select(c => new CountryModel
                            {
                                Id = c.Country.Id,
                                Name = c.Country.Name,
                                ShortName = c.Country.ShortName,
                                PicturePath = c.Country.PicturePath,
                            }),
                Transfers = p.Player.Transfers.Any() ? p.Player.Transfers
                .Select(t => new FootballPlayerTransferAdminModel
                {
                    Id = t.Id,
                    Season = t.Season,
                    Date = t.Date,
                    CurrentTeam = t.CurrentTeam,
                    CurrentTeamCountryFlag = t.CurrentTeamCountryFlag,
                    CurrentTeamLogo = t.CurrentTeamLogo,
                    PlayerId = t.PlayerId,
                    OnLoan = t.OnLoan,
                    PreviousTeam = t.PreviousTeam,
                    PreviousTeamCountryFlag = t.PreviousTeamCountryFlag,
                    TransferPrice = t.TransferPrice,
                    PreviousTeamLogo = t.PreviousTeamLogo
                }).OrderBy(t => t.SplittedDate[2])
                     .ThenBy(t => t.SplittedDate[1])
                     .ThenBy(t => t.SplittedDate[0]) : null,
                BirthDate = p.Player.BirthDate.Value,
                BirthPlace = p.Player.BirthPlace,
                Foot = p.Player.Foot,
                Height = p.Player.Height,
                Picture = p.Picture,
                PlayerNumber = p.Number,
                Position = p.Position,
                JoinedInSummer = p.JoinedInSummer,
                LeftInSummer = p.LeftInSummer,
                JoinedInWinter = p.JoinedInWinter,
                LeftInWinter = p.LeftInWinter,
                OnLoan = p.OnLoan,
                SquadType = p.SquadType,
                Type = new PlayerTypeModel
                {
                    Name = p.SquadType
                },
                ContractStartDate = p.ContractStartDate,
                ContractEndDate = p.ContractEndDate
            });

            return players;
            
        }

        public PlayerProfileModel ProfileById(int id)
        {
            var player = this.db
            .FootballPlayers
            .Where(p => p.Id == id)
            .Select(p => new PlayerProfileModel
            {
                Id = p.Id,
                Name = p.FirstName + p.LastName,
                Countries = p.Countries
                            .Select(c => new CountryModel
                            {
                                Id = c.Country.Id,
                                Name = c.Country.Name,
                                ShortName = c.Country.ShortName,
                                PicturePath = c.Country.PicturePath,
                            }),
                Transfers = p.Transfers.Any() ? p.Transfers.Select(t => new FootballPlayerTransferAdminModel
                {
                    Id = t.Id,
                    Season = t.Season,
                    Date = t.Date,
                    CurrentTeam = t.CurrentTeam,
                    CurrentTeamCountryFlag = t.CurrentTeamCountryFlag,
                    CurrentTeamLogo = t.CurrentTeamLogo,
                    PlayerId = t.PlayerId,
                    OnLoan = t.OnLoan,
                    PreviousTeam = t.PreviousTeam,
                    PreviousTeamCountryFlag = t.PreviousTeamCountryFlag,
                    TransferPrice = t.TransferPrice,
                    PreviousTeamLogo = t.PreviousTeamLogo
                }).OrderBy(t => t.SplittedDate[2])
                         .ThenBy(t => t.SplittedDate[1])
                         .ThenBy(t => t.SplittedDate[0]) : null,
                Seasons = this.db.Seasons.Select(s => new SeasonModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .OrderByDescending(o => o.Name)
                .ToList()
            }).FirstOrDefault();

            return player;
        }


        //public PlayerSeasonStatisticModel PlayerSeasonStatistic(int id, int seasonId)
        //{

        //    var playerSquads = this.db.FootballSquadPlayer
        //        .Where(s => s.PlayerId == id)
        //        .Select(s => new SquadPlayerGameStatitisticModel
        //        {
        //            SquadId = s.SquadId,
        //            PlayerId = s.PlayerId,
        //            SeasonId = s.Squad.SeasonId,
        //            Team = new TeamModel
        //            {
        //                Id = s.Squad.TeamId,
        //                Name = s.Squad.Team.Name
        //            }
        //        });


        //    List<PlayerGameStatisticModel> games = new List<PlayerGameStatisticModel>();

        //    foreach (var squad in playerSquads)
        //    {
        //        var gamesQuery = this.db
        //            .FootballGames
        //            .Where(g => g.Round.Competition.SeasonId == seasonId)
        //            .Select(g => g.GameStatistics
        //                          .Where(gs => gs.SquadId == squad.SquadId));

        //        foreach (var gameStatistics in gamesQuery)
        //        {
        //            foreach (var item in gameStatistics)
        //            {
        //                var stat = new GameSideStatisticModel
        //                {
        //                    SideId = item.SquadId,
        //                    Season = new SeasonModel
        //                    {
        //                        Id = squad.SeasonId,
        //                        Name = this.db.Seasons.Where(s => s.Id == squad.SeasonId).FirstOrDefault().Name
        //                    },

        //                    GameId = item.GameId,
        //                    Team = squad.Team.Name,
        //                    RivalTeam = this.db.FootballGameStatistics.Where(gs => gs.GameId == item.GameId && gs.SquadId != item.SquadId)
        //                    .Select(gs => new TeamModel
        //                    {
        //                        Id = gs.Squad.TeamId,
        //                        Name = gs.Squad.Team.Name
        //                    }).First(),
        //                    RivalTeamGoals = this.db.FootballGoals.Where(g => g.GameStatistic.GameId == item.GameId && g.GameStatistic.SquadId != item.SquadId && (g.Type.Name == "Гол" || g.Type.Name == "Гол от дузпа"))
        //                    .Select(g => new RivalGoalModel
        //                    {
        //                        Id = g.Id
        //                    }).ToList(),

        //                    LineUps = this.db.FootballLineUps.Where(l => l.GameStattisticId == item.Id).Select(l => new LineUpBaseModel
        //                    {
        //                        Id = l.Id,
        //                        PlayerId = l.PlayerId
        //                    }).ToList(),
        //                    Cards = this.db.FootballCards.Where(c => c.GameStatisticId == item.Id).Select(c => new CardBaseModel
        //                    {
        //                        PlayerId = c.PlayerId,
        //                        CardIcon = c.Type.Picture,
        //                        TypeId = c.TypeId,
        //                        TypeName = c.Type.Name
        //                    }).ToList(),

        //                    Goals = this.db.FootballGoals.Where(g => g.GameStatisticId == item.Id && (g.Type.Name == Constants.GOAL || g.Type.Name == Constants.PENALTY_GOAL)).Select(gl => new GoalBaseModel
        //                    {
        //                        PlayerId = gl.PlayerId,
        //                        TypeId = gl.TypeId,
        //                        TypeName = gl.Type.Name,
        //                        Assistance = gl.Assistance != null ? new GoalAssistanceBaseModel
        //                        {
        //                            PlayerId = gl.Assistance.PlayerId
        //                        } : null
        //                    }).ToList(),
        //                    Substitutes = this.db.FootballSubstitutes.Where(s => s.GameStatisticId == item.Id).Select(s => new SubstituteBaseModel
        //                    {
        //                        PlayerIn = new PlayerInBaseModel
        //                        {
        //                            PlayerId = s.PlayerIn.PlayerId,
        //                            InIcon = s.PlayerIn.InIcon

        //                        },
        //                        PlayerOut = new PlayerOutBaseModel
        //                        {
        //                            PlayerId = s.PlayerOut.PlayerId,
        //                            OutIcon = s.PlayerOut.OutIcon
        //                        }
        //                    }).ToList(),
        //                    Reserves = this.db.FootballReserves.Where(r => r.GameStattisticId == item.Id).Select(r => new ResBaseModel
        //                    {
        //                        PlayerId = r.PlayerId
        //                    }).ToList()

        //                };

        //                var substituteIn = stat.Substitutes.Where(s => s.PlayerIn.PlayerId == squad.PlayerId);
        //                var substituteOut = stat.Substitutes.Where(s => s.PlayerOut.PlayerId == squad.PlayerId);

        //                var squadGame = new PlayerGameStatisticModel
        //                {
        //                    GameId = stat.GameId,
        //                    PlayerId = id,
        //                    SeasonName = stat.Season.Name,
        //                    PlayerTeam = stat.Team,
        //                    RivalTeam = stat.RivalTeam.Name,
        //                    InLineUp = stat.LineUps.Where(l => l.PlayerId == squad.PlayerId).Any() ? true : false,
        //                    IsReserve = stat.Reserves.Where(r => r.PlayerId == squad.PlayerId).Any()
        //                                || stat.Substitutes.Where(s => s.PlayerIn.PlayerId == squad.PlayerId).Any() ? true : false,
        //                    SubstituteIn = new SubstituteInModel
        //                    {
        //                        Minute = substituteIn.Any() ?
        //                                 substituteIn.First().PlayerIn.Minute.ToString()
        //                                .MinutesAfterHalftime(substituteIn.First().PlayerIn.FirstHalf) : string.Empty,
        //                        InIcon = substituteIn.Any() ?
        //                                 substituteIn.First().PlayerIn.InIcon : string.Empty
        //                    },
        //                    SubstituteOut = new SubstituteOutModel
        //                    {
        //                        Minute = substituteOut.Any() ?
        //                                 substituteOut.First().PlayerOut.Minute.ToString()
        //                                .MinutesAfterHalftime(substituteOut.First().PlayerOut.FirstHalf) : string.Empty,
        //                        InIcon = substituteOut.Any() ?
        //                                 substituteOut.First().PlayerOut.OutIcon : string.Empty
        //                    },
        //                    Sideline = !stat.LineUps.Where(l => l.PlayerId == squad.PlayerId).Any()
        //                           && !stat.Reserves.Where(r => r.PlayerId == squad.PlayerId).Any()
        //                           && !stat.Substitutes.Where(s => s.PlayerIn.PlayerId == squad.PlayerId).Any() ? true : false,
        //                    YellowCards = stat.Cards.Where(c => c.PlayerId == squad.PlayerId && c.TypeName == Constants.YELLOW_CARD).Count(),
        //                    RedCard = stat.Cards.Where(c => c.PlayerId == squad.PlayerId && (c.TypeName == Constants.RED_CARD || c.TypeName == Constants.SECOND_YELLOW_CARD)).Count(),
        //                    ScoredGoals = stat.Goals.Where(g => g.PlayerId == squad.PlayerId && (g.TypeName == Constants.GOAL || g.TypeName == Constants.PENALTY_GOAL)).Count(),
        //                    GoalAssistances = stat.Goals.Where(g => g.Assistance != null && g.Assistance.PlayerId == squad.PlayerId).Count(),
        //                    ScoredPenalties = stat.Goals.Where(g => g.PlayerId == squad.PlayerId && g.TypeName == Constants.PENALTY_GOAL).Count(),
        //                    OwnGoals = stat.Goals.Where(g => g.PlayerId == squad.PlayerId && g.TypeName == Constants.OWN_GOAL).Count(),
        //                    AllowedGoals = stat.RivalTeamGoals.Count()
        //                };

        //                games.Add(squadGame);
        //            }
        //        }
        //    };

        //    var playerProfile = this.ProfileById(games.First().PlayerId);

        //    var playerSeasonStat = new PlayerSeasonStatisticModel
        //    {
        //        Games = games,
        //        Id = playerProfile.Id,
        //        Country = playerProfile.Country,
        //        Name = playerProfile.Name,
        //        Picture = playerProfile.Picture,
        //        PlayerNumber = playerProfile.PlayerNumber,
        //        Transfers = playerProfile.Transfers,
        //        Seasons = playerProfile.Seasons,
        //        //Type = playerProfile.Type
        //    };

        //    return playerSeasonStat;
        //}
    }
}

