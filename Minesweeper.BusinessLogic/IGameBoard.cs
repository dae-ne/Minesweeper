namespace Minesweeper.BusinessLogic
{
    public interface IGameBoard
    {
        IModel[,] Board { get; }

        int Score { get; }

        void GenerateBoard(in int columns, in int rows, in int mines);
        (int X, int Y)? GetPosition(IGameBoard board, IModel model);
        void SetNextStatus(IModel model);
        UncoverStatus UncoverField(IModel model);
    }
}