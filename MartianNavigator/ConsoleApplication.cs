using MartianNavigator.Enums;
using System.Collections.Generic;
using System.Linq;
using System;
using MartianNavigator.Services;
using MartianNavigator.Models;

namespace MartianNavigator
{
    public class ConsoleApplication
    {
        private readonly IValidationService validationService;
        private readonly INavigationService navigationService;
        public ConsoleApplication(IValidationService validationService, INavigationService navigationService)
        {
            this.validationService = validationService;
            this.navigationService = navigationService;
        }

        public void Run()
        {
            string rightUpperBound = Console.ReadLine();
            validationService.ValidateRightUpperBoundStringFormat(rightUpperBound);

            int maxX = int.Parse(rightUpperBound.Split(' ')[0]);
            int maxY = int.Parse(rightUpperBound.Split(' ')[1]);

            validationService.ValidateCoordinates(maxX, maxY);

            IList<(Robot robot, IEnumerable<CommandEnum> commands)> robotInstructions = new List<(Robot, IEnumerable<CommandEnum>)>();

            while (true)
            {
                string positionAndOrientation = Console.ReadLine().ToUpper();

                if (string.IsNullOrEmpty(positionAndOrientation))
                    break;

                validationService.ValidatePositionAndOrientationStringFormat(positionAndOrientation);

                var splittedPositionAndOrientation = positionAndOrientation.Split(' ');

                int initialX = int.Parse(splittedPositionAndOrientation[0]);
                int initialY = int.Parse(splittedPositionAndOrientation[1]);

                validationService.ValidateCoordinates(initialX, initialY);

                var orientation = Enum.Parse<OrientationEnum>(splittedPositionAndOrientation[2].ToString());

                string instructions = Console.ReadLine().ToUpper();
                validationService.ValidateInstructionsStringFormat(instructions);

                var commands = instructions.Select(i => Enum.Parse<CommandEnum>(i.ToString()));

                var robot = new Robot(initialX, initialY, orientation);

                robotInstructions.Add((robot, commands));
            }

            navigationService.Initialize(maxX, maxY);

            foreach (var (robot, commands) in robotInstructions)
            {
                navigationService.Navigate(robot, commands);

                if (robot.CurrentStatus == RobotStatusEnum.ImpossibleLanding)
                    continue;

                string robotStatus = robot.CurrentStatus == RobotStatusEnum.Lost
                    ? "LOST"
                    : string.Empty;

                Console.WriteLine(
                    $"{robot.CurrentPosition.X} {robot.CurrentPosition.Y} {robot.CurrentPosition.Orientation} {robotStatus}");
            }
        }
    }
}