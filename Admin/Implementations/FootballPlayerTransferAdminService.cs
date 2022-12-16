namespace Sportiada.Services.Admin.Implementations
{
    using Data;
    using Data.Models.Football;
    using Interfaces;
    using Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    public class FootballPlayerTransferAdminService : IFootballPlayerTransferAdminService
    {
        private readonly SportiadaDbContext db;
        public FootballPlayerTransferAdminService(SportiadaDbContext db)
        {
            this.db = db;
        }
        public void Create(int playerId, string season, string date, string prevoiousTeam, string previousTeamLogo, string previousTeamCountryFlag, 
            string currentTeam, string currentTeamLogo, string currentTeamCountryFlag, string transferPrice, string onLoan)
        {
            FootballPlayerTransfers transfer = new FootballPlayerTransfers
            {
                PlayerId = playerId,
                Season = season,
                Date = date,
                PreviousTeam = prevoiousTeam,
                PreviousTeamLogo = previousTeamLogo,
                PreviousTeamCountryFlag = previousTeamCountryFlag,
                CurrentTeam = currentTeam,
                CurrentTeamLogo = currentTeamLogo,
                CurrentTeamCountryFlag = currentTeamCountryFlag,
                TransferPrice = transferPrice,
                OnLoan = onLoan
            };

            db.Add(transfer);
            db.SaveChanges();
        }

        public void Update(int id, int playerId, string season, string date, string prevoiousTeam, string previousTeamLogo, string previousTeamCountryFlag, 
            string currentTeam, string currentTeamLogo, string currentTeamCountryFlag, string transferPrice, string onLoan)
        {
            var transfer = GetTransfer(id);

            transfer.Id = id;
            transfer.PlayerId = playerId;
            transfer.Season = season;
            transfer.Date = date;
            transfer.PreviousTeam = prevoiousTeam;
            transfer.PreviousTeamLogo = previousTeamLogo;
            transfer.PreviousTeamCountryFlag = previousTeamCountryFlag;
            transfer.CurrentTeamCountryFlag = previousTeamCountryFlag;
            transfer.CurrentTeam = currentTeam;
            transfer.CurrentTeamLogo = currentTeamLogo;
            transfer.CurrentTeamCountryFlag = currentTeamCountryFlag;
            transfer.TransferPrice = transferPrice;
            transfer.OnLoan = onLoan;

            db.Update(transfer);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var transfer = GetTransfer(id);
            db.Remove(transfer);
            db.SaveChanges();
        }

        public FootballPlayerTransfers GetTransfer(int id)
        {
            FootballPlayerTransfers transfer = db.FootballPlayerTransfers
                            .Where(t => t.Id == id)
                            .FirstOrDefault();

            return transfer;
        }
    }
}
