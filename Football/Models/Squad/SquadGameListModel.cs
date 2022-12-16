namespace Sportiada.Services.Football.Models.Squad
{
    using Models.Team;
    using Models.Season;

    public class SquadGameListModel : SquadModel
    {
        public TeamModel Team { get; set; }

        public SeasonModel Season { get; set; }
    }
}
