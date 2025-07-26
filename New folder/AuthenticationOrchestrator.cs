using Deskstones.LMS.BusinessLogic.Interface;
using Deskstones.LMS.Domain.Interface;
using Software.DataContracts.Models;

namespace Deskstones.LMS.BusinessLogic
{
    internal sealed class AuthenticationOrchestrator(IAuthenticationRepository authenticationRepository):IAuthenticationOrchestrator
    {
        public async Task<DTOGenericResponse> RegisterAsync(DTORegisterationRequest request)
        {
            return await authenticationRepository.RegisterAsync(request);
        }

        public async Task<DTOLoginResponse> LoginAsync(DTOLoginRequest request)
        {
            return await authenticationRepository.LoginAsync(request);
        }

    }
}
