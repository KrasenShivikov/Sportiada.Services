namespace Sportiada.Services.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class FootballStadiumAdminModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TeamId { get; set; }

        public FootballTeamAdminModel Team { get; set; }
    }
}
