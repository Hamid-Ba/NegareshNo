using NegareshNo.Core.ViewModels.User;
using NegareshNo.Data.Model.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NegareshNo.Core.Services.ADT
{
    public interface IUserRequestService
    {
        Task<IEnumerable<UserRequestAdminIndexVM>> GetAllRequestForAdmin();
        Task<UserRequest> GetRequestById(int requestId);
        Task<int> AddRequest(UserRequestCreateVM userRequestCreateVM);

        Task<UserRequestEditVM> GetRequestForUpdate(int requestId);
        Task<int> UpdateRequest(UserRequestEditVM userRequestEditVM);
        Task<bool> IsRequestExist(int requestId);
        Task<string> GetGivenTimeToString(int requestId);
        Task MakeDoneRequest(int requestId);
        Task DeleteRequest(int requestId);


    }
}
