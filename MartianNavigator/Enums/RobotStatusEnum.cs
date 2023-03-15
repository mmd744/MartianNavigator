using System;
using System.Collections.Generic;
using System.Text;

namespace MartianNavigator.Enums
{
    public enum RobotStatusEnum
    {
        /// <summary>
        /// Default status.
        /// </summary>
        Processing,

        /// <summary>
        /// This status is assigned to robots which made attempt to land on an occupied position.
        /// </summary>
        ImpossibleLanding,

        /// <summary>
        /// This status is assigned to robots which successfully reached final position.
        /// </summary>
        Successful,

        /// <summary>
        /// This status is for robots which fell down from Mars surface.
        /// </summary>
        Lost
    }
}
