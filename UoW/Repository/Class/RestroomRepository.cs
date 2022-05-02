using Microsoft.EntityFrameworkCore;
using BLL.Entity;

namespace UoW.Repository.Class
{
    public class RestroomRepository : GenericRepository<Restroom>
    {
        public RestroomRepository(DbContext dbContext) : base(dbContext) { }
    }
}
