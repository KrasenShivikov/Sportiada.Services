namespace Sportiada.Services.Football.Models.Tournament
{
    using Infrastructure.Constants;
    using Models.Goal;
    using Models.GameStatitistic;
    using Models.Player;
    using Sportiada.Services.Football.Models.Game;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TournamentSeasonStatisticModel
    {
        public int SeasonId { get; set; }

        public string SeasonName { get; set; }
        public IEnumerable<GameWithStatisticModel> Games { get; set; }

        public IEnumerable<PlayerIndicatorCountModel> GoalsStatistic => GetGoalsByPlayer();

        public IEnumerable<PlayerIndicatorCountModel> OwnGoalsStatistic => GetOwnGoalsByPlayer();

        public IEnumerable<PlayerPenaltiesCountModel> PenaltiesStatistic => GetPenaltiesByPlayer();

        public IEnumerable<PlayerIndicatorCountModel> GoalAsisstancesStatististic => GetPlayerGoalAssistancesByPlayer();

        public IEnumerable<PlayerIndicatorCountModel> YellowCardsStatistic => GetCardsByPlayer(Constants.YELLOW_CARD, Constants.RED_CARD);

        public IEnumerable<PlayerIndicatorCountModel> RedCardsStatistic => GetCardsByPlayer(Constants.RED_CARD, Constants.YELLOW_CARD);

        public IEnumerable<GoalkeeperStatisticModel> GoalkeepersStatistic => GetGoalkeepersUnsavedPenaltiesStatistic();

        private IEnumerable<PlayerIndicatorCountModel> GetGoalsByPlayer()
        {
            var players = new List<PlayerIndicatorCountModel>();

            foreach (var g in Games)
            {
                foreach (var s in g.TeamsStatistic)
                {
                    foreach (var gl in s.Goals.Where(gl => gl.Type.Name == Constants.GOAL || gl.Type.Name == Constants.PENALTY_GOAL))
                    {
                        if (players.Exists(sc => sc.PlayerId == gl.Player.Id))
                        {
                            var player = players.Find(p => p.PlayerId == gl.Player.Id);
                            player.IndicatorCount++;
                        }
                        else
                        {
                            var player = new PlayerIndicatorCountModel
                            {
                                PlayerId = gl.Player.Id,
                                PlayerName = gl.Player.Name,
                                TeamName = s.Squad.Team.Name,
                                IndicatorCount = 1
                            };

                            players.Add(player);
                        }
                    }

                }
            }

            return players.OrderByDescending(s => s.IndicatorCount);
        }

        private IEnumerable<PlayerIndicatorCountModel> GetOwnGoalsByPlayer()
        {
            var players = new List<PlayerIndicatorCountModel>();

            foreach (var g in Games)
            {
                foreach (var s in g.TeamsStatistic)
                {
                    foreach (var gl in s.Goals.Where(gl => gl.Type.Name == Constants.OWN_GOAL))
                    {
                        if (players.Exists(sc => sc.PlayerId == gl.Player.Id))
                        {
                            var player = players.Find(p => p.PlayerId == gl.Player.Id);
                            player.IndicatorCount++;
                        }
                        else
                        {
                            var player = new PlayerIndicatorCountModel
                            {
                                PlayerId = gl.Player.Id,
                                PlayerName = gl.Player.Name,
                                TeamName = s.Squad.Team.Name,
                                IndicatorCount = 1
                            };

                            players.Add(player);
                        }
                    }

                }
            }

            return players.OrderByDescending(s => s.IndicatorCount);
        }

        private IEnumerable<PlayerPenaltiesCountModel> GetPenaltiesByPlayer()
        {
            var players = new List<PlayerPenaltiesCountModel>();

            foreach (var g in Games)
            {
                foreach (var s in g.TeamsStatistic)
                {
                    foreach (var gl in s.Goals.Where(gl => gl.Type.Name == Constants.PENALTY_GOAL || gl.Type.Name == Constants.PENALTY_MISSED))
                    {
                        if (players.Exists(sc => sc.PlayerId == gl.Player.Id) && gl.Type.Name == Constants.PENALTY_GOAL)
                        {
                            var player = players.Find(p => p.PlayerId == gl.Player.Id);
                            player.ScoredPenalties++;
                        }

                        else if (players.Exists(sc => sc.PlayerId == gl.Player.Id) && gl.Type.Name == Constants.PENALTY_MISSED)
                        {
                            var player = players.Find(p => p.PlayerId == gl.Player.Id);
                            player.MissedPenalties++;
                        }
                        else if (!players.Exists(sc => sc.PlayerId == gl.Player.Id) && gl.Type.Name == Constants.PENALTY_GOAL)
                        {
                            var player = new PlayerPenaltiesCountModel
                            {
                                PlayerId = gl.Player.Id,
                                PlayerName = gl.Player.Name,
                                TeamName = s.Squad.Team.Name,
                                ScoredPenalties = 1,
                                MissedPenalties = 0
                            };
                            players.Add(player);
                        }
                        else if (!players.Exists(sc => sc.PlayerId == gl.Player.Id) && gl.Type.Name == Constants.PENALTY_MISSED)
                        {
                            var player = new PlayerPenaltiesCountModel
                            {
                                PlayerId = gl.Player.Id,
                                PlayerName = gl.Player.Name,
                                TeamName = s.Squad.Team.Name,
                                ScoredPenalties = 0,
                                MissedPenalties = 1
                            };
                            players.Add(player);
                        }
                    }

                }
            }

            return players.OrderByDescending(s => s.ScoredPenalties).ThenBy(s => s.MissedPenalties);
        }

        private IEnumerable<PlayerIndicatorCountModel> GetPlayerGoalAssistancesByPlayer()
        {
            var players = new List<PlayerIndicatorCountModel>();

            foreach (var g in Games)
            {
                foreach (var s in g.TeamsStatistic)
                {
                    foreach (var gl in s.Goals)
                    {
                        if (gl.Assistance != null && players.Exists(p => p.PlayerId == gl.Player.Id))
                        {
                            var player = players.Find(p => p.PlayerId == gl.Player.Id);
                            player.IndicatorCount++;
                        }
                        else if (gl.Assistance != null && !players.Exists(p => p.PlayerId == gl.Player.Id))
                        {
                            var player = new PlayerIndicatorCountModel
                            {
                                PlayerId = gl.Assistance.Player.Id,
                                PlayerName = gl.Assistance.Player.Name,
                                TeamName = s.Squad.Team.Name,
                                IndicatorCount = 1
                            };
                            players.Add(player);
                        }
                    }
                }
            }

            return players.OrderByDescending(p => p.IndicatorCount);
        }

        private IEnumerable<PlayerIndicatorCountModel> GetCardsByPlayer(string equalType, string notEqualType)
        {
            var players = new List<PlayerIndicatorCountModel>();

            foreach (var g in Games)
            {
                foreach (var s in g.TeamsStatistic)
                {
                    foreach (var c in s.Cards.Where(c => c.Type.Name == equalType || c.Type.Name != notEqualType))
                    {
                        if (players.Exists(p => p.PlayerId == c.Player.Id))
                        {
                            var player = players.Find(p => p.PlayerId == c.Player.Id);
                            player.IndicatorCount++;
                        }
                        else
                        {
                            var player = new PlayerIndicatorCountModel
                            {
                                PlayerId = c.Player.Id,
                                PlayerName = c.Player.Name,
                                TeamName = s.Squad.Team.Name,
                                IndicatorCount = 1
                            };
                            players.Add(player);
                        }
                    }
                }
            }

            return players.OrderByDescending(p => p.IndicatorCount);
        }

        private IEnumerable<GoalkeeperStatisticModel> GetGoalkeepersMatchCountStatistic()
        {
            var goalkeepers = new List<GoalkeeperStatisticModel>();

            foreach (var g in Games)
            {
                foreach (var s in g.TeamsStatistic)
                {
                    var gks = GetGoalkeepers(s);

                    foreach (var gk in gks)
                    {
                        if (goalkeepers.Exists(p => p.PlayerId == gk.PlayerId))
                        {
                            var player = goalkeepers.Find(p => p.PlayerId == gk.PlayerId);
                            player.MatchCount++;
                        }
                        else
                        {
                            var player = new GoalkeeperStatisticModel
                            {
                                PlayerId = gk.PlayerId,
                                PlayerName = gk.Name,
                                TeamName = s.Squad.Team.Name,
                                MatchCount = 1
                            };
                            goalkeepers.Add(player);
                        }
                    }
                }
            }

            return goalkeepers;
        }

        private IEnumerable<GoalkeeperStatisticModel> GetGoalkeepersCleanWebStatistis()
        {
            var goalkeepers = GetGoalkeepersMatchCountStatistic().ToList();

            foreach (var g in Games)
            {
                foreach (var s in g.TeamsStatistic)
                {
                    var gks = GetGoalkeepers(s);

                    foreach (var gk in gks)
                    {
                        var gameStatistic = g.TeamsStatistic.Find(st => st.Id != s.Id);
                        int count = 0;

                        foreach (var gl in gameStatistic.Goals)
                        {
                            if (gk.InMinute < gl.Minute && !gk.Out || (gk.IsInLineUp && !gk.Out))
                            {
                                count++;
                            }
                            else if (gk.InMinute < gl.Minute && gl.Minute < gk.OutMinute)
                            {
                                count++;
                            }
                        }

                        bool isClean = count == 0 ? true : false;
                        var player = goalkeepers.Find(p => p.PlayerId == gk.PlayerId);

                        if (isClean)
                        {
                            player.CleanWebCount++;
                        }
                    }
                }
            }

            return goalkeepers;
        }

        private IEnumerable<GoalkeeperStatisticModel> GetGoalkeepersSavedPenaltiesStatistic()
        {
            var goalkeepers = GetGoalkeepersCleanWebStatistis().ToList();

            foreach (var g in Games)
            {
                foreach (var s in g.TeamsStatistic)
                {
                    var gameStatistic = g.TeamsStatistic.Find(st => st.Id != s.Id);
                    var gks = GetGoalkeepers(s);

                    foreach (var gl in gameStatistic.Goals.Where(gl => gl.Type.Name == Constants.PENALTY_MISSED))
                    {
                        foreach (var gk in gks)
                        {

                            if (gk.IsInLineUp && !gk.Out)
                            {
                                var player = goalkeepers.Find(p => p.PlayerId == gk.PlayerId);
                                player.SavedPenalties++;
                            }

                            else if (gk.InMinute < gl.Minute && !gk.Out)
                            {
                                var player = goalkeepers.Find(p => p.PlayerId == gk.PlayerId);
                                player.SavedPenalties++;
                            }

                            else if (gk.InMinute < gl.Minute && gl.Minute < gk.OutMinute)
                            {
                                var player = goalkeepers.Find(p => p.PlayerId == gk.PlayerId);
                                player.SavedPenalties++;
                            }

                        }
                    }
                }
            }

            return goalkeepers;
        }

        private IEnumerable<GoalkeeperStatisticModel> GetGoalkeepersUnsavedPenaltiesStatistic()
        {
            var goalkeepers = GetGoalkeepersSavedPenaltiesStatistic().ToList();

            foreach (var g in Games)
            {
                foreach (var s in g.TeamsStatistic)
                {
                    var gameStatistic = g.TeamsStatistic.Find(st => st.Id != s.Id);
                    var gks = GetGoalkeepers(s);

                    foreach (var gl in gameStatistic.Goals.Where(gl => gl.Type.Name == Constants.PENALTY_GOAL))
                    {
                        foreach (var gk in gks)
                        {

                            if (gk.IsInLineUp && !gk.Out)
                            {
                                var player = goalkeepers.Find(p => p.PlayerId == gk.PlayerId);
                                player.UnsavedPenalties++;
                            }

                            else if (gk.InMinute < gl.Minute && !gk.Out)
                            {
                                var player = goalkeepers.Find(p => p.PlayerId == gk.PlayerId);
                                player.UnsavedPenalties++;
                            }
                            else if (gk.InMinute < gl.Minute && gl.Minute < gk.OutMinute)
                            {
                                var player = goalkeepers.Find(p => p.PlayerId == gk.PlayerId);
                                player.UnsavedPenalties++;
                            }
                        }
                    }
                }
            }

            return goalkeepers;
        }

        private List<GoalkeeperGameModel> GetGoalkeepers(GameTeamStatitisticFullModel stat)
        {
            var goalkeepers = new List<GoalkeeperGameModel>();

            foreach (var l in stat.LineUps.Where(l => l.Player.Position == Constants.GOALKEEPER))
            {
                var goalkeeper = new GoalkeeperGameModel
                {
                    PlayerId = l.Player.Id,
                    Name = l.Player.Name,
                    IsInLineUp = true
                };

                if (stat.Substitutes.Exists(su => su.PlayerOut.Player.Position == Constants.GOALKEEPER))
                {
                    var substitutes = stat.Substitutes.Where(su => su.PlayerOut.Player.Position == Constants.GOALKEEPER);

                    foreach (var substitute in substitutes)
                    {
                        if (substitute.PlayerOut.Player.Id == goalkeeper.PlayerId)
                        {
                            goalkeeper.Out = true;
                            goalkeeper.OutMinute = int.Parse(substitute.Minute);
                        }
                    }
                }

                goalkeepers.Add(goalkeeper);
            }

            foreach (var substitute in stat.Substitutes.Where(su => su.PlayerIn.Player.Position == Constants.GOALKEEPER))
            {
                var goalkeeper = new GoalkeeperGameModel
                {
                    PlayerId = substitute.PlayerIn.Player.Id,
                    Name = substitute.PlayerIn.Player.Name,
                    IsInLineUp = false,
                    InMinute = int.Parse(substitute.Minute)
                };

                if (stat.Substitutes.Exists(su => su.PlayerOut.Player.Position == Constants.GOALKEEPER))
                {
                    var substitutes = stat.Substitutes.Where(su => su.PlayerOut.Player.Position == Constants.GOALKEEPER);

                    foreach (var su in substitutes)
                    {
                        if (su.PlayerOut.Player.Id == goalkeeper.PlayerId)
                        {
                            goalkeeper.Out = true;
                            goalkeeper.OutMinute = int.Parse(substitute.Minute);
                        }
                    }
                }

                goalkeepers.Add(goalkeeper);
            }

            return goalkeepers;
        }

    }
}
