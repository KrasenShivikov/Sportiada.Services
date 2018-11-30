namespace Sportiada.Services.Football.Models.GameStatitistic
{  
    using Models.GameStatitisticType; 

    public class GameTeamStatisticModel
    {
        public int Id { get; set; }

        public GameStatisticTypeModel Type { get; set; }     
    }
}
