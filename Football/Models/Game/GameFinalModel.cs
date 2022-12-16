namespace Sportiada.Services.Football.Models.Game
{
    using Card;
    using Models.Coach;
    using Models.Goal;
    using Models.GameReferee;
    using Models.GameStatitistic;
    using Models.LineUp;
    using Models.Sideline;
    using Models.Stadium;
    using Models.Substitute;
    using Models.Reserve;
    using Infrastructure.Constants;
    using Infrastructure.Extensions;
    using System.Collections.Generic;
    using System.Linq;

    public class GameFinalModel
    {
        public GameWithStatisticModel Data { get; set; }

        public List<LineUpFinalModel> LineUpsHost => GetLineUps(1);

        public List<LineUpFinalModel> LineUpsGuest => GetLineUps(2);

        public List<SubstituteFinalModel> SubstitutesHost => Substitutes(1);

        public List<SubstituteFinalModel> SubstitutesGuest => Substitutes(2);

        public List<GoalFinalModel> Goals => GetGoals();

        public List<ResFinalModel> HostTeamReserves => GetReserves(1);

        public List<ResFinalModel> GuestTeamReserves => GetReserves(2);

        public List<GameRefereeViewModel> GameReferees => GetReferees();

        public string HostName => TeamName(1);

        public string GuestName => TeamName(2);

        public string HostTeamLogo => TeamLogo(1);

        public string GuestTeamLogo => TeamLogo(2);

        public int HostGoalsCount => GoalsCount(1);

        public int GuestGoalsCount => GoalsCount(2);

        public int HostGoalsFirstHalfCount => GoalsHalfTimeCount(1);

        public int GuestGoalsFirstHalfCount => GoalsHalfTimeCount(2);

        public string Date => Data.Date.ToShortDateString();

        public string Time => Data.Date.ToShortTimeString();

        public string Round => Data.Round.Name;

        public string Tournament => Data.Round.Competition.Tournament.Name;

        public string SeasonName => Data.Round.Competition.Season.Name;

        public int SeasonId => Data.Round.Competition.Season.Id;

        public int Atendance => Data.Attendance;

        public CoachGameViewModel HostTeamCoach => GetCoach(1);

        public CoachGameViewModel GuestTeamCoach => GetCoach(2);

        public StadiumModel Stadium => GetStadium();

        public List<SidelineFinalModel> HostTeamSidelines => GetSidelines(1);

        public List<SidelineFinalModel> GuestTeamSidelines => GetSidelines(2);

        public GameGeneralViewStatitistic HostTeamGeneralGameStatistic => GetTeamGeneralGameStatitistic(1);

        public GameGeneralViewStatitistic GuestTeamGeneralGameStatistic => GetTeamGeneralGameStatitistic(2);

        public CoachGameViewModel GetCoach(int typeId)
        {
            var coach = this.Data
                .TeamsStatistic
                .Where(ts => ts.Type.id == typeId)
                .First()
                .Squad.Coach;

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
                .First().Squad.Team.Stadium;

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

        private List<SidelineFinalModel> GetSidelines(int typeId)
            => this.Data
                 .TeamsStatistic
                 .Where(ts => ts.Type.id == typeId)
                 .First()
                 .Sidelines
                 .Where(s => s.Player.Id != 279)
                 .Select(s => new SidelineFinalModel
                 {
                     PlayerId = s.Player.Id,
                     PlayerName = s.Player.Name,
                     SidelineReasonPicture = s.Reason.Picture,
                     PlayerPicture = s.Player.PlayerPicture,
                     CountryPicture = s.Player.Country.PicturePath
                 }).ToList();

        private List<GameRefereeViewModel> GetReferees()
            => this.Data
                 .Referees.Select(r => new GameRefereeViewModel
                 {
                     RefereeId = r.Referee.Id,
                     Name = r.Referee.Name,
                     TypeId = r.Type.Id,
                     TypeName = r.Type.Name,
                     CountryPicture = r.Referee.Country.PicturePath
                 }).ToList();


        private List<ResFinalModel> GetReserves(int typeId)
            => this.Data
                .TeamsStatistic
                .Where(ts => ts.Type.id == typeId)
                .First()
                .Reserves.Select(r => new ResFinalModel
                {
                    PlayerId = r.PlayerId,
                    PlayerName = r.Player.Name,
                    PlayerNumber = r.Player.PlayerNumber,
                    PlayerPicture = r.Player.PlayerPicture,
                    CountryPicture = r.Player.Country.PicturePath
                }).ToList();

        private int GoalsHalfTimeCount(int typeId)
        {
            var scoredGoals = this.Data
               .TeamsStatistic
               .Where(ts => ts.Type.id == typeId).First()
               .Goals
               .Where(g => g.FirstHalf == true  && (g.Type.Name == Constants.GOAL || g.Type.Name == Constants.PENALTY_GOAL))
               .Count();

            var rivalTeamOwnGoals = this.Data
                .TeamsStatistic
                .Where(ts => ts.Type.id != typeId).First()
                .Goals
                .Where(g => g.FirstHalf == true && g.Type.Name == Constants.OWN_GOAL)
                .Count();

            return scoredGoals + rivalTeamOwnGoals;
        }

        private int GoalsCount(int typeId)
        {
            var scoredGoals = this.Data
               .TeamsStatistic
               .Where(ts => ts.Type.id == typeId).First()
               .Goals
               .Where(g => g.Type.Name == Constants.GOAL || g.Type.Name == Constants.PENALTY_GOAL)
               .Count();

            var rivalTeamOwnGoals = this.Data
                .TeamsStatistic
                .Where(ts => ts.Type.id != typeId).First()
                .Goals
                .Where(g => g.Type.Name == Constants.OWN_GOAL)
                .Count();

            return scoredGoals + rivalTeamOwnGoals;
        }


        private string TeamLogo(int typeId)
            => this.Data
               .TeamsStatistic
               .Where(ts => ts.Type.id == typeId).First()
               .Squad.Team.Logo;

        private string TeamName(int typeId)
            => this.Data
               .TeamsStatistic
               .Where(ts => ts.Type.id == typeId).First()
               .Squad.Team.Name;

        private List<LineUpFinalModel> GetLineUps(int gameStatitisticType)
        {
            List<LineUpFinalModel> lineUps = new List<LineUpFinalModel>();

            var lineUpsStatistic = GetLineUpsStatistic(gameStatitisticType);

            var cardsStatitistic = GetCardsStatistic(gameStatitisticType);

            var goalsStatistic = GetGoalsStatistic(gameStatitisticType);

            var substitues = GetSubstituteStatistic(gameStatitisticType);

            foreach (var line in lineUpsStatistic)
            {
                string playerPictureOut = string.Empty;
                var cards = new Dictionary<string, string>();
                var goals = new Dictionary<string, List<string>>();
                var goalProperties = new List<string>();

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
                        goalProperties.Add(goal.Minute.ToString().MinutesAfterHalftime(goal.FirstHalf));
                        goals[goal.Type.picture] = goalProperties;
                    }
                }

                foreach (var substitute in substitues)
                {
                    if (substitute.PlayerOut.Player.Id == line.Player.Id)
                    {
                        playerPictureOut = substitute.PlayerOut.OutIcon;
                    }
                }

                var lineUp = new LineUpFinalModel
                {
                    PlayerId = line.Player.Id,
                    PlayerName = line.Player.Name,
                    PlayerPicture = line.Player.PlayerPicture,
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

        private List<SubstituteFinalModel> Substitutes(int gameStatitisticType)
        {
            List<SubstituteFinalModel> substitutes = new List<SubstituteFinalModel>();

            var substitues = GetSubstituteStatistic(gameStatitisticType);

            var cardsStatitistic = GetCardsStatistic(gameStatitisticType);

            var goalsStatistic = GetGoalsStatistic(gameStatitisticType);

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

                var finalSubstitute = new SubstituteFinalModel
                {
                    Id = substitute.Id,
                    PlayerIn = substitute.PlayerIn,
                    PlayerOut = substitute.PlayerOut,
                    Cards = cards,
                    Goals = goals,
                    Minute = substitute.Minute.ToString().MinutesAfterHalftime(substitute.FirstHalf)
                };

                substitutes.Add(finalSubstitute);
            }

            return substitutes;

        }

        private List<GoalFinalModel> GetGoals()
        {
            List<GoalFinalModel> goals = new List<GoalFinalModel>();

            List<GoalModel> goalsHostStatistic = new List<GoalModel>();

            var scoredHostGoals = this.Data
                            .TeamsStatistic
                            .Where(gs => gs.Type.id == 1)
                            .First()
                            .Goals.Where(g => g.Type.Name == "Гол" || g.Type.Name == "Гол от дузпа");

            var guestOwnedGoals = this.Data
                            .TeamsStatistic
                            .Where(gs => gs.Type.id == 2)
                            .First()
                            .Goals.Where(g => g.Type.Name == "Автогол");

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
                goals.Add(new GoalFinalModel
                {
                    ScorerId = goal.Player.Id,
                    ScorerName = goal.Player.Name,
                    FirstHalf = goal.FirstHalf,
                    HostGoal = true,
                    Minute = goal.Minute.ToString(),
                    GoalAsistantId = goal.Assistance != null ? goal.Assistance.Player.Id : 0,
                    GoalAsistantName = goal.Assistance != null ? goal.Assistance.Player.Name : "Неизвестен",
                    CountHostGoals = 0,
                    CountGuestGoals = 0
                });
            }



            List<GoalModel> goalsGuestStatistic = new List<GoalModel>();

            var scoredGuestGoals = this.Data
                            .TeamsStatistic
                            .Where(gs => gs.Type.id == 2)
                            .First()
                            .Goals.Where(g => g.Type.Name == "Гол" || g.Type.Name == "Гол от дузпа");

            var hostOwnedGoals = this.Data
                            .TeamsStatistic
                            .Where(gs => gs.Type.id == 1)
                            .First()
                            .Goals.Where(g => g.Type.Name == "Автогол");


            foreach (var goal in scoredGuestGoals)
            {
                goalsGuestStatistic.Add(goal);
            };



            foreach (var goal in hostOwnedGoals)
            {
                goalsGuestStatistic.Add(goal);
            }



            foreach (var goal in goalsGuestStatistic)
            {

                goals.Add(new GoalFinalModel
                {
                    ScorerId = goal.Player.Id,
                    ScorerName = goal.Player.Name,
                    FirstHalf = goal.FirstHalf,
                    HostGoal = false,
                    Minute = goal.Minute.ToString(),
                    GoalAsistantId = goal.Assistance != null ? goal.Assistance.Player.Id : 0,
                    GoalAsistantName = goal.Assistance != null ? goal.Assistance.Player.Name : "Неизвестен",
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
                .Select(g => new GoalFinalModel
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

        private List<LineUpModel> GetLineUpsStatistic(int gameStatitisticType)
           => this.Data
               .TeamsStatistic
               .Where(gs => gs.Type.id == gameStatitisticType)
               .First()
               .LineUps;

        private List<CardModel> GetCardsStatistic(int gameStatitisticType)
            => this.Data
                 .TeamsStatistic
                 .Where(gs => gs.Type.id == gameStatitisticType)
                 .First()
                 .Cards;

        private List<GoalModel> GetGoalsStatistic(int gameStatitisticType)
            =>this.Data
                .TeamsStatistic
                .Where(gs => gs.Type.id == gameStatitisticType)
                .First()
                .Goals;

        private List<SubstituteModel> GetSubstituteStatistic(int gameStatitisticType)
            =>this.Data
                 .TeamsStatistic
                 .Where(gs => gs.Type.id == gameStatitisticType)
                 .First()
                 .Substitutes;

    }
}
