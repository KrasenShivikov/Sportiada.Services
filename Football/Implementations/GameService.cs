namespace Sportiada.Services.Football.Implementations
{
    using Interfaces;
    using Models.Card;
    using Models.CardType;
    using Models.Coach;
    using Models.Competitiion;
    using Models.CompetitionType;
    using Models.Game;
    using Models.GameReferee;
    using Models.GameStatitistic;
    using Models.GameStatitisticType;
    using Models.Goal;
    using Models.GoalAssistance;
    using Models.GoalType;
    using Models.LineUp;
    using Models.Player;
    using Models.PlayerIn;
    using Models.PlayerOut;
    using Models.Referee;
    using Models.Reserve;
    using Models.RefereeType;
    using Models.Round;
    using Models.Season;
    using Models.Side;
    using Models.Sideline;
    using Models.SidelineReason;
    using Models.Stadium;
    using Models.Substitute;
    using Models.Team;
    using Models.Tournament;
    using Sportiada.Data;
    using Services.Models;
    using System.Collections.Generic;
    using System.Linq;
    
    public class GameService : IGameService
    {
        private readonly SportiadaDbContext db;

        public GameService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public GameViewModel ById(int id)
        {

            var game = this.db.FootballGames.Where(g => g.Id == id).Select(g => new GameViewModel
            {
                Data = new GameWithStatisticModel
                {
                    Attendance = g.Attendance,
                    Date = g.Date,
                    Id = g.Id,
                    Round = new RoundModel
                    {
                        Id = g.Round.Id,
                        Name = g.Round.Name,
                        Competition = new CompetitionModel
                        {
                            Id = g.Round.Competition.Id,
                            Season = new SeasonModel
                            {
                                Id = g.Round.Competition.Season.Id,
                                Name = g.Round.Competition.Season.Name
                            },
                            Type = new CompetitionTypeModel
                            {
                                Id = g.Round.Competition.Type.Id,
                                Name = g.Round.Competition.Type.Name
                            },
                            Tournament = new TournamentModel
                            {
                                Id = g.Round.Competition.Tournament.Id,
                                Name = g.Round.Competition.Tournament.Name
                            }
                        }
                    },
                    TeamsStatistic = g.GameStatistics.Select(gs => new GameTeamStatitisticFullModel
                    {
                        BallPossession = gs.BallPossession,
                        Corners = gs.Corners,
                        Offsides = gs.Offsides,
                        Fouls = gs.Fouls,
                        ShotsOnTarget = gs.ShotsOnTarget,
                        ShotsWide = gs.ShotsWide,
                        Id = gs.Id,
                        LineUps = gs.LineUps.Select(l => new LineUpModel
                        {
                            Id = l.Id,
                            Player = new PlayerModel
                            {
                                Id = l.Player.Id,
                                Name = l.Player.Name,
                                PlayerNumber = l.Player.Sides.Where(s => s.Side.Id == gs.Side.Id).First().PlayerNumber,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = l.Player.Countries.Where(c => c.MainCountry == true).First().Country.LargePicturePath
                                }
                            }
                        }).ToList(),
                        Sidelines = gs.Sidelines.Select(s => new SidelineModel
                        {
                            Id = s.Id,
                            Reason = new SidelineReasonModel
                            {
                                Id = s.Reason.Id,
                                Name = s.Reason.Name,
                                Picture = s.Reason.Picture
                            },
                            Player = new PlayerModel
                            {
                                Id = s.Player.Id,
                                Name = s.Player.Name,
                                PlayerNumber = s.Player.Sides.Where(si => si.SideId == gs.SideId).First().PlayerNumber,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = s.Player.Countries.Where(c => c.MainCountry == true).First().Country.LargePicturePath
                                }
                            }
                        }).ToList(),
                        Cards = gs.Cards.Select(c => new CardModel
                        {
                            Id = c.Id,
                            Type = new CardTypeModel
                            {
                                Id = c.Type.Id,
                                Name = c.Type.Name,
                                Picture = c.Type.Picture
                            },
                            Minute = c.Minute,
                            FirstHalf = c.FirstHalf,
                            Player = new PlayerModel
                            {
                                Id = c.Player.Id,
                                Name = c.Player.Name,
                                PlayerNumber = c.Player.Sides.Where(s => s.SideId == gs.SideId).First().PlayerNumber,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = c.Player.Countries.Where(co => co.MainCountry == true).First().Country.LargePicturePath
                                }
                            }
                        }).ToList(),
                        Side = new SideGameModel
                        {
                            Id = gs.Side.Id,
                            Name = gs.Side.Name,
                            Team = new TeamGameModel
                            {
                                Id = gs.Side.TeamId,
                                Name = gs.Side.Team.Name,
                                Stadium = gs.Side
                                       .Team
                                       .Stadiums
                                       .OrderByDescending(s => s.Id)
                                       .Select(s => new StadiumModel
                                       {
                                           Id = s.Id,
                                           Name = s.Name,
                                           City = s.City
                                       }).First(),
                                Logo = gs.Side.Team.Logo
                            },

                            Coach = gs.Side
                                      .Coaches
                                      .Where(c => c.Description == "Главен" && c.UntilDate == null)
                                      .Select(c => new CoachGameModel
                                      {
                                          Id = c.CoachId,
                                          Name = c.Coach.Name,
                                          Country = new CountryFootballGameModel
                                          {
                                              PicturePath = c.Coach.Country.LargePicturePath
                                          }
                                      }).FirstOrDefault(),
                        },
                        Goals = gs.Goals.Select(gl => new GoalModel
                        {
                            Id = gl.Id,
                            Minute = gl.Minute,
                            FirstHalf = gl.FirstHalf,
                            Type = new GoalTypeModel
                            {
                                Id = gl.Type.Id,
                                Name = gl.Type.Name,
                                picture = gl.Type.picture
                            },
                            Player = new PlayerModel
                            {
                                Id = gl.Player.Id,
                                Name = gl.Player.Name,
                                //PlayerNumber = gl.Player.Sides.Where(s => s.SideId == gs.SideId).First().PlayerNumber,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = gl.Player.Countries.Where(c => c.MainCountry == true).First().Country.LargePicturePath
                                }
                            },
                            Assistance = new GoalAssistanceModel
                            {
                                Id = gl.Id,
                                Player = new PlayerModel
                                {
                                    Id = gl.Assistance.Player.Id,
                                    Name = gl.Assistance.Player.Name,
                                    //PlayerNumber = gl.Assistance.Player.Sides.Where(s => s.SideId == gs.SideId).First().PlayerNumber,
                                    Country = new CountryFootballGameModel
                                    {
                                        PicturePath = gl.Assistance.Player.Countries.Where(c => c.MainCountry == true).First().Country.LargePicturePath
                                    }
                                }
                            }
                        }).ToList(),
                        Substitutes = gs.Substitutes.Select(s => new SubstituteModel
                        {
                            Id = s.Id,
                            PlayerIn = new PlayerInModel
                            {
                                Id = s.PlayerInId,
                                FirstHalf = s.PlayerIn.FirstHalf,
                                Minute = s.PlayerIn.Minute,
                                Player = new PlayerModel
                                {
                                    Id = s.PlayerIn.PlayerId,
                                    Name = s.PlayerIn.Player.Name,
                                    PlayerNumber = s.PlayerIn.Player.Sides.Where(si => si.SideId == gs.SideId).First().PlayerNumber,
                                    Country = new CountryFootballGameModel
                                    {
                                        PicturePath = s.PlayerIn.Player.Countries.Where(c => c.MainCountry == true).First().Country.LargePicturePath
                                    }
                                },
                                InIcon = s.PlayerIn.InIcon
                            },
                            PlayerOut = new PlayerOutModel
                            {
                                Id = s.PlayerOut.Id,
                                FirstHalf = s.PlayerOut.FirstHalf,
                                Minute = s.PlayerOut.Minute,
                                Player = new PlayerModel
                                {
                                    Id = s.PlayerOut.PlayerId,
                                    Name = s.PlayerOut.Player.Name,
                                    PlayerNumber = s.PlayerOut.Player.Sides.Where(si => si.SideId == gs.SideId).First().PlayerNumber,
                                    Country = new CountryFootballGameModel
                                    {
                                        PicturePath = s.PlayerOut.Player.Countries.Where(c => c.MainCountry == true).First().Country.LargePicturePath
                                    }
                                },
                                OutIcon = s.PlayerOut.OutIcon
                            }
                        }).ToList(),
                        Reserves = gs.Reserves.Select(r => new ResModel
                        {
                            Id = r.Id,
                            Player = new PlayerModel
                            {
                                Id = r.PlayerId,
                                Name = r.Player.Name,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = r.Player.Countries.Where(c => c.MainCountry == true).First().Country.LargePicturePath
                                },
                                PlayerNumber = r.Player.Sides.Where(s => s.Side.Id == gs.Side.Id).First().PlayerNumber
                            }
                        }).ToList(),
                        Type = new GameStatisticTypeModel
                        {
                            id = gs.Type.id,
                            Name = gs.Type.Name
                        },
                    }).ToList(),
                    Referees = g.Referees.Select(gr => new GameRefereeModel
                    {
                        Id = gr.Id,
                        Referee = new RefereeModel
                        {
                            Id = gr.Referee.Id,
                            Name = gr.Referee.Name,
                            Country = new CountryFootballGameModel
                            {
                                PicturePath = gr.Referee.Country.LargePicturePath
                            }
                        },
                        Type = new RefereeTypeModel
                        {
                            Id = gr.Type.Id,
                            Name = gr.Type.Name,
                        }
                    }).ToList(),
                }
            }).First();

            return game;
        }

        public IEnumerable<GameModel> LastTwentyGames()
           => this.db
            .FootballGames
            .OrderByDescending(g => g.Date)
            .Take(20)
            .Select(g => new GameModel
            {
                Id = g.Id,
                Date = g.Date,
                HostGoals = g.GameStatistics.Where(gs => gs.TypeId == 1).First().Goals.Where(gl => gl.TypeId != 4 && gl.TypeId != 5).Count(),
                GuestGoals = g.GameStatistics.Where(gs => gs.TypeId == 2).First().Goals.Where(gl => gl.TypeId != 4 && gl.TypeId != 5).Count(),
                Statistics = g.GameStatistics.Select(gs => new GameTeamStattisticListModel
                {
                    Id = gs.Id,
                    Type = new GameStatisticTypeModel
                    {
                        id = gs.Type.id,
                        Name = gs.Type.Name
                    },
                    Side = new SideGameListModel
                    {
                        Id = gs.Side.Id,
                        Name = gs.Side.Name,
                        Team = new TeamModel
                        {
                            Id = gs.Side.Team.Id,
                            Name = gs.Side.Team.Name
                        }
                    }
                }).ToList(),
                Season = g.Round.Competition.Season.Name,
                Tournament = g.Round.Competition.Tournament.Name

            });

        public IEnumerable<GameModel> ByRound(int RoundId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GameModel> BySeasonIdByCompetition(int seasonId, int competitionId)
        => this.db
            .FootballGames
            .Where(g => g.Round.Competition.SeasonId == seasonId && g.Round.CompetitionId == competitionId)
            .Select(g => new GameModel
            {
                Id = g.Id,
                Date = g.Date,
                HostGoals = g.GameStatistics.Where(gs => gs.TypeId == 1).First().Goals.Count(),
                GuestGoals = g.GameStatistics.Where(gs => gs.TypeId == 2).First().Goals.Count(),
                Statistics = g.GameStatistics.Select(gs => new GameTeamStattisticListModel
                {
                    Id = gs.Id,
                    Type = new GameStatisticTypeModel
                    {
                        id = gs.Type.id,
                        Name = gs.Type.Name
                    },
                    Side = new SideGameListModel
                    {
                        Id = gs.Side.Id,
                        Name = gs.Side.Name,
                        Team = new TeamModel
                        {
                            Id = gs.Side.Team.Id,
                            Name = gs.Side.Team.Name
                        }
                    }
                }).ToList(),
                Season = g.Round.Competition.Season.Name,
                Tournament = g.Round.Competition.Tournament.Name
            });
    }
}
