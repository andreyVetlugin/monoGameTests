using Game1.Models.Map;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game1.Models.Figure
{
    internal class Figure : ICloneable
    {
        private Point _coordinate;
        private FigureMask _figureMask;

        public FigureMask FigureMask
        {
            get => _figureMask;
            set
            {
                if (_figureMask.Points.Count != value.Points.Count)
                {
                    throw new InvalidDataException("You cannt change value of points in existed Figure");
                }
                _figureMask = value;
            }
        }

        public Point Coordinate
        {
            get => _coordinate;
            set
            {
                var diffsSquare = value - _coordinate;
                if (diffsSquare.X > 1 || diffsSquare.Y > 1)
                {
                    throw new InvalidDataException("You can change coordinate only by one cell per one step");
                }
                _coordinate = value;
            }
        }
        public Figure(FigureType figureType, Point startCoordinate)
        {
            _figureMask = new FigureMask(figureType);
            _coordinate = startCoordinate;
        }

        public Figure(FigureType figureType)
        {
            _figureMask = new FigureMask(figureType);
        }

        private Figure()
        {
        }
        
        public List<Point> GetProjectFigureToMap()
        {
            var coords = _figureMask.Points.Select(x => _coordinate + x).ToList();
            return coords;

            // step2: удалить функцию и перенести в отдельный сервис, который будет принимать фигуру и карту, отдавать проекцию на карту? или менять саму карту? доступ к серсвису через DI

            
            // TODO: продумать нужен ли аргумент tetrisMap??? скорее всего да 
            // TODO: возможно проверку коллизии вызывать здесь throw new NotImplementedException();
            // НА TODO ответ - НЕТ!!!!!! 
        }

        public object Clone()
        {
            var newFigure = new Figure()
            {
                _figureMask = (FigureMask)FigureMask.Clone(),
                _coordinate = _coordinate   
            };
            return newFigure;
        }

        //private FigureMask FillFigureMaskByType()
        //{
            
        //}
    }
}
