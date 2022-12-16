using System;
using System.Collections.Generic;
using System.Text;

namespace Sportiada.Services.Admin.Models
{
    public class FootballGameRefereeAdminModel
    {
        public int Id { get; set; }

        public int RefereeId { get; set; }

        public FootballRefereeAdminModel Referee { get; set; }

        public int TypeId { get; set; }

        public FootballRefereeTypeAdminModel Type { get; set; }

        public int GameId { get; set; }
    }
}
