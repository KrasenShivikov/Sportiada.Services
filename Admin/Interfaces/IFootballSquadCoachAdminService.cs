namespace Sportiada.Services.Admin.Interfaces
{

    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballSquadCoachAdminService
    {
        IEnumerable<FootballSquadCoachAdminModel> CoachesBySquadId(int squadId);
        FootballSquadCoachAdminModel BySquadIdByCoachId(int squadId, int coachId);
        void Create(int squadId, int coachId, string position, string squadType, string fromDate, string toDate, bool leftInSeason);

        void Delete(int squadId, int coachId);

        FootballSquadCoach GetSquadCoach(int squadId, int coachId);
    }
}
