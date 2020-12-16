namespace Minesweeper.BusinessLogic
{
    public interface IGameBoard
    {
        Model[,] Board { get; }

        void GenerateBoard(in int columns, in int rows, in int mines);
        FieldStatus GetStatus(Model model);
        void SetNextStatus(Model model);
        void UncoverField(Model model);
    }
}