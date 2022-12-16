namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IFootballSquadPlayerAdminService
    {
        IEnumerable<FootballSquadPlayerAdminModel> PlayersBySquadId(int squadId);
        FootballSquadPlayerAdminModel SquadPlayer(int playerId, int squadId);
        void Create(int playerId, int squadId, int number, string picture, string position, string squadType, string contractStartDate,
             string contractendDate, bool onLoan, bool joinedInSummer, bool joinedInWinter, bool leftInSummer, bool leftInWinter);

        void Delete(int playerId, int squadId);

        FootballSquadPlayer GetSquadPlayer(int playerId, int squadId); 
    }
}
