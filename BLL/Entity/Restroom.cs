using System;
using System.Collections.Generic;
using System.Linq;

using BLL.Interface;

namespace BLL.Entity
{
    public class Restroom : IRestroom
    {
        private static int MAX_DAY = 14;
        private ICollection<Order> orders;

        public int Id { get; private set; }


        public Restroom(string typeRecreation, double pricePerHour, int workOut, int workUp)
        {
            TypeRecreation = typeRecreation;
            PricePerHour = pricePerHour;
            if (workOut < 0 || workUp > 23 || workOut >= workUp)
                throw new ArgumentException("WorkOut and WorkUp wrong data");

            WorkOut = workOut;
            WorkUp = workUp;

            orders = new LinkedList<Order>();
        }


        public string TypeRecreation { get; private set; }

        public int WorkOut { get; private set; }
        public int WorkUp { get; private set; }

        public double PricePerHour { get; set; }


        public ICollection<ITimeInterval> GetSchedule()
        {
            return Orders.Select(val => val.GetTimeInterval()).ToList();
        }
        public ICollection<ITimeInterval> GetSchedule(DateTime date)
        {
            var allSchedule = GetSchedule();
            return allSchedule.Where(val => val.Date.Equals(date)).ToList();
        }


        public bool IsReserve(DateTime date)
        {
            var schedule = GetSchedule(date);

            List<int> timeList = new List<int>();
            for (int time = WorkOut; time < WorkUp; time++)
                timeList.Add(time);


            foreach (var timeInterval in schedule)
                for (int time = timeInterval.SinceWhen; time < timeInterval.ToWhen; time++)
                    timeList.Remove(time);

            if (timeList.Count == 0)
                return true;
            else
                return false;
        }
        private List<int> GetFreeTime(DateTime date)
        {
            var schedule = GetSchedule(date);

            List<int> timeList = new List<int>();
            for (int time = WorkOut; time < WorkUp; time++)
                timeList.Add(time);


            foreach (var timeInterval in schedule)
                for (int time = timeInterval.SinceWhen; time < timeInterval.ToWhen; time++)
                    timeList.Remove(time);

            return timeList;
        }

        public bool ReserveRestroom(IClient client, DateTime dateTime, int workOut, int workUp)
        {
            if ((dateTime - DateTime.Now).TotalDays <= MAX_DAY && workOut >= WorkOut && workUp <= WorkUp && workOut < workUp)
            {
                var schedule = GetSchedule(dateTime);
                if (schedule != null && schedule.Count() >= 0)
                {
                    var freeTime = GetFreeTime(dateTime);

                    for (int time = workOut; time < workUp; time++)
                    {
                        var res = freeTime.Contains(time);

                        if (res == false)
                            return false;
                    }

                    var dataClient = client as Client;

                    var price = (workUp - workOut) * PricePerHour;
                    Order order = new Order(price, TypeRecreation, dateTime, workOut, workUp);

                    orders.Add(order);
                    client.AddOrder(order);

                    return true;
                }
            }

            return false;
        }

        public bool UnreserveRestroom(Order order)
        {
            order.Client.RemoveOrder(order);
            var result = Orders.Remove(order);
            return result;
        }
        public bool UnreserveRestroom(int orderId)
        {
            var order = Orders.ToList().Where(val => val.Id == orderId).FirstOrDefault();

            if (order == null)
                return false;

            order.Client.RemoveOrder(order);
            var result = Orders.Remove(order);
            return result;
        }


        public ICollection<Order> Orders
        {
            get
            {
                var list = new List<Order>();
                foreach (var item in orders)
                    list.Add(item);

                return list;
            }
            private set
            {
                orders = value;
            }
        }
    }
}
