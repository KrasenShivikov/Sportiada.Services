using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Models
{
    public class FootballRoundAdminModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CompetitionId { get; set; }

        public string CompetitionName { get; set; }
    }
}
