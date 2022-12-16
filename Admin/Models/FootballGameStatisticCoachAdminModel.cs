namespace Sportiada.Services.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FootballGameStatisticCoachAdminModel
    {
        public int Id { get; set; }

        public int CoachId { get; set; }

        public FootballCoachAdminListModel Coach { get; set; }

        public int GameStatisticId { get; set; }
    }
}
