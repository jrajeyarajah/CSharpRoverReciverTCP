using System;

namespace NasaRover
{
    public class Plateau
    {
        private int x;
        private int y;
        public bool[,] ooccupied;
        public int X
        {
            get => x;
            set
            {
                if (value > 0)
                {
                    x = value;
                }
                else
                {
                    throw (new ArgumentOutOfRangeException("X", value, "Plateau X can not be less 1"));
                }
            }
        }
        public int Y
        {
            get => y;
            set
            {
                if (value > 0)
                {
                    y = value;
                }
                else
                {
                    throw (new ArgumentOutOfRangeException("Y", value, "Plateau Y can not be less 1"));
                }
            }
        }

        public Plateau(int x, int y)
        {
            X = x;
            Y = y;
            ooccupied = new bool[x+1, y+1];
            Console.WriteLine($"Plateau created {x + 1}x{y + 1}.");
        }

        public void SetObstacle(int X, int y)
        {
            ooccupied[x, y] = true;
        }
        public void Status()
        {
            for (int yy = 0; yy < this.y; yy++)
            {
                for (int xx = 0; xx < this.x; xx++)
                {
                    Console.Write( Convert.ToInt16(ooccupied[xx, yy]));
                }
                Console.WriteLine();
            }
        }
    }

}
