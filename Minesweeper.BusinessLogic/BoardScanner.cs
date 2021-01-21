using System.Collections.Generic;
using System.Linq;

namespace Minesweeper.BusinessLogic
{
    public class BoardScanner : IBoardScanner
    {
        public IEnumerable<IModel> FindAdjacentEmpty(IGameBoard board, IModel model)
        {
            var position = board.GetPosition(board, model);

            if (position == null)
            {
                return new List<IModel>();
            }

            var emptyAdjecentFields = Find(board, position.Value.X, position.Value.Y);

            // Adding all fields around empty adjecent
            var adjecentWithValue = new HashSet<IModel>();

            foreach (var field in emptyAdjecentFields)
            {
                var fieldPosition = board.GetPosition(board, field);

                for (var v = fieldPosition.Value.Y - 1; v <= fieldPosition.Value.Y + 1; v++)
                {
                    for (var h = fieldPosition.Value.X - 1; h <= fieldPosition.Value.X + 1; h++)
                    {
                        if (h >= 0
                            && v >= 0
                            && h < board.Board.GetLength(1)
                            && v < board.Board.GetLength(0))
                        {
                            adjecentWithValue.Add(board.Board[v, h]);
                        }
                    }
                }
            }

            emptyAdjecentFields.UnionWith(adjecentWithValue);
            return emptyAdjecentFields;
        }

        private HashSet<IModel> Find(IGameBoard board, int x, int y, HashSet<IModel> emptyFields = null)
        {
            if (emptyFields == null)
            {
                emptyFields = new HashSet<IModel>();
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
