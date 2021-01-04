namespace Minesweeper.BusinessLogic
{
    public interface IModel
    {
        FieldStatus Status { get; set; }
        FieldValues Value { get; set; }
    }
}