using MvvmCross.ViewModels;
using Minesweeper.Presentation.Core.ViewModels;

namespace Minesweeper.Presentation.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MenuViewModel>();
        }
    }
}
