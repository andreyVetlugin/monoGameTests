using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Models.Figure;
using Microsoft.Xna.Framework;

namespace Game1.Models.Map
{
    internal class CollisionChecker
    {
        public bool CheckCollision(TetrisMap map, Figure.Figure figure)
        {
            var filledCellsFromMap = map.GetFilledCellsCoordinates();
            var figurePoints = figure.GetProjectFigureToMap();

            if (figurePoints.Intersect(filledCellsFromMap).Count() != 0)
            {
                return true;
            }
            return false;
        }

    }
}
