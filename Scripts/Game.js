$(function () {

    // Import
    var Drawing = window.Drawing;

    // Export
    window.Game = {};

    //////////////////////
    // Input
    //////////////////////

    Game.keyboard = {
        keys: { leftArrow: 37, rightArrow: 39, upArrow: 38, downArrow: 40, spacebar: 32 },
        keysDown: {},
        handleKeyDown: function (evt) {
            evt.preventDefault();
            Game.keyboard.keysDown[evt.keyCode] = true;
        },
        handleKeyUp: function (evt) {
            evt.preventDefault();
            Game.keyboard.keysDown[evt.keyCode] = false;
        }
    };

    window.addEventListener("keydown", Game.keyboard.handleKeyDown, true);
    window.addEventListener("keyup", Game.keyboard.handleKeyUp, true);

    ////////////////
    // SignalR Hub
    ////////////////

    Game.hub = $.connection.spaceHub;

    ////////////////////////////
    // Send commands to server
    ////////////////////////////
    Game.commands = {};
    Game.commands.send = function () {
        var keys = Game.keyboard.keys;
        var keysDown = Game.keyboard.keysDown;
        if (keysDown[keys.leftArrow]) {
            Game.hub.server.turnLeft();
        }
        if (keysDown[keys.rightArrow]) {
            Game.hub.server.turnRight();
        }
        if (keysDown[keys.upArrow]) {
            Game.hub.server.accelerate();
        }
        if (keysDown[keys.downArrow]) {
            Game.hub.server.decelerate();
        }
        if (keysDown[keys.spacebar]) {
            Game.hub.server.fire();
        }
        setTimeout(Game.commands.send, 30);
    };

    Game.commands.startSending = function () {
        Game.commands.send();
    };

    ///////////////////////////////////
    // Draw world state from server
    ///////////////////////////////////

    Game.hub.client.draw = function (worldState) {
        Drawing.clear();
        for (var i = 0, len = worldState.Lasers.length; i < len; i++) {
            Drawing.drawLaser(worldState.Lasers[i]);
        }
        for (var i = 0, len = worldState.SpaceShips.length; i < len; i++) {
            Drawing.drawSpaceShip(worldState.SpaceShips[i]);
        }
    };

    ////////////////////
    // Start connection
    ////////////////////
    $.connection.hub.start().done(function () {
        Game.hub.server.start();
        Game.commands.startSending();
    });

});