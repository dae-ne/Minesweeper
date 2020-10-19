using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Presentation.ViewModels
{
    class GameViewModel
    {
        private double _windowWidth;

        public double WindowWidth
        {
            get { return _windowWidth; }
            set { _windowWidth = value; }
        }

        private double _windowHeight;

        public double WindowHeight
        {
            get { return _windowHeight; }
            set { _windowHeight = value; }
        }

        public void DoSomething()
        {
            WindowWidth = 100.0;
            WindowHeight = 100.0;
        }
    }
}
