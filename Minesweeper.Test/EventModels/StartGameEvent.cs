namespace Minesweeper.Test.EventModels
{
    class StartGameEvent
    {
        public int BoardHeith { get; set; }
        public int BoardWidth { get; set; }
        public int NumberOfMines { get; set; }
    }
}
