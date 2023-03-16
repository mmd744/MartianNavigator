using MartianNavigator.Enums;

namespace MartianNavigator.Models
{
    public interface IRobot
    {
        IPosition CurrentPosition { get; }
        RobotStatusEnum CurrentStatus { get; }

        IPosition GetPossibleNextPosition(CommandEnum command);
        void SetPosition(IPosition newPosition);
        void SetStatus(RobotStatusEnum status);
        void Turn(CommandEnum command);
    }
}