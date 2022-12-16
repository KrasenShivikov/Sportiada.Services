namespace Sportiada.Services.Admin.Interfaces
{
    using Models;
    using Data.Models.Football;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public interface IFootballGameStatisticAdminService
    {
        void Create(int typeId, int squadId, int gameId, int ballPossession, int corners, int shotsOnTarget, int shotsWide, int fouls, int offsides);

        void Update(int id, int typeId, int squadId, int gameId, int ballPossession, int corners, int shotsOnTarget, int shotsWide, int fouls, int offsides);

        void Delete(int id);

        FootballGameStatistic GetGameStatistic(int id);

        IEnumerable<FootballGameStatisticTypeAdminModel> GameStatisticTypes();
    }
}
