using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Models.Figure;
using Game1.Models.Map;

namespace Game1.Views
{
    internal class InGameDisplayData
    {
        public FigureType NextFigureType;
        public FigureType SavedFigureType;
        public TetrisMap TetrisMap;
        public Figure TetrisFigure;
        public int Score;
        public InGameDisplayData() { }
    }
}
