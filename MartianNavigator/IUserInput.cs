using MartianNavigator.Enums;
using System.Collections.Generic;

namespace MartianNavigator
{
    public interface IUserInput
    {
        IEnumerable<CommandEnum> Commands { get; set; }
        int InitialX { get; set; }
        OrientationEnum InitialOrientation { get; set; }
        int InitialY { get; set; }
        int MaxX { get; set; }
        int MaxY { get; set; }
    }
}