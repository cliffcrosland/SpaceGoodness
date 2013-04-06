using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceGoodness.Models
{
    public class Laser : SpaceEntity
    {
        public const int Lifetime = Game.Fps * 3;

        public Laser() : base()
        {
            Life = 0;
        }

        public void Live()
        {
            if (Life < Lifetime)
            {
                Life++;
            }
        }

        public bool IsDead()
        {
            return Life >= Lifetime;
        }

        public int Life { get; private set; }
    }
}