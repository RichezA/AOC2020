using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var template = File.ReadAllText("input.txt").Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            // Part1(template);
            Part2(template);
        }

        static void Part1(string[] groups)
        {
            var answer = 0;
            foreach (var group in groups)
            {
                var people = group.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                answer += new HashSet<char>(GetQuestions(people)).Count;
            }

            System.Console.WriteLine($"Part1: Sum of singlely answered questions is: {answer}");
        }

        static void Part2(string[] groups)
        {
            var answer = 0;
            foreach (var group in groups)
            {
                // 6 for last
                var people = group.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                var groupQuestions = GetQuestions(people);
                var singleQuestions = new HashSet<char>(groupQuestions);
                var count = 0;
                foreach (var singleQuestion in singleQuestions)
                {
                    if (groupQuestions.Where(q => q == singleQuestion).Count() == people.Count())
                    {
                        count++;
                    }
                }

                answer += count;
            }

            System.Console.WriteLine($"Part2: Sum of questions answered by all the members of a group is: {answer}");
        }

        static IEnumerable<char> GetQuestions(string[] people)
        {
            var characters = new List<char>();
            foreach (var questions in people)
            {
                // questions answered by one person in a group
                foreach (var question in questions)
                {
                    characters.Add(question);
                }
            }

            return characters;
        }
    }
}
