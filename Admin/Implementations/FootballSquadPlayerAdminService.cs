

namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models.Football;
    using Interfaces;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using Sportiada.Services.Admin.Models;

    public class FootballSquadPlayerAdminService : IFootballSquadPlayerAdminService
    {
        private readonly SportiadaDbContext db;

        public FootballSquadPlayerAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<FootballSquadPlayerAdminModel> PlayersBySquadId(int squadId)
        {
            var squadPlayers = db.FootballSquadPlayer
                          .Where(p => p.SquadId == squadId)
                          .Select(p => new FootballSquadPlayerAdminModel
                          {
                              PlayerId = p.PlayerId,
                              SquadId = p.SquadId,
                              Number = p.Number,
                              Picture = p.Picture,
                              Country = p.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath,
                              Position = p.Position,
                              ContractStartDate = p.ContractStartDate,
                              ContractEndDate = p.ContractEndDate,
                              SquadType = p.SquadType,
                              OnLoan = p.OnLoan,
                              JoinedInSummer = p.JoinedInSummer,
                              JoinedInWinter = p.JoinedInWinter,
                              LeftInSummer = p.LeftInSummer,
                              LeftInwinter = p.LeftInWinter,
                              Player = new FootballPlayerAdminModel
                              {
                                  FirstName = p.Player.FirstName,
                                  LastName = p.Player.LastName,
                                  ProfileName = p.Player.ProfileName,
                              }
                          });

            return squadPlayers;
        }
        public FootballSquadPlayerAdminModel SquadPlayer(int playerId, int squadId)
        {
            var squadPlayer = db.FootballSquadPlayer
                          .Where(p => p.PlayerId == playerId && p.SquadId == squadId)
                          .Select(p => new FootballSquadPlayerAdminModel
                          {
                              PlayerId = p.PlayerId,
                              SquadId = p.SquadId,
                              Number = p.Number,
                              Picture = p.Picture,
                              Country = p.Player.Countries.Where(c => c.MainCountry).FirstOrDefault().Country.PicturePath,
                              Position = p.Position,
                              ContractStartDate = p.ContractStartDate,
                              ContractEndDate = p.ContractEndDate,
                              SquadType = p.SquadType,
                              OnLoan = p.OnLoan,
                              JoinedInSummer = p.JoinedInSummer,
                              JoinedInWinter = p.JoinedInWinter,
                              LeftInSummer = p.LeftInSummer,
                              LeftInwinter = p.LeftInWinter,
                              Player = new FootballPlayerAdminModel
                              {
                                  FirstName = p.Player.FirstName,
                                  LastName = p.Player.LastName,
                                  ProfileName = p.Player.ProfileName
                              }
                          }).FirstOrDefault();

            return squadPlayer;
        }

        public void Create(int playerId, int squadId, int number, string picture, string position, string squadType, string contractStartDate, string contractendDate,
                            bool onLoan, bool joinedInSummer, bool joinedInWinter, bool leftInSummer, bool leftInWinter)
        {
            FootballSquadPlayer squadPlayer = new FootballSquadPlayer
            {
                PlayerId = playerId,
                SquadId = squadId,
                Number = number,
                Picture = picture,
                Position = position,
                SquadType = squadType,
                ContractStartDate = contractStartDate,
                ContractEndDate = contractendDate,
                OnLoan = onLoan,
                JoinedInSummer = joinedInSummer,
                JoinedInWinter = joinedInWinter,
                LeftInSummer = leftInSummer,
                LeftInWinter = leftInWinter
            };

            db.Add(squadPlayer);
            db.SaveChanges();
        }

        public void Delete(int playerId, int squadId)
        {
            FootballSquadPlayer squadPlayer = GetSquadPlayer(playerId, squadId);

            db.Remove(squadPlayer);
            db.SaveChanges();
        }

        public FootballSquadPlayer GetSquadPlayer(int playerId, int squadId)
        {
            FootballSquadPlayer squadPlayer = db.FootballSquadPlayer
                    .Where(p => p.PlayerId == playerId & p.SquadId == squadId)
                    .FirstOrDefault();

            return squadPlayer;
        }

        
    }
}
