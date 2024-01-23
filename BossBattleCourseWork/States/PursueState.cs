using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossBattleCourseWork.States
{
    public class PursueState : State
    {
        private Player _player;

        public PursueState(Player player)
        {
            _player = player;
        }

        public override void Enter(Agent agent)
        {

        }

        public override void Execute(Agent agent, GameTime gameTime)
        {
            Vector2 direction = _player.Position - agent.Position;
            direction.Normalize();

            agent.Velocity = direction * agent.Speed;
            agent.Position += agent.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Exit(Agent agent)
        {

        }
    }
}
