using Game1.Models.Figure;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    internal class FigureMover
    {
        // подписаться на событие нажатие клавиш движения\ротейта


        private CollissionChecker;
        public FigureMover() { }
        public void RotateFigure(Figure figure)
        { 
            //..
            int a = 0;
            int b = 0;
            figure.ChangeMask(RotateDirection.counterClockWise);
        }
        public void MoveFigure(Figure figure) 
        {
            var direction = new Point(0, -1);
            figure.ChangeCoordinate(direction);
            //collitionCheck;

        }
    }
}
