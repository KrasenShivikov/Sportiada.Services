namespace Sportiada.Services.Football.Models.Squad
{
    using Infrastructure.Constants;
    using Models.Card;
    using Models.Game;
    using Models.GameStatitistic;
    using Models.Goal;
    using Models.GoalAssistance;
    using Models.Player;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class SquadSeasonStatisticModel
    {
        public int TeamId { get; set; }
        public IEnumerable<GameWithStatisticModel> Games { get; set; }

        public int OverallWins => GetSquadGamesResults().Where(g => g.ScoredGoals > g.AllowedGoals).Count();

        public int OverallLosses => GetSquadGamesResults().Where(g => g.ScoredGoals < g.AllowedGoals).Count();

        public int OverallDraws => GetSquadGamesResults().Where(g => g.ScoredGoals == g.AllowedGoals).Count();

        public int HomeWins => GetSquadGamesResults().Where(g => g.ScoredGoals > g.AllowedGoals && g.IsHost).Count();

        public int AwayWins => GetSquadGamesResults().Where(g => g.ScoredGoals > g.AllowedGoals && !g.IsHost).Count();

        public int HomeLosses => GetSquadGamesResults().Where(g => g.ScoredGoals < g.AllowedGoals && g.IsHost).Count();

        public int AwayLosses => GetSquadGamesResults().Where(g => g.ScoredGoals < g.AllowedGoals && !g.IsHost).Count();

        public int NomeDraws => GetSquadGamesResults().Where(g => g.ScoredGoals == g.AllowedGoals && g.IsHost).Count();

        public int AwayDraws => GetSquadGamesResults().Where(g => g.ScoredGoals == g.AllowedGoals && !g.IsHost).Count();

        public IEnumerable<SquadSeasonGameStatistic> SquadGamesResults => GetSquadGamesResults();

        public IEnumerable<CardsByPlayerModel> YellowCard => GetYellowCardByPlayerByTournament();

        public IEnumerable<CardsByPlayerModel> RedCard => GetRedCardByPlayerByTournament();

        public IEnumerable<GoalsByPlayerModel> ScoredGoals => GetScoredGoalsByPlayerByTournament();

        public IEnumerable<GoalsByPlayerModel> OwnGoals => GetOwnGoalsByPlayerByTournament();

        public IEnumerable<GoalAssistanceByPlayerModel> GoalAssistances => GetGoalAssistancesByPlayerByTournament();

        public IEnumerable<GoalsByPlayerModel> ScoredPenalties => GetScoredPenaltiesByPlayerByTournament();

        public IEnumerable<GoalsByPlayerModel> MissedPenalties => GetMissedPenaltiesByPlayerByTournament();

        public IEnumerable<GoalsByPlayerModel> SavedPenalties => GetSavedPenaltiesByPlayerByTournament();


        private IEnumerable<SquadSeasonGameStatistic> GetSquadGamesResults()
        {
            var gamesResults = Games
                .Select(g => new SquadSeasonGameStatistic
                {
                    Tournament = g.Round.Competition.Tournament.Name,
                    Round = g.Round.Name,
                    CoachName = g.TeamsStatistic
                    .Where(s => s.Squad.Team.Id == TeamId)
                    .FirstOrDefault()
                    .Squad
                    .Coach.Name,
                    Opponent = g.TeamsStatistic
                    .Where(s => s.Squad.Team.Id != TeamId)
                    .FirstOrDefault()
                    .Squad.Team.Name,
                    IsHost = g.TeamsStatistic
                    .Where(s => s.Squad.Team.Id == TeamId)
                    .FirstOrDefault().Type.id == 1 ? true : false,
                    ScoredGoals = g.TeamsStatistic
                    .Where(s => s.Squad.Team.Id == TeamId)
                    .FirstOrDefault()
                    .Goals.Count(),
                    AllowedGoals = g.TeamsStatistic
                    .Where(s => s.Squad.Team.Id != TeamId)
                    .FirstOrDefault()
                    .Goals.Count()
                });

            return gamesResults;
        }
        private IEnumerable<GoalsByPlayerModel>GetSavedPenaltiesByPlayerByTournament()
        {
            var savedPenalties = GetSavedPanalties();
            return GetGoalsByPlayerByTournament(savedPenalties);
        }
        private IEnumerable<GoalsByPlayerModel> GetScoredGoalsByPlayerByTournament()
        {
            var goals = GetScoredGoals();
            return GetGoalsByPlayerByTournament(goals);
        }

        private IEnumerable<GoalsByPlayerModel> GetOwnGoalsByPlayerByTournament()
        {
            var ownGoals = GetGoalsByType(Constants.OWN_GOAL);
            return GetGoalsByPlayerByTournament(ownGoals);
        }

        private IEnumerable<GoalsByPlayerModel> GetScoredPenaltiesByPlayerByTournament()
        {
            var scoredPenalties = GetGoalsByType(Constants.PENALTY_GOAL);
            return GetGoalsByPlayerByTournament(scoredPenalties);
        }

        private IEnumerable<GoalsByPlayerModel> GetMissedPenaltiesByPlayerByTournament()
        {
            var missedPenalties = GetGoalsByType(Constants.PENALTY_MISSED);
            return GetGoalsByPlayerByTournament(missedPenalties);
        }

        private IEnumerable<CardsByPlayerModel> GetYellowCardByPlayerByTournament()
        {
            var cards = GetCards()
                .Where(c => c.CardType == Constants.YELLOW_CARD);

            return GetCardsByPlayerByTournament(cards);
        }

        private IEnumerable<CardsByPlayerModel> GetRedCardByPlayerByTournament()
        {
            var cards = GetCards()
                .Where(c => c.CardType == Constants.RED_CARD || c.CardType == Constants.SECOND_YELLOW_CARD);

            return GetCardsByPlayerByTournament(cards);
        }

        private IEnumerable<GoalAssistancesByPlayerByTournamentModel> GetGoalAssistances()
        {
            List<GoalAssistancesByPlayerByTournamentModel> ga = new List<GoalAssistancesByPlayerByTournamentModel>();

            foreach (var game in Games)
            {
                foreach (var gs in game.TeamsStatistic.Where(s => s.Squad.Team.Id == TeamId))
                {
                    foreach (var goal in gs.Goals)
                    {
                        if (goal.Assistance != null)
                        {
                            ga.Add(new GoalAssistancesByPlayerByTournamentModel
                            {
                                PlayerName = goal.Assistance.Player.Name,
                                TournamentName = game.Round.Competition.Tournament.Name
                            });
                        }         
                    }
                }
            }

            return ga;
        }

        private IEnumerable<GoalByPlayerByTurnamentModel> GetGoals()
        {
            List<GoalByPlayerByTurnamentModel> goals = new List<GoalByPlayerByTurnamentModel>();

            foreach (var game in Games)
            {
                foreach (var gs in game.TeamsStatistic.Where(s => s.Squad.Team.Id == TeamId))
                {
                    foreach (var goal in gs.Goals)
                    {
                        goals.Add(new GoalByPlayerByTurnamentModel
                        {
                            PlayerName = goal.Player.Name,
                            GoalType = goal.Type.Name,
                            TournamentName = game.Round.Competition.Tournament.Name
                        });
                    }
                }
            }

            return goals;
        }

        private IEnumerable<CardByPlayerByTournamentModel> GetCards()
        {
            List<CardByPlayerByTournamentModel> cards = new List<CardByPlayerByTournamentModel>();

            foreach (var game in Games)
            {
                foreach (var gs in game.TeamsStatistic.Where(s => s.Squad.Team.Id == TeamId))
                {
                    foreach (var card in gs.Cards)
                    {
                        cards.Add(new CardByPlayerByTournamentModel
                        {
                            PlayerName = card.Player.Name,
                            CardType = card.Type.Name,
                            TournamentName = game.Round.Competition.Tournament.Name
                        });
                    }
                }
            }

            return cards;
        }

        private IEnumerable<GoalByPlayerByTurnamentModel> GetSavedPanalties()
        {
            List<GoalByPlayerByTurnamentModel> missedPenalties = new List<GoalByPlayerByTurnamentModel>();

            foreach (var game in Games)
            {
                foreach (var gs in game.TeamsStatistic.Where(s => s.Squad.Team.Id != TeamId))
                {
                    var goalkeepers = GetGameGoalkeepers(gs);

                    foreach (var goal in gs.Goals.Where(g => g.Type.Name == Constants.PENALTY_MISSED))
                    {
                        var penMinute = goal.Minute;
                        foreach (var gk in goalkeepers)
                        {
                            if (gk.IsInLineUp && gk.Out == false)
                            {
                                missedPenalties.Add(new GoalByPlayerByTurnamentModel
                                {
                                    PlayerName = goal.Player.Name,
                                    TournamentName = game.Round.Competition.Tournament.Name
                                });
                            }
                            else if (gk.In == true && gk.Out == false && gk.InMinute <= goal.Minute)
                            {
                                missedPenalties.Add(new GoalByPlayerByTurnamentModel
                                {
                                    PlayerName = goal.Player.Name,
                                    TournamentName = game.Round.Competition.Tournament.Name
                                });
                            }
                            else if (gk.In == true && gk.Out == true && gk.InMinute <= goal.Minute && goal.Minute < gk.OutMinute)
                            {
                                missedPenalties.Add(new GoalByPlayerByTurnamentModel
                                {
                                    PlayerName = goal.Player.Name,
                                    TournamentName = game.Round.Competition.Tournament.Name
                                });
                            }
                        }
                    }
                }
            }

            return missedPenalties;
        }

        private IEnumerable<GoalkeeperGameModel> GetGameGoalkeepers(GameTeamStatitisticFullModel stat)
        {
            List<GoalkeeperGameModel> goalkeepers = new List<GoalkeeperGameModel>();

            foreach (var l in stat.LineUps.Where(p => p.Player.Position == Constants.GOALKEEPER))
            {
                GoalkeeperGameModel gk = new GoalkeeperGameModel
                {
                    PlayerId = l.Player.Id,
                    IsInLineUp = true,
                    Name = l.Player.Name
                };
                goalkeepers.Add(gk);
            }

            foreach (var item in stat.Substitutes)
            {
                if (item.PlayerIn.Player.Position == Constants.GOALKEEPER)
                {
                    GoalkeeperGameModel gk = new GoalkeeperGameModel
                    {
                        PlayerId = item.PlayerIn.Player.Id,
                        IsInLineUp = false,
                        In = true,
                        InMinute = int.Parse(item.Minute),
                        InFirstHalf = item.FirstHalf
                    };

                    goalkeepers.Add(gk);
                }

                if (goalkeepers.Exists(gk => gk.PlayerId == item.PlayerOut.Player.Id))
                {
                    GoalkeeperGameModel gk = goalkeepers.Find(p => p.PlayerId == item.PlayerOut.Player.Id);
                    gk.OutMinute = int.Parse(item.Minute);
                    gk.OutFirstHalf = item.FirstHalf;
                    gk.Out = true;
                }
            }

            return goalkeepers;
        }


        private IEnumerable<GoalByPlayerByTurnamentModel> GetScoredGoals()
        {
            IEnumerable<GoalByPlayerByTurnamentModel> goals = GetGoals();
            return goals.Where(g => g.GoalType == Constants.GOAL || g.GoalType == Constants.PENALTY_GOAL);
        }

        private IEnumerable<GoalByPlayerByTurnamentModel> GetGoalsByType(string type)
        {
            IEnumerable<GoalByPlayerByTurnamentModel> goals = GetGoals();
            return goals.Where(g => g.GoalType == type);
        }

        private IEnumerable<GoalAssistanceByPlayerModel> GetGoalAssistancesByPlayerByTournament()
        {
            var goalAssistances = GetGoalAssistances();
            List<GoalAssistanceByPlayerModel> playersGoalAssitancesByTournaments = new List<GoalAssistanceByPlayerModel>();

            foreach (var ga in goalAssistances)
            {
                if (!playersGoalAssitancesByTournaments.Exists(g => g.PlayerName == ga.PlayerName))
                {
                    var goalAssistance = new GoalAssistanceByPlayerModel();

                    goalAssistance.PlayerName = ga.PlayerName;
                    goalAssistance.GoalAssistancesByTournament.Add(ga.TournamentName, 0);
                    playersGoalAssitancesByTournaments.Add(goalAssistance);
                }

                else
                {
                    var goalAssistance = playersGoalAssitancesByTournaments.Find(g => g.PlayerName == ga.PlayerName);

                    if (goalAssistance.GoalAssistancesByTournament.ContainsKey(ga.TournamentName))
                    {
                        goalAssistance.GoalAssistancesByTournament[ga.TournamentName] += 1;
                    }
                    else
                    {
                        goalAssistance.GoalAssistancesByTournament[ga.TournamentName] = 0;
                    }
                }
            }

            return playersGoalAssitancesByTournaments;
        }
        private IEnumerable<CardsByPlayerModel> GetCardsByPlayerByTournament(IEnumerable<CardByPlayerByTournamentModel> cards)
        {
            List<CardsByPlayerModel> playersCardsByTournaments = new List<CardsByPlayerModel>();

            foreach (var c in cards)
            {
                if (!playersCardsByTournaments.Exists(pc => pc.PlayerName == c.PlayerName))
                {
                    var card = new CardsByPlayerModel();

                    card.PlayerName = card.PlayerName;
                    card.CardsByTournament.Add(c.TournamentName, 0);
                    playersCardsByTournaments.Add(card);
                }

                else
                {
                    var card = playersCardsByTournaments.Find(pc => pc.PlayerName == c.PlayerName);

                    if (card.CardsByTournament.ContainsKey(c.TournamentName))
                    {
                        card.CardsByTournament[c.TournamentName] += 1;
                    }
                    else
                    {
                        card.CardsByTournament[c.TournamentName] = 0;
                    }
                }
            }

            return playersCardsByTournaments;
        }

        private IEnumerable<GoalsByPlayerModel> GetGoalsByPlayerByTournament(IEnumerable<GoalByPlayerByTurnamentModel> goals)
        {
            List<GoalsByPlayerModel> playersGoalsByTournaments = new List<GoalsByPlayerModel>();

            foreach (var gl in goals)
            {
                if (!playersGoalsByTournaments.Exists(pg => pg.PlayerName == gl.PlayerName))
                {
                    var goal = new GoalsByPlayerModel();

                    goal.PlayerName = goal.PlayerName;
                    goal.GoalsByTournament.Add(gl.TournamentName, 0);
                    playersGoalsByTournaments.Add(goal);
                }

                else
                {
                    var goal = playersGoalsByTournaments.Find(g => g.PlayerName == gl.PlayerName);

                    if (goal.GoalsByTournament.ContainsKey(gl.TournamentName))
                    {
                        goal.GoalsByTournament[gl.TournamentName] += 1;
                    }
                    else
                    {
                        goal.GoalsByTournament[gl.TournamentName] = 0;
                    }
                }
            }

            return playersGoalsByTournaments;
        }
    }
}
