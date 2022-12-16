namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballGoalAssistanceAdminService
    {
        FootballGoalAssistanceAdminModel ById(int id);
        void Create(int playerId, int goalId);

        void Update(int id, int playerId, int goalId);

        void Delete(int id);

        FootballGoalAssistance GetAssistance(int id);
    }
}
