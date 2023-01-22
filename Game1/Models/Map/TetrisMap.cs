using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1.Models.Map
{
    internal class TetrisMap : ICloneable
    {
        private List<List<FieldValue>> _map;

        private Point _mapSize = new Point(10, 22);

        public Point GetSize()
        {
            return new Point(_mapSize.X, _mapSize.Y);
        }

        public List<FieldValue> this[int index]
        {
            get => _map[index];
            set => _map[index] = value;
        }

        public List<Point> GetFilledCellsCoordinates()
        {
            var filledCellsCoords = new List<Point>();
            for (int x = 0; x < _map.Count; x++)
            {
                for (int y = 0; y < _map[x].Count; y++)
                {
                    if (_map[x][y] == FieldValue.Block)
                    {
                        filledCellsCoords.Add(new(x, y));
                    }
                }
            }

            return filledCellsCoords;
        }

        public TetrisMap()
        {
            _map = Enumerable.Range(0, _mapSize.X)
               .Select(i => new List<FieldValue>(Enumerable.Range(0, _mapSize.Y).Select(x => FieldValue.None)))
               .ToList();
        }

        private TetrisMap(TetrisMap map)
        {
            _map = GetMapCopy();
            _mapSize = map.GetSize();
        }

        private List<List<FieldValue>> GetMapCopy()
        {
            //TODO: проверить на копировсание данных и не связность. ... вообще можно ли как-то автоматизировать такую проверку для всех типов\выходных данных?
            return _map.Select(arr => new List<FieldValue>(arr.Select(x => x).ToList())).ToList();
        }

        public object Clone()
        {
            return new TetrisMap(this);
        }
    }
}
