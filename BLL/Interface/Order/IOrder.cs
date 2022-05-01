using BLL.ConnectionInterface;

namespace BLL.Interface
{
    public interface IOrder : IIdable, IDate, ISingleClientConnection, ISingleRestroomConnection
    {
        string TypeRecreation { get; }
        double OrderAmount { get; }

        ITimeInterval GetTimeInterval();
    }
}
