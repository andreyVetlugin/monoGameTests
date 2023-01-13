using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Game1.Models.Figure
{
    internal class FigureMask : ICloneable
    {
        private List<Point> _points;
        public List<Point> Points
        {
            get => _points;
            set
            {
                for (var i = 0; i < value.Count; i++)
                {
                    for (var j = 1; j < value.Count; j++)
                    {
                        if (i != j && value[i].Equals(value[j]))
                        {
                            throw new InvalidDataException("You tried to set equal points in FigureMask");
                        }

                        if (value[i].X * value[i].X > 1 || value[i].Y * value[i].Y > 1)
                        {
                            throw new ArgumentOutOfRangeException(nameof(value));
                        }
                    }
                }
                _points = value;
            }
        }

        public FigureType FigureType { get; }
        public FigureMask(FigureType type)
        {
            FigureType = type;
            Points = new List<Point>();
            //TODO: fill _points by figureType
            //TODO: coordinates verification
        }

        public object Clone()
        {
            var newFigureMask = new FigureMask(FigureType)
            {
                Points = Points.ToList()
            };
            return newFigureMask;
        }
    }
}
