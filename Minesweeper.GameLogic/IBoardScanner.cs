using System.Collections.Generic;

namespace Minesweeper.GameLogic
{
    public interface IBoardScanner
    {
        IEnumerable<IModel> FindAdjacentEmpty(IGameBoard board, IModel model);
    }
}