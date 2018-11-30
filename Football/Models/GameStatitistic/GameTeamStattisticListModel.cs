namespace Sportiada.Services.Football.Models.GameStatitistic
{
    using Models.Side;

    public class GameTeamStattisticListModel : GameTeamStatisticModel
    {
        public SideGameListModel Side { get; set; }
    }
}
