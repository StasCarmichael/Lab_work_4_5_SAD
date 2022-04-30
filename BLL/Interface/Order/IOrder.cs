
using BLL.ConnectionInterface;

namespace BLL.Interface
{
    public interface IOrder : IIdable, IDate, ISingleClientConnection
    {
        double OrderAmount { get; }

        ITimeInterval GetTimeInterval();
    }
}
