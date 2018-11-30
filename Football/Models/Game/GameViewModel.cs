namespace Sportiada.Services.Football.Models.Game
{
    using Models.Coach;
    using Models.Goal;
    using Models.GameReferee;
    using Models.GameStatitistic;
    using Models.LineUp;
    using Models.Sideline;
    using Models.Stadium;
    using Models.Substitute;
    using Models.Reserve;
    using Sportiada.Web.Infrastructure.Extensions;
    using System.Collections.Generic;
    using System.Linq;
    
    public class GameViewModel
    {
        public GameWithStatisticModel Data { get; set; }

        public List<LineUpViewModel> LineUpsHost => GetLineUps(1);

        public List<LineUpViewModel> LineUpsGuest => GetLineUps(2);

        public List<SubstituteViewModel> SubstitutesHost => Substitutes(1);

        public List<SubstituteViewModel> SubstitutesGuest => Substitutes(2);

        public List<GoalViewModel> Goals => GetGoals();

        public List<ResViewModel> HostTeamReserves => GetReserves(1);

        public List<ResViewModel> GuestTeamReserves => GetReserves(2);

        public List<GameRefereeViewModel> GameReferees => GetReferees();

        public string HostName => TeamName(1);

        public string GuestName => TeamName(2);

        public string HostTeamLogo => TeamLogo(1);

        public string GuestTeamLogo => TeamLogo(2);

        public int HostGoalsCount => GoalsCount(1);

        public int GuestGoalsCount => GoalsCount(2);

        public int HostGoalsFirstHalfCount => GoalsHalfTimeCount(1);

        public int GuestGoalsFirstHalfCount => GoalsHalfTimeCount(2);

        public string Date => this.Data.Date.ToShortDateString();

        public string Time => this.Data.Date.ToShortTimeString();

        public string Round => this.Data.Round.Name;

        public string Tournament => this.Data.Round.Competition.Tournament.Name;

        public string Season => this.Data.Round.Competition.Season.Name;

        public int Atendance => this.Data.Attendance;

        public CoachGameViewModel HostTeamCoach => GetCoach(1);

        public CoachGameViewModel GuestTeamCoach => GetCoach(2);

        public StadiumModel Stadium => GetStadium();

        public List<SidelineViewModel> HostTeamSidelines => GetSidelines(1);

        public List<SidelineViewModel> GuestTeamSidelines => GetSidelines(2);

        public GameGeneralViewStatitistic HostTeamGeneralGameStatistic => GetTeamGeneralGameStatitistic(1);

        public GameGeneralViewStatitistic GuestTeamGeneralGameStatistic => GetTeamGeneralGameStatitistic(2);

        public CoachGameViewModel GetCoach(int typeId)
        {
            var coach = this.Data
                .TeamsStatistic
                .Where(ts => ts.Type.id == typeId)
                .First()
                .Side.Coach;

            var result = new CoachGameViewModel
            {
                Id = coach.Id,
                Name = coach.Name,
                CountryPicture = coach.Country.PicturePath
            };

            return result;
        }

        public StadiumModel GetStadium()
        {
            var stadium = this.Data
                .TeamsStatistic
                .Where(ts => ts.Type.id == 1)
                .First().Side.Team.Stadium;

            var result = new StadiumModel
            {
                Id = stadium.Id,
                Name = stadium.Name,
                City = stadium.City
            };

            return result;
        }

        private GameGeneralViewStatitistic GetTeamGeneralGameStatitistic(int typeId)
            => this.Data
                 .TeamsStatistic
                 .Where(ts => ts.Type.id == typeId)
                 .Select(g => new GameGeneralViewStatitistic
                 {
                     BallPossession = g.BallPossession,
                     Corners = g.Corners,
                     ShotsOnTarget = g.ShotsOnTarget,
                     ShotsWide = g.ShotsWide,
                     Fouls = g.Fouls,
                     Offsides = g.Offsides
                 }).First();

        private List<SidelineViewModel> GetSidelines(int typeId)
            => this.Data
                 .TeamsStatistic
                 .Where(ts => ts.Type.id == typeId)
                 .First()
                 .Sidelines
                 .Select(s => new SidelineViewModel
                 {
                     PlayerId = s.Player.Id,
                     PlayerName = s.Player.Name,
                     SidelineReasonPicture = s.Reason.Picture,
                     CountryPicture = s.Player.Country.PicturePath
                 }).ToList();

        private List<GameRefereeViewModel> GetReferees()
            => this.Data
                 .Referees.Select(r => new GameRefereeViewModel
                 {
                     RefereeId = r.Referee.Id,
                     Name = r.Referee.Name,
                     TypeId = r.Type.Id,
                     CountryPicture = r.Referee.Country.PicturePath
                 }).ToList();


        private List<ResViewModel> GetReserves(int typeId)
            => this.Data
                .TeamsStatistic
                .Where(ts => ts.Type.id == typeId)
                .First()
                .Reserves.Select(r => new ResViewModel
                {
                    PlayerId = r.PlayerId,
                    PlayerName = r.Player.Name,
                    PlayerNumber = r.Player.PlayerNumber,
                    CountryPicture = r.Player.Country.PicturePath
                }).ToList();

        private int GoalsHalfTimeCount(int typeId)
            => this.Data
                .TeamsStatistic
                .Where(ts => ts.Type.id == typeId).First()
                .Goals
                .Where(g => g.FirstHalf == true && g.Type.Id == 1)
                .Count();

        private int GoalsCount(int typeId)
        {
            var scoredGoals = this.Data
               .TeamsStatistic
               .Where(ts => ts.Type.id == typeId).First()
               .Goals
               .Where(g => g.Type.Id == 1 || g.Type.Id == 2)
               .Count();

            var rivalTeamOwnGoals = this.Data
                .TeamsStatistic
                .Where(ts => ts.Type.id != typeId).First()
                .Goals
                .Where(g => g.Type.Id == 3)
                .Count();

            return scoredGoals + rivalTeamOwnGoals;
        }


        private string TeamLogo(int typeId)
            => this.Data
               .TeamsStatistic
               .Where(ts => ts.Type.id == typeId).First()
               .Side.Team.Logo;

        private string TeamName(int typeId)
            => this.Data
               .TeamsStatistic
               .Where(ts => ts.Type.id == typeId).First()
               .Side.Team.Name;

        private List<LineUpViewModel> GetLineUps(int gameStatitisticType)
        {
            List<LineUpViewModel> lineUps = new List<LineUpViewModel>();

            var lineUpsStatistic = this.Data
                            .TeamsStatistic
                            .Where(gs => gs.Type.id == gameStatitisticType)
                            .First()
                            .LineUps;

            var cardsStatitistic = this.Data
                            .TeamsStatistic
                            .Where(gs => gs.Type.id == gameStatitisticType)
                            .First()
                            .Cards;

            var goalsStatistic = this.Data
                            .TeamsStatistic
                            .Where(gs => gs.Type.id == gameStatitisticType)
                            .First()
                            .Goals;

            var substitues = this.Data
                            .TeamsStatistic
                            .Where(gs => gs.Type.id == gameStatitisticType)
                            .First()
                            .Substitutes;



            foreach (var line in lineUpsStatistic)
            {
                string playerPictureOut = string.Empty;
                var cards = new Dictionary<string, string>();
                var goals = new Dictionary<string, string>();

                foreach (var card in cardsStatitistic)
                {
                    if (card.Player.Id == line.Player.Id)
                    {
                        cards[card.Type.Picture] = card.Minute.ToString().MinutesAfterHalftime(card.FirstHalf);
                    }
                }

                foreach (var goal in goalsStatistic)
                {
                    if (goal.Player.Id == line.Player.Id)
                    {
                        goals[goal.Type.picture] = goal.Minute.ToString().MinutesAfterHalftime(goal.FirstHalf);
                    }
                }

                foreach (var substitute in substitues)
                {
                    if (substitute.PlayerOut.Player.Id == line.Player.Id)
                    {
                        playerPictureOut = substitute.PlayerOut.OutIcon;
                    }
                }

                var lineUp = new LineUpViewModel
                {
                    PlayerId = line.Player.Id,
                    PlayerName = line.Player.Name,
                    Cards = cards,
                    Goals = goals,
                    PictureOut = playerPictureOut,
                    PlayerNumber = line.Player.PlayerNumber,
                    CountryPicture = line.Player.Country.PicturePath

                };

                lineUps.Add(lineUp);
            }

            return lineUps;
        }

        private List<SubstituteViewModel> Substitutes(int gameStatitisticType)
        {
            List<SubstituteViewModel> substitutes = new List<SubstituteViewModel>();

            var substitues = this.Data
                           .TeamsStatistic
                           .Where(gs => gs.Type.id == gameStatitisticType)
                           .First()
                           .Substitutes;

            var cardsStatitistic = this.Data
                            .TeamsStatistic
                            .Where(gs => gs.Type.id == gameStatitisticType)
                            .First()
                            .Cards;

            var goalsStatistic = this.Data
                            .TeamsStatistic
                            .Where(gs => gs.Type.id == gameStatitisticType)
                            .First()
                            .Goals;

            foreach (var substitute in substitues)
            {
                var cards = new Dictionary<string, string>();
                var goals = new Dictionary<string, string>();

                foreach (var card in cardsStatitistic)
                {
                    if (card.Player.Id == substitute.PlayerIn.Player.Id)
                    {
                        cards[card.Type.Picture] = card.Minute.ToString().MinutesAfterHalftime(card.FirstHalf);
                    }
                }

                foreach (var goal in goalsStatistic)
                {
                    if (goal.Player.Id == substitute.PlayerIn.Player.Id)
                    {
                        cards[goal.Type.picture] = goal.Minute.ToString().MinutesAfterHalftime(goal.FirstHalf);
                    }
                }

                var finalSubstitute = new SubstituteViewModel
                {
                    Id = substitute.Id,
                    PlayerIn = substitute.PlayerIn,
                    PlayerOut = substitute.PlayerOut,
                    Cards = cards,
                    Goals = goals,
                    Minute = substitute.PlayerOut.Minute.ToString().MinutesAfterHalftime(substitute.PlayerOut.FirstHalf)
                };

                substitutes.Add(finalSubstitute);
            }

            return substitutes;

        }

        private List<GoalViewModel> GetGoals()
        {
            List<GoalViewModel> goals = new List<GoalViewModel>();

            List<GoalModel> goalsHostStatistic = new List<GoalModel>();

            var scoredHostGoals = this.Data
                            .TeamsStatistic
                            .Where(gs => gs.Type.id == 1)
                            .First()
                            .Goals.Where(g => g.Type.Id != 4 && g.Type.Id != 5);

            var guestOwnedGoals = this.Data
                            .TeamsStatistic
                            .Where(gs => gs.Type.id == 2)
                            .First()
                            .Goals.Where(g => g.Type.Id == 3);

            foreach (var goal in scoredHostGoals)
            {
                goalsHostStatistic.Add(goal);
            };


            foreach (var goal in guestOwnedGoals)
            {
                goalsHostStatistic.Add(goal);
            }


            foreach (var goal in goalsHostStatistic)
            {
                goals.Add(new GoalViewModel
                {
                    ScorerId = goal.Player.Id,
                    ScorerName = goal.Player.Name,
                    FirstHalf = goal.FirstHalf,
                    HostGoal = true,
                    Minute = goal.Minute.ToString(),
                    GoalAsistantId = goal.Assistance.Player.Id,
                    GoalAsistantName = goal.Assistance.Player.Name,
                    CountHostGoals = 0,
                    CountGuestGoals = 0
                });
            }



            List<GoalModel> goalsGuestStatistic = new List<GoalModel>();

            var scoredGuestGoals = this.Data
                            .TeamsStatistic
                            .Where(gs => gs.Type.id == 2)
                            .First()
                            .Goals.Where(g => g.Type.Id != 4 && g.Type.Id != 5);

            var hostOwnedGoals = this.Data
                            .TeamsStatistic
                            .Where(gs => gs.Type.id == 1)
                            .First()
                            .Goals.Where(g => g.Type.Id == 3);


            foreach (var goal in scoredGuestGoals)
            {
                goalsGuestStatistic.Add(goal);
            };



            foreach (var goal in guestOwnedGoals)
            {
                goalsGuestStatistic.Add(goal);
            }



            foreach (var goal in goalsGuestStatistic)
            {

                goals.Add(new GoalViewModel
                {
                    ScorerId = goal.Player.Id,
                    ScorerName = goal.Player.Name,
                    FirstHalf = goal.FirstHalf,
                    HostGoal = false,
                    Minute = goal.Minute.ToString(),
                    GoalAsistantId = goal.Assistance.Player.Id,
                    GoalAsistantName = goal.Assistance.Player.Name,
                    CountHostGoals = 0,
                    CountGuestGoals = 0
                });
            }


            int countHostGoal = 0;
            int countGuestGoal = 0;


            foreach (var goal in goals.OrderByDescending(g => g.FirstHalf).ThenBy(g => int.Parse(g.Minute)))
            {
                if (goal.HostGoal == true)
                {
                    countHostGoal += 1;
                    goal.CountHostGoals = countHostGoal;
                    goal.CountGuestGoals = countGuestGoal;
                }

                else
                {
                    countGuestGoal += 1;
                    goal.CountHostGoals = countHostGoal;
                    goal.CountGuestGoals = countGuestGoal;
                }
            }

            return goals
                .OrderByDescending(g => g.FirstHalf)
                .ThenBy(g => int.Parse(g.Minute))
                .Select(g => new GoalViewModel
                {
                    CountGuestGoals = g.CountGuestGoals,
                    CountHostGoals = g.CountHostGoals,
                    FirstHalf = g.FirstHalf,
                    GoalAsistantId = g.GoalAsistantId,
                    GoalAsistantName = g.GoalAsistantName,
                    HostGoal = g.HostGoal,
                    ScorerId = g.ScorerId,
                    ScorerName = g.ScorerName,
                    Minute = g.Minute.ToString().MinutesAfterHalftime(g.FirstHalf)
                }).ToList();
        }

    }
}
