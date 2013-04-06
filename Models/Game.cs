using Microsoft.AspNet.SignalR;
using SpaceGoodness.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace SpaceGoodness.Models
{
    public class Game
    {
        private static Game instance = new Game();
        
        public const int Fps = 60;
        public const int Timestep = 1000 / Fps;
        public const int CollisionDistance = 5;
        public const int WorldWidth = 1000;
        public const int WorldHeight = 600;


        private Entities entities = Entities.Instance;
        private Thread gameLoopThread = null;
        private object startLocker = new object();

        private Game()
        {
        }

        public void StartIfNotAlreadyStarted()
        {
            lock (startLocker)
            {
                if (gameLoopThread == null)
                {
                    gameLoopThread = new Thread(Play);
                    gameLoopThread.Start();
                }
            }
        }

        private void Play()
        {
            while (true)
            {
                Thread.Sleep(Timestep);
                Step();
                Draw();
            }
        }

        private void Step()
        {
            EntitiesSnapshot snapshot = entities.GetSnapshot();
            ISet<SpaceShip> aliveShips = new HashSet<SpaceShip>();
            aliveShips.UnionWith(snapshot.SpaceShips);
            ISet<Laser> aliveLasers = new HashSet<Laser>();
            aliveLasers.UnionWith(snapshot.Lasers);
            // Remove dead spaceships and lasers
            foreach (var laser in snapshot.Lasers)
            {
                if (laser.IsDead())
                {
                    entities.RemoveSpaceEntity(laser.Id);
                    aliveLasers.Remove(laser);
                }
            }
            foreach (var spaceShip in snapshot.SpaceShips)
            {
                if (spaceShip.IsDead())
                {
                    entities.RemoveSpaceEntity(spaceShip.Id);
                    aliveShips.Remove(spaceShip);
                }
            }
            // Update space ship and laser position. Check for laser collisions with space ships.
            foreach (var spaceShip in aliveShips)
            {
                spaceShip.Position = Tuples.Add(spaceShip.Position, Tuples.Scale(spaceShip.Velocity, Timestep));
                spaceShip.Position = Tuples.WorldWrap(spaceShip.Position, WorldWidth, WorldHeight);
                foreach (var laser in aliveLasers)
                {
                    laser.Position = Tuples.Add(laser.Position, Tuples.Scale(laser.Velocity, Timestep));
                    laser.Position = Tuples.WorldWrap(laser.Position, WorldWidth, WorldHeight);
                    laser.Live();
                    double distance = Tuples.Magnitude(Tuples.Minus(spaceShip.Position, laser.Position));
                    if (distance <= CollisionDistance)
                    {
                        spaceShip.TakeHit();
                    }
                }
            }
        }

        private void Draw()
        {
            SpaceHub.AllClientsDraw();
        }

        public static Game Instance
        {
            get
            {
                return instance;
            }
        }
    }
}