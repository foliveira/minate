namespace Minate.DomainModel.Entities
{
    using System.Collections.Generic;
    using System.Data.Linq.Mapping;
    using System.Web.Configuration;
    using System.Linq;
    using System;

    public class Game
    {
        private static readonly int MaxPlayers = int.Parse(WebConfigurationManager.AppSettings["MaxPlayers"]);

        private Player _currentPlayer;

        public Game()
        {
            Board = Board.MakeBoard();

            Players = new List<Player>();
            Plays = new List<Play>();
            Messages = new List<Message>();

            CurrentPlayer = null;
            Identifier = -1;
        }

        #region Game Properties

        public int Identifier { get; set; }

        public Board Board { get; set; }

        public IList<Player> Players { get; set; }

        public IList<Play> Plays { get; set; }

        public IList<Message> Messages { get; set; }

        public Player CurrentPlayer
        {
            get { return _currentPlayer ?? Players.First(); }
            set { _currentPlayer = value; }
        }

        #endregion

        #region Game State

        public bool Full
        {
            get
            {
                return Players.Count == MaxPlayers;
            }
        }

        public bool Finished
        {
            get
            {
                return FrontRunner.Mines > (Board.TotalBombs/2) || (Players.Where(p => p.Playing).Count() == 1 && Full);
            }
        }

        public Player Winner
        {
            get
            {
                return FrontRunner;
            }
        }
        
        private Player FrontRunner
        {
            get
            {
                var maxMines = Players.Where(p=>p.Playing).Max(p => p.Mines);
                return Players.Where(p => p.Mines == maxMines).First();
            }
        }

        #endregion

        #region Game Actions

        public IEnumerable<Board.Cell> MakePlay(int x, int y)
        {
            IEnumerable<Board.Cell> cellsToOpen = null;

            if (Full)
            {
                cellsToOpen = OpenCells(x, y);

                if (cellsToOpen.Any())
                {
                    AddPlay(cellsToOpen);

                    if (!cellsToOpen.ElementAt(0).Bomb)
                        NextTurn();
                    else
                        CurrentPlayer.IncrementMines();
                }
            }

            return cellsToOpen;
        }

        private void AddPlay(IEnumerable<Board.Cell> play)
        {
            var p = new Play
                          {
                              Identifier = (Plays.Count == 0) ? 0 : Plays.Last().Identifier + 1,
                              Cells = play,
                              Owner = CurrentPlayer
                           };
            Plays.Add(p);
        }

        private IEnumerable<Board.Cell> OpenCells(int x, int y)
        {
            var cellsToOpen = new List<Board.Cell>();
            var cell = Board[x, y];
            
            if (!cell.Exposed)
            {
                cellsToOpen.Add(cell);
                cell.Exposed = true;

                if (!cell.Bomb && cell.Neighbors == 0)
                {
                    for (var i = x - 1; i <= x + 1; ++i)
                    {
                        for (var j = y - 1; j <= y + 1; ++j)
                        {
                            if (!Board.CheckBounds(i, j))
                                continue;

                            cellsToOpen.AddRange(OpenCells(i, j));
                        }
                    }
                }
            }

            return cellsToOpen.Distinct();
        }

        public void Forfeit(string playerName)
        {
            Players.Where(p => string.Equals(playerName, p.Name)).First().Playing = false;
        }

        public Message AddMessage(Player player, string message)
        {
            var msg = new Message
                             {
                                 Identifier = Messages.Count,
                                 Date = DateTime.Now, 
                                 Player = player, 
                                 Text = message
                             };
            Messages.Add(msg);
            return msg;
        }

        #endregion

        #region Players Manipulation

        public Player Join(User user)
        {
            Player player;

            Players.Add(player = new Player
                                     {
                                         Identifier = Players.Count,
                                         Mines = 0,
                                         Name = user.Username,
                                         UserId = user.Identifier,
                                         Playing = true
                                     });

            return player;
        }

        public bool HasPlayer(string name)
        {
            return Players.Where(p => p.Name.Equals(name)).Any();
        }

        private void NextTurn()
        {
            var currentPlayer = CurrentPlayer;

            CurrentPlayer = Players.SkipWhile(p => !p.Equals(currentPlayer)).Skip(1).FirstOrDefault() ?? Players.ElementAt(0);
        }

        #endregion

        #region Nested Class

        public class Play
        {
            public int Identifier { get; internal set; }

            public IEnumerable<Board.Cell> Cells { get; internal set; }

            public Player Owner { get; internal set; }
        }

        public class Player
        {
            public int UserId { get; internal set; }

            public int Identifier { get; internal set; }

            public string Name { get; internal set; }

            public int Mines { get; internal set; }

            public bool Playing { get; internal set; }

            public void IncrementMines()
            {
                Mines++;
            }

            public override bool Equals(object obj)
            {
                var player = obj as Player;

                return player != null && Name.Equals(player.Name);
            }
        }
        
        public class Message
        {
            public int Identifier { get; set; }
            public DateTime Date { get; set; }
            public Player Player { get; set; }
            public string Text { get; set; }

            public override string ToString()
            {
                return string.Format("[{0}] {1}: {2}", Date.ToShortTimeString(), Player.Name, Text);
            }
        }

        #endregion
    }
}