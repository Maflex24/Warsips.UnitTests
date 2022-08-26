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

            var mapColumns = map.ColumnsContext.Count;

            Assert.Equal(columns, mapColumns);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(6)]
        public void MapConstructor_ForDifferentRows_CheckAllRowsLength(int rows)
        {
            var exampleColumnsQty = 10;
            var map = new Map(exampleColumnsQty, rows);

            foreach (var column in map.ColumnsContext)
            {
                var rowContextListCount = column.Value.Length;

                Assert.Equal(rows, rowContextListCount);
            }
        }
    }
}