using System;
using UoW.Repository.Class;

namespace UoW.UnitWork
{
    public interface IUnitOfWork : IDisposable
    {
        AnticafeRepository Anticafes { get; }
        RestroomRepository Restrooms { get; }
        ClientRepository Clients { get; }
        OrderRepository Orders { get; }


        void Save();
    }
}
