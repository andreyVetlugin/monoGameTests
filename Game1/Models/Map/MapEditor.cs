using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1.Models.Map
{
    //TODO: test: после инициализации проверить массив на заполненность только нулями

    internal class MapEditor
    {
        public TetrisMap Map { get; }

        public MapEditor()
        {
            Map = new TetrisMap();
        }

        public void AddBlocksToMap(Point[] pointsToAdd)
        {
            foreach (var point in pointsToAdd)
            {
                Map[point.X][point.Y] = FieldValue.Block;
            }
        }
    }
}
