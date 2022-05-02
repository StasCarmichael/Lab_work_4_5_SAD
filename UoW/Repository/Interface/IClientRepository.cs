using BLL.Interface;

namespace UoW.Repository.Interface
{
    public interface IClientRepository
    {
        public IClient GetByName(string name);
    }
}
