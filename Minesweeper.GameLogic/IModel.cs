namespace Minesweeper.GameLogic
{
    public interface IModel
    {
        int Id { get; }
        FieldStatus Status { get; set; }
        FieldValues Value { get; set; }
    }
}