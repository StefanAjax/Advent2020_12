using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Advent2020_12
{
    internal class Program
    {
        static bool DebugMode = false;
        static void Main(string[] args)
        {
            var instructions = ParseInputData("trueData.txt");
            var startingCoordinates = new Point(0, 0);
            var startingWayPoint = new Point(10, 1);
            Direction startingDirection = Direction.E;  //Start pointing East
            Ship shipPart1 = new Ship(startingCoordinates, startingWayPoint, startingDirection);
            foreach (var instruction in instructions)
            {
                shipPart1.PerformInstructionPart1(instruction);
            }
            int resultPart1 = shipPart1.CalculateManHattanDistance();
            Console.WriteLine(resultPart1);

            Ship shipPart2 = new Ship(startingCoordinates, startingWayPoint, startingDirection);
            foreach (var instruction in instructions) 
                {
                    shipPart2.PerformInstructionPart2(instruction);
                }
            int resultPart2 = shipPart2.CalculateManHattanDistance();
            Console.WriteLine(resultPart2);

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
            public Point Coord { get; set; }
            public Point WayPoint { get; set; }
            public Direction Direction { get; set; }
            public Ship(Point coord, Point wayPoint, Direction direction)
            {
                Coord = coord;
                Direction = direction;
                WayPoint = wayPoint;
            }
            public int CalculateManHattanDistance()
            {
                return (Math.Abs(Coord.X) + Math.Abs(Coord.Y));
            }
            private readonly static Point[] directionPoints =
            {
                new Point(1,0),     // East
                new Point(0,-1),    // South
                new Point(-1,0),    // West
                new Point(0,1),     // North
            };

            private static Dictionary<char, int> directionNumbers = new Dictionary<char, int>() {
                {'E', 0 },
                {'S', 1 },
                {'W', 2 },
                {'N', 3 },
            };
            public void PerformInstructionPart1(Instruction instruction)
            {
                char type = instruction.Type;
                int value = instruction.Value;

                switch (type)
                {
                    case 'F':
                        Coord = Add(Coord, Scale(directionPoints[(int)Direction], value));
                        break;
                    case 'L':
                        int leftTurns = value / 90;
                        Direction = (Direction)((((int)Direction - leftTurns) % 4 + 4) % 4);
                        break;
                    case 'R':
                        int rightTurns = value / 90;
                        Direction = (Direction)(((int)Direction + rightTurns) % 4);
                        break;
                    default:
                        Coord = Add(Coord, Scale(directionPoints[directionNumbers[type]], value));
                        break;
                }
                DebugLog($"Coord: ({Coord.X},{Coord.Y}) Dir: ({Direction})");
            }

            public void PerformInstructionPart2(Instruction instruction)
            {
                char type = instruction.Type;
                int value = instruction.Value;

                switch (type)
                {
                    case 'F':
                        Coord = Add(Coord, Scale(WayPoint, value));
                        break;
                    case 'L':
                        int leftTurns = value / 90;
                        for (int i = 0; i < leftTurns; i++)
                        {
                            WayPoint = new Point(-WayPoint.Y, WayPoint.X);
                        }
                        break;
                    case 'R':
                        int rightTurns = value / 90;
                        for (int i = 0; i < rightTurns; i++)
                        {
                            WayPoint = new Point(WayPoint.Y, -WayPoint.X);
                        }
                        break;
                    default:
                        WayPoint = Add(WayPoint, Scale(directionPoints[directionNumbers[type]], value));
                        break;
                }
                DebugLog($"Coord: ({Coord.X},{Coord.Y}) WayPoint: {WayPoint.X},{WayPoint.Y}");
            }
            private static Point Add(Point a, Point b) { 
                return new Point(a.X+b.X, a.Y+b.Y);
            }
            private static Point Scale(Point point, int scale)
            {
                return new Point(point.X*scale, point.Y*scale);
            }
        }
        public static List<Instruction> ParseInputData(string path)
        {
            var result = new List<Instruction>();
            string lines = File.ReadAllText(path);
            string pattern = @"(?<type>^\w)(?<value>\d+)";
            Regex regex = new Regex(pattern, RegexOptions.Multiline);
            MatchCollection matches = regex.Matches(lines);
            foreach (Match match in matches)
            {
                char type = char.Parse(match.Groups["type"].Value);
                int value = int.Parse(match.Groups["value"].Value);
                DebugLog(type + " " + value);
                result.Add(new Instruction(type, value));
            }
            return result;
        }
        public readonly struct Instruction(char type, int value)
        {
            public char Type { get; } = type;
            public int Value { get; } = value;
        }
        enum Direction
        {
            E = 0,
            S = 1,
            W = 2,
            N = 3,
        }
    }
}
