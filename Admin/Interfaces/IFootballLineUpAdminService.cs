namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballLineUpAdminService
    {
        FootballLineUpAdminModel LineUpById(int id);
        void Create(int playerId, int gameStatisticId);

        void Update(int id, int playerId, int gameStatisticId);

        void Delete(int id);

        FootballLineUp GetLineUp(int id);
    }
}
