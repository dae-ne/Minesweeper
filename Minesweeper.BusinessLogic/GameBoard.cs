using System;

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

        public FieldStatus GetStatus(Model model)
        {
            //TODO
            return model.Status;
        }

        public void UncoverField(Model model)
        {
            //TODO
            model.Status = FieldStatus.Uncovered;
        }

        public void SetNextStatus(Model model)
        {
            var status = GetStatus(model);

            switch (status)
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
                        if (y < 0 || y >= columns)
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
