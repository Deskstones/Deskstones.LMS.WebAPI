using Deskstones.LMS.BusinessLogic.Interface;
using Deskstones.LMS.Domain.Interface;
using Software.DataContracts.Models;

namespace Deskstones.LMS.BusinessLogic
{
    internal class UserProfileOrchestrator(IUserProfileRepository userProfileRepository):IUserProfileOrchestrator
    {
        public async Task<DTOGenericResponse> CreateOrUpdateUserProfileAsync(int userId, DTOUserProfileUpdateRequest request)
        {
            return await userProfileRepository.CreateOrUpdateUserProfileAsync(userId, request);
        }
    }
}
