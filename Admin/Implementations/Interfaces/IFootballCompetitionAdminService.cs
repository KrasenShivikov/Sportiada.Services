
namespace Sportiada.Services.Admin.Interfaces
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballCompetitionAdminService
    {
        IEnumerable<FootballCompetitionAdminModel> CompetitionsByTournamentId(int tournamentId);
        void Create(int seasonId, int tournamentId, int typeId);

        void Update(int id, int seasonId, int tournamentId, int typeId);

        void Delete(int id);
    }
}
