namespace Minesweeper.BusinessLogic
{
    // TODO: Change model references to interface
    public class Model : IModel
    {
        public Model(int id) => Id = id;

        public int Id { get; }
        public FieldValues Value { get; set; }
        public FieldStatus Status { get; set; }

    }
}
