using System.Collections.Generic;
using BLL.Entity;

namespace BLL.ConnectionInterface
{
    public interface IManyRestroomConnection
    {
        ICollection<Restroom> Restrooms { get; }
    }
}
