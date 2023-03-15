using MartianNavigator.Enums;
using MartianNavigator.Models;
using System;
using System.Collections.Generic;

namespace MartianNavigator.Services
{
    public class NavigationService : INavigationService
    {
        #region Fields

        private readonly GridSurface surface;

        #endregion

        #region Constructors

        public NavigationService(int maxX, int maxY)
        {
            this.surface = new GridSurface(maxX, maxY);
        }

        #endregion

        #region Methods

        public void Navigate(Robot robot, IEnumerable<CommandEnum> commands)
        {
            if (surface.IsOccupied(robot.CurrentPosition)) // RobotStatusEnum.ImpossibleLanding
            {
                robot.SetStatus(RobotStatusEnum.ImpossibleLanding);
                return;
            }

            foreach (var command in commands)
            {
                if (Constants.TurningCommands.Contains(command))
                {
                    robot.Turn(command);
                }
                else
                {
                    var possibleNextPosition = robot.GetPossibleNextPosition(command);
                    var isNextPositionOutOfRange = surface.IsOutOfRange(possibleNextPosition);
                    var isCurrentPositionScented = surface.IsScented(robot.CurrentPosition);

                    if (isNextPositionOutOfRange)
                    {
                        if (isCurrentPositionScented) // feels the scent and stops
                        {
                            continue;
                        }
                        else // falls and leaves scent if does not feel the scent
                        {
                            surface.SetPositionStatus(robot.CurrentPosition, GridPositionStatusEnum.Scented);
                            robot.SetStatus(RobotStatusEnum.Lost);
                        }
                    }
                    else // next position is Not out of range
                    {
                        if (surface.IsOccupied(possibleNextPosition))
                        {
                            continue;
                        }
                        else // move
                        {
                            robot.SetPosition(possibleNextPosition);
                        }
                    }
                }

                if (robot.CurrentStatus == RobotStatusEnum.Lost)
                    return;
            }

            robot.SetStatus(RobotStatusEnum.Successful);
            surface.SetPositionStatus(robot.CurrentPosition, GridPositionStatusEnum.Occupied);
        }

        #endregion
    }
}
