using System.Collections.Generic;

namespace Minesweeper.BusinessLogic
{
    public class BoardScanner : IBoardScanner
    {
        public IEnumerable<Model> FindAdjacentEmpty(IGameBoard board, Model model)
        {
            var position = FindPosition(board, model);

            if (position == null)
            {
                return new List<Model>();
            }

            return Find(board, position.Value.X, position.Value.Y);
        }

        /// <returns>null when the board does not contain the model</returns>
        private (int X, int Y)? FindPosition(IGameBoard board, Model model)
        {
            var boardHeight = board.Board.GetLength(0);
            var boardWidth = board.Board.GetLength(1);

            for (var y = 0; y < boardHeight; y++)
            {
                for (var x = 0; x < boardWidth; x++)
                {
                    if (board.Board[y, x].Equals(model))
                    {
                        return (x, y);
                    }
                }
            }

            return null;
        }

        private IEnumerable<Model> Find(IGameBoard board, int x, int y, HashSet<Model> emptyFields = null)
        {
            if (emptyFields == null)
            {
                emptyFields = new HashSet<Model>();
            }

            if (x >= 0 && y >= 0 && x < board.Board.GetLength(1) && y < board.Board.GetLength(0))
            {
                if (board.Board[y, x].Value == FieldValues.Empty)
                {
                    emptyFields.Add(board.Board[y, x]);

                    for (var v = y - 1; v <= y + 1; v++)
                    {
                        for (var h = x - 1; h <= x + 1; h++)
                        {
                            if (h >= 0
                                && v >= 0
                                && h < board.Board.GetLength(1)
                                && v < board.Board.GetLength(0)
                                && !emptyFields.Contains(board.Board[v, h]))
                            {
                                Find(board, h, v, emptyFields);
                            }
                        }
                    }
                }
            }

            return emptyFields;
        }
    }
}
