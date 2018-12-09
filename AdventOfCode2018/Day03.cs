using System;
using System.Collections.Generic;
using System.Data.Common;

namespace AdventOfCode2018
{
    public class Day03
    {
        // Day 3 problem: https://adventofcode.com/2018/day/3
        
        private string[] input = System.IO.File.ReadAllLines(@"/Users/serenachen/RiderProjects/AdventOfCode2018/AdventOfCode2018/day03input");
        
        public void Run()
        {
            PartOne();
            
        }

        private void PartOne()
        {
            //string[] input = { "#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4", "#3 @ 5,5: 2x2"};
            int[] parsedLine = new int[4];
            int count = 0;
            
            int[][] fabric = new int[1000][];
            for (int i = 0; i < fabric.Length; i++)
            {
                fabric[i] = new int[1000];
            }
            
            // parse x, y, width, height from a line of input, then mark its occupancy
            // in array fabric
            foreach (var item in input)
            {
                parsedLine = ParseLine(item);
 
                for (int col = parsedLine[0]; col < parsedLine[0] + parsedLine[2]; col++)
                {
                    for (int row = parsedLine[1]; row < parsedLine[1] + parsedLine[3]; row++)
                    {
                        fabric[col][row] = fabric[col][row] + 1;
                    }
                }
            }

            // iterate through each square inch of the fabric and see if its occcupancy
            // is greater than one
            for (int col = 0; col < 1000; col++)
            {
                for (int row = 0; row < 1000; row++)
                {
                    if (fabric[col][row] > 1)
                    {
                        count++;
                    }
                }
            }
            
            PartTwo(fabric);
            
            Console.WriteLine("Day 3, Part 1: " + count);
        }

        private void PartTwo(int[][] fabric)
        {
            int line = 1;
            foreach (var item in input)
            {
                int[] parsedLine = ParseLine(item);
                bool isOverlapping = true;

                for (int col = parsedLine[0]; col < parsedLine[0] + parsedLine[2]; col++)
                {
                    for (int row = parsedLine[1]; row < parsedLine[1] + parsedLine[3]; row++)
                    {
                        if (fabric[col][row] > 1)
                        {
                            isOverlapping = false;
                            break;

                        }
                    }
                }

                if (isOverlapping)
                {
                    break;
                }

                line++;
            }
            
            Console.WriteLine("Day 3, Part 2: " + line);
        }

        /**
         * Output:
         * 
         * parsedLine[0]: number of inches between left edge of fabric and left edge of rectangle
         * parsedLine[1]: number of inches between to edge of fabric and top edge of rectangle
         * parsedLine[2]: rectangle width
         * parsedLine[3]: rectangle height
         */
        private int[] ParseLine(string line)
        {
            int[] parsedLine = new int[5];

            string substr = line.Substring(line.IndexOf("@") + 2); //cut off everything to left of @
            int commaIndex = substr.IndexOf(",");
            int colonIndex = substr.IndexOf(":");
            int xIndex = substr.IndexOf("x");
            int spaceIndex = substr.IndexOf(" ");

            parsedLine[0] = Convert.ToInt32(substr.Substring(0, commaIndex));  
            parsedLine[1] = Convert.ToInt32(substr.Substring(commaIndex + 1, colonIndex - commaIndex - 1));    
            parsedLine[2] = Convert.ToInt32(substr.Substring(colonIndex + 2, xIndex - spaceIndex - 1));    
            parsedLine[3] = Convert.ToInt32(substr.Substring(xIndex + 1));
         
            return parsedLine;
        }
    }
}