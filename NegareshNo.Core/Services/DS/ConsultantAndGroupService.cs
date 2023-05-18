using Microsoft.EntityFrameworkCore;
using NegareshNo.Core.Services.ADT;
using NegareshNo.Core.ViewModels.Consultant;
using NegareshNo.Data.Model.Consulting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegareshNo.Core.Services.DS
{
    public class ConsultantAndGroupService : IConsultantAndGroupService
    {
        private readonly UnitOfWork.UnitOfWork UW;

        public ConsultantAndGroupService(UnitOfWork.UnitOfWork uw) => UW = uw;

        public async Task<IEnumerable<ConsultantAndGroupIndexVM>> GetAllConsultantWithHisGroups()
        {
            var consultantRoleId = await UW.Context.Roles.Where(r => r.RoleTitle.Contains("مشاور")).Select(r => r.RoleId).FirstOrDefaultAsync();
            var consultants = await UW.Context.Role_Consultants.Include(c => c.Consultant).ThenInclude(c => c.Consultant_Groups).Where(r => (r.RoleId == consultantRoleId) && (r.Consultant.Consultant_Groups.Count != 0)).ToListAsync();

            return consultants.Select(c => new ConsultantAndGroupIndexVM
            {
                ConsultantId = c.ConsultantId,
                ConsultantName = c.Consultant.ConsultantFullName,
                GroupId = UW.Context.Consultant_Groups.Where(g => g.ConsultantId == c.ConsultantId).Select(g => g.Group.GroupTitle).ToArray()
            }).ToList();
        }

        public async Task<IEnumerable<ConsultingGroup>> GetConsutantResponsibility(int consultantId)
        {
            var consultant = await UW.GetRepository<Consultant>().GetEntityByIdAsync(consultantId);

            var responsibilties = await UW.Context.Consultant_Groups.Include(g => g.Group)
                .Where(c => c.ConsultantId == consultantId).Select(g => g.Group).ToListAsync();

            return responsibilties;
        }

        public async Task<bool> CanCreateResponsibility(int consultantId)
        {
            if (await UW.Context.Consultant_Groups.AnyAsync(c => c.ConsultantId == consultantId)) return false;
            return true;
        }

        public async Task AddResponsibilitesToConsultant(int consultantId, int[] GroupsId)
        {
            if (await UW.Context.Consultants.AnyAsync(r => r.ConsultantId == consultantId))
            {
                var consultant = await UW.GetRepository<Consultant>().GetEntityByIdAsync(consultantId);

                UW.GetRepository<Consultant_Group>().DeleteRangeOfEntities(UW.Context.Consultant_Groups.Where(pr => pr.ConsultantId == consultantId).ToList());
                if (GroupsId != null)
                {
                    UW.GetRepository<Consultant_Group>().AddRangeOfEntities(GroupsId.Select(p => new Consultant_Group { GroupId = p, ConsultantId = consultantId })
                        .ToList());

                    await UW.CommitAsync();
                }
            }
        }

        public async Task DeleteConsultantResponsibilites(int consultantId)
        {
            if (await UW.Context.Consultants.AnyAsync(r => r.ConsultantId == consultantId))
            {
                UW.GetRepository<Consultant_Group>().DeleteRangeOfEntities(UW.Context.Consultant_Groups.Where(r => r.ConsultantId == consultantId).ToList());
                await UW.CommitAsync();
            }
        }

        public async Task<ConsultantAndGroupEditVM> GetResponsibilityForEdit(int consultantId)
        {
            if (await UW.Context.Consultant_Groups.AnyAsync(c => c.ConsultantId == consultantId))
            {
                return UW.Context.Consultants.Include(g => g.Consultant_Groups).Where(r => r.ConsultantId == consultantId).Select(r => new ConsultantAndGroupEditVM
                {
                    ConsultantId = r.ConsultantId,
                    ConsultantName = r.ConsultantFullName,
                    GroupsId = GetConsultantsResponsibilitiesById(consultantId).ToArray()

                }).SingleOrDefault();
            }

            return null;
        }

        public IEnumerable<int> GetConsultantsResponsibilitiesById(int consultantId)
        {
            var consultant = UW.Context.Consultants.Include(rp => rp.Consultant_Groups).SingleOrDefault(r => r.ConsultantId == consultantId);

            if (consultant != null)
                return from c in consultant.Consultant_Groups select c.GroupId;

            return null;
        }

        public async Task<IEnumerable<Consultant>> GetGroupsConsultantById(int groupId)
        {
            var consultant = await UW.Context.Consultant_Groups.Include(c => c.Consultant).Where(g => g.GroupId == groupId).Select(c => c.Consultant).ToListAsync();
            return consultant;
        }
    }
}
