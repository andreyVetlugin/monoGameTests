using Game1.Models.Figure;
using Game1.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D ballTexture;
        private FigureManager figureGenerator;
        private Figure currentFigure;
        private InGameDisplayData _displayInfo;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        SpriteBatch spriteBatch;
        Texture2D whiteRectangle;

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Create a 1px square rectangle texture that will be scaled to the
            // desired size and tinted the desired color at draw time
            whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            spriteBatch.Dispose();
            // If you are creating your texture (instead of loading it with
            // Content.Load) then you must Dispose of it
            whiteRectangle.Dispose();
        }

        protected override void Draw(GameTime gameTime)
        {
            // Разбить на слои\скрины дерево квадрантов и отрисовывать только часть?
            
            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            // Option One (if you have integer size and coordinates)
            spriteBatch.Draw(whiteRectangle, new Rectangle(10, 20, 80, 30),
                Color.Chocolate);

            // Option Two (if you have floating-point coordinates)
            spriteBatch.Draw(whiteRectangle, new Vector2(10f, 20f), null,
                Color.Chocolate, 0f, Vector2.Zero, new Vector2(80f, 30f),
                SpriteEffects.None, 0f);

                //spriteBatch.Draw();
            spriteBatch.End();
        }
        

        // TEST Tetris field drawing
        protected void TetrisFieldDraw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        //protected override void Initialize()
        //{
        //    // TODO: Add your initialization logic here hello hello hello

        //    base.Initialize();
        //}

        //protected override void LoadContent()
        //{
        //    _spriteBatch = new SpriteBatch(GraphicsDevice);

        //    ballTexture = Content.Load<Texture2D>("ball");

        //    // TODO: use this.Content to load your game content here
        //}

        //protected override void Update(GameTime gameTime)
        //{
        //    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //        Exit();

        //    // TODO: Add your update logic here

        //    base.Update(gameTime);
        //}

        //protected override void Draw(GameTime gameTime)
        //{
        //    GraphicsDevice.Clear(Color.CornflowerBlue);
        //    _spriteBatch.Begin();
        //    var rectangle = new Texture2D(GraphicsDevice, 10, 10);
        //    _spriteBatch.Draw(rectangle,new Rectangle(), Color.Black);
        //    // TODO: Add your drawing code here
        //    _spriteBatch.End();
        //    base.Draw(gameTime);
        //}
    }
}