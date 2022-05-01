using System.Collections.Generic;
using BLL.Entity;

namespace BLL.ConnectionInterface
{
    public interface IManyOrderConnection
    {
        ICollection<Order> Orders { get; }
    }
}
