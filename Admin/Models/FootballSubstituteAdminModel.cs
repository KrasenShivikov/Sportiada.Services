using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Models
{
    public class FootballSubstituteAdminModel
    {
        public int Id { get; set; }

        public int PlayerInId { get; set; }

        public FootballPlayerInAdminModel PlayerIn { get; set; }

        public int PlayerOutId { get; set; }

        public FootballPlayerOutAdminModel PlayerOut { get; set; }

        public int GameStatisticId { get; set; }

        public int Minute { get; set; }

        public bool FirstHalf { get; set; }
    }
}
