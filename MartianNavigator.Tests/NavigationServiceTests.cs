using MartianNavigator.Enums;
using MartianNavigator.Models;
using MartianNavigator.Services;
using MartianNavigator.Tests.TestCaseModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace MartianNavigator.Tests
{
    public class NavigationServiceTests
    {
        private readonly NavigationService navigationService;
        public NavigationServiceTests()
        {
            this.navigationService = new NavigationService();
        }

        #region TestCase sources

        // Sample input from tech task
        private static IEnumerable<NavigateInput> NavigateTestCases()
        {
            yield return new NavigateInput
            {
                MaxX = 5,
                MaxY = 3,
                RobotInfos = new List<RobotInfo>
                {
                    new RobotInfo
                    {
                        InitialX = 1,
                        InitialY = 1,
                        InitialOrientation = OrientationEnum.E,
                        Commands = new List<CommandEnum>
                        {
                            CommandEnum.R,
                            CommandEnum.F,
                            CommandEnum.R,
                            CommandEnum.F,
                            CommandEnum.R,
                            CommandEnum.F,
                            CommandEnum.R,
                            CommandEnum.F
                        },
                        ExpectedX = 1,
                        ExpectedY = 1,
                        ExpectedOrientation = OrientationEnum.E,
                        ExpectedRobotStatus = RobotStatusEnum.Successful
                    },
                    new RobotInfo
                    {
                        InitialX = 3,
                        InitialY = 2,
                        InitialOrientation = OrientationEnum.N,
                        Commands = new List<CommandEnum>
                        {
                            CommandEnum.F,
                            CommandEnum.R,
                            CommandEnum.R,
                            CommandEnum.F,
                            CommandEnum.L,
                            CommandEnum.L,
                            CommandEnum.F,
                            CommandEnum.F,
                            CommandEnum.R,
                            CommandEnum.R,
                            CommandEnum.F,
                            CommandEnum.L,
                            CommandEnum.L,
                        },
                        ExpectedX = 3,
                        ExpectedY = 3,
                        ExpectedOrientation = OrientationEnum.N,
                        ExpectedRobotStatus = RobotStatusEnum.Lost
                    },
                    new RobotInfo
                    {
                        InitialX = 0,
                        InitialY = 3,
                        InitialOrientation = OrientationEnum.W,
                        Commands = new List<CommandEnum> // LLFFFLFLFL
                        {
                            CommandEnum.L,
                            CommandEnum.L,
                            CommandEnum.F,
                            CommandEnum.F,
                            CommandEnum.F,
                            CommandEnum.L,
                            CommandEnum.F,
                            CommandEnum.L,
                            CommandEnum.F,
                            CommandEnum.L
                        },
                        ExpectedX = 2,
                        ExpectedY = 3,
                        ExpectedOrientation = OrientationEnum.S,
                        ExpectedRobotStatus = RobotStatusEnum.Successful
                    }
                }
            };
        }

        #endregion

        #region Mock setups

        private static void Mock_GridSurface_IsOccupied(Mock<IGridSurface> gridSurfaceMock, bool value) =>
            gridSurfaceMock.Setup(gsm => gsm.IsOccupied(It.IsAny<IPosition>())).Returns(value);

        private static void Mock_GridSurface_IsOccupied(Mock<IGridSurface> gridSurfaceMock, IPosition position, bool value) =>
            gridSurfaceMock.Setup(gsm =>
                gsm.IsOccupied(It.Is<IPosition>(p => p.X.Equals(position.X) && p.Y.Equals(position.Y)))).Returns(value);

        private static void Mock_GridSurface_IsOutOfRange(Mock<IGridSurface> gridSurfaceMock, bool value) =>
            gridSurfaceMock.Setup(gsm => gsm.IsOutOfRange(It.IsAny<IPosition>())).Returns(value);

        private static void Mock_GridSurface_IsScented(Mock<IGridSurface> gridSurfaceMock, bool value) =>
            gridSurfaceMock.Setup(gsm => gsm.IsScented(It.IsAny<IPosition>())).Returns(value);


        #endregion

        [TestCaseSource(nameof(NavigateTestCases))]
        public void Navigate_Test(NavigateInput input)
        {
            IGridSurface grid = new GridSurface(input.MaxX, input.MaxY);

            foreach (var ri in input.RobotInfos)
            {
                IPosition position = new Position(ri.InitialX, ri.InitialY, ri.InitialOrientation);
                IRobot robot = new Robot(position);
                navigationService.Initialize(grid);
                navigationService.Navigate(robot, ri.Commands);

                Assert.Multiple((TestDelegate)(() =>
                {
                    Assert.That(robot.CurrentPosition.X, Is.EqualTo(ri.ExpectedX));
                    Assert.That(robot.CurrentPosition.Y, Is.EqualTo(ri.ExpectedY));
                    Assert.That(robot.CurrentPosition.Orientation, Is.EqualTo(ri.ExpectedOrientation));
                    Assert.That(robot.CurrentStatus, Is.EqualTo(ri.ExpectedRobotStatus));
                }));
            }
        }

        [Test]
        public void Navigate_LandingOccupiedPosition_ChangesRobotStatus()
        {
            IRobot robot = new Robot();

            var gridSurfaceMock = new Mock<IGridSurface>();
            Mock_GridSurface_IsOccupied(gridSurfaceMock, true);

            navigationService.Initialize(gridSurfaceMock.Object);

            navigationService.Navigate(robot, It.IsAny<IEnumerable<CommandEnum>>());

            Assert.That(robot.CurrentStatus, Is.EqualTo(RobotStatusEnum.ImpossibleLanding));
        }

        [Test]
        public void Navigate_OneTurningCommand_Success()
        {
            IPosition position = new Position(1, 1, OrientationEnum.N);
            IRobot robot = new Robot(position);

            var gridSurfaceMock = new Mock<IGridSurface>();
            Mock_GridSurface_IsOccupied(gridSurfaceMock, false);

            navigationService.Initialize(gridSurfaceMock.Object);

            navigationService.Navigate(robot, new List<CommandEnum> { CommandEnum.L });

            Assert.Multiple(() =>
            {
                Assert.That(robot.CurrentPosition.Orientation, Is.EqualTo(OrientationEnum.W));
                Assert.That(robot.CurrentStatus, Is.EqualTo(RobotStatusEnum.Successful));
            });

            gridSurfaceMock.Verify(gsm => gsm.SetPositionStatus(position, GridPositionStatusEnum.Occupied), Times.Once);
        }

        [Test]
        public void Navigate_ScentedEdge_SkipsInstruction()
        {
            IPosition position = new Position(1, 1, OrientationEnum.N);
            IRobot robot = new Robot(position);

            var gridSurfaceMock = new Mock<IGridSurface>();
            Mock_GridSurface_IsOccupied(gridSurfaceMock, false);
            Mock_GridSurface_IsOutOfRange(gridSurfaceMock, true);
            Mock_GridSurface_IsScented(gridSurfaceMock, true);

            navigationService.Initialize(gridSurfaceMock.Object);

            navigationService.Navigate(robot, new List<CommandEnum> { CommandEnum.F });

            Assert.Multiple(() => // same position as before calling Navigate means that 1 out of 1 F instruction was skipped
            {
                Assert.That(robot.CurrentPosition.X, Is.EqualTo(position.X));
                Assert.That(robot.CurrentPosition.Y, Is.EqualTo(position.Y));
            });
        }

        [Test]
        public void Navigate_NotScentedEdge_Failure()
        {
            IPosition position = new Position(1, 1, OrientationEnum.N);
            IRobot robot = new Robot(position);

            var gridSurfaceMock = new Mock<IGridSurface>();
            Mock_GridSurface_IsOccupied(gridSurfaceMock, false);
            Mock_GridSurface_IsOutOfRange(gridSurfaceMock, true);
            Mock_GridSurface_IsScented(gridSurfaceMock, false);

            navigationService.Initialize(gridSurfaceMock.Object);

            navigationService.Navigate(robot, new List<CommandEnum> { CommandEnum.F });

            Assert.That(robot.CurrentStatus, Is.EqualTo(RobotStatusEnum.Lost));
            gridSurfaceMock.Verify(gsm => gsm.SetPositionStatus(position, GridPositionStatusEnum.Scented));
        }

        [Test]
        public void Navigate_NextPositionOccupied_SkipsInstruction()
        {
            IPosition position = new Position(1, 1, OrientationEnum.N);
            IRobot robot = new Robot(position);
            var command = CommandEnum.F;

            var gridSurfaceMock = new Mock<IGridSurface>();

            Mock_GridSurface_IsOccupied( // initial position should not be occupied
                gridSurfaceMock,
                position,
                false);
            Mock_GridSurface_IsOccupied( // possible next position is occupied
                gridSurfaceMock,
                robot.GetPossibleNextPosition(command),
                true);

            navigationService.Initialize(gridSurfaceMock.Object);

            navigationService.Navigate(robot, new List<CommandEnum> { command });

            Assert.Multiple(() => // same position as before calling Navigate means that 1 out of 1 F instruction was skipped
            {
                Assert.That(robot.CurrentPosition.X, Is.EqualTo(position.X));
                Assert.That(robot.CurrentPosition.Y, Is.EqualTo(position.Y));
            });
        }

        [Test]
        public void Navigate_NextPositionVacant_FollowsInstruction()
        {
            IPosition position = new Position(1, 1, OrientationEnum.N);
            IRobot robot = new Robot(position);
            var command = CommandEnum.F;

            var gridSurfaceMock = new Mock<IGridSurface>();
            Mock_GridSurface_IsOccupied(gridSurfaceMock, false);
            Mock_GridSurface_IsOutOfRange(gridSurfaceMock, false);

            navigationService.Initialize(gridSurfaceMock.Object);

            navigationService.Navigate(robot, new List<CommandEnum> { command });

            Assert.Multiple(() =>
            {
                Assert.That(robot.CurrentPosition.X, Is.EqualTo(position.X));
                Assert.That(robot.CurrentPosition.Y, Is.EqualTo(position.Y + 1));
                Assert.That(robot.CurrentStatus, Is.EqualTo(RobotStatusEnum.Successful));
            });

            gridSurfaceMock.Verify(gsm => gsm.SetPositionStatus(robot.CurrentPosition, GridPositionStatusEnum.Occupied), Times.Once);
        }
    }
}