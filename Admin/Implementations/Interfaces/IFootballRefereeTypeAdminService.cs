namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballRefereeTypeAdminService
    {
        IEnumerable<FootballRefereeTypeAdminModel> All();

        void Create(string name);

        void Update(int id, string name);

        void Delete(int id);

        FootballRefereeType GetRefereeType(int id);
    }
}
