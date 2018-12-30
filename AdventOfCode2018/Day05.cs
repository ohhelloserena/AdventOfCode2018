using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day05
    {
        // Day 5 problem: https://adventofcode.com/2018/day/5
        
        private string[] input = System.IO.File.ReadAllLines(@"/Users/serenachen/RiderProjects/AdventOfCode2018/AdventOfCode2018/day05input");
        
        public void Run()
        {
            List<char> inputList = new List<char>();
            inputList.AddRange(input[0]);
            //Console.WriteLine("Day 5, Part 1: " + PartOne(inputList).ToString());

            List<char> inputListTwo = new List<char>();
            inputListTwo.AddRange(input[0]);
            Console.WriteLine("Day 5, Part 2: " + PartTwo(inputListTwo).ToString());
            
        }

        private int PartOne(List<char> inputList)
        {
            bool foundMatch = true;

            while (foundMatch)
            {
                foundMatch = false;
                
                for (int i = 0; i < inputList.Count - 1; i++)
                {
                    char unitOne = inputList[i];
                    char unitTwo = inputList[i + 1];
                
                    if ((char.IsUpper(unitOne) && char.IsLower(unitTwo) && char.ToLower(unitOne) == unitTwo) 
                        || (char.IsLower(unitOne) && char.IsUpper(unitTwo) && unitOne == char.ToLower(unitTwo)))
                    {
                        foundMatch = true;
                        inputList.RemoveAt(i);
                        inputList.RemoveAt(i);
                        break;
                    }
                }
            }
            return inputList.Count();
        }

        private int PartTwo(List<char> inputList)
        {
            int shortestPolymerLength = 0;

            for (char c = 'A'; c <= 'Z'; c++)
            {
                List<char> inputListCopy = new List<char>();
                inputListCopy.AddRange(inputList);
                
                inputListCopy.RemoveAll(item => item == c);
                inputListCopy.RemoveAll(item => item == char.ToLower(c));
                
                int currLen = PartOne(inputListCopy);

                if (c == 'A')
                {
                    shortestPolymerLength = currLen;
                }

                if (currLen < shortestPolymerLength)
                {
                    shortestPolymerLength = currLen;
                }
            }
            
            return shortestPolymerLength;
        }
        
    }
}