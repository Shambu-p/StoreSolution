using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreBackendClean.Domain.common
{
    public class AuthInput
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}