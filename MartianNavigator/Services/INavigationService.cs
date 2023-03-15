using MartianNavigator.Enums;
using MartianNavigator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianNavigator.Services
{
    public interface INavigationService
    {
        /// <summary>
        /// Initializes Navigation Service with grid surface. This method must be called before any other method of this service.
        /// </summary>
        public void Initialize(int maxX, int maxY);

        /// <summary>
        /// Navigates robot according to instructions and modifies his position.
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="instructions"></param>
        void Navigate(Robot robot, IEnumerable<CommandEnum> instructions);
    }
}
