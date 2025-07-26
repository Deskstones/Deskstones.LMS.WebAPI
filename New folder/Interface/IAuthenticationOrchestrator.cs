namespace Deskstones.LMS.BusinessLogic.Interface
{
    using Software.DataContracts.Models;

    public interface IAuthenticationOrchestrator
    {
        Task<DTOGenericResponse> RegisterAsync(DTORegisterationRequest request);
        Task<DTOLoginResponse> LoginAsync(DTOLoginRequest request);
    }
}
