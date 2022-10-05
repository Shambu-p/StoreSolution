using StoreSolution.Application.common.Models;

namespace StoreSolution.Application.common.Interfaces
{
    public interface IIdentityService {

        UserAuthentication generateIdentity(UserAuthentication user);
        UserAuthentication getUser();

    }
}