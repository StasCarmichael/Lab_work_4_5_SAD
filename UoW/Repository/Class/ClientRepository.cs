using System.Linq;
using Microsoft.EntityFrameworkCore;
using BLL.Entity;
using UoW.Repository.Interface;
using BLL.Interface;

namespace UoW.Repository.Class
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public IClient GetByName(string name)
        {
            return Get().Where(val => val.Name == name).FirstOrDefault();
        }


        public ClientRepository(DbContext dbContext) : base(dbContext) { }

    }
}
