namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IFootballPlayerOutAdminService
    {
        FootballPlayerOutAdminModel ById(int id);

        void Create(int playerId, int substituteId, string outIcon);

        void Update(int id, int playerId, int substituteId, string outIcon);

        void Delete(int id);

        FootballPlayerOut GetPlayerOut(int id);
    }
}
