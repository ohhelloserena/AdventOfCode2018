using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;

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
            Console.WriteLine("Day 2, Part 1: " + (twoLetters * threeLetters));
        }

        private void PartTwo()
        {
            //string[] input = {"abcde", "fghij", "klmno", "pqrst", "fguij", "axcye", "wvxyz"};
            int index = -1;
            string commonLetters = string.Empty;
            bool foundMatch = false;

            while (!foundMatch)
            {
                index++;
                
                for (int i = 0; i < input.Length; i++) 
                {
                    string outer = input[i].Remove(index, 1);

                    for (int j = i + 1; j < input.Length; j++)
                    {
                        string inner = input[j].Remove(index, 1);

                        if (outer.Equals(inner))
                        {
                            commonLetters = outer;
                            foundMatch = true;
                            break;
                        }
                    }

                    if (foundMatch)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine("Day 2, Part 2: " + commonLetters);
        }
    }
}