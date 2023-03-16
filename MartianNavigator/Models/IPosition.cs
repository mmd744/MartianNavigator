using MartianNavigator.Enums;

namespace MartianNavigator.Models
{
    public interface IPosition
    {
        int X { get; set; }
        OrientationEnum Orientation { get; set; }
        int Y { get; set; }

        IPosition CreateCopy();
    }
}