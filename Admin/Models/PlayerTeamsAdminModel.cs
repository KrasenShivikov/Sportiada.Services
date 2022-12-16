using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Models
{
    public class PlayerTeamsAdminModel
    {
        public int PlayerId { get; set; }
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime UntilDate { get; set; }

        public bool OnLoan { get; set; }

        public string TeamSelections { get; set; }
    }
}
