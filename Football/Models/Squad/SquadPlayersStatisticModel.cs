namespace Sportiada.Services.Football.Models.Squad
{
    using Game;
    using Infrastructure.Constants;
    using Infrastructure.Extensions;
    using Player;
    using Models.Season;
    using Models.Card;
    using Models.Goal;
    using Models.GoalAssistance;
    using Substitute;
    using System.Collections.Generic;
    using System.Linq;
    using Sportiada.Services.Football.Models.Coach;
    using Sportiada.Services.Football.Models.SquadCoach;

    public class SquadPlayersStatisticModel
    {
        public int TeamId { get; set; }
        public IEnumerable<GameWithStatisticModel> Games { get; set; }

        public IEnumerable<PlayerProfileModel> Players { get; set; }

        public IEnumerable<SquadCoachModel> Coaches { get; set; }

        public IDictionary<string, IEnumerable<PlayerGameStatisticModel>> PlayersGamesStatistic
            => GetPlayersGamesStatistic();

        private IDictionary<string, IEnumerable<PlayerGameStatisticModel>> GetPlayersGamesStatistic()
        {
            IDictionary<string, IEnumerable<PlayerGameStatisticModel>> result 
                = new Dictionary<string, IEnumerable<PlayerGameStatisticModel>>();

            foreach (var p in Players)
            {
                result.Add(p.Name, GetPlayerGamesStatitistic(p));
            }

            return result;
        }
        private IEnumerable<PlayerGameStatisticModel> GetPlayerGamesStatitistic(PlayerProfileModel player)
        {
            var result = Games
                .Select(g => new PlayerGameStatisticModel
                {
                    GameId = g.Id,
                    PlayerId = player.Id,
                    TournamentName = g.Round.Competition.Tournament.Name,
                    SeasonName = g.Round.Competition.Season.Name,
                    PlayerTeam = g.TeamsStatistic.Where(t => t.Squad.Team.Id == TeamId).FirstOrDefault().Squad.Team.Name,
                    RivalTeam = g.TeamsStatistic.Where(t => t.Squad.Team.Id != TeamId).FirstOrDefault().Squad.Team.Name,
                    InLineUp = IsInLineUp(g, player.Id, TeamId),
                    IsReserve = IsInReserves(g, player.Id, TeamId),
                    Sideline = IsInSideline(g, player.Id, TeamId),
                    SidelineReasons = GetSidelineReson(g, player.Id, TeamId),
                    SubstituteIn = GetSubstitutesIn(g, player.Id, TeamId),
                    SubstituteOut = GetSubstitutesOut(g, player.Id, TeamId),
                    ScoredGoals = GetScoredGoals(g, player.Id, TeamId),
                    GoalAssistances = GetGoalAssistences(g, player.Id, TeamId),
                    OwnGoals = GetGoals(g, player.Id, TeamId, Constants.OWN_GOAL),
                    AllowedGoals = GetAllowedGoals(g, player, TeamId),
                    RedCard = GetCard(g, player.Id, TeamId, Constants.RED_CARD),
                    YellowCard = GetCard(g, player.Id, TeamId, Constants.YELLOW_CARD),
                    SecondYellowCard = GetCard(g, player.Id, TeamId, Constants.SECOND_YELLOW_CARD),
                    MissedPenalties = GetGoals(g, player.Id, TeamId, Constants.PENALTY_MISSED),
                    SavedPenalties = GetSavedPenalties(g, player, TeamId),
                    ScoredPenalties = GetGoals(g, player.Id, TeamId, Constants.PENALTY_GOAL)
                });

            return result;
        }

        private IEnumerable<string> GetSidelineReson(GameWithStatisticModel game, int playerId, int teamId)
        {
            var reasons = game
                .TeamsStatistic
                .Where(s => s.Squad.Team.Id == teamId)
                .FirstOrDefault()
                .Sidelines
                .Where(s => s.Player.Id == playerId)
                .Select(s => s.Reason.Picture);

            return reasons;
        }

        private bool IsInLineUp(GameWithStatisticModel game, int playerId, int teamId)
        {
            var lineUps = game.TeamsStatistic.Where(s => s.Squad.Team.Id == teamId).FirstOrDefault().LineUps;

            bool isInLineUps = lineUps
                .Exists(l => l.Player.Id == playerId);

            return isInLineUps;
        }

        private bool IsInReserves(GameWithStatisticModel game, int playerId, int teamId)
        {
            var reserves = game.TeamsStatistic.Where(s => s.Squad.Team.Id == teamId).FirstOrDefault().Reserves.ToList();

            bool inReserves = reserves.Exists(r => r.Player.Id == playerId);

            return inReserves;
        }

        private bool IsInSideline(GameWithStatisticModel game, int playerId, int teamId)
        {
            bool inSidelines = false;
            var lineUps = game.TeamsStatistic.Where(s => s.Squad.Team.Id == teamId).FirstOrDefault().LineUps.ToList();
            var reserves = game.TeamsStatistic.Where(s => s.Squad.Team.Id == teamId).FirstOrDefault().Reserves.ToList();

            if (!lineUps.Exists(l => l.Player.Id == playerId) && !reserves.Exists(r => r.Player.Id == playerId))
            {
                inSidelines = true;
            }

            return inSidelines;
        }

        private IEnumerable<GoalPlayerStatisticModel> GetGoals(GameWithStatisticModel game, int playerId, int teamId, string goalType)
        {
            var goals = game.
                TeamsStatistic.
                Where(s => s.Squad.Team.Id == teamId).FirstOrDefault()
                .Goals
                .Where(gl => gl.Type.Name == goalType && gl.Player.Id == playerId)
                .Select(gl => new GoalPlayerStatisticModel 
                {
                    Icon = gl.Type.picture,
                    Minute = gl.Minute.ToString().MinutesAfterHalftime(gl.FirstHalf)
                });

            return goals;
        }

        private IEnumerable<GoalPlayerStatisticModel> GetScoredGoals(GameWithStatisticModel game, int playerId, int teamId)
        {
            var goals = game.
                TeamsStatistic.
                Where(s => s.Squad.Team.Id == teamId).FirstOrDefault()
                .Goals
                .Where(gl => (gl.Type.Name == Constants.GOAL || gl.Type.Name == Constants.PENALTY_GOAL) && gl.Player.Id == playerId)
                .Select(gl => new GoalPlayerStatisticModel
                {
                    Icon = gl.Type.picture,
                    Minute = gl.Minute.ToString().MinutesAfterHalftime(gl.FirstHalf)
                });

            return goals;
        }

        private IEnumerable<GoalAssistancePlayerStatisticModel> GetGoalAssistences(GameWithStatisticModel game, int playerId, int teamId)
        {
            var goalAssistance = game
                .TeamsStatistic
                .Where(s => s.Squad.Team.Id == teamId)
                .FirstOrDefault()
                .Goals
                .Where(gl => gl.Assistance != null && gl.Assistance.Player.Id == playerId)
                .Select(gl => new GoalAssistancePlayerStatisticModel
                {
                    Minute = gl.Minute.ToString().MinutesAfterHalftime(gl.FirstHalf)
                });

            return goalAssistance;
        }

        private IEnumerable<GoalPlayerStatisticModel> GetAllowedGoals(GameWithStatisticModel game, PlayerProfileModel Player, int teamId)
        {
            List<GoalPlayerStatisticModel> allowedGoals = new List<GoalPlayerStatisticModel>();

            bool isGoalkeeper = Player.Position == Constants.GOALKEEPER;
            var rivalGoals = game.TeamsStatistic.Where(s => s.Squad.Team.Id != teamId).FirstOrDefault().Goals;
            var substituteIn = GetSubstitutesIn(game, Player.Id, teamId);
            var substituteOut = GetSubstitutesOut(game, Player.Id, teamId);

            if (substituteOut != null && isGoalkeeper)
            {
                rivalGoals
                    .Where(rg => rg.Minute < int.Parse(substituteOut.Minute) && rg.Type.Name != Constants.PENALTY_MISSED)
                    .Select(rg => new GoalPlayerStatisticModel
                    {
                        Icon = rg.Type.picture,
                        Minute = rg.Minute.ToString().MinutesAfterHalftime(rg.FirstHalf)
                    })
                    .ToList()
                    .ForEach(rg =>
                    {
                        allowedGoals.Add(rg);
                    });


            }
            else if (substituteIn != null && isGoalkeeper )
            {
                rivalGoals
                    .Where(rg => rg.Minute > int.Parse(substituteIn.Minute) && rg.Type.Name != Constants.PENALTY_MISSED)
                    .Select(rg => new GoalPlayerStatisticModel
                    {
                        Icon = rg.Type.picture,
                        Minute = rg.Minute.ToString().MinutesAfterHalftime(rg.FirstHalf)
                    })
                    .ToList()
                    .ForEach(rg =>
                    {
                        allowedGoals.Add(rg);
                    });
            }
            else if (substituteOut != null && substituteIn != null && isGoalkeeper )
            {
                rivalGoals
                    .Where(rg => rg.Minute > int.Parse(substituteIn.Minute) && rg.Minute < int.Parse(substituteOut.Minute) && rg.Type.Name != Constants.PENALTY_MISSED)
                    .Select(rg => new GoalPlayerStatisticModel
                    {
                        Icon = rg.Type.picture,
                        Minute = rg.Minute.ToString().MinutesAfterHalftime(rg.FirstHalf)
                    })
                    .ToList()
                    .ForEach(rg =>
                    {
                        allowedGoals.Add(rg);
                    });

            }

            else if (substituteOut == null && substituteIn == null && isGoalkeeper && IsInLineUp(game, Player.Id, teamId))
            {
                rivalGoals
                    .Where(rg => rg.Type.Name != Constants.PENALTY_MISSED)
                    .Select(rg => new GoalPlayerStatisticModel
                    {
                        Icon = rg.Type.picture,
                        Minute = rg.Minute.ToString().MinutesAfterHalftime(rg.FirstHalf)
                    })
                    .ToList()
                    .ForEach(rg =>
                    {
                        allowedGoals.Add(rg);
                    });

            }

            return allowedGoals;

        }

        private IEnumerable<GoalPlayerStatisticModel> GetSavedPenalties(GameWithStatisticModel game, PlayerProfileModel Player, int teamId)
        {
            List<GoalPlayerStatisticModel> savedPenalties = new List<GoalPlayerStatisticModel>();

            var rivalMissedPenalties = game
                .TeamsStatistic.Where(s => s.Squad.Team.Id != teamId)
                .FirstOrDefault()
                .Goals
                .Where(g => g.Type.Name == Constants.PENALTY_MISSED)
                .ToList();

            

            bool isGoalkeeper = Player.Position == Constants.GOALKEEPER;

            var substituteIn = GetSubstitutesIn(game, Player.Id, teamId);
            var substituteOut = GetSubstitutesOut(game, Player.Id, teamId);

            if (substituteOut != null && isGoalkeeper)
            {
                rivalMissedPenalties
                   .Where(rg => rg.Minute < int.Parse(substituteOut.Minute))
                   .Select(rg => new GoalPlayerStatisticModel
                   {
                       Icon = rg.Type.picture,
                       Minute = rg.Minute.ToString().MinutesAfterHalftime(rg.FirstHalf)
                   })
                   .ToList()
                   .ForEach(rg =>
                   {
                       savedPenalties.Add(rg);
                   });
            }
            else if (substituteIn != null && isGoalkeeper)
            {
                rivalMissedPenalties
                    .Where(rg => rg.Minute > int.Parse(substituteIn.Minute))
                    .Select(rg => new GoalPlayerStatisticModel
                    {
                        Icon = rg.Type.picture,
                        Minute = rg.Minute.ToString().MinutesAfterHalftime(rg.FirstHalf)
                    })
                   .ToList()
                   .ForEach(rg =>
                   {
                       savedPenalties.Add(rg);
                   });
            }
            else if (substituteOut != null && substituteIn != null && isGoalkeeper)
            {
                rivalMissedPenalties
                    .Where(rg => rg.Minute > int.Parse(substituteIn.Minute) && rg.Minute < int.Parse(substituteOut.Minute))
                    .Select(rg => new GoalPlayerStatisticModel
                    {
                        Icon = rg.Type.picture,
                        Minute = rg.Minute.ToString().MinutesAfterHalftime(rg.FirstHalf)
                    })
                   .ToList()
                   .ForEach(rg =>
                   {
                       savedPenalties.Add(rg);
                   });

            }
            else if (substituteOut == null && substituteIn == null && isGoalkeeper
                    && IsInLineUp(game, Player.Id, teamId))
            {
                rivalMissedPenalties
                    .Select(rg => new GoalPlayerStatisticModel
                    {
                        Icon = rg.Type.picture,
                        Minute = rg.Minute.ToString().MinutesAfterHalftime(rg.FirstHalf)
                    })
                    .ToList()
                    .ForEach(rg =>
                    {
                        savedPenalties.Add(rg);
                    });

            }

            return savedPenalties;
        }


        private CardPlayerStatisticModel GetCard(GameWithStatisticModel game, int playerId, int teamId, string cardType)
        {
            var card = game.TeamsStatistic
                .Where(s => s.Squad.Team.Id == teamId)
                .FirstOrDefault()
                .Cards.Where(c => c.Type.Name == cardType && c.Player.Id == playerId)
                .Select(c => new CardPlayerStatisticModel 
                {
                    Icon = c.Type.Picture,
                    Minute = c.Minute.ToString().MinutesAfterHalftime(c.FirstHalf)
                }).FirstOrDefault();

            return card;
        }
   
        private SubstituteInModel GetSubstitutesIn(GameWithStatisticModel game, int playerId, int teamId)
        {
            var substitute = game.TeamsStatistic.Where(s => s.Squad.Team.Id == teamId).FirstOrDefault()
                .Substitutes
                .Where(s => s.PlayerIn.Player.Id == playerId)
                .FirstOrDefault();

            if (substitute != null)
            {
                var substituteIn = new SubstituteInModel
                {
                    Minute = substitute.Minute,
                    Icon = substitute.PlayerIn.InIcon
                };
                return substituteIn;
            }
            else
            {
                return null;
            }
        }

        private SubstituteOutModel GetSubstitutesOut(GameWithStatisticModel game, int playerId, int teamId)
        {
            var substitute = game.TeamsStatistic.Where(s => s.Squad.Team.Id == teamId).FirstOrDefault()
                .Substitutes
                .Where(s => s.PlayerOut.Player.Id == playerId)
                .FirstOrDefault();

            if (substitute != null)
            {
                var substituteOut = new SubstituteOutModel
                {
                    Minute = substitute.Minute,
                    Icon = substitute.PlayerOut.OutIcon
                };
                return substituteOut;
            }
            else
            {
                return null;
            }
        }




    }
}
