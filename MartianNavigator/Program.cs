using MartianNavigator.Enums;
using MartianNavigator.Models;
using MartianNavigator.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartianNavigator
{
    public class Program
    {
        static void Main()
        {
            try
            {
                IValidationService validationService = new ValidationService();

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

                INavigationService navigationService = new NavigationService(maxX, maxY);

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
            catch (Exception ex)
            {
                Console.WriteLine(ex is FormatException || ex is ArgumentOutOfRangeException
                    ? ex.Message
                    : "Something went wrong.");

                return;
            }
            
            Console.ReadKey();
        }
    }
}
