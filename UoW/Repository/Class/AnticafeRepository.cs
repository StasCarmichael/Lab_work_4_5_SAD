using System.Linq;

using Microsoft.EntityFrameworkCore;
using BLL.Entity;
using UoW.Repository.Interface;
using BLL.Interface;

namespace UoW.Repository.Class
{
    public class AnticafeRepository : GenericRepository<Anticafe>, IAnticafeRepository
    {
        public IAnticafe GetByName(string name)
        {
            return Get().Where(val => val.Name == name).FirstOrDefault();
        }


        public AnticafeRepository(DbContext dbContext) : base(dbContext) { }
    }
}
