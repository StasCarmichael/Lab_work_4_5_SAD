using BLL.Interface;

namespace UoW.Repository.Interface
{
    public interface IAnticafeRepository
    {
        public IAnticafe GetByName(string name);
    }
}
