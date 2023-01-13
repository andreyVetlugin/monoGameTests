using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Models.Figure
{
    public class FigureGenerator
    {
        private Dictionary<FigureType, int> typesCounts;

        Figure GenerateNewFigureType()
        {
            // TODO:....

            return new Figure(FigureType.I);
        }
    }
}
