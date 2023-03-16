using MartianNavigator.Enums;
using MartianNavigator.Models;
using NUnit.Framework;

namespace MartianNavigator.Tests
{
    public class RobotTests
    {
        private readonly Robot robot;
        public RobotTests()
        {
            this.robot = new Robot();
        }

        [Test]
        [TestCase(OrientationEnum.W, CommandEnum.R, OrientationEnum.N)]
        [TestCase(OrientationEnum.W, CommandEnum.L, OrientationEnum.S)]
        [TestCase(OrientationEnum.N, CommandEnum.R, OrientationEnum.E)]
        [TestCase(OrientationEnum.N, CommandEnum.L, OrientationEnum.W)]
        [TestCase(OrientationEnum.E, CommandEnum.R, OrientationEnum.S)]
        [TestCase(OrientationEnum.E, CommandEnum.L, OrientationEnum.N)]
        [TestCase(OrientationEnum.S, CommandEnum.R, OrientationEnum.W)]
        [TestCase(OrientationEnum.S, CommandEnum.L, OrientationEnum.E)]
        public void Turn_Test(OrientationEnum initialOrientation, CommandEnum command, OrientationEnum expectedOrientation)
        {
            robot.SetPosition(new Position(0, 0, initialOrientation));

            robot.Turn(command);

            Assert.That(robot.CurrentPosition.Orientation, Is.EqualTo(expectedOrientation));
        }

        [Test]
        [TestCase(0, 0, OrientationEnum.W, -1, 0)]
        [TestCase(0, 0, OrientationEnum.N, 0, 1)]
        [TestCase(0, 0, OrientationEnum.E, 1, 0)]
        [TestCase(0, 0, OrientationEnum.S, 0, -1)]
        public void GetPossibleNextPosition_Test(
            int initialX,
            int initialY,
            OrientationEnum orientation,
            int expectedX,
            int expectedY)
        {
            robot.SetPosition(new Position(initialX, initialY, orientation));

            var result = robot.GetPossibleNextPosition(CommandEnum.F);

            Assert.Multiple(() =>
            {
                Assert.That(result.X, Is.EqualTo(expectedX));
                Assert.That(result.Y, Is.EqualTo(expectedY));
            });
        }

        [Test]
        [TestCase(RobotStatusEnum.Lost)]
        public void SetStatus_Test(RobotStatusEnum newStatus)
        {
            robot.SetStatus(newStatus);

            Assert.That(newStatus, Is.EqualTo(robot.CurrentStatus));
        }

        [Test]
        [TestCase(1, 1, OrientationEnum.N)]
        public void SetPosition_Test(
            int newX, 
            int newY, 
            OrientationEnum newOrientation)
        {
            robot.SetPosition(new Position(newX, newY, newOrientation));

            Assert.Multiple(() =>
            {
                Assert.That(robot.CurrentPosition.X, Is.EqualTo(newX));
                Assert.That(robot.CurrentPosition.Y, Is.EqualTo(newY));
                Assert.That(robot.CurrentPosition.Orientation, Is.EqualTo(newOrientation));
            });
        }
    }
}
