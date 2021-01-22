using Xunit;

namespace Minesweeper.GameLogic.Tests
{
    public class GameBoardTests
    {
        private readonly GameBoard _gameBoard;

        public GameBoardTests()
        {
            _gameBoard = new GameBoard();
        }

        [Theory]
        [InlineData(2, 2, 2, 2)]
        [InlineData(20, 17, 30, 30)]
        [InlineData(40, 10, 15, 15)]
        public void GenerateBoard_ShouldHaveExpectedNumberOfMines(int columns, int rows, int mines, int expected)
        {
            _gameBoard.GenerateBoard(columns, rows, mines);
            mines = 0;

            foreach (var field in _gameBoard.Board)
            {
                if (field.Value == FieldValues.Mine)
                {
                    mines++;
                }
            }

            var actual = mines;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(FieldStatus.Covered, FieldValues.Mine, UncoverStatus.Mine)]
        [InlineData(FieldStatus.Flag, FieldValues.Mine, UncoverStatus.NoUncover)]
        [InlineData(FieldStatus.Uncovered, FieldValues.Mine, UncoverStatus.NoUncover)]
        [InlineData(FieldStatus.Covered, FieldValues.Five, UncoverStatus.Normal)]
        [InlineData(FieldStatus.Covered, FieldValues.Empty, UncoverStatus.EmptyField)]
        public void UncoverField_ShouldReturnProperStatus(FieldStatus status, FieldValues value, UncoverStatus expected)
        {
            _gameBoard.GenerateBoard(2, 2, 1);
            var field = new Model(0);
            field.Status = status;
            field.Value = value;
            var actual = _gameBoard.UncoverField(field);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(FieldStatus.Covered, FieldStatus.Flag)]
        [InlineData(FieldStatus.Flag, FieldStatus.QuestionMark)]
        [InlineData(FieldStatus.QuestionMark, FieldStatus.Covered)]
        [InlineData(FieldStatus.Uncovered, FieldStatus.Uncovered)]
        public void SetNextStatus_ShouldChangeFieldState(FieldStatus status, FieldStatus expected)
        {
            var field = new Model(0);
            field.Status = status;
            _gameBoard.SetNextStatus(field);
            var actual = field.Status;

            Assert.Equal(expected, actual);
        }
    }
}
