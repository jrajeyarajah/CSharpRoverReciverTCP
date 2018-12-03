using System;

namespace NasaRover
{
    public enum Direction { N, E, S, W };
    public class RoverUnit
    {
        private Direction facing;
        private int x;
        private int y;
        private Plateau playground; // to keep track of where other Rovers are and to know the edge of Plateau
        //private string _Name;

        public RoverUnit(Plateau p, string x, string y, string face)
        {
            playground = p;
           // _Name = Name;
            PositionRover(x, y, face);
        }

        public string RoverStatus() //Provide current location and direction Rover is facing
        {
            return $" at {x} {y} {facing}";
        }

        public void PositionRover(string x, string y, string face) //Positions the Rover on the Plateau
        {
            if (!int.TryParse(x, out int intX))
            {
                throw (new ArgumentOutOfRangeException("x", x, "has to be a numeric value."));
            }
            else if (!int.TryParse(y, out int intY))
            {
                throw (new ArgumentOutOfRangeException("y", y, "has to be a numeric value."));
            }
            else if (!Enum.IsDefined(typeof(Direction), face.ToUpper()))
            {
                throw (new ArgumentOutOfRangeException("Direction", face, "has to be 'N','E','S' or 'W'."));
            }
            else
            {
                PositionRover(intX, intY, (Direction)Enum.Parse(typeof(Direction), face, true));
            }
        }


        public void PositionRover(int x, int y, Direction face) //Positions the Rover on the Plateau
        {
            if (x < 0 && x > playground.X)
            {
                throw (new ArgumentOutOfRangeException("x", x, $"has to be > 0  and < {playground.X}."));
            }
            else if (y < 0 && y > playground.Y)
            {
                throw (new ArgumentOutOfRangeException("y", y, $"has to be > 0  and < {playground.Y}."));
            }
            else
            {
                this.x = x;
                this.y = y;
                facing = face;
                playground.ooccupied[x, y] = true;
            }
        }

        public void MoveRover(int x, int y, Direction face) //Positions the Rover on the Plateau
        {
            playground.ooccupied[this.x, this.y] = false;
            PositionRover(x, y, face);
            Console.Write(".");

        }


        private void TurnRover(int side) //Turns the Rover Left -1 or Right +1
        {
            facing = (Direction)((((int)facing + side) % 4) < 0 ? 3 : ((int)facing + side) % 4);
            // if less than 0 (-1) it becomes 3
        }

        public void processMessage(char message) //message processing
        {
            switch (message)
            {
                case 'M':
                    switch (facing)
                    {
                        case Direction.N:
                            if (y + 1 <= playground.Y && !playground.ooccupied[x, y + 1])
                            {
                                MoveRover(x, y + 1, Direction.N);
                            }
                            else { Console.WriteLine($"Ignoring instruction edge of plateau or another rover at {x} {y+1}."); }

                            break;
                        case Direction.E:
                            if (x + 1 <= playground.Y && !playground.ooccupied[x + 1, y])
                            {                               
                                MoveRover(x + 1, y, Direction.E);
                            }
                            else { Console.WriteLine($"Ignoring instruction edge of plateau or another rover  at {x+1} {y}."); }
                            break;
                        case Direction.S:
                            if (y - 1 >= 0 && !playground.ooccupied[x, y - 1])
                            {
                                MoveRover(x, y - 1, Direction.S);
                            }
                            else { Console.WriteLine($"Ignoring instruction edge of plateau or another rover at {x} {y - 1}."); }
                            break;
                        case Direction.W:
                            if (x - 1 >= 0 && !playground.ooccupied[x - 1, y])
                            {
                                MoveRover(x - 1, y, Direction.W);
                            }
                            else { Console.WriteLine($"Ignoring instruction edge of plateau or another rover  at {x-1} {y}."); }
                            break;
                    }
                    break;
                case 'L':
                    TurnRover(-1);
                    break;
                case 'R':
                    TurnRover(1);
                    break;
                default:
                    throw (new ArgumentOutOfRangeException("y", y, $"has to be > 0  and < ssss"));
                    break;
            }
        }
    }
}
