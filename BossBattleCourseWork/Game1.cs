using BossBattleCourseWork;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BossBattleCourseWork
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Player _player;
        private ShapeBatcher _shapeBatcher;

        private List<Rectangle> Map;
        private Agent _agent;
        private Agent _agent2;
        private Agent _agent3;
        private Agent _agent4;

        private Graph _graph;
        private PathMap path;
        private GameLogic _logic;

        private List<Agent> _agents;
        Patrol agent2Patrol;
        Patrol agent3Patrol;
        Patrol agent4Patrol;

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
            Map.Add(new Rectangle(new Vector2(1250, 225), Color.Black, 300, 250));
            Map.Add(new Rectangle(new Vector2(350, 500), Color.Black, 100, 500));
            Map.Add(new Rectangle(new Vector2(200, 475), Color.Black, 200, 250));
            Map.Add(new Rectangle(new Vector2(100, 850), Color.Black, 200, 300));
            Map.Add(new Rectangle(new Vector2(375, 875), Color.Black, 150, 50));
            Map.Add(new Rectangle(new Vector2(575, 700), Color.Black, 150, 100));
            Map.Add(new Rectangle(new Vector2(575, 300), Color.Black, 150, 100));
            Map.Add(new Rectangle(new Vector2(925, 700), Color.Black, 150, 100));
            Map.Add(new Rectangle(new Vector2(925, 300), Color.Black, 150, 100));
            Map.Add(new Rectangle(new Vector2(750, 500), Color.Black, 100, 100));
            Map.Add(new Rectangle(new Vector2(750, 875), Color.Black, 300, 50));
            Map.Add(new Rectangle(new Vector2(1200, 600), Color.Black, 400, 300));
            Map.Add(new Rectangle(new Vector2(1200, 875), Color.Black, 300, 50));

            _graph = new Graph();
            path = new PathMap(_graph);
            
            // Player & agent initialization
            _player = new Player(new Vector2(550, 550), new Vector2(0, 0), 25);
            _agent = new Agent(new Vector2(550, 500), new Vector2(0, 0), 25, Color.Blue, 1);

            _agent2 = new Agent(new Vector2(500, 950), new Vector2(0, 0), 25, Color.Blue, 2);
            agent2Patrol = new Patrol(_player, _graph, Map, new Vector2(500, 950), new Vector2(250, 950), new Vector2(250, 800), new Vector2(500, 800));

            _agent3 = new Agent(new Vector2(1000, 950), new Vector2(0, 0), 25, Color.Blue, 2);
            agent3Patrol = new Patrol(_player, _graph, Map, new Vector2(1000, 950), new Vector2(1000, 800), new Vector2(1400, 800), new Vector2(1400, 950));

            _agent4 = new Agent(new Vector2(1050, 400), new Vector2(0, 0), 25, Color.Blue, 2);
            agent4Patrol = new Patrol(_player, _graph, Map, new Vector2(1050, 400), new Vector2(1050, 50), new Vector2(1450, 50), new Vector2(1450, 400));
            _agents = new List<Agent>
            {
                _agent
                , _agent2
                , _agent3
                , _agent4
            };

            _logic = new GameLogic(_agents, _player, Map);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            path.NodeCreator(Map);
            path.CreateEdges();
           // _agent.StateMachine.ChangeState(new IdleState(_player, Map, _graph));
            _agent2.StateMachine.ChangeState(agent2Patrol);
            _agent3.StateMachine.ChangeState(agent3Patrol);
            _agent4.StateMachine.ChangeState(agent4Patrol);
        }

        protected override void Update(GameTime gameTime)
        {
            float pSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _player.Update(pSeconds, Map);

            _agent.StateMachine.Update(gameTime);
            _agent2.StateMachine.Update(gameTime);
            _agent3.StateMachine.Update(gameTime);
            _agent4.StateMachine.Update(gameTime);

            _logic.Update(gameTime, Map);
            if (_player.IsDead == true)
            {
                Exit();
            }
             _agents.RemoveAll(agent => agent.IsDead);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _shapeBatcher.Begin();

            for (int i = 0; i < 1500; i += 50)
            {
                _shapeBatcher.DrawLine(new Vector2(i, 0), new Vector2(i, 1000), 1, Color.Pink);
            }

            // Horizontal lines
            for (int j = 0; j < 1000; j += 50)
            {
                _shapeBatcher.DrawLine(new Vector2(0, j), new Vector2(1500, j), 1, Color.Pink);
            }

            foreach (Rectangle rectangle in Map)
            {
                _shapeBatcher.Draw(rectangle);
            }
            _shapeBatcher.Draw(_player, Color.Blue);
            foreach (Agent agent in _agents)
            {
                _shapeBatcher.Draw(agent, agent.Colour);
            }
            _shapeBatcher.End();
            

            base.Draw(gameTime);
        }
    }
}