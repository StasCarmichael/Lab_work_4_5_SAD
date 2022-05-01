using System;
using System.Collections.Generic;

using BLL.Entity;

using BLL.ConnectionInterface;

namespace BLL.Interface
{
    public interface IRestroom : IIdable, IManyOrderConnection
    {
        string TypeRecreation { get; }

        int WorkOut { get; }
        int WorkUp { get; }


        double PricePerHour { get; }
        bool IsReserve(DateTime date);


        ICollection<ITimeInterval> GetSchedule();
        ICollection<ITimeInterval> GetSchedule(DateTime date);


        bool ReserveRestroom(IClient client, DateTime dateTime, int workOut, int workUp);
        bool UnreserveRestroom(Order order);
        bool UnreserveRestroom(int orderId);
    }
}
