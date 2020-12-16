namespace Minesweeper.BusinessLogic
{
    public interface IGameBoard
    {
        Model[,] Board { get; }

        void GenerateBoard(in int columns, in int rows, in int mines);
        FieldStatus GetStatus(in int columns, in int rows);
        void SetNextStatus(in int columns, in int rows);
        void UncoverField(in int columns, in int rows);
    }
}