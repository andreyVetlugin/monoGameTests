using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1.Views
{
    internal enum MoveDirection
    {
        Down = 0,
        Left = 1,
        Right = 2
    }

    internal static class MoveDirectionExtention
    {
        public static Point GetMovePoint(this MoveDirection moveDirection)
        {
            
            //TODO: как можно поправить это место ? 
            Point movePoint = new Point(0, 0);
            switch (moveDirection)
            {
                case MoveDirection.Down:
                    movePoint = new Point(0, 1);
                    break;
                case MoveDirection.Left:
                    movePoint = new Point(-1, 0);
                    break;
                case MoveDirection.Right:
                    movePoint = new Point(1, 0);
                    break;
            }
            return movePoint;
        }
    }
}
