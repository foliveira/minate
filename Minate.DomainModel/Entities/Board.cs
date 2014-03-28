namespace Minate.DomainModel.Entities
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Board
    {
        #region Board Properties

        /// <summary>
        /// 
        /// </summary>
        public int TotalBombs { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int FlaggedBombs { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int Width { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int Height { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public IList<Cell> Cells { get; set; }

        #endregion

        #region Board Logic

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal Cell this[int x, int y]
        {
            get { return Cells[y + x * (Width + 1)]; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal bool CheckBounds(int x, int y)
        {
            return ((0 <= x) && (x <= Width) && (0 <= y) && (y <= Height));
        }

        #endregion

        #region Board Creation

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Board MakeBoard(int height = 14, int width = 14, int bombs = 51)
        {
            var board = new Board
                            {
                                Cells = new List<Cell>(),
                                FlaggedBombs = 0,
                                Height = height,
                                Width = width,
                                TotalBombs = bombs
                            };

            for (var i = 0; i < board.Width + 1; i++)
            {
                for (var j = 0; j < board.Height + 1; j++)
                {
                    board.Cells.Add(new Cell
                                        {
                                            X = i,
                                            Y = j,
                                            Bomb = false,
                                            Exposed = false,
                                            Neighbors = 0
                                        }
                                    );
                }
            }

            var bombsToPlace = board.TotalBombs;

            do
            {
                while (true)
                {
                    var random = new Random((int) (DateTime.Now.Ticks*DateTime.UtcNow.Millisecond));
                    var i = random.Next(0, board.Width + 1);
                    var j = random.Next(0, board.Height + 1);

                    var cell = board[i, j];
            
                    if (cell.Bomb)
                        continue;
                    
                    cell.Bomb = true;

                    for (var k = i - 1; k <= i + 1; k++)
                    {
                        for (var l = j - 1; l <= j + 1; l++)
                        {
                            if (board.CheckBounds(k, l))
                                board[k, l].Neighbors++;
                        }
                    }
                    break;
                }
            } while (--bombsToPlace > 0);

            return board;
        }

        #endregion

        #region Nested class
        
        public class Cell
        {
            #region Cell Properties

            /// <summary>
            /// 
            /// </summary>
            public bool Bomb { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool Exposed { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int Neighbors { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int X { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int Y { get; set; }

            #endregion

            #region Cell Identity Management

            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object obj)
            {
                var otherCell = obj as Cell;

                if (otherCell == null)
                    return false;

                return (otherCell.X == X) && (otherCell.Y == Y);

            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return X.GetHashCode() * Y.GetHashCode() * Neighbors;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                var rep = new StringBuilder(string.Format("{0:00}{1:00}", X, Y));

                rep.Append(string.Format("{0}", Bomb ? 1 : 0));
                rep.Append(string.Format("{0}", Exposed ? 1 : 0));
                rep.Append(string.Format("{0}", Neighbors));

                return rep.ToString();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cell"></param>
            public void Parse(string cell)
            {
                X = int.Parse(cell.Substring(0, 2));
                Y = int.Parse(cell.Substring(2, 2));
                Bomb = int.Parse(cell.Substring(4, 1)) == 1;
                Exposed = int.Parse(cell.Substring(5, 1)) == 1;
                Neighbors = int.Parse(cell.Substring(6, 1));
            }

            #endregion
        }

        #endregion
    }
}