using System;
using System.Collections.Generic;
using System.Linq;


namespace BLL.Interface
{
    public interface IAnticafe : IIdable
    {
        string Name { get; }
        string Address { get; }

        bool AddRestroom(string typeRecreation, double pricePerHour, int workOut, int workUp);
    }
}
