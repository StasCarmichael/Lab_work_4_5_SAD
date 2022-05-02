using System;
using Microsoft.EntityFrameworkCore;
using UoW.Repository.Class;

namespace UoW.UnitWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;

        private AnticafeRepository anticafes = null;
        private RestroomRepository restrooms = null;
        private ClientRepository clients = null;
        private OrderRepository orders = null;


        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public AnticafeRepository Anticafes
        {
            get
            {
                if (anticafes == null)
                    anticafes = new AnticafeRepository(dbContext);
                return anticafes;
            }
        }
        public RestroomRepository Restrooms
        {
            get
            {
                if (restrooms == null)
                    restrooms = new RestroomRepository(dbContext);
                return restrooms;
            }
        }
        public ClientRepository Clients
        {
            get
            {
                if (clients == null)
                    clients = new ClientRepository(dbContext);
                return clients;
            }
        }
        public OrderRepository Orders
        {
            get
            {
                if (orders == null)
                    orders = new OrderRepository(dbContext);
                return orders;
            }
        }


        public void Save() { dbContext.SaveChanges(); }


        #region IDisposeble

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    dbContext.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
