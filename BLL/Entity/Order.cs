using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BLL.Interface;

namespace BLL.Entity
{
    public class Order : IOrder
    {

        private Order() { }
        public Order(double orderAmount, DateTime date, int sinceWhen, int toWhen)
        {
            Year = date.Year;
            Month = date.Month;
            Day = date.Day;

            SinceWhen = sinceWhen;
            ToWhen = toWhen;

            OrderAmount = orderAmount;
        }


        [Key]
        public int Id { get; private set; }

        public double OrderAmount { get; private set; }


        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }

        public int SinceWhen { get; private set; }
        public int ToWhen { get; private set; }


        public ITimeInterval GetTimeInterval()
        {
            return new TimeInterval(Year, Month, Day, SinceWhen, ToWhen);
        }


        public int ClientId { get; private set; }
        public Client Client { get; private set; }
    }
}
