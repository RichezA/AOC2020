using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day7
{
    class Program
    {
        static Dictionary<string, string> bags = File.ReadAllLines("input.txt")
                               .Select(x => x.Trim(new char[] { ' ', '.' }).Replace(" bags", string.Empty).Replace(" bag", string.Empty))
                               .ToDictionary(x => x.Split(" contain ")[0], x => x.Split(" contain ")[1]);

        static void Main(string[] args)
        {
            System.Console.WriteLine($"Part1: Number of bags that can contain a shiny gold bag is {Part1()}");
            System.Console.WriteLine($"Part2: Number of total bags contained in a single shiny bag is {Part2()}");
        }

        static int Part1()
        {
            var count = 0;
            foreach (var key in bags.Keys)
            {
                if (Validate(key))
                {
                    count++;
                }
            }

            return count;
        }

        static int Part2(string bagColor = "shiny gold")
        {
            var count = 0;
            foreach (var subContent in bags[bagColor].Split(", "))
            {
                if (subContent != "no other" && int.TryParse(subContent.Substring(0, 1), out var times))
                {
                    count += times + (times * Part2(subContent.Substring(2)));
                }
                else
                    break;
            }

            return count;
        }

        static bool Validate(string value)
        {
            if (bags[value].Contains("shiny gold"))
            {
                return true;
            }
            else
            {
                foreach (var subContent in bags[value].Split(", "))
                {
                    if (subContent != "no other")
                    {
                        if (!Validate(subContent.Substring(2)))
                        {
                            continue;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

    }
}
