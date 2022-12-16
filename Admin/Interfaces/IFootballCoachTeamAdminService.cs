namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballCoachTeamAdminService
    {
        void Create(int coachId, string team, string teamLogo, string teamCountryFlag, DateTime fromDate, DateTime untilDate, string position, int matches);

        void Update(int id, int coachId, string team, string teamLogo, string teamCountryFlag, DateTime fromDate, DateTime untilDate, string position, int matches);

        void Delete(int id);

        FootballCoachTeam GetCoachTeam(int id);
    }
}
