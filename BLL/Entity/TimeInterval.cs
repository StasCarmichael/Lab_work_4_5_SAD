using System;
using BLL.Interface;

namespace BLL.Entity
{
    public class TimeInterval : ITimeInterval
    {
        public TimeInterval(int year, int month, int day, int sinceWhen, int toWhen)
        {
            SinceWhen = sinceWhen;
            ToWhen = toWhen;

            Date = new DateTime(year, month, day);
        }

        public int SinceWhen { get; private set; }
        public int ToWhen { get; private set; }
        public DateTime Date { get; private set; }
    }
}
