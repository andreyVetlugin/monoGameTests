using Game1.Models.Figure;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Models.Map;
using Game1.Views;

namespace Game1
{
    internal class FigureMover
    {
        // подписаться на событие нажатие клавиш движения\ротейта
        private CollisionChecker _collisionChecker;

        public FigureMover()
        {
            _collisionChecker = new CollisionChecker();
            //TODO: сделать в DI
        }
        public void RotateFigure(Figure figure)
        {
            //..
            throw new NotImplementedException();
            int a = 0;
            int b = 0;
            //figure.ChangeMaskByRotating();.ChangeMask(RotateDirection.counterClockWise);
        }
        public bool TryMoveFigure(Figure figure, TetrisMap map, MoveDirection moveDirection)
        {
            var figureForMove = (Figure)figure.Clone();
            figureForMove.Coordinate += moveDirection.GetMovePoint();
            var collisionResult = _collisionChecker.CheckCollision(map, figureForMove);
            if (collisionResult)
            {
                return false;
            }

            figure.Coordinate += moveDirection.GetMovePoint();
            return true;
        }
    }
}
