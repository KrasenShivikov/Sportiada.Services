namespace Sportiada.Services.Admin.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;
    public interface IFootballTournamentService
    {
        IEnumerable<FootballTournamentAdminModel> All();

        FootballTournamentProfileAdminModel ById(int id);

        FootballTournamentBaseAdminModel ForDelete(int id);

        void CreateTournament(string name, int countryId);

        void UpdateTournament(int id, string name, int countryId);

        void DeleteTournament(int id);
    }
}
