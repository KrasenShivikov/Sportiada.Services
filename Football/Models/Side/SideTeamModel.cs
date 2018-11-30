namespace Sportiada.Services.Football.Models.Side
{
    using Models.Player;
    using Models.Season;
    using System.Collections.Generic;
    using System.Linq;

    public class SideTeamModel : SideGameListModel
    {
        public List<PlayerSideModel> Players { get; set; }

        public List<PlayerSideModel> FirstTeam => this.Players.Where(p => p.FirstTeam == true).ToList();

        public List<PlayerSideModel> Under23 => this.Players.Where(p => p.Under23 == true).ToList();

        public List<PlayerSideModel> Under18 => this.Players.Where(p => p.Under18 == true).ToList();

        public List<PlayerSideModel> OnLoan => this.Players.Where(p => p.OnLoanOut == true).ToList();

    }
}
