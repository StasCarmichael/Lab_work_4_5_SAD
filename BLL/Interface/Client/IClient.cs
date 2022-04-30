using BLL.ConnectionInterface;

namespace BLL.Interface
{
    public interface IClient : IIdable, IAccountable, IManyOrderConnection
    {
        string Name { get; set; }
        string Surname { get; set; }
    }
}
