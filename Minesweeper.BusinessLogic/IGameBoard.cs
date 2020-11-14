namespace Minesweeper.BusinessLogic
{
    public interface IGameBoard
    {
        Model[,] Board { get; }

        void GenerateBoard(int rows, int columns, int mines);
        bool IsFieldMarkedWithFlag(int column, int row);
        bool IsFieldMarkedWithQuestionMark(int column, int row);
        bool IsFieldUncovered(int column, int row);
        void SetNextFlag(int column, int row);
        void UncoverField(int column, int row);
    }
}