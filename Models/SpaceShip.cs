using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceGoodness.Models
{
    public class SpaceShip : SpaceEntity
    {
        public const int MaxHealth = 10;
        public const double TurnDelta = Math.PI / 32.0;
        public const double LaserSpeed = 0.1;
        public const int LaserGunOpeningDist = 10;

        public SpaceShip() : base()
        {
            Health = MaxHealth;
            Rotation = 0;
        }

        public void TakeHit()
        {
            if (Health > 0)
            {
                // Health--;
            }
        }

        public bool IsDead()
        {
            return Health <= 0;
        }

        public void TurnLeft()
        {
            Rotation -= TurnDelta;
        }

        public void TurnRight()
        {
            Rotation += TurnDelta;
        }

        public void Accelerate()
        {
            if (Tuples.Magnitude(Velocity) == 0.0)
            {
                Velocity = Tuples.PolarToCartesian(0.1, Rotation);
            }
            else
            {
                Velocity = Tuples.Add(Velocity, Tuples.PolarToCartesian(0.05, Rotation));
            }
        }

        public void Decelerate()
        {
            if (Tuples.Magnitude(Velocity) <= 0.1)
            {
                Velocity = Tuples.Zero;
            }
            else
            {
                Velocity = Tuples.Scale(Velocity, 0.96);
            }
        }

        public Laser Fire()
        {
            double speed = Tuples.Magnitude(Velocity) + LaserSpeed;
            return new Laser()
            {
                Position = Tuples.Add(this.Position, Tuples.PolarToCartesian(LaserGunOpeningDist, this.Rotation)),
                Velocity = Tuples.PolarToCartesian(speed, this.Rotation)
            };
        }
     
        public int Health { get; private set; }

        // Measured in Radians
        public double Rotation { get; private set; }
    }
}