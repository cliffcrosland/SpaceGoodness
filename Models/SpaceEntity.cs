using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceGoodness.Models
{
    public class SpaceEntity
    {
        protected SpaceEntity()
        {
            Position = new Tuple<double, double>(0, 0);
            Velocity = new Tuple<double, double>(0, 0);
            Acceleration = new Tuple<double, double>(0, 0);
        }

        public int Id { get; set; }
        public Tuple<double, double> Position { get; set; }
        public Tuple<double, double> Velocity { get; set; }
        public Tuple<double, double> Acceleration { get; set; }
    }
}