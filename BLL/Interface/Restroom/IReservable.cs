using System;
using BLL.Entity;

namespace BLL.Interface
{
    public interface IReservable
    {
        (bool result, Order order) ReserveSpecialProgramRestroom(IClient client, string namePersonalProgram, DateTime dateTime);


        (bool result, Order order) ReserveRestroom(IClient client, DateTime dateTime, int workOut, int workUp);
        bool UnreserveRestroom(Order order);
        bool UnreserveRestroom(int orderId);
    }
}
