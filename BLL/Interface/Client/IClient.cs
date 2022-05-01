using BLL.Entity;
using BLL.ConnectionInterface;

namespace BLL.Interface
{
    public interface IClient : IIdable, IAccountable, IManyOrderConnection
    {
        string Name { get; set; }
        string Surname { get; set; }

        void AddOrder(Order order);
        public bool RemoveOrder(Order order);
        public bool RemoveOrder(int orderId);
    }
}
