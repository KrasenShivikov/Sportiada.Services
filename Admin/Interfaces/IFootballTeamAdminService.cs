namespace Sportiada.Services.Admin.Interfaces
{
    using Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballTeamAdminService
    {
        FootballTeamAdminModel ById(int id);
        IEnumerable<FootballTeamAdminModel> All();

        void CreateFootballTeam(string name, int cityId, string logo);

        void UpdateFootballTeam(int id, int cityId, string name, string logo);

        void DeleteFootballTeam(int id);
    }
}
