using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace day3
{
    class Program
    {
        static string[] template = File.ReadAllLines("input.txt").ToArray();
        static void Main(string[] args)
        {
            Part1();
            Part2();
        }

        static int findTrees(int moveRight, int moveDown)
        {
            const int col = 31;
            var currentMove = 0;
            var trees = 0;

            for (var i = 0; i < template.Length; i += moveDown)
            {
                if (template[i][currentMove] == '#')
                {
                    trees++;
                }

                currentMove = (currentMove + moveRight) % col;
            }

            return trees;
        }

        static void Part1()
        {
            Console.WriteLine($"Number of trees encountered is {findTrees(3, 1)}");
        }

        static void Part2()
        {
            var moves = new List<KeyValuePair<int, int>>()
            {
                new KeyValuePair<int, int>(1, 1),
                new KeyValuePair<int, int>(3, 1),
                new KeyValuePair<int, int>(5, 1),
                new KeyValuePair<int, int>(7, 1),
                new KeyValuePair<int, int>(1, 2),
            };

            var trees = 1;

            foreach (var move in moves)
            {
                trees *= findTrees(move.Key, move.Value);
            }

            System.Console.WriteLine($"Number of trees is {trees}");
        }
    }
}
