using System.Collections.Generic;

namespace TVStation.Services
{
    public static class OccurencesUtils
    {
        public static Dictionary<string, DayOfWeek> DaysOfWeekMap = new Dictionary<string, DayOfWeek>(StringComparer.OrdinalIgnoreCase)
        {
            {"Sunday", DayOfWeek.Sunday},
            {"Monday", DayOfWeek.Monday},
            {"Tuesday", DayOfWeek.Tuesday},
            {"Wednesday", DayOfWeek.Wednesday},
            {"Thursday", DayOfWeek.Thursday},
            {"Friday", DayOfWeek.Friday},
            {"Saturday", DayOfWeek.Saturday}
        };


        public static List<DayOfWeek> ParseFrequency(this string frequency)
        {
            return frequency.Split(',')
                .Where(day => DaysOfWeekMap.ContainsKey(day.Trim()))
                .Select(day => DaysOfWeekMap[day.Trim()])
                .ToList();
        }

        public static IEnumerable<DateTime> GetOccurrences(DateTime start, string daysOfWeek, int episodeCount)
        {
            return GetOccurrences(start, daysOfWeek.ParseFrequency(), episodeCount);
        }


        public static IEnumerable<DateTime> GetOccurrences(DateTime start, List<DayOfWeek> daysOfWeek, int episodeCount)
        {
            if (daysOfWeek == null || daysOfWeek.Count == 0 || episodeCount <= 0)
                yield break;

            daysOfWeek = daysOfWeek.OrderBy(d => d).ToList();

            int weeksAdded = 0;
            int foundCount = 0;

            while (foundCount < episodeCount)
            {
                foreach (var day in daysOfWeek)
                {
                    int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
                    daysToAdd += weeksAdded * 7;

                    var nextDate = start.AddDays(daysToAdd);

                    if (nextDate >= start)
                    {
                        yield return nextDate;
                        foundCount++;

                        if (foundCount >= episodeCount)
                            yield break;
                    }
                }
                weeksAdded++;
            }
        }
    }
}
