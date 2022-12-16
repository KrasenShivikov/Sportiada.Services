namespace Sportiada.Services.Admin.Interfaces
{
    using Data.Models.Football;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballSubstituteAdminService
    {
        FootballSubstituteAdminModel ById(int id);
        void Create(int gameStatisticId, int playerInId, int playerOutId, int minute, bool firstHalf);

        void Update(int id, int gameStatisticId, int playerInId, int playerOutId, int minute, bool firstHalf);

        void Delete(int id);

        FootballSubstitute GetSubstitute(int id);
    }
}
