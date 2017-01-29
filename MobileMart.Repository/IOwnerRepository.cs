﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;

namespace MobileMart.Repository
{
    public interface IOwnerRepository
    {
        void Insert(Owner entity);
        IEnumerable<Owner> Get();
    }
}
