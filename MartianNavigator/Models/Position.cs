using MartianNavigator.Enums;

namespace MartianNavigator.Models
{
    public class Position : IPosition
    {
        #region Constructors

        public Position(int x, int y, OrientationEnum orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
        }

        #endregion

        #region Properties

        public int X { get; set; }
        public int Y { get; set; }
        public OrientationEnum Orientation { get; set; }

        #endregion

        #region Methods

        public IPosition CreateCopy() => new Position(X, Y, Orientation);

        #endregion
    }
}
