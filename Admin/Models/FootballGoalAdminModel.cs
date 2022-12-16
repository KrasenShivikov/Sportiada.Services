using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Models
{
    public class FootballGoalAdminModel
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public FootballGoalTypeAdminModel Type { get; set; }

        public int PlayerId { get; set; }

        public FootballPlayerGameAdminModel Player { get; set; }

        public int Minute { get; set; }

        public bool FirstHalf { get; set; }

        public int GameStatisticId { get; set; }

        public int AssistanceId { get; set; }

        public FootballGoalAssistanceAdminModel Assistance { get; set; }
    }
}
