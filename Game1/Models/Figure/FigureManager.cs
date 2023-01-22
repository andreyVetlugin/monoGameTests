using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Game1.Models.Map;
using Microsoft.Xna.Framework;


namespace Game1.Models.Figure
{
    internal class FigureManager
    {
        public Figure CurrentFigure { get; set; }

        private Stack<FigureType> _typesList = new(14);
        private int _bagSize;
        private readonly CollisionChecker _collisionChecker = new CollisionChecker(); // передавать через DI 

        private object test = null; // ПОЧЕМУ НЕ РАБОТАЕТ ОГРАНИЧЕНИЯ ДЛЯ КОМПИЛЯЦИИ НА null????

        public FigureManager()
        {

        }

        public bool TrySpawnNextFigureToMap(TetrisMap map)
        {
            if (_typesList.Count == 0)
            {
                GenerateBags(2);
            }

            var figure = new Figure(_typesList.Peek(), new Point(5,1));
            var collisionStatus = _collisionChecker.CheckCollision(map, figure);

            if (collisionStatus)
            {
                return false;
            }

            CurrentFigure = figure;

            return true;

            //TODO: как-то передать характеристики карты
            //TODO: collision 
        }

        private void GenerateBags(int countOfBags)
        { //TODO: random filling
            for (int i = 0; i < countOfBags; i++)
            {
                Enum.GetValues<FigureType>().ToList().ForEach(x => _typesList.Push(x));
            }
        }
    }
}
