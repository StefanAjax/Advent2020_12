using System.Drawing;

namespace Advent2020_12
{
    internal class Program
    {
        static bool DebugMode = false;
        static void Main(string[] args)
        {
            Ship ship = new Ship(0, 0, Direction.East);
        }


        static void DebugLog(string message)
        {
            if (DebugMode)
            {
                Console.WriteLine(message);
            }
        }

        class Ship
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Direction Direction { get; set; }

            public Ship(int x, int y, Direction direction)
            {
                X = x;
                Y = y;
                Direction = direction;
            }

            public void performInstruction(char type, int arg)
            {

            }
            
        }

        public record Instruction(char Type, int Value);

        enum Direction
        {
            East,
            North,
            South,
            West,
        }
    }
}
