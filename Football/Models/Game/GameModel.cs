namespace Sportiada.Services.Football.Models.Game
{
    using System;
    using System.Collections.Generic;
    using Models.GameStatitistic;

    public class GameModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public List<GameTeamStattisticListModel> Statistics { get; set; }

        public int HostGoals { get; set; }

        public int GuestGoals { get; set; }

        public string Season { get; set; }

        public string Tournament { get; set; }
    }
}
