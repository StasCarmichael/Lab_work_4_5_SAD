using Microsoft.EntityFrameworkCore;
using BLL.Entity;

namespace UoW.Repository.Class
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(DbContext dbContext) : base(dbContext) { }
    }
}
