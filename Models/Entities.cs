using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceGoodness.Models
{
    public class Entities
    {
        private static Entities instance = new Entities();

        private Dictionary<int, SpaceEntity> entities;
        private Dictionary<string, SpaceShip> userSpaceShips;
        private int entityIdCounter;
        private object locker = new object();

        private Entities()
        {
            entities = new Dictionary<int, SpaceEntity>();
            userSpaceShips = new Dictionary<string, SpaceShip>();
            entityIdCounter = 0;
        }

        public SpaceEntity GetSpaceEntity(int id)
        {
            lock (locker)
            {
                return entities[id];
            }
        }

        public int AddSpaceEntity(SpaceEntity entity)
        {
            lock (locker)
            {
                if (entity.Id == 0)
                {
                    entity.Id = ++entityIdCounter;
                }
                if (!entities.ContainsKey(entity.Id))
                {
                    entities[entity.Id] = entity;
                }
                return entity.Id;
            }
        }

        public void SetSpaceEntity(int id, SpaceEntity entity)
        {
            lock (locker)
            {
                entities[id] = entity;
            }
        }

        public void RemoveSpaceEntity(int id)
        {
            lock (locker)
            {
                entities.Remove(id);
            }
        }

        public ISet<Laser> GetAllLasers()
        {
            lock (locker)
            {
                ISet<Laser> ret = new HashSet<Laser>();
                foreach (var entity in entities.Values)
                {
                    if (entity is Laser)
                    {
                        ret.Add((Laser) entity);
                    }
                }
                return ret;
            }
        }

        public ISet<SpaceShip> GetAllSpaceShips()
        {
            lock (locker)
            {
                ISet<SpaceShip> ret = new HashSet<SpaceShip>();
                foreach (var entity in entities.Values)
                {
                    if (entity is SpaceShip)
                    {
                        ret.Add((SpaceShip) entity);
                    }
                }
                return ret;
            }
        }

        public EntitiesSnapshot GetSnapshot()
        {
            lock (locker)
            {
                return new EntitiesSnapshot
                {
                    SpaceShips = GetAllSpaceShips(),
                    Lasers = GetAllLasers()
                };
            }
        }

        public SpaceShip GetUserSpaceShip(string connectionId)
        {
            lock (locker)
            {
                return userSpaceShips[connectionId];
            }
        }

        public void AddUserSpaceShip(string connectionId, SpaceShip ship)
        {
            lock (locker)
            {
                userSpaceShips[connectionId] = ship;
                AddSpaceEntity(ship);
            }
        }

        public void RemoveUserSpaceShip(string connectionId)
        {
            lock (locker)
            {
                SpaceShip ship = userSpaceShips[connectionId];
                userSpaceShips.Remove(connectionId);
                entities.Remove(ship.Id);
            }
        }

        public ISet<string> GetAllUserConnectionIds()
        {
            lock (locker)
            {
                ISet<string> ret = new HashSet<string>();
                foreach (string connectionId in userSpaceShips.Keys)
                {
                    ret.Add(connectionId);
                }
                return ret;
            }
        }

        public static Entities Instance
        {
            get
            {
                return instance;
            }
        }
    }

    public class EntitiesSnapshot
    {
        public ISet<SpaceShip> SpaceShips { get; set; }
        public ISet<Laser> Lasers { get; set; }
    }
}