using System.Collections.Generic;

namespace Minesweeper.BusinessLogic
{
    public interface IBoardScanner
    {
        IEnumerable<IModel> FindAdjacentEmpty(IGameBoard board, IModel model);
    }
}