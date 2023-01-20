using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1.Views
{
    internal class InGameDisplayInfo
    {
        public int CellSize { get; set; }
        public Point BlockSize
        {
            get
            {
                return new Point(CellSize - 2 * CellThickness, CellSize - 2 * CellThickness); //TODO: сделать обрезание в зависимости от от толщины клетки. 1 пиксель?
                                                                                                  //TODO: как сделать отрисовку блока так, чтобы учитывать сдвиг от толщины клетки? так же положить в переменную и прибавлять ? 
            }
        }

        public int CellThickness { get; set; } = 1;
        public Color BorderCellColor { get; set; }
        public Color BackgroundColor { get; set; }
        public Color MapBlockColor { get; set; }
        public Color FigureBlockColor { get; set; }

    }
}
