using System;

namespace Minesweeper.BusinessLogic
{
    public class GameBoard : IGameBoard
    {
        public Model[,] Board { get; private set; }

        public void GenerateBoard(int rows, int columns, int mines)
        {
            Board = new Model[rows, columns];

            for (var row = 0; row < Board.GetLength(0); row++)
            {
                for (var column = 0; column < Board.GetLength(1); column++)
                {
                    Board[row, column] = new Model();
                }
            }

            for (var i = 0; i < mines; i++)
            {
                while (!AddMineToRandomPlace(rows, columns)) { }
            }

            EvaluateFields(rows, columns);
        }

        public void UncoverField(int row, int column)
        {
            Board[row, column].IsUncovered = true;
        }

        public void SetNextFlag(int row, int column)
        {
            if (Board[row, column].IsMarkedWithFlag)
            {
                Board[row, column].IsMarkedWithFlag = false;
                Board[row, column].IsMarkedWithQuestionMark = true;
            }

            else if (Board[row, column].IsMarkedWithQuestionMark)
            {
                Board[row, column].IsMarkedWithFlag = false;
                Board[row, column].IsMarkedWithQuestionMark = false;
            }

            else
            {
                Board[row, column].IsMarkedWithFlag = true;
                Board[row, column].IsMarkedWithQuestionMark = false;
            }
        }

        public bool IsFieldUncovered(int row, int column) => Board[row, column].IsUncovered;

        public bool IsFieldMarkedWithFlag(int row, int column) => Board[row, column].IsMarkedWithFlag;

        public bool IsFieldMarkedWithQuestionMark(int row, int column) => Board[row, column].IsMarkedWithQuestionMark;

        private bool AddMineToRandomPlace(int rows, int columns)
        {
            var rand = new Random();
            var row = rand.Next(0, rows);
            var column = rand.Next(0, columns);

            if (Board != null && Board[row, column]?.Value == -1)
            {
                return false;
            }

            if (Board != null)
            {
                Board[row, column].Value = -1;
            }

            return true;
        }

        private void EvaluateFields(int rows, int columns)
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

                    for (var i = row - 1; i < row + 1; i++)
                    {
                        if (i < 0)
                        {
                            i = 0;
                        }

                        for (var j = column - 1; j < column + 1; j++)
                        {
                            if (j < 0)
                            {
                                j = 0;
                            }

                            if (Board[i, j].Value == -1)
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
