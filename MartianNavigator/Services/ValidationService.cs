using MartianNavigator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartianNavigator.Services
{
    public class ValidationService : IValidationService
    {
        #region Format Validations

        public void ValidateRightUpperBoundStringFormat(string rightUpperBoundString)
        {
            var splitted = rightUpperBoundString.Split(' ');
            if (splitted.Count() != 2 || splitted.Any(v => !int.TryParse(v, out _)))
            {
                throw new FormatException("Right upper bound string format is incorrect. Example: \"5 3\" ");
            }
        }

        public void ValidatePositionAndOrientationStringFormat(string positionAndOrientationString)
        {
            var splitted = positionAndOrientationString.Split(' ');

            if (splitted.Count() != 3 || 
                !int.TryParse(splitted[0], out _) || 
                !int.TryParse(splitted[1], out _) ||
                !Enum.TryParse<OrientationEnum>(splitted[2], out _))
            {
                throw new FormatException("Position and orientation string format is incorrect. Example: \"1 1 E\" ");
            }
        }

        public void ValidateInstructionsStringFormat(string instructionsString)
        {
            if (string.IsNullOrEmpty(instructionsString) ||
            instructionsString.Length > 99 ||
            instructionsString.Any(i => !Enum.TryParse<CommandEnum>(i.ToString(), out _)))
            {
                throw new FormatException("Instructions string format is incorrect. Example: \"RFRFRFRF\" ");
            }
        }

        #endregion

        #region Value Validations

        public void ValidateCoordinates(params int[] coordinates)
        {
            foreach (var coordinate in coordinates)
            {
                if (coordinate > 50 || coordinate < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        $"Coordinate value must be positive and less than 50. Provided value: {coordinate}");
                }
            }
        }

        #endregion
    }
}
