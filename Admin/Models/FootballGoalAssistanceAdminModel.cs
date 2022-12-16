using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Models
{
    public class FootballGoalAssistanceAdminModel
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public FootballPlayerGameAdminModel Player { get; set; }

        public int GoalId { get; set; }

        public FootballGoalAdminModel Goal { get; set; }
    }
}
