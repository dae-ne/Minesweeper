using System.Linq;
using Xunit;

namespace Minesweeper.GameLogic.Tests
{
    public class BoardScannerTests
    {
        private readonly BoardScanner _boardScanner;
        private readonly GameBoard _gameBoard;

        public BoardScannerTests()
        {
            _boardScanner = new BoardScanner();
            _gameBoard = new GameBoard();
        }

        [Theory]
        [InlineData(10, 10, 0, 0, 0)]
        [InlineData(4, 4, 2, 2, 9)]
        public void FindAdjacentEmpty_ShouldBeAProperCollectionLength(int columns, int rows, int emptyFiledsWidth, int emptyFieldsHeight, int expected)
        {
            _gameBoard.GenerateBoard(columns, rows, 0);

            foreach (var field in _gameBoard.Board)
            {
                field.Value = FieldValues.Mine;
            }

            for (var y = 0; y < emptyFieldsHeight; y++)
            {
                for (var x = 0; x < emptyFiledsWidth; x++)
                {
                    _gameBoard.Board[y, x].Value = FieldValues.Empty;
                }
            }

            var actual = _boardScanner.FindAdjacentEmpty(_gameBoard, _gameBoard.Board[0, 0]).Count();

            Assert.Equal(expected, actual);
        }
    }
}
