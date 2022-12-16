namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Interfaces;
    using Models;
    using Sportiada.Data.Models.Football;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class FootballGameAdminService : IFootballGameAdminService
    {
        private readonly SportiadaDbContext db;
        public FootballGameAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<FootballGameListAdminModel> GamesByRoundId(int roundId)
        {
            var games = db.FootballGames
                        .Where(g => g.RoundId == roundId)
                        .Select(g => new FootballGameListAdminModel
                        {
                            Id = g.Id,
                            Date = g.Date,
                            Attendance = g.Attendance
                        });

            return games;
        }
        public FootballGameAdminModel ById(int id)
        {
            var game = db.FootballGames
                        .Where(g => g.Id == id)
                        .Select(g => new FootballGameAdminModel
                        {
                            Id = g.Id,
                            Date = g.Date,
                            Attendance = g.Attendance,
                            Round = new FootballRoundAdminModel
                            {
                                Id = g.Round.Id,
                                Name = g.Round.Name,
                            },
                            Referees = g.Referees
                                       .Select(r => new FootballGameRefereeAdminModel 
                                       {
                                           GameId = g.Id,
                                           Id = r.Id,
                                           Referee = new FootballRefereeAdminModel
                                           { 
                                               Id = r.Referee.Id,
                                               Name = r.Referee.Name,
                                               Picture = r.Referee.Picture,
                                               Country = new CountryAdminModel
                                               {
                                                   Name = r.Referee.Country.Name,
                                                   PicturePath = r.Referee.Country.PicturePath
                                               },
                                           },
                                           RefereeId = r.RefereeId,
                                           Type = new FootballRefereeTypeAdminModel
                                           {
                                               Name = r.Type.Name
                                           }
                                       }),
                            GameStatistics = g.GameStatistics
                                            .Select(s => new FootballGameStatisticAdminModel
                                            {
                                                Id = s.Id,
                                                BallPossession = s.BallPossession,
                                                Fouls = s.Fouls,
                                                Corners = s.Corners,
                                                Offsides = s.Offsides,
                                                ShotsWide = s.ShotsWide,
                                                ShotsOnTarget = s.ShotsOnTarget,
                                                SquadId = s.SquadId,
                                                Squad = new FootballSquadAdminModel
                                                {
                                                    Id = s.SquadId,
                                                    Coaches = s.Squad.Coaches
                                                        .Where(c => c.Position.ToLower() == "старши треньор" || c.Position.ToLower() == "мениджър" && c.LeftInSeason == false)
                                                        .Select(c => new FootballSquadCoachAdminModel
                                                        {
                                                            Coach = new FootballCoachAdminModel
                                                            {
                                                                ShortName = c.Coach.ShortName
                                                            }
                                                        }),
                                                    Team = new FootballTeamAdminModel
                                                    {
                                                        Id = s.Squad.Team.Id,
                                                        Name = s.Squad.Team.Name,
                                                        City = new CityAdminModel
                                                        {
                                                            Name = s.Squad.Team.City.Name,
                                                            Id = s.Squad.Team.City.Id,
                                                            Country = new CountryAdminModel
                                                            {
                                                                Id = s.Squad.Team.City.Country.Id,
                                                                Name = s.Squad.Team.City.Country.Name,
                                                                ShortName = s.Squad.Team.City.Country.ShortName,
                                                                PicturePath = s.Squad.Team.City.Country.PicturePath
                                                            }
                                                        }
                                                    }
                                                },
                                                TypeId = s.TypeId,
                                                Type = new FootballGameStatisticTypeAdminModel
                                                {
                                                    Name = s.Type.Name
                                                },
                                                Coaches = s.Coaches
                                                           .Select(c => new FootballGameStatisticCoachAdminModel 
                                                           {
                                                               Id = c.Id,
                                                               CoachId = c.Coach.Id,
                                                               GameStatisticId = c.GameStatisticId,
                                                               Coach = new FootballCoachAdminListModel
                                                               {
                                                                   Id = c.Coach.Id,
                                                                   FirstName = c.Coach.FirstName,
                                                                   LastName = c.Coach.LastName,
                                                                   CountryName = c.Coach.Country.Name,
                                                                   CountryPicture = c.Coach.Country.PicturePath,
                                                                   Picture = c.Coach.Picture,
                                                                   ShortName = c.Coach.ShortName
                                                               }
                                                                
                                                               
                                                           }),
                                                LineUps = s.LineUps
                                                           .Select(l => new FootballLineUpAdminModel
                                                           {
                                                               Id = l.Id,
                                                               GameStattisticId = l.GameStattisticId,
                                                               PlayerId = l.PlayerId,
                                                               Player = new FootballPlayerGameAdminModel
                                                               {
                                                                   Id = l.PlayerId,
                                                                   Number = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == l.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Number,
                                                                   Picture = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == l.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Picture,
                                                                   Country = l.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath,
                                                                   ProfileName = l.Player.ProfileName
                                                               }
                                                           }),
                                                Goals = s.Goals
                                                        .Select(goal => new FootballGoalAdminModel
                                                        {
                                                            Id = goal.Id,
                                                            PlayerId = goal.PlayerId,
                                                            Player = new FootballPlayerGameAdminModel
                                                            {
                                                                Id = goal.PlayerId,
                                                                Number = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == goal.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Number,
                                                                Picture = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == goal.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Picture,
                                                                Country = goal.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath,
                                                                ProfileName = goal.Player.ProfileName
                                                            },
                                                            FirstHalf = goal.FirstHalf,
                                                            TypeId = goal.TypeId,
                                                            Type = new FootballGoalTypeAdminModel
                                                            {
                                                                Id = goal.TypeId,
                                                                Name = goal.Type.Name,
                                                                Picture = goal.Type.picture
                                                            },
                                                            Minute = goal.Minute,
                                                            AssistanceId = goal.AssistanceId,
                                                            Assistance = goal.Assistance != null ? new FootballGoalAssistanceAdminModel
                                                            {
                                                                Id = goal.Assistance.Id,
                                                                PlayerId = goal.PlayerId,
                                                                Player = new FootballPlayerGameAdminModel
                                                                {
                                                                    Id = goal.Assistance.PlayerId,
                                                                    Number = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == goal.Assistance.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Number,
                                                                    Picture = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == goal.Assistance.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Picture,
                                                                    Country = goal.Assistance.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath,
                                                                    ProfileName = goal.Assistance.Player.ProfileName
                                                                }
                                                            } : null,
                                                        }),
                                                Cards = s.Cards
                                                        .Select(c => new FootballCardAdminModel
                                                        {
                                                            Id = c.Id,
                                                            PlayerId = c.PlayerId,
                                                            Player = new FootballPlayerGameAdminModel
                                                            {
                                                                Id = c.PlayerId,
                                                                Number = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == c.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Number,
                                                                Picture = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == c.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Picture,
                                                                Country = c.Player.Countries.Where(country => country.MainCountry).FirstOrDefault().Country.PicturePath,
                                                                ProfileName = c.Player.ProfileName
                                                            },
                                                            TypeId = c.TypeId,
                                                            Type = new FootballCardTypeAdminModel
                                                            {
                                                                Name = c.Type.Name,
                                                                Picture = c.Type.Picture
                                                            },
                                                            Minute = c.Minute,
                                                            FirstHalf = c.FirstHalf
                                                        }),
                                                Reserves = s.Reserves
                                                            .Select(r => new FootballReserveAdminModel
                                                            {
                                                                Id = r.Id,
                                                                PlayerId = r.PlayerId,
                                                                Player = new FootballPlayerGameAdminModel
                                                                {
                                                                    Id = r.PlayerId,
                                                                    Number = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == r.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Number,
                                                                    Picture = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == r.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Picture,
                                                                    Country = r.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath,
                                                                    ProfileName = r.Player.ProfileName
                                                                }
                                                            }),
                                                Substitutes = s.Substitutes != null ? s.Substitutes
                                                               .Select(ss => new FootballSubstituteAdminModel
                                                               {
                                                                   Id = ss.Id,
                                                                   GameStatisticId = ss.GameStatisticId,
                                                                   Minute = ss.Minute,
                                                                   FirstHalf = ss.firstHalf,
                                                                   PlayerInId = ss.PlayerInId,
                                                                   PlayerIn = ss.PlayerIn != null ? new FootballPlayerInAdminModel
                                                                   {
                                                                       Id = ss.PlayerIn.Id,
                                                                       PlayerId = ss.PlayerIn.PlayerId,
                                                                       Player = new FootballPlayerGameAdminModel
                                                                       {
                                                                           Id = ss.PlayerIn.PlayerId,
                                                                           Number = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == ss.PlayerIn.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Number,
                                                                           Picture = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == ss.PlayerIn.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Picture,
                                                                           Country = ss.PlayerIn.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath,
                                                                           ProfileName = ss.PlayerIn.Player.ProfileName
                                                                       },
                                                                       InIcon = ss.PlayerIn.InIcon,
                                                                   } : null,
                                                                   PlayerOutId = ss.PlayerOutId,
                                                                   PlayerOut = ss.PlayerOut != null ? new FootballPlayerOutAdminModel
                                                                   {
                                                                       Id = ss.PlayerOut.Id,
                                                                       PlayerId = ss.PlayerOutId,
                                                                       Player = new FootballPlayerGameAdminModel
                                                                       {
                                                                           Id = ss.PlayerOut.PlayerId,
                                                                           Number = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == ss.PlayerOut.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Number,
                                                                           Picture = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == ss.PlayerOut.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Picture,
                                                                           Country = ss.PlayerOut.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath,
                                                                           ProfileName = ss.PlayerOut.Player.ProfileName
                                                                       },
                                                                       OutIcon = ss.PlayerOut.OutIcon
                                                                   } : null

                                                               }) : null,
                                                Sidelines = s.Sidelines
                                                             .Select(ss => new FootballSidelineAdminModel
                                                             {
                                                                 Id = ss.Id,
                                                                 PlayerId = ss.PlayerId,
                                                                 Player = new FootballPlayerGameAdminModel
                                                                 {
                                                                     Id = ss.PlayerId,
                                                                     Number = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == ss.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Number,
                                                                     Picture = db.FootballSquadPlayer
                                                                            .Where(sp => sp.PlayerId == ss.PlayerId && sp.SquadId == s.SquadId)
                                                                            .FirstOrDefault().Picture,
                                                                     Country = ss.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath,
                                                                     ProfileName = ss.Player.ProfileName
                                                                 },
                                                                 ReasonId = ss.ReasonId,
                                                                 Reason = new FootballSidelineReasonAdminModel
                                                                 {
                                                                     Name = ss.Reason.Name,
                                                                     Picture = ss.Reason.Picture
                                                                 }
                                                             })
                                            }).ToList()

                        }).FirstOrDefault();

            return game;

        }

        public void Create(int attendance, DateTime date, int roundId)
        {
            FootballGame game = new FootballGame();
            game.Attendance = attendance;
            game.Date = date;
            game.RoundId = roundId;

            db.Add(game);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            FootballGame game = GetGame(id);

            db.Remove(game);
            db.SaveChanges();
        }


        public void Update(int id, int attendance, DateTime date, int roundId)
        {
            FootballGame game = GetGame(id);
            game.Id = id;
            game.Attendance = attendance;
            game.Date = date;
            game.RoundId = roundId;

            db.Update(game);
            db.SaveChanges();
        }

        public FootballGame GetGame(int id)
        {
            FootballGame game = db.FootballGames
                                .Where(g => g.Id == id)
                                .FirstOrDefault();

            return game;
        }
    }
}
