using MartianNavigator.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianNavigator
{
    public class UserInput : IUserInput
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public int InitialX { get; set; }
        public int InitialY { get; set; }
        public OrientationEnum InitialOrientation { get; set; }
        public IEnumerable<CommandEnum> Commands { get; set; }
    }
}
