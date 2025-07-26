namespace Deskstones.LMS.Domain.Interface
{
    using Software.DataContracts.Models;

    public interface IAuthenticationRepository
    {
        Task<DTOGenericResponse> RegisterAsync(DTORegisterationRequest request);
        Task<DTOLoginResponse> LoginAsync(DTOLoginRequest request);
    }
}
