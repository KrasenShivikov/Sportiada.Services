namespace Sportiada.Services.Football.Models.Competitiion
{
    using Models.CompetitionType;
    using Models.Season;
    using Models.Tournament;

    public class CompetitionModel
    {
        public int Id { get; set; }

        public SeasonModel Season { get; set; }

        public CompetitionTypeModel Type { get; set; }

        public TournamentModel Tournament { get; set; }
    }
}
