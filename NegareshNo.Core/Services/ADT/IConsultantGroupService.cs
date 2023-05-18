using Microsoft.AspNetCore.Http;
using NegareshNo.Core.ViewModels.Consultant;
using NegareshNo.Data.Model.Consulting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NegareshNo.Core.Services.ADT
{
    public interface IConsultantGroupService
    {
        Task<IEnumerable<ConsultantGroupIndexVM>> GetAllGroupForAdmin();
        Task<ConsultingGroup> GetGroupById(int groupId);
        Task<IEnumerable<ConsultingGroup>> GetAllGroupForSite(); 

        Task<int> AddGroup(ConsultingGroup consultantGroup, IFormFile ImageFile);
        Task<int> UpdateGroup(ConsultingGroup consultantGroup, IFormFile ImageFile);
        Task DeleteGroup(int groupId);
        Task<bool> IsGroupTitleExist(string groupTitle);
        Task<bool> IsGroupExist(int groupId);
    }
}
