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
    using Models.Squad;
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
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Sportiada.Data.Models.Football;

    public class GameService : IGameService
    {
        private readonly SportiadaDbContext db;

        public GameService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<GameWithStatisticModel> GamesBySquadBySeason(int squadId, int seasonId)
        {
            var games = db.FootballGames
                .Where(g => g.Round.Competition.SeasonId == seasonId && g.GameStatistics.Exists(gs => gs.SquadId == squadId));

            var result = GetGames(games);

            return result;
        }

        public GameFinalModel ById(int id)
        {

            var game = this.db.FootballGames.Where(g => g.Id == id).Select(g => new GameFinalModel
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
                                Name = l.Player.FirstName + l.Player.LastName,
                                PlayerNumber = l.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Number,
                                PlayerPicture = l.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Picture,
                                Position = l.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Position,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = l.Player.Countries.Where(c => c.MainCountry == true).First().Country.PicturePath
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
                                Name = s.Player.FirstName + s.Player.LastName,
                                PlayerNumber = s.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Number,
                                PlayerPicture = s.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Picture,
                                Position = s.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Position,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = s.Player.Countries.Where(c => c.MainCountry == true).First().Country.PicturePath
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
                                Name = c.Player.FirstName + c.Player.LastName,
                                PlayerNumber = c.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Number,
                                PlayerPicture = c.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Picture,
                                Position = c.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Position,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = c.Player.Countries.Where(co => co.MainCountry == true).First().Country.PicturePath
                                }
                            }
                        }).ToList(),
                        Squad = new SquadGameModel
                        {
                            Id = gs.Squad.Id,
                            Name = $"{gs.Squad.Team.Name} - {gs.Squad.Season.Name}",
                            Team = new TeamGameModel
                            {
                                Id = gs.Squad.TeamId,
                                Name = gs.Squad.Team.Name,
                                Stadium = gs.Squad
                                           .Team
                                           .Stadiums
                                           .OrderByDescending(s => s.Id)
                                           .Select(s => new StadiumModel
                                           {
                                               Id = s.Id,
                                               Name = s.Name,
                                               City = s.City
                                           }).First(),
                                Logo = gs.Squad.Team.Logo
                            },

                            Coach = gs.Squad
                                          .Coaches
                                          .Select(c => new CoachGameModel
                                          {
                                              Id = c.CoachId,
                                              Name = $"{c.Coach.FirstName} {c.Coach.LastName}",
                                              Country = new CountryFootballGameModel
                                              {
                                                  PicturePath = c.Coach.Country.PicturePath
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
                                Name = gl.Player.ProfileName,
                                PlayerNumber = gl.Player.Squads.Where(s => s.SquadId == gs.SquadId).First().Number,
                                PlayerPicture = gl.Player.Squads.Where(s => s.SquadId == gs.SquadId).First().Picture,
                                Position = gl.Player.Squads.Where(s => s.SquadId == gs.SquadId).First().Position,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = gl.Player.Countries.Where(c => c.MainCountry == true).First().Country.PicturePath
                                }
                            },
                            Assistance = gl.Assistance != null ? new GoalAssistanceModel
                            {
                                Id = gl.Id,
                                Player = new PlayerModel
                                {
                                    Id = gl.Assistance.Player.Id,
                                    Name = gl.Assistance.Player.ProfileName,
                                    PlayerNumber = gl.Assistance.Player.Squads.Where(s => s.SquadId == gs.SquadId).FirstOrDefault().Number,
                                    PlayerPicture = gl.Assistance.Player.Squads.Where(s => s.SquadId == gs.SquadId).FirstOrDefault().Picture,
                                    Position = gl.Assistance.Player.Squads.Where(s => s.SquadId == gs.SquadId).FirstOrDefault().Position,
                                    Country = new CountryFootballGameModel
                                    {
                                        PicturePath = gl.Assistance.Player.Countries.Where(c => c.MainCountry == true).First().Country.PicturePath
                                    }
                                }
                            } : null
                        }).ToList(),
                        Substitutes = gs.Substitutes.Select(s => new SubstituteModel
                        {
                            Id = s.Id,
                            Minute = s.Minute.ToString(),
                            FirstHalf = s.firstHalf,
                            PlayerIn = new PlayerInModel
                            {
                                Id = s.PlayerInId,
                                Player = new PlayerModel
                                {
                                    Id = s.PlayerIn.PlayerId,
                                    Name = s.PlayerIn.Player.FirstName + s.PlayerIn.Player.LastName,
                                    PlayerNumber = s.PlayerIn.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Number,
                                    PlayerPicture = s.PlayerIn.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Picture,
                                    Position = s.PlayerIn.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Position,
                                    Country = new CountryFootballGameModel
                                    {
                                        PicturePath = s.PlayerIn.Player.Countries.Where(c => c.MainCountry == true).First().Country.PicturePath
                                    }
                                }
                            },
                            PlayerOut = new PlayerOutModel
                            {
                                Id = s.PlayerOut.Id,
                                Player = new PlayerModel
                                {
                                    Id = s.PlayerOut.PlayerId,
                                    Name = s.PlayerOut.Player.ProfileName,
                                    PlayerNumber = s.PlayerOut.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Number,
                                    PlayerPicture = s.PlayerOut.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Picture,
                                    Position = s.PlayerOut.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Position,
                                    Country = new CountryFootballGameModel
                                    {
                                        PicturePath = s.PlayerOut.Player.Countries.Where(c => c.MainCountry == true).First().Country.PicturePath
                                    }
                                }
                            }
                        }).ToList(),
                        Reserves = gs.Reserves.Select(r => new ResModel
                        {
                            Id = r.Id,
                            Player = new PlayerModel
                            {
                                Id = r.PlayerId,
                                Name = r.Player.ProfileName,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = r.Player.Countries.Where(c => c.MainCountry == true).FirstOrDefault().Country.PicturePath
                                },
                                PlayerNumber = r.Player.Squads.Where(s => s.SquadId == gs.SquadId).FirstOrDefault().Number,
                                PlayerPicture = r.Player.Squads.Where(s => s.SquadId == gs.SquadId).FirstOrDefault().Picture,
                                Position = r.Player.Squads.Where(s => s.SquadId == gs.SquadId).FirstOrDefault().Position,
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
                                PicturePath = gr.Referee.Country.PicturePath
                            }
                        },
                        Type = new RefereeTypeModel
                        {
                            Id = gr.Type.Id,
                            Name = gr.Type.Name,
                        }
                    }).ToList()
                }
            }).FirstOrDefault();

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
                HostGoals = g.GameStatistics.Where(gs => gs.TypeId == 1).First().Goals.Where(gl => gl.Type.Name.ToLower() == "гол"|| gl.Type.Name.ToLower() == "гол от дузпа").Count() +
                            g.GameStatistics.Where(gs => gs.TypeId == 2).First().Goals.Where(gl => gl.Type.Name.ToLower() == "автогол").Count(),
                GuestGoals = g.GameStatistics.Where(gs => gs.TypeId == 2).First().Goals.Where(gl => gl.Type.Name.ToLower() == "гол" || gl.Type.Name.ToLower() == "гол от дузпа").Count() +
                             g.GameStatistics.Where(gs => gs.TypeId == 1).First().Goals.Where(gl => gl.Type.Name.ToLower() == "автогол").Count(),
                Statistics = g.GameStatistics.Select(gs => new GameTeamStattisticListModel
                {
                    Id = gs.Id,
                    Type = new GameStatisticTypeModel
                    {
                        id = gs.Type.id,
                        Name = gs.Type.Name
                    },
                    Squad = new SquadGameListModel
                    {
                        Id = gs.Squad.Id,
                        Name = $"{gs.Squad.Team.Name} - {gs.Squad.Season.Name}",
                        Team = new TeamModel
                        {
                            Id = gs.Squad.Team.Id,
                            Name = gs.Squad.Team.Name
                        }
                    }
                }).ToList(),
                Season = g.Round.Competition.Season.Name,
                Tournament = g.Round.Competition.Tournament.Name

            });

        public IEnumerable<GameModel> ByTournament(int page, int pageSize, int tournamentId)
        {
            var games = this.db
            .FootballGames
            .Where(g => g.Round.Competition.TournamentId == tournamentId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
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
                    Squad = new SquadGameListModel
                    {
                        Id = gs.Squad.Id,
                        Name = $"{gs.Squad.Team.Name} - {gs.Squad.Season.Name}",
                        Team = new TeamModel
                        {
                            Id = gs.Squad.Team.Id,
                            Name = gs.Squad.Team.Name
                        }
                    }
                }).ToList(),
                Season = g.Round.Competition.Season.Name,
                Tournament = g.Round.Competition.Tournament.Name,
                SeasonId = g.Round.Competition.SeasonId,
                TournamentId = g.Round.Competition.TournamentId,
                Round = g.Round.Name
            }).ToList();

            return games;
        }
    

        public int CountByTournament(int tournamentId)
        => this.db
            .FootballGames
            .Where(g => g.Round.Competition.TournamentId == tournamentId)
            .Count();

        public IEnumerable<GameModel> ByRound(int roundId)
        {
            var games = db
              .FootballGames
              .Where(g => g.RoundId == roundId)
              .Select(g => new GameModel
              {
                  Id = g.Id,
                  Date = g.Date,
                  HostGoals = g.GameStatistics.Where(gs => gs.TypeId == 1).First().Goals.Count(),
                  GuestGoals = g.GameStatistics.Where(gs => gs.TypeId == 2).First().Goals.Count(),
                  Season = g.Round.Competition.Season.Name,
                  SeasonId = g.Round.Competition.SeasonId,
                  Tournament = g.Round.Competition.Tournament.Name,
                  TournamentId = g.Round.Competition.TournamentId,
                  Round = g.Round.Name,
                  Statistics = g.GameStatistics.Select(gs => new GameTeamStattisticListModel
                  {
                      Id = gs.Id,
                      Type = new GameStatisticTypeModel
                      {
                          id = gs.Type.id,
                          Name = gs.Type.Name
                      },
                      Squad = new SquadGameListModel
                      {
                          Id = gs.Squad.Id,
                          Name = $"{gs.Squad.Team.Name} - {gs.Squad.Season.Name}",
                          Team = new TeamModel
                          {
                              Id = gs.Squad.Team.Id,
                              Name = gs.Squad.Team.Name
                          }
                      }
                  }).ToList(),
              }).ToList();

            return games;
        }
                

        public IEnumerable<GameModel> ByRounds(IEnumerable<int> roundIds)
        {
            List<GameModel> games = new List<GameModel>();
            foreach (var id in roundIds)
            {
                List<GameModel> gamesPerRound = ByRound(id).ToList();
                gamesPerRound.ForEach(g =>
                {
                    games.Add(g);
                });
            }
            return games;
        }

        public IEnumerable<GameWithStatisticModel> GamesByTournamentBySeason(int tournamentId, int seasonId)
        {
            var games = db.FootballGames
               .Where(g => g.Round.Competition.SeasonId == seasonId && g.Round.Competition.Tournament.Id == tournamentId);

            var result = GetGames(games);

            return result;
        }

        private IEnumerable<GameWithStatisticModel> GetGames(IQueryable<FootballGame> games)
        {
            var result = games
                .Select(g => new GameWithStatisticModel
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
                                Name = l.Player.FirstName + l.Player.LastName,
                                PlayerNumber = l.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Number,
                                PlayerPicture = l.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Picture,
                                Position = l.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Position,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = l.Player.Countries.Where(c => c.MainCountry == true).First().Country.PicturePath
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
                                Name = s.Player.FirstName + s.Player.LastName,
                                PlayerNumber = s.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Number,
                                PlayerPicture = s.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Picture,
                                Position = s.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Position,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = s.Player.Countries.Where(c => c.MainCountry == true).First().Country.PicturePath
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
                                Name = c.Player.FirstName + c.Player.LastName,
                                PlayerNumber = c.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Number,
                                PlayerPicture = c.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Picture,
                                Position = c.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Position,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = c.Player.Countries.Where(co => co.MainCountry == true).First().Country.PicturePath
                                }
                            }
                        }).ToList(),
                        Squad = new SquadGameModel
                        {
                            Id = gs.Squad.Id,
                            Name = $"{gs.Squad.Team.Name} - {gs.Squad.Season.Name}",
                            Team = new TeamGameModel
                            {
                                Id = gs.Squad.TeamId,
                                Name = gs.Squad.Team.Name,
                                Stadium = gs.Squad
                                           .Team
                                           .Stadiums
                                           .OrderByDescending(s => s.Id)
                                           .Select(s => new StadiumModel
                                           {
                                               Id = s.Id,
                                               Name = s.Name,
                                               City = s.City
                                           }).First(),
                                Logo = gs.Squad.Team.Logo
                            },

                            Coach = gs.Squad
                                          .Coaches
                                          .Select(c => new CoachGameModel
                                          {
                                              Id = c.CoachId,
                                              Name = $"{c.Coach.FirstName} {c.Coach.LastName}",
                                              Country = new CountryFootballGameModel
                                              {
                                                  PicturePath = c.Coach.Country.PicturePath
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
                                Name = gl.Player.ProfileName,
                                PlayerNumber = gl.Player.Squads.Where(s => s.SquadId == gs.SquadId).First().Number,
                                PlayerPicture = gl.Player.Squads.Where(s => s.SquadId == gs.SquadId).First().Picture,
                                Position = gl.Player.Squads.Where(s => s.SquadId == gs.SquadId).First().Position,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = gl.Player.Countries.Where(c => c.MainCountry == true).First().Country.PicturePath
                                }
                            },
                            Assistance = gl.Assistance != null ? new GoalAssistanceModel
                            {
                                Id = gl.Id,
                                Player = new PlayerModel
                                {
                                    Id = gl.Assistance.Player.Id,
                                    Name = gl.Assistance.Player.ProfileName,
                                    PlayerNumber = gl.Assistance.Player.Squads.Where(s => s.SquadId == gs.SquadId).FirstOrDefault().Number,
                                    PlayerPicture = gl.Assistance.Player.Squads.Where(s => s.SquadId == gs.SquadId).FirstOrDefault().Picture,
                                    Position = gl.Assistance.Player.Squads.Where(s => s.SquadId == gs.SquadId).FirstOrDefault().Position,
                                    Country = new CountryFootballGameModel
                                    {
                                        PicturePath = gl.Assistance.Player.Countries.Where(c => c.MainCountry == true).First().Country.PicturePath
                                    }
                                }
                            } : null
                        }).ToList(),
                        Substitutes = gs.Substitutes.Select(s => new SubstituteModel
                        {
                            Id = s.Id,
                            Minute = s.Minute.ToString(),
                            FirstHalf = s.firstHalf,
                            PlayerIn = new PlayerInModel
                            {
                                Id = s.PlayerInId,
                                Player = new PlayerModel
                                {
                                    Id = s.PlayerIn.PlayerId,
                                    Name = s.PlayerIn.Player.FirstName + s.PlayerIn.Player.LastName,
                                    PlayerNumber = s.PlayerIn.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Number,
                                    PlayerPicture = s.PlayerIn.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Picture,
                                    Position = s.PlayerIn.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Position,
                                    Country = new CountryFootballGameModel
                                    {
                                        PicturePath = s.PlayerIn.Player.Countries.Where(c => c.MainCountry == true).First().Country.PicturePath
                                    }
                                }
                            },
                            PlayerOut = new PlayerOutModel
                            {
                                Id = s.PlayerOut.Id,
                                Player = new PlayerModel
                                {
                                    Id = s.PlayerOut.PlayerId,
                                    Name = s.PlayerOut.Player.ProfileName,
                                    PlayerNumber = s.PlayerOut.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Number,
                                    PlayerPicture = s.PlayerOut.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Picture,
                                    Position = s.PlayerOut.Player.Squads.Where(si => si.SquadId == gs.SquadId).FirstOrDefault().Position,
                                    Country = new CountryFootballGameModel
                                    {
                                        PicturePath = s.PlayerOut.Player.Countries.Where(c => c.MainCountry == true).First().Country.PicturePath
                                    }
                                }
                            }
                        }).ToList(),
                        Reserves = gs.Reserves.Select(r => new ResModel
                        {
                            Id = r.Id,
                            Player = new PlayerModel
                            {
                                Id = r.PlayerId,
                                Name = r.Player.ProfileName,
                                Country = new CountryFootballGameModel
                                {
                                    PicturePath = r.Player.Countries.Where(c => c.MainCountry == true).FirstOrDefault().Country.PicturePath
                                },
                                PlayerNumber = r.Player.Squads.Where(s => s.SquadId == gs.SquadId).FirstOrDefault().Number,
                                PlayerPicture = r.Player.Squads.Where(s => s.SquadId == gs.SquadId).FirstOrDefault().Picture,
                                Position = r.Player.Squads.Where(s => s.SquadId == gs.SquadId).FirstOrDefault().Position,
                            }
                        }).ToList(),
                        Type = new GameStatisticTypeModel
                        {
                            id = gs.Type.id,
                            Name = gs.Type.Name
                        },
                    }).ToList()
                });

            return result;
        }
    }
}
