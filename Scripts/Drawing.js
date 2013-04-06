$(function () {

    // Export
    window.Drawing = {};

    //////////////////////
    // Initialize Drawing
    //////////////////////

    Drawing.canvas = document.getElementById("canvas");
    Drawing.context = canvas.getContext("2d");

    //////////////////
    // Draw entities
    //////////////////

    Drawing.drawLaser = function (laser) {
        
        //console.log("Laser");
        //console.log("position");
        //console.log({ x: laser.Position.Item1, y: laser.Position.Item2 });
        //console.log("velocity");
        //console.log({ velx: laser.Velocity.Item1, vely: laser.Position.Item2 });
        var c = Drawing.context;
        var x = laser.Position.Item1;
        var y = laser.Position.Item2;
        var radius = 5;
        c.beginPath();
        c.arc(x, y, radius, 0, 2 * Math.PI, false);
        c.closePath();
        c.stroke();
    };

    Drawing.drawSpaceShip = function (spaceShip) {
        //console.log("Spaceship");
        //console.log("position");
        //console.log({ x: spaceShip.Position.Item1, y: spaceShip.Position.Item2 });
        //console.log("velocity");
        //console.log({ velx: spaceShip.Velocity.Item1, vely: spaceShip.Position.Item2 });
        //console.log("rotation");
        //console.log(spaceShip.Rotation);
        var x = spaceShip.Position.Item1;
        var y = spaceShip.Position.Item2;
        var angle = spaceShip.Rotation;
        var radius = 20;
        var numSides = 3;
        drawShipPolygon(Drawing.context, x, y, angle, radius, numSides);
    };

    Drawing.clear = function () {
        Drawing.canvas.width = Drawing.canvas.width; // Seems hacky, but works great.
    };

    //////////////////////////
    // Utilities for drawing
    //////////////////////////

    var drawShipPolygon = function (c, x, y, angle, radius, numSides) {
        var delta = 2 * Math.PI / numSides;
        var r = radius + 10; // to make direction of ship clear
        c.beginPath();
        c.moveTo(x + r * Math.cos(angle), y + r * Math.sin(angle));
        r = radius;
        for (i = 1; i < numSides; i++) {
            angle += delta;
            c.lineTo(x + r * Math.cos(angle), y + r * Math.sin(angle));
        }
        c.closePath();
        c.stroke();
    };

    Drawing.drawShipPolygon = drawShipPolygon;
});