using NegareshNo.Core.ViewModels.Consultant;
using NegareshNo.Data.Model.Consulting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NegareshNo.Core.Services.ADT
{
    public interface IConsultantAndGroupService
    {
        Task<IEnumerable<ConsultantAndGroupIndexVM>> GetAllConsultantWithHisGroups();
        Task<IEnumerable<ConsultingGroup>> GetConsutantResponsibility(int consultantId);

        Task<bool> CanCreateResponsibility(int consultantId);
        Task AddResponsibilitesToConsultant(int consultantId, int[] GroupsId);
        Task<ConsultantAndGroupEditVM> GetResponsibilityForEdit(int consultantId);
        Task DeleteConsultantResponsibilites(int consultantId);
        IEnumerable<int> GetConsultantsResponsibilitiesById(int consultantId);
        Task<IEnumerable<Consultant>> GetGroupsConsultantById(int groupId);
    }
}
