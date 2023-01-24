using System.Collections.Generic;
using System.Linq;
using Game1.Models.Figure;
using Game1.Models.Map;
using Game1.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    internal class TetrisDrawer : Game
    {

        private GraphicsDeviceManager _graphics;
        private InGameDisplayInfo _displayInfo;
        private InGameDisplayData _displayData; // any property cannt be null.. get instance by default

        private int _millisecondsPerTick = 2000; // TODO: перенести в DI
        private int _millisecondsInCurrentTick = 0;

        private FigureManager _figureManager = new FigureManager();

        private int _score = 0; //TODO!!
        private int _scorePerLine = 100;
        private int _scorePerDoubleLines = 300;
        private int _scorePerTripleLines = 500;
        private int _scorePerQuadraLines = 800;


        private SpriteBatch _spriteBatch;
        private Texture2D _whiteRectangle;
        private FigureMover _figureMover; // TODO: DI?

        private Point _mapSize = new(10, 20);// TODO: передавать через DI с другой старотовой инфой. Класс GameInitInfo?
        public Point StartDrawPoint { get; set; } = new(0, 0); // как и где контролировать? как минимум передавать в конструктор.

        public TetrisDrawer(InGameDisplayInfo displayInfo, InGameDisplayData displayData /*change to initGameData*/, Point mapSize, Point startDrawPoint, FigureManager FIGUREMANAGER /*TODO ВРЕМЕННО*/)
        {
            _displayInfo = displayInfo;
            //_displayData = displayData;
            _mapSize = mapSize;
            StartDrawPoint = startDrawPoint;


            _graphics = new(this)
            {
                PreferredBackBufferWidth = 1200,
                PreferredBackBufferHeight = 800
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _displayData = displayData;
            _figureMover = new();

            _figureManager = FIGUREMANAGER;

            //_displayData.TetrisFigure = new Figure(FigureType.I, new Point(5, 5));
            //_displayData.TetrisFigure.FigureMask.Points.Add(new Point(0, 0));
            //_displayData.TetrisFigure.FigureMask.Points.Add(new Point(1, 1));
            //_displayData.TetrisFigure.FigureMask.Points.Add(new Point(0, 1));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new(GraphicsDevice);
            _whiteRectangle = new(GraphicsDevice, 1, 1);
            _whiteRectangle.SetData(new[] { Color.White });
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            _spriteBatch.Dispose();
            _whiteRectangle.Dispose();
        }

        private void HandleFigureMove(MoveDirection direction)// TODO: придумать нормальное название 
        {
            var succesMove = _figureMover.TryMoveFigure(_displayData.TetrisFigure, _displayData.TetrisMap, direction);
            if (!succesMove && direction == MoveDirection.Down)
            {
                HandleFigureFall();
            }
        }

        private void MakePointsPartOfMap(List<Point> pointsToAdd) // TODO: все то же
        {
            pointsToAdd.ForEach(point => _displayData.TetrisMap[point.X][point.Y] = FieldValue.Block);
        }

        private void SpawnNextFigure() // TODO вообще убрать, должно где-то обрабатываться внутри какого-то сервиса и отсюда быть доступ только к DisplayData
        {
            var spawnStatus = _figureManager.TrySpawnNextFigureToMap(_displayData.TetrisMap);
            _displayData.TetrisFigure = _figureManager.CurrentFigure;
        }

        private void HandleFigureFall()
        {
            var newPointsCoords = _displayData.TetrisFigure.GetProjectFigureToMap();
            MakePointsPartOfMap(newPointsCoords);


            var newPointsYCoords = newPointsCoords.Select(point => point.Y).Distinct().ToList();
            var fullLinesCoords = GetFullLinesCoordinates(newPointsYCoords);

            DestroyLines(fullLinesCoords);
            AddScoreByDestoryedLines(fullLinesCoords.Count);
            // FallUpperBlocks

            SpawnNextFigure();

        }

        private void DestroyLines(List<int> linesCoords)
        {
            foreach (var y in linesCoords)
            {
                for (var x = 0; x < _displayData.TetrisMap.GetSize().X; x++)
                {
                    _displayData.TetrisMap[x][y] = FieldValue.None;
                }
            }
        }

        private List<int> GetFullLinesCoordinates(List<int> potentialFullLines)
        {
            var fullLinesCoords = new List<int>();
            foreach (var y in potentialFullLines)
            {
                var isLineFull = true;
                for (var x = 0; x < _displayData.TetrisMap.GetSize().X; x++)
                {
                    if (_displayData.TetrisMap[x][y] == FieldValue.None)
                    {
                        isLineFull = false;
                        break;
                    }
                }

                if (isLineFull)
                {
                    fullLinesCoords.Add(y);
                }

            }
            return fullLinesCoords;
        }

        private void AddScoreByDestoryedLines(int linesCount)
        {
            _score += linesCount switch
            {
                1 => _scorePerLine,
                2 => _scorePerDoubleLines,
                3 => _scorePerTripleLines,
                4 => _scorePerQuadraLines,
                _ => 0
            };
        }

        private void HandleKeyboardInput() // TODO: !!!!! в отдельный слой\менеджер\сервис!!!
        {
            KeyboardState state = Keyboard.GetState();

            MoveDirection direction;

            if (state.IsKeyDown(Keys.Left))
            {
                direction = MoveDirection.Left;
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                direction = MoveDirection.Right;
            }
            else if (state.IsKeyDown(Keys.Down))
            {
                direction = MoveDirection.Down;
            }
            else
            {
                return;
            }

            HandleFigureMove(direction);
            //_figureMover.TryMoveFigure(_displayData.TetrisFigure, _displayData.TetrisMap, direction);
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: заменить tickTime на какую-нибудь константу
            _millisecondsInCurrentTick += gameTime.ElapsedGameTime.Milliseconds;
            HandleKeyboardInput();

            if (_millisecondsInCurrentTick < _millisecondsPerTick)
            {
                return;
            }

            _millisecondsInCurrentTick = 0;
            HandleFigureMove(MoveDirection.Down);
            //_figureMover.TryMoveFigure(_displayData.TetrisFigure, _displayData.TetrisMap, MoveDirection.Down);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Разбить на слои\скрины дерево квадрантов и отрисовывать только часть?

            base.Draw(gameTime);
            GraphicsDevice.Clear(_displayInfo.BackgroundColor);
            _spriteBatch.Begin();

            ////////Draw field////////


            for (int i = 0; i < _mapSize.X + 1; i++)
            {
                _spriteBatch.Draw(_whiteRectangle, new Rectangle(StartDrawPoint.X + i * _displayInfo.CellSize, StartDrawPoint.Y,
                    _displayInfo.CellThickness, _displayInfo.CellSize * _mapSize.Y + _displayInfo.CellThickness), _displayInfo.BorderCellColor);
            }

            for (int i = 0; i < _mapSize.Y + 1; i++)
            {
                _spriteBatch.Draw(_whiteRectangle, new Rectangle(StartDrawPoint.X, StartDrawPoint.Y + i * _displayInfo.CellSize,
                    _displayInfo.CellSize * _mapSize.X + _displayInfo.CellThickness, _displayInfo.CellThickness), _displayInfo.BorderCellColor);
            }


            ////////Draw figure////////


            foreach (var figurePoint in _displayData.TetrisFigure.GetProjectFigureToMap())  // TODO вынести метод закраски rectangle отдельный метод
            {
                var coords = figurePoint * new Point(_displayInfo.CellSize);
                var drawRectangleCoordinates = new Rectangle(
                    StartDrawPoint.X + coords.X + _displayInfo.CellThickness,
                    StartDrawPoint.Y + coords.Y + _displayInfo.CellThickness,
                    _displayInfo.CellSize - _displayInfo.CellThickness,
                    _displayInfo.CellSize - _displayInfo.CellThickness);

                _spriteBatch.Draw(_whiteRectangle, drawRectangleCoordinates, _displayInfo.FigureBlockColor);
            }


            ////////Draw filled map blocks////////

            foreach (var mapBlocksCoords in _displayData.TetrisMap.GetFilledCellsCoordinates())
            {
                var coords = mapBlocksCoords * new Point(_displayInfo.CellSize);
                var drawRectangleCoordinates = new Rectangle(
                    StartDrawPoint.X + coords.X + _displayInfo.CellThickness,
                    StartDrawPoint.Y + coords.Y + _displayInfo.CellThickness,
                    _displayInfo.CellSize - _displayInfo.CellThickness,
                    _displayInfo.CellSize - _displayInfo.CellThickness);

                _spriteBatch.Draw(_whiteRectangle, drawRectangleCoordinates, _displayInfo.MapBlockColor);
            }


            //// Option One (if you have integer size and coordinates)
            //_spriteBatch.Draw(_whiteRectangle, new Rectangle(10, 20, 80, 30),
            //    Color.Chocolate);

            //// Option Two (if you have floating-point coordinates)
            //_spriteBatch.Draw(_whiteRectangle, new Vector2(10f, 20f), null,
            //    Color.Chocolate, 0f, Vector2.Zero, new Vector2(80f, 30f),
            //    SpriteEffects.None, 0f);

            //spriteBatch.Draw();
            _spriteBatch.End();
        }
    }
}
