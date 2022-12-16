namespace Sportiada.Services.Football.Models.Player
{
    public class PlayerSquadModel : PlayerModel
    {
        public string PlayerPicture { get; set; }

        public bool OnLoanOut { get; set; }

        public bool FirstTeam { get; set; }

        public bool Under23 { get; set; }

        public bool Under18 { get; set; }
    }
}
