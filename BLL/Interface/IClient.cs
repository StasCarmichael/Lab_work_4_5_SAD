﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BLL.ConnectionInterface;

namespace BLL.Interface
{
    public interface IClient : IIdable, IAccountable
    {
        string Name { get; set; }
        string Surname { get; set; }
    }
}
