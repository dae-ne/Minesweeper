using System;

namespace Minesweeper.BusinessLogic
{
    public class GameBoard : IGameBoard
    {
        public IModel[,] Board { get; private set; }

        public void GenerateBoard(in int columns, in int rows, in int mines)
        {
            Board = new Model[rows, columns];
            int counter = 1;

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    Board[row, column] = new Model(counter++);
                }
            }

            for (var i = 0; i < mines; i++)
            {
                try
                {
                    while (!AddMineToRandomPlace(columns, rows)) { }
                }
                catch
                {
                    throw;
                }
            }

            EvaluateFields(columns, rows);
        }

        /// <returns>null when the board does not contain the model</returns>
        public (int X, int Y)? GetPosition(IGameBoard board, IModel model)
        {
            var boardHeight = board.Board.GetLength(0);
            var boardWidth = board.Board.GetLength(1);
            var position = model.Id - 1;

            if (position >= 0 && position < boardHeight * boardWidth)
            {
                try
                {
                    var posY = position / boardWidth;
                    var posX = position % boardWidth;
                    return (posX, posY);
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        public void UncoverField(IModel model)
        {
            model.Status = FieldStatus.Uncovered;
        }

        public void SetNextStatus(IModel model)
        {
            switch (model.Status)
            {
                case FieldStatus.Covered:
                    model.Status = FieldStatus.Flag;
                    break;

                case FieldStatus.Flag:
                    model.Status = FieldStatus.QuestionMark;
                    break;

                case FieldStatus.QuestionMark:
                    model.Status = FieldStatus.Covered;
                    break;
            }    
        }

        private bool AddMineToRandomPlace(in int columns, in int rows)
        {
            var rand = new Random();
            var row = rand.Next(0, rows);
            var column = rand.Next(0, columns);

            try
            {
                if (Board[row, column].Value == FieldValues.Mine)
                {
                    return false;
                }

                Board[row, column].Value = FieldValues.Mine;
            }
            catch
            {
                throw;
            }

            return true;
        }

        private void EvaluateFields(in int columns, in int rows)
        {
            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    if (Board[row, column].Value == FieldValues.Mine)
                    {
                        continue;
                    }

                    var counter = 0;

                    for (var y = row - 1; y <= row + 1; y++)
                    {
                        if (y < 0 || y >= rows)
                        {
                            continue;
                        }

                        for (var x = column - 1; x <= column + 1; x++)
                        {
                            if (x < 0 || x >= columns)
                            {
                                continue;
                            }

                            if (Board[y, x].Value == FieldValues.Mine)
                            {
                                counter++;
                            }
                        }
                    }

                    Board[row, column].Value = (FieldValues)counter;
                }
            }
        }
    }
}
