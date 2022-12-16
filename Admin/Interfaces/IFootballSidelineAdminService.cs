namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IFootballSidelineAdminService
    {
        FootballSidelineAdminModel ById(int id);

        void Create(int playerId, int gameStatisticId, int reasonId);

        void Update(int id, int playerId, int gameStatisticId, int reasonid);

        void Delete(int id);

        FootballSideline GetSideline(int id);

    }
}
