using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace day4
{
    class Program
    {
        private static readonly string EMPTY_LINE = "\r\n\r\n";
        static void Main(string[] args)
        {
            //Part1();
            Part2();
        }

        static void Part1()
        {
            var template = File.ReadAllText("input.txt").Split(new string[] { EMPTY_LINE }, StringSplitOptions.RemoveEmptyEntries);
            var keywords = new List<string>()
            {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid",
            };
            var count = 0;

            foreach (var line in template)
            {
                var valid = true;
                foreach (var keyword in keywords)
                {
                    if (!line.Contains(keyword))
                    {
                        valid = false;
                    }
                }

                if (valid)
                {
                    count++;
                }
            }

            System.Console.WriteLine($"Part1: Number of valid passports: {count}");
        }


        static bool ValidateBetweenToIntegers(string value, int least, int most)
            => int.TryParse(value, out var parsed) && parsed >= least && parsed <= most;

        static bool ValidateHeight(string value)
        {
            int size = 0;
            return (
                value.Contains("cm") &&
                int.TryParse(value.Replace("cm", string.Empty), out size) &&
                size >= 150 &&
                size <= 193
            ) || (
                value.Contains("in") &&
                int.TryParse(value.Replace("in", string.Empty), out size) &&
                size >= 59 &&
                size <= 76
            );
        }

        static bool ValidateEyeColor(string value)
        {
            var colors = new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            return value.Length == 3 && colors.Contains(value);
        }

        static void Part2()
        {
            var template = File.ReadAllText("day4.txt")
                  .Replace(Environment.NewLine, "|")
                  .Replace("||", "-")
                  .Replace("|", ";")
                  .Replace(" ", ";")
                  .Split("-");

            var count = 0;
            foreach (var line in template)
            {
                var fields = line.Split(";");
                if (fields.Count() == 8 || (fields.Count() == 7 && !line.Contains("cid")))
                {
                    var isValid = false;
                    foreach (var field in fields)
                    {
                        var attr = field.Split(":");
                        var key = attr[0];
                        var val = attr[1];
                        switch (key)
                        {
                            case "byr":
                                isValid = val.Length == 4 && ValidateBetweenToIntegers(val, 1920, 2002);
                                break;
                            case "iyr":
                                isValid = val.Length == 4 && ValidateBetweenToIntegers(val, 2010, 2020);
                                break;
                            case "eyr":
                                isValid = val.Length == 4 && ValidateBetweenToIntegers(val, 2020, 2030);
                                break;
                            case "hgt":
                                isValid = ValidateHeight(val);
                                break;
                            case "hcl":
                                isValid = val.Length == 7 && Regex.IsMatch(val, "^#[0-9a-f]{6}$");
                                break;
                            case "ecl":
                                isValid = ValidateEyeColor(val);
                                break;
                            case "pid":
                                isValid = Regex.IsMatch(val, "^\\d{9}$");
                                break;
                            case "cid":
                                isValid = true;
                                break;
                            default:
                                break;
                        }
                        if (!isValid)
                        {
                            break;
                        }
                    }

                    if (isValid)
                    {
                        count++;
                    }
                }
            }

            System.Console.WriteLine($"Part2: Number of valid passports: {count}");
        }
    }
}
