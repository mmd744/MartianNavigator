using MartianNavigator.Enums;
using MartianNavigator.Models;
using NUnit.Framework;

namespace MartianNavigator.Tests
{
    public class PositionTests
    {
        private readonly Position position;
        public PositionTests()
        {
            this.position = new Position(1, 1, OrientationEnum.N);
        }

        [Test]
        public void CreateCopy_Test()
        {
            var result = position.CreateCopy();

            Assert.Multiple(() =>
            {
                Assert.That(result.X, Is.EqualTo(position.X));
                Assert.That(result.Y, Is.EqualTo(position.Y));
                Assert.That(result.Orientation, Is.EqualTo(position.Orientation));
                Assert.That(result.GetHashCode(), !Is.EqualTo(position.GetHashCode()));
            });
        }
    }
}
