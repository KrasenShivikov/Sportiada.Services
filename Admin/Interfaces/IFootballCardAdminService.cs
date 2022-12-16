namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballCardAdminService
    {
        FootballCardAdminModel ById(int id);
        void Create(int typeId, int playerId, int gameStatisticId, int minute, bool firstHalf);

        void Update(int id, int typeId, int playerId, int gameStatisticId, int minute, bool firstHalf);

        void Delete(int id);

        FootballCard GetCard(int id);
    }
}
