using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warship.Classes;
using Xunit;

namespace Warship.UnitTests
{
    public class ShipTests
    {
        [Theory]
        [InlineData("Mini-T", 3)]
        [InlineData("Big-Bum", 7)]
        public void ShipConstructor_ForFewShips_CheckTheirProperties(string shipName, int shipLength)
        {
            var map = new Map(10, 10);
            var ship = new Ship(shipName, shipLength, map);

            Assert.Equal(shipName, ship.Name);
            Assert.Equal(shipLength, ship.ShipLength);
        }

        [Fact]
        public void PositionShip_ForRandomShipPosition_CheckIsShipPlaced()
        {
            var map = new Map(10, 10);
            var ship = new Ship("Big Heavy", 6, map);

            ship.PositionShip();

            var columnValues = map.MapContext.Values;
            int shipValues = 0;

            foreach (var column in columnValues)
            {
                var validValues = column.Where(v => v == 's').Count();
                shipValues += validValues;
            }

            Assert.Equal(ship.ShipLength, shipValues);
        }

        [Fact]
        public void PositionShip_ForThreeShipsRandomPositioned_CheckShipFieldsOnMapAndCoordinatesAmount()
        {
            var map = new Map(10, 10);
            var ships = new List<Ship>()
            {
                new Ship("Little", 3, map),
                new Ship("Middle", 5, map),
                new Ship("Great War", 7, map)
            };

            ships.ForEach(s => s.PositionShip());

            var columnValues = map.MapContext.Values;
            int shipValues = 0;

            foreach (var column in columnValues)
            {
                var validValues = column.Where(v => v == map.shipChar).Count();
                shipValues += validValues;
            }

            var shipLengthSum = ships.Select(s => s.ShipLength).Sum();

            var coordinatesElementsSum = ships.Select(s => s.Coordinates.ToList().Count()).Sum();

            Assert.Equal(shipLengthSum, shipValues);
            Assert.Equal(shipLengthSum, coordinatesElementsSum);
        }

        [Fact]
        public void PositionShip_ForFewShips_CheckMapAndCoordinatesCompatibility()
        {
            var map = new Map(10, 10);
            var ships = new List<Ship>()
            {
                new Ship("Little", 3, map),
                new Ship("Little 2", 3, map),
                new Ship("Middle", 5, map),
                new Ship("Great War", 7, map)
            };

            ships.ForEach(s => s.PositionShip());

            foreach (var ship in ships)
            {
                foreach (var shipCoordinate in ship.Coordinates)
                {
                    Assert.True(map.MapContext[shipCoordinate.X][shipCoordinate.Y] == map.shipChar);
                }
            }
        }

        [Fact]
        public void PositionShip_ForFewShips_CheckAreCoordinatesInTheSameBlock()
        {
            var map = new Map(10, 10);
            var ships = new List<Ship>()
            {
                new Ship("Little", 3, map),
                new Ship("Little 2", 3, map),
                new Ship("Middle", 5, map),
                new Ship("Great War", 7, map)
            };

            ships.ForEach(s => s.PositionShip());

            foreach (var ship in ships)
            {
                for (var i = 0; i < ship.Coordinates.Count - 1; i++)
                {
                    var currentCoordinate = ship.Coordinates[i];
                    var nextCoordinate = ship.Coordinates[i + 1];

                    var nextColumnIsInLine = currentCoordinate.X + 1 == nextCoordinate.X;
                    var nextRowIsInLine = currentCoordinate.Y + 1 == nextCoordinate.Y;

                    Assert.True(nextRowIsInLine || nextColumnIsInLine);
                }
            }
        }
    }
}
