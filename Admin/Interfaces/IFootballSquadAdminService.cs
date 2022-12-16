namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballSquadAdminService
    {
        int GetSquadIdByTeamIdBySeasonsId(int seasonId, int teamId);

        IEnumerable<FootballSquadAdminListModel> All();

        FootballSquadAdminModel FootballSquadById(int id);

        void Create(int seasonId, int teamId);

        void Update(int id, int squadId, int teamId);

        void Delete(int id);

        FootballSquad GetSquad(int id);
    }
}
