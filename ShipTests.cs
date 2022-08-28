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
        public void PositionShip_ForThreeShipsRandomPositioned_CheckHowManyShipsFieldsOnMap()
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

            Assert.Equal(15, shipValues);
        }
    }
}
