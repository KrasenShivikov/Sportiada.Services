namespace Sportiada.Services.Football.Models.Side
{
    using Models.Team;
    using Models.Season;

    public class SideGameListModel : SideModel
    {
        public TeamModel Team { get; set; }

        public SeasonModel Season { get; set; }
    }
}
