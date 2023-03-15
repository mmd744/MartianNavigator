using MartianNavigator.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianNavigator.Models
{
    public class GridSurface
    {
        #region Fields

        private readonly GridPositionStatusEnum[,] surface;

        private readonly int maxX;
        private readonly int maxY;

        #endregion

        #region Constructors

        public GridSurface(int x, int y)
        {
            this.maxX = x;
            this.maxY = y;
            this.surface = new GridPositionStatusEnum[this.maxX + 1, this.maxY + 1];
        }

        #endregion

        #region Methods

        public bool IsOutOfRange(Position position) =>
            position.X > this.maxX || position.X < 0 || 
            position.Y > this.maxY || position.Y < 0;

        public bool IsOccupied(Position position) =>
            this.surface[position.X, position.Y] == GridPositionStatusEnum.Occupied;

        public bool IsScented(Position position) =>
            this.surface[position.X, position.Y] == GridPositionStatusEnum.Scented;

        public void SetPositionStatus(Position position, GridPositionStatusEnum status) =>
            this.surface[position.X, position.Y] = status;

        #endregion
    }
}
