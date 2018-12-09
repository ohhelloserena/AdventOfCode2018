using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;

namespace AdventOfCode2018
{
    public class Day01
    {
        private string[] input = System.IO.File.ReadAllLines(@"/Users/serenachen/RiderProjects/AdventOfCode2018/AdventOfCode2018/day01input");

        public void Run()
        {
            PartOne();
            PartTwo();
        }

        private void PartOne()
        {
            int sum = 0;

            foreach (string i in input)
            {
                sum += Convert.ToInt32(i);
            }
            
            Console.WriteLine("Day 1, Part 1: " + sum);
        }

        private void PartTwo()
        {
            HashSet<int> frequency = new HashSet<int>();
            int sum = 0;
            bool isRepeated = false;

            while (!isRepeated)
            {
                foreach (var i in input)
                {
                    sum += Convert.ToInt32(i);

                    if (frequency.Contains(sum))
                    {
                        Console.WriteLine("Day 1, Part 2: " + sum);
                        isRepeated = true;
                        break;
                    }
                    else
                    {
                        frequency.Add(sum);
                    }
                }
            }
        }
        
    }
}