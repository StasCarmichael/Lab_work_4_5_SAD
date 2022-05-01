using BLL.Entity;

namespace BLL.ConnectionInterface
{
    public interface ISingleRestroomConnection
    {
        int RestroomId { get; }
        Restroom Restroom { get; }
    }
}
