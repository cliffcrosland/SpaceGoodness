using Microsoft.AspNet.SignalR;
using SpaceGoodness.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SpaceGoodness.Hubs
{
    public class SpaceHub : Hub
    {
        //////////////////////////////////////
        // Override connect/disconnect events
        //////////////////////////////////////

        public override Task OnDisconnected()
        {
            // Entities.Instance.RemoveUserSpaceShip(Context.ConnectionId);
            return base.OnDisconnected();
        }

        //////////////////////////////
        // Handle events from Client
        //////////////////////////////

        public void Start()
        {
            Entities.Instance.AddUserSpaceShip(Context.ConnectionId, new SpaceShip());
            Game.Instance.StartIfNotAlreadyStarted();
        }

        public void Fire()
        {
            Laser laser = GetUserShip().Fire();
            Entities.Instance.AddSpaceEntity(laser);
        }

        public void TurnLeft()
        {
            GetUserShip().TurnLeft();
        }

        public void TurnRight()
        {
            GetUserShip().TurnRight();
        }

        public void Accelerate()
        {
            GetUserShip().Accelerate();
        }

        public void Decelerate()
        {
            GetUserShip().Decelerate();
        }

        ////////////////////////////////////
        // Send world state back to Client
        ////////////////////////////////////

        public static void AllClientsDraw()
        {
            GlobalHost.ConnectionManager.GetHubContext<SpaceHub>().Clients.All.draw(Entities.Instance.GetSnapshot());
        }

        /////////
        // Utils
        /////////s
        private SpaceShip GetUserShip()
        {
            return Entities.Instance.GetUserSpaceShip(Context.ConnectionId);
        }

    }
}