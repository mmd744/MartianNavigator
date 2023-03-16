using MartianNavigator.Enums;

namespace MartianNavigator.Models
{
    public interface IGridSurface
    {
        GridPositionStatusEnum GetPositionStatus(IPosition position);
        bool IsOccupied(IPosition position);
        bool IsOutOfRange(IPosition position);
        bool IsScented(IPosition position);
        void SetPositionStatus(IPosition position, GridPositionStatusEnum status);
    }
}