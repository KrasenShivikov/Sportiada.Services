

namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballPlayerTransferAdminService
    {
        void Create(int playerId, string season, string date, string prevoiousTeam, string previousTeamLogo, string previousTeamCountryFlag,
            string currentTeam, string currentTeamLogo, string currentTeamCountryFlag, string transferprice, string onLoan);

        void Update(int id, int playerId, string season, string date, string prevoiousTeam, string previousTeamLogo, string previousTeamCountryFlag, 
            string currentTeam, string currentTeamLogo, string currentTeamCountryFlag, string transferprice, string onLoan);

        void Delete(int id);

        FootballPlayerTransfers GetTransfer(int id);
    }
}
