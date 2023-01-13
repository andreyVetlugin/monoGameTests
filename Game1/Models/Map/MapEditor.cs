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
        private TetrisMap _map;
        public TetrisMap Map
        {
            get => _map; //TODO: проверить, можно ли будет менять данные у карты через get извне
        }

        public MapEditor()
        {
            _map = new TetrisMap();
        }

        public void AddBlocksToMap(Point[] pointsToAdd)
        {
            foreach (var point in pointsToAdd)
            {
                _map[point.X][point.Y] = FieldValue.Block;
            }
        }
    }
}
