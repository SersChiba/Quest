using System;
using System.Drawing;

namespace Quest
{
    abstract class Mover
    {
        private const int MoveInterval = 10;
        protected Point location;
        public Point Location { get { return location; } }
        protected Game game;

        public Mover(Game game, Point location)
        {
            this.game = game;
            this.location = location;
        }
        public bool Nearby(Point locationToCheck, int distance)
        {
            if (Math.Abs(location.X - locationToCheck.X) < distance &&
                (Math.Abs(location.Y - locationToCheck.Y) < distance))
                return true;
            else return false;
        }
        public Point Move(Direction moveDirection, Rectangle boundaries)
        {
            Point nextLocation = location;
            switch (moveDirection)
            {
                case Direction.Up:
                    if (nextLocation.Y - MoveInterval >= boundaries.Top)
                        nextLocation.Y -= MoveInterval;
                    break;
                case Direction.Down:
                    if (nextLocation.Y + MoveInterval <= boundaries.Bottom)
                        nextLocation.Y += MoveInterval;
                    break;
                case Direction.Left:
                    if (nextLocation.X - MoveInterval >= boundaries.Left)
                        nextLocation.X -= MoveInterval;
                    break;
                case Direction.Right:
                    if (nextLocation.X + MoveInterval <= boundaries.Right)
                        nextLocation.X += MoveInterval;
                    break;
                default:
                    break;
            }
            return nextLocation;
        }
    }
    public enum Direction { Up, Down, Left, Right }
}
