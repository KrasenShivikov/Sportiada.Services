namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballGoalTypeAdminService
    {
        IEnumerable<FootballGoalTypeAdminModel> All();

        void Create(string name, string picture);

        void Update(int id, string name, string picture);

        void Delete(int id);

        FootballGoalType GetGoalType(int id);
    }
}
