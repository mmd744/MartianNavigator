using MartianNavigator.Enums;
using MartianNavigator.Models;
using System.Collections.Generic;

namespace MartianNavigator.Services
{
    public interface INavigationService
    {
        /// <summary>
        /// Initializes Navigation Service with grid surface. This method must be called before any other method of this service.
        /// </summary>
        public void Initialize(IGridSurface gridSurface);

        /// <summary>
        /// Navigates robot according to instructions and modifies his position.
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="instructions"></param>
        void Navigate(IRobot robot, IEnumerable<CommandEnum> instructions);
    }
}
