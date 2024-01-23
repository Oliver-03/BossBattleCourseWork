using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace BossBattleCourseWork.States
{
    public class IdleState : State
    {
        private float idleDuration;
        private float elapsedTime;
        private Player _player;
        public IdleState(Player player)
        {
            idleDuration = 10f;
            elapsedTime = 0f;
            _player = player;

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
                agent.StateMachine.ChangeState(new PursueState(_player));
            }
        }

        public override void Exit(Agent agent)
        {
            agent.Colour = Color.Red;
        }
    }
}
