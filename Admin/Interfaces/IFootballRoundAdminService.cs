namespace Sportiada.Services.Admin.Interfaces
{
    using Sportiada.Data.Models.Football;
    using Sportiada.Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballRoundAdminService
    {
        IEnumerable<FootballRoundAdminModel> RoundsByCompetition(int competitionId);

        void Create(string name, int competitionId);

        void Update(int id, string name, int competitionId);

        void Delete(int id);

        FootballRound GetRound(int id);
    }
}
