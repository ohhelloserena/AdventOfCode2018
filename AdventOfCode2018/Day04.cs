using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day04
    {
        // Day 4 problem: https://adventofcode.com/2018/day/4

        private string[] input =
            System.IO.File.ReadAllLines(
                @"/Users/serenachen/RiderProjects/AdventOfCode2018/AdventOfCode2018/day04input");

        public void Run()
        {
            PartOne();
        }

        private void PartOne()
        {
            Dictionary<string, List<string>> sleepingSchedule = new Dictionary<string, List<string>>();
            Dictionary<int, int> napTimes = new Dictionary<int, int>();

            //Add date as key in dictionary sleepingSchedule
            foreach (string s in input)
            {
                if (s.Contains("Guard"))
                {
                    string startDate = GetDate(s);

                    //If guard starts in hour 23, roll over their start date to next day
                    if (GetHour(s) == 23)
                    {
                        int intMonth = GetMonth(s);
                        int intDay = GetDay(s);

                        if ((intDay == 30 && HasThirtyDays(intMonth)) || intDay == 31)
                        {
                            intMonth++;
                            intDay = 1;
                        }
                        else
                        {
                            intDay++;
                        }

                        string strMonth = intMonth < 10 ? "0" + intMonth.ToString() : intMonth.ToString();
                        string strDay = intDay < 10 ? "0" + intDay.ToString() : intDay.ToString();

                        startDate = strMonth + "-" + strDay;
                    }

                    if (!sleepingSchedule.ContainsKey(startDate))
                    {
                        sleepingSchedule.Add(startDate, new List<string>());
                        sleepingSchedule[startDate].Add(GetGuardId(s).ToString());
                    }
                }
            }

            //Add wake/asleep times to dictionary
            foreach (string s in input)
            {
                if (!s.Contains("Guard"))
                {
                    string date = GetDate(s);
                    sleepingSchedule[date].Add(s);
                    InsertionSort(sleepingSchedule[date]);
                }
            }

            foreach (KeyValuePair<string, List<string>> entry in sleepingSchedule)
            {
                int sleepTime = -1;
                int wakeTime = -1;
                int totalNapTime = 0;
                
                for (int i = 1; i < sleepingSchedule[entry.Key].Count; i++)
                {
                    if (sleepingSchedule[entry.Key][i].Contains("falls asleep"))
                    {
                        sleepTime = GetMinutes(sleepingSchedule[entry.Key][i]);
                    }
                    else if (sleepingSchedule[entry.Key][i].Contains("wakes up"))
                    {
                        wakeTime = GetMinutes(sleepingSchedule[entry.Key][i]);
                    }

                    //Calculate sleep times per day
                    if (wakeTime != -1 && sleepTime != -1)
                    {
                        totalNapTime += (wakeTime - sleepTime);
                        wakeTime = -1;
                        sleepTime = -1;
                    }
                }
                
                //Calculate sleep times per guard Id
                int guardId = Int32.Parse(entry.Value[0]);

                if (napTimes.ContainsKey(guardId))
                {
                    napTimes[guardId] += totalNapTime;
                }
                else
                {
                    napTimes.Add(guardId, totalNapTime);
                }
            }
            
            var maxSleepGuardID = napTimes.FirstOrDefault(x => x.Value == napTimes.Values.Max()).Key;
            List<int> minutesAsleepList = new List<int>();

            foreach (KeyValuePair<string, List<string>> entry in sleepingSchedule)
            {
                int sleepTime = -1;
                int wakeTime = -1;
                
                if (entry.Value[0] == maxSleepGuardID.ToString())
                {
                    for (int i = 1; i < sleepingSchedule[entry.Key].Count; i++)
                    {
                        if (sleepingSchedule[entry.Key][i].Contains("falls asleep"))
                        {
                            sleepTime = GetMinutes(sleepingSchedule[entry.Key][i]);
                        }
                        else if (sleepingSchedule[entry.Key][i].Contains("wakes up"))
                        {
                            wakeTime = GetMinutes(sleepingSchedule[entry.Key][i]);
                        }

                        if (sleepTime != -1 && wakeTime != -1)
                        {
                            while (wakeTime != sleepTime)
                            {
                                minutesAsleepList.Add(sleepTime);
                                sleepTime++;
                            }

                            sleepTime = -1;
                            wakeTime = -1;
                        }
                    }
                }                
            }

            int minAsleepMost = minutesAsleepList.GroupBy(i => i)
                .OrderByDescending(grp => grp.Count())
                .Select(grp => grp.Key).First();
            
            Console.WriteLine("Day 4, Part 1: " + minAsleepMost * maxSleepGuardID);
        }

        private List<string> InsertionSort(List<string> inputList)
        {
            if (inputList.Count < 3)
            {
                return inputList;
            }

            for (int i = 2; i < inputList.Count; i++)
            {
                int j = i;

                while (j > 1)
                {
                    int prevMin = GetMinutes(inputList[j - 1]);
                    int currMin = GetMinutes(inputList[j]);

                    if (prevMin > currMin)
                    {
                        string temp = inputList[j - 1];
                        inputList[j - 1] = inputList[j];
                        inputList[j] = temp;
                        j--;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return inputList;
        }

        private bool HasThirtyDays(int month)
        {
            int[] monthsArray = {9, 4, 6, 11};

            if (monthsArray.Contains(month))
            {
                return true;
            }

            return false;
        }


        private string GetDate(string line)
        {
            return line.Substring(6, 5);
        }

        private int GetMonth(string line)
        {
            return Int32.Parse(line.Substring(6, 2));
        }

        private int GetDay(string line)
        {
            return Int32.Parse(line.Substring(9, 2));
        }

        private int GetHour(string line)
        {
            return Int32.Parse(line.Substring(12, 2));
        }

        private int GetMinutes(string line)
        {
            return Int32.Parse(line.Substring(15, 2));
        }

        private int GetGuardId(string line)
        {
            if (line.Contains("Guard"))
            {
                int start = line.IndexOf("#") + 1;
                int end = line.IndexOf("b");
                int idLength = end - start - 1;

                return Int32.Parse(line.Substring(start, idLength));
            }

            return -1;
        }
    }
}