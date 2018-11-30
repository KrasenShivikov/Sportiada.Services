namespace Sportiada.Services.Football.Models.Game
{
    using Models.GameReferee;
    using Models.GameStatitistic;
    using Models.Round;
    using System;
    using System.Collections.Generic;

    public class GameWithStatisticModel
    {
        public int Id { get; set; }

        public int Attendance { get; set; }

        public DateTime Date { get; set; }

        public RoundModel Round { get; set; }

        public List<GameTeamStatitisticFullModel> TeamsStatistic { get; set; }

        public List<GameRefereeModel> Referees { get; set; }
    }
}
