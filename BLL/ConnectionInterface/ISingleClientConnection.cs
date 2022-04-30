using BLL.Entity;

namespace BLL.ConnectionInterface
{
    public interface ISingleClientConnection
    {
        int ClientId { get; }
        Client Client { get; }
    }
}
