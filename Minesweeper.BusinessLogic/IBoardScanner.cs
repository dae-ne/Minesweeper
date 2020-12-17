using System.Collections.Generic;

namespace Minesweeper.BusinessLogic
{
    public interface IBoardScanner
    {
        IEnumerable<Model> FindAdjacentEmpty(IGameBoard board, Model model);
    }
}