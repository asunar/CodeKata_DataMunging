using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeKata_DataMunging
{
    public class Class1
    {


        [Test]
        public void FindMaxSpread()
        {
            var lines =
                File.ReadAllLines(Environment.CurrentDirectory + @"\" + "weather.dat")
                    .Skip(8)
                    .Where(x => char.IsNumber(x.TrimStart().ToCharArray()[0]));


            var dayMaxSpread = lines.Select(
                                x => new {
                                            Day =x.Substring(1, 3).Trim()  ,
                                            Spread = Convert.ToDecimal(x.Substring(5, 3).Trim()) - Convert.ToDecimal(x.Substring(11, 3).Trim())
                                          }
                                 ).OrderByDescending(x => x.Spread).First();


            Assert.AreEqual(dayMaxSpread.Day, "9");
            Assert.AreEqual(dayMaxSpread.Spread, 54);

        }

        [Test]
        public void FindMinGoalDifference()
        {
            var lines =
                File.ReadAllLines(Environment.CurrentDirectory + @"\" + "football.dat")
                    .Skip(5)
                    .Where(x => char.IsNumber(x.TrimStart().ToCharArray()[0]));

            var minGoalDifference = lines.Select(
                x => new
                    {
                        Team = x.Substring(7, 16).Trim(),
                        GoalDifference =
                        Math.Abs( 
                        Convert.ToInt32(x.Substring(43, 4).Trim()) - Convert.ToInt32(x.Substring(50, 6).Trim()))
                    }
                ).OrderBy(x => x.GoalDifference).First();

           Assert.AreEqual(minGoalDifference.Team, "Aston_Villa");
           Assert.AreEqual(minGoalDifference.GoalDifference, 1);
        }

        private IEnumerable<string> GetLinesToProcess(string fileName, int numberOfLinesToSkip, Func<string, bool> isValidLine)
        {
            var lines =
                File.ReadAllLines(Environment.CurrentDirectory + @"\" + fileName)
                    .Skip(numberOfLinesToSkip)
                    .Where(isValidLine);
            return lines;
        }

        [Test]
        public void FindMinGoalDifference_DRY()
        {
            Func<string, bool> isValidLine = x => char.IsNumber(x.TrimStart().ToCharArray()[0]);
            var lines = GetLinesToProcess("football.dat", 5, isValidLine);

            var minGoalDifference = lines.Select(
                x => new
                {
                    Team = x.Substring(7, 16).Trim(),
                    GoalDifference =
                    Math.Abs(
                    Convert.ToInt32(x.Substring(43, 4).Trim()) - Convert.ToInt32(x.Substring(50, 6).Trim()))
                }
                ).OrderBy(x => x.GoalDifference).First();

            Assert.AreEqual(minGoalDifference.Team, "Aston_Villa");
            Assert.AreEqual(minGoalDifference.GoalDifference, 1);
        }

        [Test]
        public void FindMaxSpread_DRY()
        {
            Func<string, bool> isValidLine = x => char.IsNumber(x.TrimStart().ToCharArray()[0]);
            var lines = GetLinesToProcess("weather.dat", 8, isValidLine);

            var dayMaxSpread = lines.Select(
                                x => new
                                {
                                    Day = x.Substring(1, 3).Trim(),
                                    Spread = Convert.ToDecimal(x.Substring(5, 3).Trim()) - Convert.ToDecimal(x.Substring(11, 3).Trim())
                                }
                                 ).OrderByDescending(x => x.Spread).First();


            Assert.AreEqual(dayMaxSpread.Day, "9");
            Assert.AreEqual(dayMaxSpread.Spread, 54);

        }

    }
}
