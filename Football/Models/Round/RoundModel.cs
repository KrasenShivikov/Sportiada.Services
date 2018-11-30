namespace Sportiada.Services.Football.Models.Round
{
    using Models.Competitiion;

    public class RoundModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CompetitionModel Competition { get; set; }
    }
}
