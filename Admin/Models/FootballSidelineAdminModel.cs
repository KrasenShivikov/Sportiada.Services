using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Models
{
    public class FootballSidelineAdminModel
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public FootballPlayerGameAdminModel Player { get; set; }

        public int ReasonId { get; set; }

        public FootballSidelineReasonAdminModel Reason { get; set; }

        public int GameStatisticId { get; set; }

    }
}
