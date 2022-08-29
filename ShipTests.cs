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
                shipValues += column.Count(v => v == map.shipChar);
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

        [Theory]
        [InlineData('A', 2)]
        [InlineData('A', 3)]
        [InlineData('A', 4)]
        public void IsOnTarget_ForVerticalPositions_ReturnTrue(char x, int y)
        {
            var map = new Map(10, 10);
            var ship = new Ship("Destroyer", 3, map);

            ship.Coordinates = new List<Coordinate>()
            {
                new Coordinate('A', 2),
                new Coordinate('A', 3),
                new Coordinate('A', 4)
            };

            var targetCoordinate = new Coordinate(x, y);
            var isOnTarget = ship.IsOnTarget(targetCoordinate);

            Assert.True(isOnTarget);
        }

        [Theory]
        [InlineData('B', 2)]
        [InlineData('A', 5)]
        [InlineData('A', 1)]
        public void IsOnTarget_ForVerticalPositions_ReturnFalse(char x, int y)
        {
            var map = new Map(10, 10);
            var ship = new Ship("Destroyer", 3, map);

            ship.Coordinates = new List<Coordinate>()
            {
                new Coordinate('A', 2),
                new Coordinate('A', 3),
                new Coordinate('A', 4)
            };

            var targetCoordinate = new Coordinate(x, y);
            var isOnTarget = ship.IsOnTarget(targetCoordinate);

            Assert.False(isOnTarget);
        }

        [Theory]
        [InlineData('A', 2)]
        [InlineData('B', 2)]
        [InlineData('C', 2)]
        public void IsOnTarget_ForHorizontalPositions_ReturnTrue(char x, int y)
        {
            var map = new Map(10, 10);
            var ship = new Ship("Destroyer", 3, map);

            ship.Coordinates = new List<Coordinate>()
            {
                new Coordinate('A', 2),
                new Coordinate('B', 2),
                new Coordinate('C', 2)
            };

            var targetCoordinate = new Coordinate(x, y);
            var isOnTarget = ship.IsOnTarget(targetCoordinate);

            Assert.True(isOnTarget);
        }

        [Theory]
        [InlineData('A', 1)]
        [InlineData('A', 3)]
        [InlineData('B', 3)]
        [InlineData('C', 3)]
        public void IsOnTarget_ForHorizontalPositions_ReturnFalse(char x, int y)
        {
            var map = new Map(10, 10);
            var ship = new Ship("Destroyer", 3, map);

            ship.Coordinates = new List<Coordinate>()
            {
                new Coordinate('A', 2),
                new Coordinate('B', 2),
                new Coordinate('C', 2)
            };

            var targetCoordinate = new Coordinate(x, y);
            var isOnTarget = ship.IsOnTarget(targetCoordinate);

            Assert.False(isOnTarget);
        }

        [Fact]
        public void IncreaseDamage_ForNotDestroyedShip_CheckIsDamageIncreased()
        {
            var map = new Map(3, 3);
            var ship = new Ship("Someship", 3, map);

            ship.IncreaseDamage();

            Assert.True(ship.DamagesAmount == 1);
        }

        [Fact]
        public void IncreaseDamage_ForDestroyedShip_CheckIsDestroyedAndDamageEqualLength()
        {
            var map = new Map(3, 3);
            var ship = new Ship("Someship", 3, map);

            ship.IncreaseDamage();
            ship.IncreaseDamage();
            ship.IncreaseDamage();

            Assert.True(ship.DamagesAmount == 3);
            Assert.True(ship.IsDestroyed);
        }
    }
}
