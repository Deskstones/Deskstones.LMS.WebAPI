namespace Deskstones.LMS.BusinessLogic.Interface
{
    using Software.DataContracts.Models;

    public interface IUserProfileOrchestrator
    {
        Task<DTOGenericResponse> CreateOrUpdateUserProfileAsync(DTOUserProfileUpdateRequest request);
    }
}
