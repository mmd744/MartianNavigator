using System;
using System.Collections.Generic;
using System.Text;

namespace MartianNavigator.Enums
{
    public enum GridPositionStatusEnum
    {
        /// <summary>
        /// Default status of a vacant position.
        /// </summary>
        Vacant,

        /// <summary>
        /// This status is for positions from which robot previously fell out of Mars surface.
        /// </summary>
        Scented,

        /// <summary>
        /// This status is for positions which are currently occupied by another robot.
        /// </summary>
        Occupied
    }
}
