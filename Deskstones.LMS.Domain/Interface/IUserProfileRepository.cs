using Software.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deskstones.LMS.Domain.Interface
{
    public interface IUserProfileRepository
    {
        Task<DTOGenericResponse> CreateOrUpdateUserProfileAsync(DTOUserProfileUpdateRequest request);
    }
}
