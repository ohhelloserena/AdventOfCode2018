using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day06
    {
        // Day 6 problem: https://adventofcode.com/2018/day/6
        
        private string[] input = System.IO.File.ReadAllLines(@"/Users/serenachen/RiderProjects/AdventOfCode2018/AdventOfCode2018/day06input");

//        private string[] input = new string[]
//        {
//            "1, 1",
//            "1, 6",
//            "8, 3",
//            "3, 4",
//            "5, 5",
//            "8, 9"
//        };
        
        public void Run()
        {
            PartOne();
        }

        private void PartOne()
        {
            int[] maxCoordinates = GetMaxCoordinates();

            int[,] grid = new int[maxCoordinates[0]+1, maxCoordinates[1]+1];

            //Fill in the grid with the input index that yields the smallest Manhattan distance from the current grid element
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    var coordinates = new int[2];
                    var distanceDictionary = new Dictionary<int, int>();
                    
                    for (int x = 0; x < input.Length; x++)
                    {
                        coordinates = ParseCoordinates(input[x]);
                        distanceDictionary[x] = Math.Abs(coordinates[1] - j) + Math.Abs(coordinates[0] - i);
                    }

                    int minVal = distanceDictionary.Values.Min();
                    int minKey = distanceDictionary.First(x => x.Value == minVal).Key;

                    distanceDictionary.Remove(minKey);

                    grid[i, j] = distanceDictionary.Values.Min() == minVal ? -1 : minKey;
                }
            }
            
            //Find the input indices that border the edge of the grid
            var badInputIndices = new HashSet<int>();
            
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                badInputIndices.Add(grid[i,0]);
                badInputIndices.Add(grid[i, maxCoordinates[1]]);
            }

            for (int j = 0; j < grid.GetLength(1); j++)
            {
                badInputIndices.Add(grid[0,j]);
                badInputIndices.Add(grid[maxCoordinates[0], j]);
            }
            
            //Count occurrences of input indices in grid
            Dictionary<int, int> frequency = new Dictionary<int, int>();
           
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    int inputIndex = grid[i, j];
                    
                    if (frequency.ContainsKey(inputIndex))
                    {
                        int currVal = frequency[inputIndex];
                        frequency[inputIndex] = currVal + 1;
                    }
                    else
                    {
                        frequency.Add(inputIndex, 1);
                    }
                }
            }
            

            bool isFound = false;

            while (!isFound)
            {
                var keyOfMaxValue = frequency.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                
                if (!badInputIndices.Contains(keyOfMaxValue))
                {
                    isFound = true;
                    Console.WriteLine("Day 6, Part 1: " + frequency.Values.Max());
                }
                else
                {
                    frequency.Remove(keyOfMaxValue);
                }
            }
        }

        //Get max coordinates for the grid
        private int[] GetMaxCoordinates()
        {
            int maxX = 0;
            int maxY = 0;

            foreach (string i in input)
            {
                int[] coordinates = ParseCoordinates(i);
                

                if (coordinates[0] > maxX)
                {
                    maxX = coordinates[0];
                }

                if (coordinates[1] > maxY)
                {
                    maxY = coordinates[1];
                }
            }
            
            return new int[] {maxX, maxY};
        }

        //Return int coordinates given a string[] input element
        private int[] ParseCoordinates(string str)
        {
            string[] temp = str.Split(new[] {", "}, StringSplitOptions.None);
            int tempX = Convert.ToInt32(temp[0]);
            int tempY = Convert.ToInt32(temp[1]);

            return new int[] {tempX, tempY};
        }
    }
}