﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Repository
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
       
    }
}
