namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballPlayerInAdminService
    {
        FootballPlayerInAdminModel ById(int id);

        void Create(int playerId, int substituteId, string inIcon);

        void Update(int id, int playerId, int substituteId, string inIcon);

        void Delete(int id);

        FootballPlayerIn GetPlayerIn(int id);
    }
}
