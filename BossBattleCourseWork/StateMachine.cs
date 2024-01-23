using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossBattleCourseWork
{
    public class StateMachine
    {
        private Agent _agent;
        private State _currentState;
        private State _previousState;

        public StateMachine(Agent agent)
        {
            _agent = agent;
            _currentState = null;
            _previousState = null;
        }

        public void ChangeState(State newState)
        {
            if (_currentState != null)
            {
                _currentState.Exit(_agent);
                _previousState = _currentState;
            }

            _currentState = newState;
            _currentState.Enter(_agent);
        }

        public void Update(GameTime gameTime)
        {
            if (_currentState != null)
            {
                _currentState.Execute(_agent, gameTime);
            }
        }

        public void RevertState()
        {
            if (_previousState != null)
            {
                ChangeState(_previousState);
            }
        }
    }
}
