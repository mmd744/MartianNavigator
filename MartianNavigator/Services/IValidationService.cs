using System;
using System.Collections.Generic;

namespace MartianNavigator.Services
{
    public interface IValidationService
    {
        /// <summary>
        /// Checks if user input for right upper bound of grid is in required format.
        /// </summary>
        /// <param name="rightUpperBoundString"></param>
        void ValidateRightUpperBoundStringFormat(string rightUpperBoundString);

        /// <summary>
        /// Checks if user input for initial position and orientation of robot is in required format.
        /// </summary>
        /// <param name="positionAndOrientationString"></param>
        void ValidatePositionAndOrientationStringFormat(string positionAndOrientationString);

        /// <summary>
        /// Checks if user input for robot's instructions is in correct format.
        /// </summary>
        /// <param name="instructionsString"></param>
        void ValidateInstructionsStringFormat(string instructionsString);


        /// <summary>
        /// Checks if coordinates are not negative and greater than 50.
        /// </summary>
        /// <param name="coordinates"></param>
        void ValidateCoordinates(params int[] coordinates);
    }
}
