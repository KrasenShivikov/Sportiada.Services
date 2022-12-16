namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballGoalAdminService
    {
        FootballGoalAdminModel ById(int id);
        void Create(int playerId, int gameStatisticId, int assistanceId, int typeId, int minute, bool firstHalf);

        void Update(int id, int playerId, int gameStatisticId, int assistanceId, int typeId, int minute, bool firstHalf);

        void Delete(int id);

        FootballGoal GetGoal(int id);
    }
}
