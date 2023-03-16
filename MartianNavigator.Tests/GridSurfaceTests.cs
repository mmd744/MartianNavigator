using MartianNavigator.Enums;
using MartianNavigator.Models;
using Moq;
using NUnit.Framework;

namespace MartianNavigator.Tests
{
    public class GridSurfaceTests
    {
        private readonly GridSurface surface;
        public GridSurfaceTests()
        {
            this.surface = new GridSurface(10, 10);
        }

        [Test]
        [TestCase(11, 11, true)]
        [TestCase(5, 3, false)]
        public void IsOutOfRange_Test(int x, int y, bool expectedResult)
        {
            var result = surface.IsOutOfRange(new Position(x, y, It.IsAny<OrientationEnum>()));

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(5, 5, true)]
        [TestCase(3, 5, false)]
        public void IsOccupied_Test(int x, int y, bool expectedResult)
        {
            surface.SetPositionStatus(new Position(5, 5, It.IsAny<OrientationEnum>()), GridPositionStatusEnum.Occupied);

            var result = surface.IsOccupied(new Position(x, y, It.IsAny<OrientationEnum>()));

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(5, 5, true)]
        [TestCase(3, 5, false)]
        public void IsScented_Test(int x, int y, bool expectedResult)
        {
            surface.SetPositionStatus(new Position(5, 5, It.IsAny<OrientationEnum>()), GridPositionStatusEnum.Scented);

            var result = surface.IsScented(new Position(x, y, It.IsAny<OrientationEnum>()));

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(4, 4, GridPositionStatusEnum.Scented)]
        public void SetPositionStatus_Test(
            int x, int y,
            GridPositionStatusEnum newGridPositionStatus)
        {
            var position = new Position(x, y, It.IsAny<OrientationEnum>());

            surface.SetPositionStatus(position, newGridPositionStatus);

            Assert.That(newGridPositionStatus, Is.EqualTo(surface.GetPositionStatus(position)));
        }
    }
}
