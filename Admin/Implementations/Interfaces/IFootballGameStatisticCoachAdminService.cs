namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballGameStatisticCoachAdminService
    {
        FootballGameStatisticCoachAdminModel ById(int id);

        void Create(int coachId, int gameStatisticId);

        void Update(int id, int coachId, int gameStatisticId);

        void Delete(int id);

        FootballGameStatisticCoach GetGameStatisticCoach(int id);
    }
}
