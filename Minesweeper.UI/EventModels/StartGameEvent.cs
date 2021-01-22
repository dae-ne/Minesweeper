namespace Minesweeper.UI.EventModels
{
    class StartGameEvent
    {
        public int BoardHeight { get; set; }
        public int BoardWidth { get; set; }
        public int NumberOfMines { get; set; }
    }
}
