using Microsoft.EntityFrameworkCore;
using NegareshNo.Core.Services.ADT;
using NegareshNo.Core.ViewModels.User;
using NegareshNo.Data.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegareshNo.Core.Services.DS
{
    public class UserRequestService : IUserRequestService
    {
        private readonly UnitOfWork.UnitOfWork UW;

        public UserRequestService(UnitOfWork.UnitOfWork uw) => UW = uw;

        public async Task<IEnumerable<UserRequestAdminIndexVM>> GetAllRequestForAdmin()
        {
            return await UW.Context.UserRequests.Include(c => c.Consultant).Include(g => g.ConsultingGroup).Select(u => new UserRequestAdminIndexVM
            {
                RequestId = u.RequestId,
                ConsultantId = u.ConsultantId,
                FullName = u.FullName,
                GivenTime = u.GivenTime,
                GroupId = u.GroupId,
                ConsultantName = u.Consultant.ConsultantFullName,
                GroupName = u.ConsultingGroup.GroupTitle,
                HasTime = u.HasTime,
                IsDone = u.IsDone,
                PhoneNumber = u.PhoneNumber,
                RegistrationConsultingTime = u.RegistrationConsultingTime,
                Title = u.Title
            }).ToListAsync();
        }

        public async Task<UserRequest> GetRequestById(int requestId) => await UW.GetRepository<UserRequest>().GetEntityByIdAsync(requestId);


        public async Task<int> AddRequest(UserRequestCreateVM userRequestCreateVM)
        {
            if (userRequestCreateVM != null)
            {
                var request = new UserRequest();

                request.FullName = userRequestCreateVM.FullName;
                request.Title = userRequestCreateVM.Title;
                request.Description = userRequestCreateVM.Description;
                request.ConsultantId = userRequestCreateVM.ConsultantId;
                request.GroupId = userRequestCreateVM.GroupId;
                request.PhoneNumber = userRequestCreateVM.PhoneNumber;
                request.TellPhoneNumber = userRequestCreateVM.TellPhoneNumber;

                request.IsDone = false;
                request.HasTime = false;
                request.RegistrationConsultingTime = DateTime.Now;
                request.IsDelete = false;

                await UW.GetRepository<UserRequest>().AddEntityAsync(request);
                await UW.CommitAsync();

                return request.RequestId;
            }

            return 0;
        }

        public async Task<UserRequestEditVM> GetRequestForUpdate(int requestId)
        {
            return await UW.Context.UserRequests.Where(u => u.RequestId == requestId).Select(u => new UserRequestEditVM
            {
                RequestId = u.RequestId,
                FullName = u.FullName,
                Description = u.Description,
                GivenTime = u.GivenTime,
                HasTime = u.HasTime,
                PhoneNumber = u.PhoneNumber,
                RegistrationConsultingTime = u.RegistrationConsultingTime,
                TellPhoneNumber = u.TellPhoneNumber,
                Title = u.Title,
                ConsultantId = u.ConsultantId
            }).SingleOrDefaultAsync();
        }

        public async Task<int> UpdateRequest(UserRequestEditVM userRequestEditVM)
        {
            if (userRequestEditVM != null)
            {
                if (await IsRequestExist(userRequestEditVM.RequestId))
                {
                    var request = await GetRequestById(userRequestEditVM.RequestId);

                    request.HasTime = userRequestEditVM.HasTime;
                    request.GivenTime = userRequestEditVM.GivenTime;
                    request.ConsultantId = userRequestEditVM.ConsultantId;

                    UW.GetRepository<UserRequest>().UpdateEntity(request);
                    await UW.CommitAsync();

                    return request.RequestId;
                }
            }

            return 0;
        }

        public async Task<bool> IsRequestExist(int requestId) => await UW.Context.UserRequests.AnyAsync(u => u.RequestId == requestId);

        public async Task<string> GetGivenTimeToString(int requestId)
        {
            var giveTime = await UW.Context.UserRequests.Where(r => r.RequestId == requestId).Select(r => r.GivenTime).SingleOrDefaultAsync();
            return giveTime.ToString();
        }

        public async Task MakeDoneRequest(int requestId)
        {
            if (await IsRequestExist(requestId))
            {
                var request = await GetRequestById(requestId);

                if (!request.IsDone) request.IsDone = true;
                else request.IsDone = false;

                UW.GetRepository<UserRequest>().UpdateEntity(request);
                await UW.CommitAsync();
            }
        }

        public async Task DeleteRequest(int requestId)
        {
            if (await IsRequestExist(requestId))
            {
                var request = await GetRequestById(requestId);
                UW.GetRepository<UserRequest>().DeleteEntity(request);
                await UW.CommitAsync();
            }
        }
    }
}
