using Warship.Classes;
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
        [InlineData('A', 10)]
        [InlineData('J', 1)]
        [InlineData('J', 10)]
        public void IsCoordinateExist_ForValidValues_ReturnTrue(char x, int y)
        {
            var coordinate = new Coordinate(x, y);
            var map = new Map(10, 10);

            var coordinateExist = map.IsCoordinateExist(coordinate);

            Assert.True(coordinateExist);
        }

        [Theory]
        [InlineData('A', 11)]
        [InlineData('K', 1)]
        [InlineData('K', 11)]
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
            var coordinate = new Coordinate('D', 10);
            var map = new Map(10, 10);

            map.MarkShoot(coordinate, true);

            var mapFieldContext = map.MapContext[coordinate.X][coordinate.Y - 1];
            var wasShooted =
                mapFieldContext == map.hitChar;

            Assert.True(wasShooted);
        }

        [Fact]
        public void WasCoordinateShooted_ForShootedAndMissed_ReturnTrue()
        {
            var coordinate = new Coordinate('D', 10);
            var map = new Map(10, 10);

            map.MarkShoot(coordinate, false);

            var mapFieldContext = map.MapContext[coordinate.X][coordinate.Y - 1];
            var wasShooted = mapFieldContext == map.missedChar;

            Assert.True(wasShooted);
        }

        [Fact]
        public void WasCoordinateShooted_ForNotShooted_ReturnFalse()
        {
            var coordinate = new Coordinate('D', 10);
            var map = new Map(10, 10);

            var mapFieldContext = map.MapContext[coordinate.X][coordinate.Y - 1];
            var wasShooted =
                mapFieldContext == map.hitChar ||
                mapFieldContext == map.missedChar;

            Assert.False(wasShooted);
        }

        [Fact]
        public void MarkShoot_ForSuccessfulShot_ReturnTrue()
        {
            var coordinate = new Coordinate('D', 10);
            var map = new Map(10, 10);

            map.MarkShoot(coordinate, true);

            var isMarked = map.MapContext[coordinate.X][coordinate.Y - 1] == map.hitChar;

            Assert.True(isMarked);
        }

        [Fact]
        public void MarkShoot_ForMissedShot_ReturnTrue()
        {
            var coordinate = new Coordinate('D', 10);
            var map = new Map(10, 10);

            map.MarkShoot(coordinate, false);

            var isMarked = map.MapContext[coordinate.X][coordinate.Y - 1] == map.missedChar;

            Assert.True(isMarked);
        }
    }
}