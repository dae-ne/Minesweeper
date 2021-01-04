namespace Minesweeper.BusinessLogic
{
    public interface IGameBoard
    {
        Model[,] Board { get; }

        void GenerateBoard(in int columns, in int rows, in int mines);
        FieldStatus GetStatus(IModel model);
        void SetNextStatus(IModel model);
        void UncoverField(IModel model);
    }
}