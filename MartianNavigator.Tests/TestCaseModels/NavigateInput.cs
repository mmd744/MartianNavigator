using MartianNavigator.Enums;
using System.Collections.Generic;

namespace MartianNavigator.Tests.TestCaseModels
{
    public class NavigateInput
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public IList<RobotInfo> RobotInfos { get; set; }
    }

    public class RobotInfo
    {
        public int InitialX { get; set; }
        public int InitialY { get; set; }
        public OrientationEnum InitialOrientation { get; set; }
        public IEnumerable<CommandEnum> Commands { get; set; }

        public int ExpectedX { get; set; }
        public int ExpectedY { get; set; }
        public OrientationEnum ExpectedOrientation { get; set; }
        public RobotStatusEnum ExpectedRobotStatus { get; set; }
    }
}
