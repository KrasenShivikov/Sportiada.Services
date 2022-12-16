namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballStadiumAdminService
    {
        FootballStadiumAdminModel ById(int id);
        void Create(int teamId, string name);

        void Update(int id, int teamId, string name);

        void Delete(int id);

        FootballStadium GetFootballStadium(int id);
    }
}
