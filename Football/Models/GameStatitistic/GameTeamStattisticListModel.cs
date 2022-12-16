namespace Sportiada.Services.Football.Models.GameStatitistic
{
    using Models.Squad;

    public class GameTeamStattisticListModel : GameTeamStatisticModel
    {
        public SquadGameListModel Squad { get; set; }
    }
}
