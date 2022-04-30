using System.Collections.Generic;
using BLL.Entity;

namespace BLL.ConnectionInterface
{
    public interface IManyOrderConnection
    {
        void AddOrder(Order order);
        public bool RemoveOrder(Order order);
        public bool RemoveOrder(int orderId);


        ICollection<Order> Orders { get; }
    }
}
