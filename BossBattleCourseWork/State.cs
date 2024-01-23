using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossBattleCourseWork
{
    public abstract class State
    {
        public abstract void Enter(Agent agent);
        public abstract void Execute(Agent agent, GameTime gameTime);
        public abstract void Exit(Agent agent);
    }
}
