namespace Minesweeper.BusinessLogic
{
    public interface IGameBoard
    {
        IModel[,] Board { get; }

        void GenerateBoard(in int columns, in int rows, in int mines);
        (int X, int Y)? GetPosition(IGameBoard board, IModel model);
        void SetNextStatus(IModel model);
        void UncoverField(IModel model);
    }
}