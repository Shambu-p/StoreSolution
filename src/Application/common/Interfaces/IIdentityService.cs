using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreSolution.Application.common.Models;

namespace StoreSolution.Application.common.Interfaces
{
    public interface IIdentityService {

        UserAuthentication generateIdentity(UserAuthentication user);
        UserAuthentication getUser();

    }
}