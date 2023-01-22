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
            _points = new List<Point>();

            switch (type)
            {
                case FigureType.I:
                    _points.AddRange(new List<Point> { new(0, 1), new(0, 0), new(0, -1), new(0, -2) });
                    break;
                case FigureType.J:
                    _points.AddRange(new List<Point> { new(0, 1), new(0, 0), new(0, -1), new(-1, -1) });
                    break;
                case FigureType.L:
                    _points.AddRange(new List<Point> { new(0, 1), new(0, 0), new(0, -1), new(1, -1) });
                    break;
                case FigureType.O:
                    _points.AddRange(new List<Point> { new(0, 1), new(0, 0), new(1, 0), new(1, 1) });
                    break;
                case FigureType.S:
                    _points.AddRange(new List<Point> { new(0, 1), new(0, 0), new(1, 1), new(-1, 0) });
                    break;
                case FigureType.T:
                    _points.AddRange(new List<Point> { new(0, 1), new(0, 0), new(1, 0), new(-1, 0) });
                    break;
                case FigureType.Z:
                    _points.AddRange(new List<Point> { new(0, 1), new(0, 0), new(-1, 1), new(1, 0) });
                    break;
            }



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
