using BLL.Entity; 

namespace BLL.ConnectionInterface
{
    public interface ISingleAnticafeConnection
    {
        int AnticafeId { get; }
        Anticafe Anticafe { get; }
    }
}
