using MartianNavigator.Enums;
using System;
using System.Collections.Generic;

namespace MartianNavigator.Models
{
    public class Robot : IRobot
    {
        #region Constructors

        public Robot() { }

        public Robot(IPosition position) =>
            CurrentPosition = position;

        public Robot(int x, int y, OrientationEnum orientation) =>
            CurrentPosition = new Position(x, y, orientation);

        #endregion

        #region Fields

        /// <summary>
        /// A storage for rules of modifying current position of a robot when executing the <see cref="CommandEnum.F"/> command depending on current orientation.
        /// </summary>
        private static readonly IDictionary<OrientationEnum, Action<IPosition>> OrientationToMovementMappings =
            new Dictionary<OrientationEnum, Action<IPosition>>
            {
                { OrientationEnum.W, p => p.X -= 1 },
                { OrientationEnum.N, p => p.Y += 1 },
                { OrientationEnum.E, p => p.X += 1 },
                { OrientationEnum.S, p => p.Y -= 1 }
            };

        #endregion

        #region Properties

        public IPosition CurrentPosition { get; private set; }
        public RobotStatusEnum CurrentStatus { get; private set; }

        #endregion

        #region Methods

        public void SetPosition(IPosition newPosition) =>
            CurrentPosition = newPosition;

        public void SetStatus(RobotStatusEnum status) =>
            CurrentStatus = status;

        public void Turn(CommandEnum command)
        {
            int orientationValue = (int)CurrentPosition.Orientation;

            int newOrientationValue = command == CommandEnum.R
                ? (orientationValue + 1) % 4
                : (orientationValue + 3) % 4;

            CurrentPosition.Orientation = (OrientationEnum)newOrientationValue;
        }

        public IPosition GetPossibleNextPosition(CommandEnum command)
        {
            IPosition result = CurrentPosition.CreateCopy();

            if (command == CommandEnum.F)
            {
                OrientationToMovementMappings[CurrentPosition.Orientation](result);
            }
            else
            {
                throw new NotImplementedException();
            }

            return result;
        }

        #endregion
    }
}
