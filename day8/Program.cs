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

        static void Main(string[] args)
        {
            var template = File.ReadAllLines("input.txt");
            foreach (var item in template.Select((value, index) => (value, index)))
            {
                instructions.Add(item.index, item.value);
            }

            System.Console.WriteLine($"Part1: Acc number is : {Part1()}");
        }

        static int Part1()
        {
            if (instructions.Any())
            {
                ProcessInstruction(instructions.First());
            }
            return acc;
        }

        static void ProcessInstruction(KeyValuePair<int, string> instruction)
        {
            if (done.Contains(instruction.Key))
            {
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
}
