using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day8
{
    class Program
    {
        static int acc = 0;
        static List<int> done = new List<int>();

        static Dictionary<int, string> instructions = new Dictionary<int, string>();

        static bool isFinished = false;

        static void Main(string[] args)
        {
            var template = File.ReadAllLines("test.txt");
            foreach (var item in template.Select((value, index) => (value, index)))
            {
                instructions.Add(item.index, item.value);
            }

            System.Console.WriteLine($"Part1: Acc number is : {Part1()}");
            System.Console.WriteLine($"Part2: Acc number is : {Part2()}");
        }

        static int Part1()
        {
            if (instructions.Any())
            {
                ProcessInstruction(instructions.First());
            }

            return acc;
        }

        static int Part2()
        {
            isFinished = false;
            acc = 0;
            foreach (var item in instructions)
            {
                if (isFinished)
                {
                    break;
                }

                var copy = instructions.Clone();

                switch (copy[item.Key].Split()[0])
                {
                    case "nop":
                        copy[item.Key] = copy[item.Key].Replace("nop", "jmp");
                        ProcessInstruction(copy.First());
                        break;
                    case "jmp":
                        copy[item.Key] = copy[item.Key].Replace("jmp", "nop");
                        ProcessInstruction(copy.First());
                        break;
                }
            }

            return acc;
        }
        static void ProcessInstruction(KeyValuePair<int, string> instruction)
        {
            if (instruction.Key == instructions.Count - 1 || done.Contains(instruction.Key) || isFinished)
            {
                isFinished = true;
                return;
            }

            var splitted = instruction.Value.Split();
            var key = splitted[0];
            var value = splitted[1];
            var next = 1;
            switch (key)
            {
                case "acc":
                    acc += int.Parse(value);
                    break;
                case "nop":
                    break;
                case "jmp":
                    int.TryParse(value, out next);
                    break;

            }

            done.Add(instruction.Key);
            ProcessInstruction(instructions.ElementAt(instruction.Key + next));
        }
    }

    static class DictionaryHelpers
    {
        public static Dictionary<int, string> Clone(this Dictionary<int, string> origin)
        {
            var clone = new Dictionary<int, string>(origin.Count, origin.Comparer);

            foreach (var item in origin)
            {
                clone.Add(item.Key, (string)item.Value.Clone());
            }

            return clone;
        }
    }
}
