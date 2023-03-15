using MartianNavigator.Enums;
using System.Collections.Generic;

namespace MartianNavigator
{
    public static class Constants
    {
        /// <summary>
        /// A constant which holds only those commands for changing robot's orientation.
        /// </summary>
        public static IList<CommandEnum> TurningCommands =
            new List<CommandEnum> { CommandEnum.L, CommandEnum.R };

        /// <summary>
        /// A constant which holds only those commands for moving a robot (Only Forward now, may add another in future.)
        /// </summary>
        public static IList<CommandEnum> MovementCommands =
            new List<CommandEnum> { CommandEnum.F };
    }
}
