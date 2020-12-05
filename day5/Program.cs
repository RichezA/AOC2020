using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day5
{
    class Program
    {
        private const int LINE_LENGTH = 10;

        private const int ROW_KEY_LENGTH = 7;

        private const int FIRST_ROW = 0;

        private const int LAST_ROW = 127;

        private const int FIRST_COL = 0;

        private const int LAST_COL = 7;

        static void Main(string[] args)
        {
            var template = File.ReadAllLines("input.txt");
            //Part1(template);
            Part2(template);
        }

        static void Part1(string[] template)
        {
            var highest = 0;
            foreach (var line in template)
            {
                var seatId = GetSeatId(line);
                if (seatId > highest)
                {
                    highest = seatId;
                }
            }
            System.Console.WriteLine($"Part1: Highest is {highest}");
        }

        static void Part2(string[] template)
        {
            var seats = new List<int>();
            foreach (var line in template)
            {
                seats.Add(GetSeatId(line));
            }

            seats.Sort();
            var range = new HashSet<int>(Enumerable.Range(seats.Min(), seats.Max()));
            range.ExceptWith(seats);
            System.Console.WriteLine($"Part2: seat id is {range.First()}");
        }

        static int GetSeatId(string line)
        {
            // 7 first characters is the row from 0 to 127
            // 3 last represents the column
            var rowKeys = line.Substring(0, ROW_KEY_LENGTH);
            var colKeys = line.Substring(ROW_KEY_LENGTH, line.Length - ROW_KEY_LENGTH);
            var row = CalculatePosition(rowKeys, FIRST_ROW, LAST_ROW);
            var col = CalculatePosition(colKeys, FIRST_COL, LAST_COL);
            var seatId = CalculateSeatId(row, col);
            return seatId;
        }

        static int CalculatePosition(string positions, int lower, int upper)
        {
            foreach (var character in positions)
            {
                (lower, upper) = CalculatePosition(character, lower, upper);
            }

            return upper;
        }

        static (int lower, int upper) CalculatePosition(char pos, int lower, int upper)
        {
            var diff = ((upper - lower) / 2) + lower;
            switch (pos)
            {
                case 'F':
                case 'L':
                    // Takes the lower half
                    return (lower: lower, upper: diff);
                case 'B':
                case 'R':
                    // Takes the upper half
                    return (lower: diff, upper: upper);
                default:
                    return (lower: -1, upper: 0);
            }
        }

        static int CalculateSeatId(int row, int col)
            => (row * 8) + col;
    }
}
