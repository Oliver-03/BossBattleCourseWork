using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BossBattleCourseWork
{
    internal class Patrol: State
    {
        private Player _player;
        private Graph _graph;
        private List<Rectangle> _map;
        private Vector2 Position1;
        private Vector2 Position2;
        private Vector2 Position3;
        private Vector2 Position4;
        private Vector2 currentTargetPosition;

        public Patrol(Player player, Graph graph, List<Rectangle> map, Vector2 position1, Vector2 position2, Vector2 position3, Vector2 position4)
        {
            _player = player;
            _graph = graph;
            _map = map;
            Position1 = position1;
            Position2 = position2;
            Position3 = position3;
            Position4 = position4;
        }

        public override void Enter(Agent agent)
        {
            agent.Colour = Color.Green;
            currentTargetPosition = Position1;
        }

        public override void Execute(Agent agent, GameTime gameTime)
        {
            float distanceThreshold = 10.0f; // Adjust this threshold based on your requirements

            if (Vector2.Distance(agent.Position, currentTargetPosition) < distanceThreshold)
            {
                // Switch to the next position in the sequence
                UpdateNextTargetPosition();
            }

            MoveToPosition(agent, currentTargetPosition, gameTime);

            if (Vector2.Distance(agent.Position, _player.Position) <= 250)
            {
                agent.StateMachine.ChangeState(new PursueState(_player, _graph, _map, currentTargetPosition, gameTime));
            }
        }

        public override void Exit(Agent agent)
        {

        }

        private void MoveToPosition(Agent agent, Vector2 targetPosition, GameTime gameTime)
        {
            Vector2 direction = targetPosition - agent.Position;
            direction.Normalize();
            agent.Velocity = direction * agent.Speed;
            agent.Position += agent.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void UpdateNextTargetPosition()
        {
            if (currentTargetPosition == Position1)
            {
                currentTargetPosition = Position2;
            }
            else if (currentTargetPosition == Position2)
            {
                currentTargetPosition = Position3;
            }
            else if (currentTargetPosition == Position3)
            {
                currentTargetPosition = Position4;
            }
            else if (currentTargetPosition == Position4)
            {
                currentTargetPosition = Position1;
            }
        }
    }
}
