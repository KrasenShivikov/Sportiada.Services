namespace Sportiada.Services.AlpineSki.Models.Skier
{
    using Implementations;
    using Models.Result; 
    using System.Collections.Generic;
    using System.Linq;

    public class SkierStandingStatisticModel : SkierProfileModel
    {
        public List<ResultSkierStatisticAlpineSkiModel> Results { get; set; }

        public List<Dictionary<int, int>> StandingStatistic => GetStandingStatistic();

        public List<Dictionary<string, Dictionary<int, int>>> DisciplineStatistic => GetDisciplineStatistic();

        private List<Dictionary<int, int>> GetStandingStatistic()
        {

            List<Dictionary<int, int>> overallStandingStatistic = new List<Dictionary<int, int>>();

            var resultsByPlace = this.Results
                     .GroupBy(r => r.Place);

            foreach (var resultByPlace in resultsByPlace)
            {
                var countResultsByPlace = resultByPlace.Select(r => new List<ResultSkierStatisticAlpineSkiModel>()).Count();

                Dictionary<int, int> standingStatistic = new Dictionary<int, int>();
                standingStatistic[resultByPlace.Key] = countResultsByPlace;
                overallStandingStatistic.Add(standingStatistic);
            }

            return overallStandingStatistic;
        }

        private List<Dictionary<string, Dictionary<int, int>>> GetDisciplineStatistic()
        {
            List<Dictionary<string, Dictionary<int, int>>> overallDisciplineStatitistic
                = new List<Dictionary<string, Dictionary<int, int>>>();
       

            var disciplineQuery = this.Results
                         .Where(r => r.Stage == ResultAlpineSkiService.Stage || r.CompetitionsAlpineSki.Discipline.Id == 1 || r.CompetitionsAlpineSki.Discipline.Id == 2)
                         .GroupBy(r => r.CompetitionsAlpineSki.Discipline.Name);

            foreach (var discipline in disciplineQuery)
            {
                Dictionary<string, Dictionary<int, int>> disciplineStatistic
                = new Dictionary<string, Dictionary<int, int>>();

                var places = discipline
                    .GroupBy(d => d.Place);

                foreach (var place in places)
                {
                    var count = place.Select(p => new List<ResultSkierStatisticAlpineSkiModel>()).Count();

                    Dictionary<int, int> standingStatistic = new Dictionary<int, int>();
                    standingStatistic[place.Key] = count;
                    disciplineStatistic.Add(discipline.Key, standingStatistic);

                }

                overallDisciplineStatitistic.Add(disciplineStatistic);

            }

            return overallDisciplineStatitistic;
        }
    }
}
