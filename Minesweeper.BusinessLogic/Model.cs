namespace Minesweeper.BusinessLogic
{
    public class Model
    {
        public int Value { get; internal set; }
        public bool IsUncovered { get; internal set; }
        public bool IsMarkedWithFlag { get; internal set; }
        public bool IsMarkedWithQuestionMark { get; internal set; }

    }
}
