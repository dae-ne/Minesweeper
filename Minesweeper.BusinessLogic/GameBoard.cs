using System;
using System.Linq;

namespace Minesweeper.BusinessLogic
{
    public class GameBoard : IGameBoard
    {
        public Model[,] Board { get; private set; }

        public void GenerateBoard(in int columns, in int rows, in int mines)
        {
            Board = new Model[rows, columns];

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    Board[row, column] = new Model();
                }
            }

            for (var i = 0; i < mines; i++)
            {
                try
                {
                    while (!AddMineToRandomPlace(rows, columns)) { }
                }
                catch
                {
                    throw;
                }
            }

            EvaluateFields(columns, rows);
        }

        public FieldStatus GetStatus(in int columns, in int rows)
        {
            return Board[rows, columns].Status;
        }

        public void UncoverField(in int columns, in int rows)
        {
            Board[rows, columns].Status = FieldStatus.Uncovered;
        }

        public void SetNextStatus(in int columns, in int rows)
        {
            var status = GetStatus(columns, rows);

            switch (status)
            {
                case FieldStatus.Covered:
                    Board[rows, columns].Status = FieldStatus.Flag;
                    break;

                case FieldStatus.Flag:
                    Board[rows, columns].Status = FieldStatus.QuestionMark;
                    break;

                case FieldStatus.QuestionMark:
                    Board[rows, columns].Status = FieldStatus.Covered;
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
                if (Board[row, column].Value == -1)
                {
                    return false;
                }

                Board[row, column].Value = -1;
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
                    if (Board[row, column].Value == -1)
                    {
                        continue;
                    }

                    var counter = 0;

                    for (var y = row - 1; y < row + 1; y++)
                    {
                        if (y < 0 || y >= columns)
                        {
                            continue;
                        }

                        for (var x = column - 1; x < column + 1; x++)
                        {
                            if (x < 0 || x >= columns)
                            {
                                continue;
                            }

                            if (Board[y, x].Value == -1)
                            {
                                counter++;
                            }
                        }
                    }

                    Board[row, column].Value = counter;
                }
            }
        }
    }
}
