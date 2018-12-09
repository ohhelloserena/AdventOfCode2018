using System;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day02
    {
        private string[] input = System.IO.File.ReadAllLines(@"/Users/serenachen/RiderProjects/AdventOfCode2018/AdventOfCode2018/day02input");

        public void Run()
        {
            PartOne();
            PartTwo();
        }

        private void PartOne()
        {
            int twoLetters = 0;
            int threeLetters = 0;

            foreach (var i in input)
            {
                bool hasFoundTwo = false;
                bool hasFoundThree = false;

                var charCount = i.GroupBy(x => x)
                    .Select(x => new
                    {
                        Character = x.Key,
                        Count = x.Count()
                    });

                foreach (var item in charCount)
                {
                    if (item.Count == 2 && hasFoundTwo == false)
                    {
                        twoLetters += 1;
                        hasFoundTwo = true;
                    } 
                    else if (item.Count == 3 && hasFoundThree == false)
                    {
                        threeLetters += 1;
                        hasFoundThree = true;
                    }
                }
            }
            Console.WriteLine("Day 2, Part 2: " + (twoLetters * threeLetters));
        }

        private void PartTwo()
        {
            
        }
    }
}