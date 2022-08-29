using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Warship.Classes;
using Xunit;

namespace Warship.UnitTests
{
    public class CoordinateTests
    {
        [Theory]
        [InlineData("B14")]
        [InlineData("A1")]
        [InlineData("Z19")]
        public void CreateCoordinateFromInput_ForValidInputs_ReturnsCoordinate(string userInput)
        {
            var x = userInput[0];
            var y = int.Parse(userInput.Substring(1));

            var coordinate = new Coordinate(userInput);

            Assert.Equal(x, coordinate.X);
            Assert.Equal(y - 1, coordinate.Y);
        }
    }
}
