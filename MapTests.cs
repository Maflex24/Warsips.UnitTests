using Warships.Classes;
using Xunit;

namespace Warship.UnitTests
{
    public class MapTests
    {
        [Theory]
        [InlineData(10)]
        [InlineData(6)]
        public void MapConstructor_ForDifferentColumns_CheckColumnsAmount(int columns)
        {
            var exampleRowsQty = 5;
            var map = new Map(columns, exampleRowsQty);

            var mapColumns = map.MapContext.Count;

            Assert.Equal(columns, mapColumns);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(6)]
        public void MapConstructor_ForDifferentRows_CheckAllRowsLength(int rows)
        {
            var exampleColumnsQty = 10;
            var map = new Map(exampleColumnsQty, rows);

            foreach (var column in map.MapContext)
            {
                var rowContextListCount = column.Value.Length;

                Assert.Equal(rows, rowContextListCount);
            }
        }

        [Theory]
        [InlineData('A', 1)]
        [InlineData('A', 0)]
        [InlineData('A', 9)]
        [InlineData('J', 1)]
        [InlineData('J', 0)]
        [InlineData('J', 9)]
        public void IsCoordinateExist_ForValidValues_ReturnTrue(char x, int y)
        {
            var coordinate = new Coordinate(x, y);
            var map = new Map(10, 10);

            var coordinateExist = map.IsCoordinateExist(coordinate);

            Assert.True(coordinateExist);
        }

        [Theory]
        [InlineData('A', 11)]
        [InlineData('A', 10)]
        [InlineData('K', 1)]
        [InlineData('K', 11)]
        [InlineData('K', 10)]
        public void IsCoordinateExist_ForWrongValues_ReturnFalse(char x, int y)
        {
            var coordinate = new Coordinate(x, y);
            var map = new Map(10, 10);

            var coordinateExist = map.IsCoordinateExist(coordinate);

            Assert.False(coordinateExist);
        }

        [Fact]
        public void WasCoordinateShooted_ForShootedAndHitted_ReturnTrue()
        {
            var coordinate = new Coordinate('D', 9);
            var map = new Map(10, 10);

            map.MarkShoot(coordinate, true);

            var mapFieldContext = map.MapContext[coordinate.X][coordinate.Y];
            var wasShooted =
                mapFieldContext == map.hitChar;

            Assert.True(wasShooted);
        }

        [Fact]
        public void WasCoordinateShooted_ForShootedAndMissed_ReturnTrue()
        {
            var coordinate = new Coordinate('D', 9);
            var map = new Map(10, 10);

            map.MarkShoot(coordinate, false);

            var mapFieldContext = map.MapContext[coordinate.X][coordinate.Y];
            var wasShooted = mapFieldContext == map.missedChar;

            Assert.True(wasShooted);
        }

        [Fact]
        public void WasCoordinateShooted_ForNotShooted_ReturnFalse()
        {
            var coordinate = new Coordinate('D', 9);
            var map = new Map(10, 10);

            var mapFieldContext = map.MapContext[coordinate.X][coordinate.Y];
            var wasShooted =
                mapFieldContext == map.hitChar ||
                mapFieldContext == map.missedChar;

            Assert.False(wasShooted);
        }

        [Fact]
        public void MarkShoot_ForSuccessfulShot_ReturnTrue()
        {
            var coordinate = new Coordinate('D', 9);
            var map = new Map(10, 10);

            map.MarkShoot(coordinate, true);

            var isMarked = map.MapContext[coordinate.X][coordinate.Y] == map.hitChar;

            Assert.True(isMarked);
        }

        [Fact]
        public void MarkShoot_ForMissedShot_ReturnTrue()
        {
            var coordinate = new Coordinate('D', 9);
            var map = new Map(10, 10);

            map.MarkShoot(coordinate, false);

            var isMarked = map.MapContext[coordinate.X][coordinate.Y] == map.missedChar;

            Assert.True(isMarked);
        }

        [Fact]
        public void IsShipOnTargetField_ForShipOnField_ReturnTrue()
        {
            var map = new Map(5, 5);
            var coordinate = new Coordinate('C', 3);

            map.MapContext['C'][3] = map.shipChar;

            Assert.True(map.IsShipOnTargetField(coordinate));
        }

        [Fact]
        public void IsShipOnTargetField_ForNoShipOnField_ReturnFalse()
        {
            var map = new Map(5, 5);
            var coordinate = new Coordinate('C', 3);

            Assert.False(map.IsShipOnTargetField(coordinate));
        }

        [Theory]
        [InlineData(10)]
        [InlineData(3)]
        [InlineData(5)]
        public void GetRowsAmount_CheckIsCorrectly(int rows)
        {
            var map = new Map(10, rows);

            Assert.Equal(rows, map.GetRowsAmount());
        }

        [Theory]
        [InlineData(10)]
        [InlineData(3)]
        [InlineData(5)]
        public void GetColumnsAmount_CheckIsCorrectly(int columns)
        {
            var map = new Map(columns, 10);

            Assert.Equal(columns, map.GetColumnsAmount());
        }
    }
}