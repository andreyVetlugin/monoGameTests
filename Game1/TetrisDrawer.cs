using Game1.Models.Figure;
using Game1.Models.Map;
using Game1.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    internal class TetrisDrawer : Game
    {

        private GraphicsDeviceManager _graphics { get; set; }
        private InGameDisplayInfo _displayInfo { get; set; }
        private InGameDisplayData _displayData { get; set; } // any property cannt be null.. get instance by default

        private SpriteBatch _spriteBatch { get; set; }
        private Texture2D _whiteRectangle { get; set; }

        private Point _mapSize { get; set; } = new Point(10, 20);// TODO: передавать через DI с другой старотовой инфой. Класс GameInitInfo?
        public Point StartDrawPoint { get; set; } = new Point(0, 0); // как и где контролировать? как минимум передавать в конструктор.

        public TetrisDrawer(InGameDisplayInfo displayInfo, InGameDisplayData displayData /*change to initGameData*/, Point mapSize, Point startDrawPoint)
        {
            _displayInfo = displayInfo;
            //_displayData = displayData;
            _mapSize = mapSize;
            StartDrawPoint = startDrawPoint;


            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _displayData = displayData;


            //_displayData.TetrisFigure = new Figure(FigureType.I, new Point(5, 5));
            //_displayData.TetrisFigure.FigureMask.Points.Add(new Point(0, 0));
            //_displayData.TetrisFigure.FigureMask.Points.Add(new Point(1, 1));
            //_displayData.TetrisFigure.FigureMask.Points.Add(new Point(0, 1));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
            _whiteRectangle.SetData(new[] { Color.White });
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            _spriteBatch.Dispose();
            _whiteRectangle.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Разбить на слои\скрины дерево квадрантов и отрисовывать только часть?

            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.White);
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


            foreach (var figurePoint in _displayData.TetrisFigure.GetProjectFigureToMap())
            {
                var coords = figurePoint * new Point(_displayInfo.CellSize);
                var drawRectangleCoordinates = new Rectangle(
                    StartDrawPoint.X + coords.X + _displayInfo.CellThickness,
                    StartDrawPoint.Y + coords.Y + _displayInfo.CellThickness,
                    _displayInfo.CellSize - _displayInfo.CellThickness,
                    _displayInfo.CellSize - _displayInfo.CellThickness);
                _spriteBatch.Draw(_whiteRectangle, drawRectangleCoordinates, _displayInfo.FigureBlockColor);
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
