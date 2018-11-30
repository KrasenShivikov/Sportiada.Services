namespace Sportiada.Services.Football.Models.Player
{
    using System.Collections.Generic;
    using System.Linq;

    public class PlayerSeasonStatisticModel : PlayerProfileModel
    {
        public List<PlayerGameStatisticModel> Games { get; set; }

        public List<IGrouping<string, PlayerGameStatisticModel>> GamesByTeam => this.Games.GroupBy(g => g.PlayerTeam).ToList();
    }
}
