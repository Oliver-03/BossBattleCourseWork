using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace BossBattleCourseWork
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player _player;
        private ShapeBatcher _shapeBatcher;
        private List<Rectangle> Map;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Map = new List<Rectangle>();

        }

        protected override void Initialize()
        {
            // TODO: This is screen initialization logic 
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1500;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();

            //This is initialization for map
            _shapeBatcher = new ShapeBatcher(this);
            Map.Add(new Rectangle(new Vector2(150, 150), Color.Black, 100, 100));
            Map.Add(new Rectangle(new Vector2(400, 75), Color.Black, 200, 150));
            Map.Add(new Rectangle(new Vector2(800, 75), Color.Black, 200, 150));
            Map.Add(new Rectangle(new Vector2(1250, 250), Color.Black, 300, 250));
            Map.Add(new Rectangle(new Vector2(350, 500), Color.Black, 100, 500));
            Map.Add(new Rectangle(new Vector2(200, 450), Color.Black, 200, 250));
            Map.Add(new Rectangle(new Vector2(100, 850), Color.Black, 200, 350));
            Map.Add(new Rectangle(new Vector2(350, 875), Color.Black, 150, 100));
            Map.Add(new Rectangle(new Vector2(575, 700), Color.Black, 150, 150));
            Map.Add(new Rectangle(new Vector2(575, 300), Color.Black, 150, 150));
            Map.Add(new Rectangle(new Vector2(925, 700), Color.Black, 150, 150));
            Map.Add(new Rectangle(new Vector2(925, 300), Color.Black, 150, 150));
            Map.Add(new Rectangle(new Vector2(750, 500), Color.Black, 100, 100));
            Map.Add(new Rectangle(new Vector2(750, 900), Color.Black, 250, 50));
            Map.Add(new Rectangle(new Vector2(1200, 625), Color.Black, 400, 300));
            Map.Add(new Rectangle(new Vector2(1200, 880), Color.Black, 300, 75));
            _player = new Player(new Vector2(550, 500), new Vector2(0, 0), 10);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here
            float pSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _player.Update(pSeconds, Map);
            Debug.WriteLine(_player.Velocity);
                 base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _shapeBatcher.Begin();
            foreach (Rectangle rectangle in Map)
            {
                _shapeBatcher.Draw(rectangle);
            }
            _shapeBatcher.Draw(_player, Color.Blue);
            _shapeBatcher.End();
            

            base.Draw(gameTime);
        }
    }
}