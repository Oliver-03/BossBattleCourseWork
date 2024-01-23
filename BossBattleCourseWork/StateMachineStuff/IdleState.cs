using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BossBattleCourseWork
{
    public class IdleState : State
    {
        private float idleDuration;
        private float elapsedTime;
        private Player _player;
        private List<Rectangle> map;
        private Graph _graph;
        public IdleState(Player player, List<Rectangle> map, Graph graph)
        {
            idleDuration = 10f;
            elapsedTime = 0f;
            _player = player;
            this.map = map;
            _graph = graph;
        }

        public override void Enter(Agent agent)
        {
            agent.Velocity = Vector2.Zero;
            agent.Colour = Color.Green;
            elapsedTime = 0f;
        }

        public override void Execute(Agent agent, GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= idleDuration)
            {
                agent.StateMachine.ChangeState(new PursueState(_player, _graph));
            }
        }

        public override void Exit(Agent agent)
        {
            agent.Colour = Color.Red;
        }
    }
}
