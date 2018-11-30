namespace Sportiada.Services.Football.Models.Team
{
    using Models.Stadium;
    using System.Collections.Generic;

    public class TeamGameModel : TeamModel
    {
        public StadiumModel Stadium { get; set; }

        public string Logo { get; set; }
    }
}
