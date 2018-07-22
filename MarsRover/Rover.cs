using System;

namespace MarsRover
{
    public class Rover
    {
        #region Variables
        public Position Position { get { return _position; } }
        Position _position { get; set; } = new Position(0, 0, Direction.N);

        Canvas _canvas { get; set; }

        #endregion

        #region Constructor
        public Rover(Canvas canvas) { _canvas = canvas; }
        #endregion

        #region Actions
        public Rover Explore(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            foreach (var p in path.ToUpper())
            {
                if (p.ToString().Equals("M"))
                {
                    Move();
                }
                else if (Enum.TryParse<Spin>(p.ToString(), true, out Spin s))
                {
                    SpinTo(s);
                }
                else
                {
                    throw new MyCustomException("Invalid Path");
                }
            }

            return this;
        }

        public void DropAt(string position)
        {
            if (string.IsNullOrWhiteSpace(position))
            {
                throw new MyCustomException("Null position");
            }

            var p = new Position();

            var positions = position.Split(' ');

            if (int.TryParse(positions[0], out int x))
            {
                p.X = x;
            }
            else
            {
                throw new MyCustomException("Invalid X");
            }

            if (int.TryParse(positions[1], out int y))
            {
                p.Y = y;
            }
            else
            {
                throw new MyCustomException("Invalid Y");
            }

            if (Enum.TryParse(positions[2], true, out Direction d))
            {
                p.Direction = d;
            }
            else
            {
                throw new MyCustomException("Invalid Direction");
            }

            this._position = p;
        }

        #endregion

        #region Action Helpers
        private Rover Move()
        {
            switch (this._position.Direction)
            {
                case Direction.N:
                    this._position.Y++;
                    break;
                case Direction.E:
                    this._position.X++;
                    break;
                case Direction.W:
                    this._position.X--;
                    break;
                case Direction.S:
                    this._position.Y--;
                    break;
                default:
                    break;
            };

            // Check if Rover goes out of bounds
            if (
                this._position.X < 0 ||
                this._position.Y < 0 ||
                this._position.X > this._canvas.X ||
                this._position.Y > this._canvas.Y)
            {
                throw new OutOfBoundException("Rover fell over the ledge");
            }

            return this;
        }

        private Rover SpinTo(Spin to)
        {
            if (to.Equals(Spin.L))
            {
                _position.Direction = _position.Direction.OnLeft();
            }
            else if (to.Equals(Spin.R))
            {
                _position.Direction = _position.Direction.OnRight();
            }

            return this;
        }
        #endregion
    }

    public class Position
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public Direction Direction { get; set; } = Direction.N;

        public Position() { }

        public Position(int x, int y, Direction d)
        {
            this.X = x;
            this.Y = y;
            this.Direction = d;
        }

        public override string ToString()
        {
            return $"{X} {Y} {Direction}";
        }
    }

    // Class for canvas cordinates
    public class Canvas
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public Canvas() { }

        public Canvas(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Canvas(string xy)
        {
            if (string.IsNullOrWhiteSpace(xy))
            {
                throw new MyCustomException("Null - xy");
            }

            var upRight = xy.Split(' ');

            if (int.TryParse(upRight[0], out int x))
            {
                this.X = x;
            }
            else
            {
                throw new MyCustomException("Invalid X");
            }

            if (int.TryParse(upRight[1], out int y))
            {
                this.Y = y;
            }
            else
            {
                throw new MyCustomException("Invalid Y");
            }
        }
    }

    public class MyCustomException : Exception
    {
        public MyCustomException(string errorMessage) : base(errorMessage)
        { }
    }

    public class OutOfBoundException : Exception
    {
        public OutOfBoundException(string errorMessage) : base(errorMessage)
        { }
    }


    // ENUM for Directions
    public enum Direction
    {
        N, W, S, E
    }

    // ENUM for SPIN values
    public enum Spin
    {
        L, R
    }

    public static class Helper
    {
        // Get which direction is on left side
        public static Direction OnLeft(this Direction d)
        {
            if (d.Equals(Direction.N))      // If facing North
            {
                return Direction.W;         // Then left is West 
            }
            else if (d.Equals(Direction.W)) // If facing West
            {
                return Direction.S;         // Then left is South 
            }
            else if (d.Equals(Direction.S)) // If facing South
            {
                return Direction.E;         // Then left is East 
            }

            return Direction.N;             // Else left is North 
        }

        // Get which direction is on right side
        public static Direction OnRight(this Direction d)
        {
            if (d.Equals(Direction.N))      // If facing North
            {
                return Direction.E;         // Then right is East 
            }
            else if (d.Equals(Direction.E)) // If facing East
            {
                return Direction.S;         // Then right is South 
            }
            else if (d.Equals(Direction.S)) // If facing South
            {
                return Direction.W;         // Then right is West 
            }

            return Direction.N;             // Else right is North 
        }
    }
}
