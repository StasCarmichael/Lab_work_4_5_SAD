using BLL.Entity;
using BLL.ConnectionInterface;

namespace BLL.Interface
{
    public interface IAnticafe : IIdable, IManyRestroomConnection
    {
        string Name { get; set; }
        string Address { get; }

        (bool result, IRestroom restroom) AddRestroom(string typeRecreation, double pricePerHour, int workOut, int workUp);
        bool RemoveRestroom(Restroom restroom);
        bool RemoveRestroom(int restroomId);

        IRestroom GetRestroom(int restroomId);

    }
}
