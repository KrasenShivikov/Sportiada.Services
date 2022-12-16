namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models.Football;
    using Interfaces;
    using Sportiada.Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class FootballSquadAdminService : IFootballSquadAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballSquadAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<FootballSquadAdminListModel> All()
        {
            var squads = db.FootballSquads
                    .Select(s => new FootballSquadAdminListModel
                    {
                        Id = s.Id,
                        Season = s.Season.Name,
                        TeamName = s.Team.Name
                    });

            return squads;
        }

        public int GetSquadIdByTeamIdBySeasonsId(int seasonId, int teamId)
        {
            int squadId = db.FootballSquads
                    .Where(s => s.SeasonId == seasonId && s.TeamId == teamId)
                    .FirstOrDefault()
                    .Id;

            return squadId;
        }

        public FootballSquadAdminModel FootballSquadById(int id)
        {
            var squad = db.FootballSquads
                        .Where(s => s.Id == id)
                        .Select(s => new FootballSquadAdminModel
                        {
                            Id = s.Id,
                            SeasonId = s.SeasonId,
                            Season = new SeasonAdminModel
                            {
                                Name = s.Season.Name
                            },
                            TeamId = s.TeamId,
                            Team = new FootballTeamAdminModel
                            {
                                Name = s.Team.Name,
                                City = new CityAdminModel
                                {
                                    Id = s.Team.CityId,
                                    Name = s.Team.City.Name
                                }
                            },
                            Players = s.Players.Select(sp => new FootballSquadPlayerAdminModel 
                            {
                                SquadId = sp.SquadId,
                                PlayerId = sp.PlayerId,
                                Player = new FootballPlayerAdminModel
                                {
                                    FirstName = sp.Player.FirstName,
                                    LastName = sp.Player.LastName,
                                    ProfileName = sp.Player.ProfileName
                                },
                                Number = sp.Number,
                                OnLoan = sp.OnLoan,
                                JoinedInSummer = sp.JoinedInSummer,
                                JoinedInWinter = sp.JoinedInWinter,
                                LeftInSummer = sp.LeftInSummer,
                                LeftInwinter = sp.LeftInWinter,
                                SquadType = sp.SquadType,
                                Picture = sp.Picture,
                                Position=sp.Position,
                                ContractStartDate = sp.ContractStartDate,
                                ContractEndDate = sp.ContractEndDate
                            }),
                            Coaches = s.Coaches.Select(sc => new FootballSquadCoachAdminModel 
                            {
                                Coach = new FootballCoachAdminModel
                                {
                                    Id = sc.CoachId,
                                    FirstName = sc.Coach.FirstName,
                                    LastName = sc.Coach.LastName,
                                    ShortName = sc.Coach.ShortName,
                                    BirthDate = sc.Coach.BirthDate.Value,
                                    Picture = sc.Coach.Picture,
                                    Country = new CountryAdminModel
                                    {
                                        PicturePath = sc.Coach.Country.PicturePath
                                    }
                                },
                                FromDate = sc.FromDate,
                                ToDate = sc.ToDate,
                                Position = sc.Position,
                                SquadType = sc.SquadType,
                                LeftInSeason = sc.LeftInSeason,
                            })
                        }).FirstOrDefault();

            return squad;
        }
        public void Create(int seasonId, int teamId)
        {
            var squad = new FootballSquad
            {
                SeasonId = seasonId,
                TeamId = teamId
            };

            db.Add(squad);
            db.SaveChanges();
        }

        public void Update(int id, int seasonId, int teamId)
        {
            var squad = GetSquad(id);
            squad.Id = id;
            squad.SeasonId = seasonId;
            squad.TeamId = teamId;

            db.Update(squad);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var squad = GetSquad(id);

            db.Remove(squad);
            db.SaveChanges();
        }

        public FootballSquad GetSquad(int id)
        {
            var squad = db.FootballSquads
                        .Where(s => s.Id == id)
                        .FirstOrDefault();

            return squad;
        }
    }
}
