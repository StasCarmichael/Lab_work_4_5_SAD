using BLL.Entity;

namespace BLL.ConnectionInterface
{
    public interface ISingleClientConnection
    {
        int UserId { get; }
        Client Client { get; }
    }
}
