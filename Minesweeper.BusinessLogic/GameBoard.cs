using System;

namespace Minesweeper.BusinessLogic
{
    public class GameBoard
    {
        public int[,] BoardValues;

        public void GenerateBoard(int rows, int columns, int mines)
        {
            BoardValues = new int[rows, columns];

            for (int i = 0; i < mines; i++)
            {
                while (!AddMineToRandomPlace(rows, columns)) ;
            }

            EvaluateFields(rows, columns);
        }

        private bool AddMineToRandomPlace(int rows, int columns)
        {
            var rand = new Random();
            var row = rand.Next(0, rows);
            var column = rand.Next(0, columns);
            
            if (BoardValues[column, row] == -1)
            {
                return false;
            }

            BoardValues[row, column] = -1;
            return true;
        }

        private void EvaluateFields(int rows, int columns)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (BoardValues[row, column] == -1)
                    {
                        continue;
                    }

                    var counter = 0;

                    for (int i = row - 1; i < row + 1; i++)
                    {
                        if (i < 0)
                        {
                            i = 0;
                        }

                        for (int j = column - 1; j < column + 1; j++)
                        {
                            if (j < 0)
                            {
                                j = 0;
                            }

                            if (BoardValues[i, j] == -1)
                            {
                                counter++;
                            }
                        }
                    }

                    BoardValues[row, column] = counter;
                }
            }
        }
    }
}
