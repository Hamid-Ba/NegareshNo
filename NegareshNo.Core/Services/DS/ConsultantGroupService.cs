using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NegareshNo.Core.Generators;
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
    public class ConsultantGroupService : IConsultantGroupService
    {
        private readonly UnitOfWork.UnitOfWork UW;

        public ConsultantGroupService(UnitOfWork.UnitOfWork uw) => UW = uw;

        public async Task<IEnumerable<ConsultantGroupIndexVM>> GetAllGroupForAdmin()
        {
            return await UW.Context.ConsultingGroups.Select(c => new ConsultantGroupIndexVM
            {
                GroupId = c.GroupId,
                GroupTitle = c.GroupTitle,
                Summery = c.Summery
            }).ToListAsync();
        }

        public async Task<ConsultingGroup> GetGroupById(int groupId) => await UW.GetRepository<ConsultingGroup>().GetEntityByIdAsync(groupId);

        public async Task<int> AddGroup(ConsultingGroup consultantGroup, IFormFile ImageFile)
        {
            if (consultantGroup != null)
            {
                consultantGroup.ImageName = CreateImage.AddImageForCreateTime(ImageFile, "image\\ConsultantGroupImage", "Default.png");
                await UW.GetRepository<ConsultingGroup>().AddEntityAsync(consultantGroup);
                await UW.CommitAsync();
                return consultantGroup.GroupId;
            }

            return 0;
        }

        public async Task<int> UpdateGroup(ConsultingGroup consultantGroup, IFormFile ImageFile)
        {
            if (consultantGroup != null)
            {
                var group = await GetGroupById(consultantGroup.GroupId);
                group.ImageName = CreateImage.AddImages(ImageFile, consultantGroup.ImageName, "image\\ConsultantGroupImage", "Default.png");
                group.GroupTitle = consultantGroup.GroupTitle;
                group.Summery = consultantGroup.Summery;
                UW.GetRepository<ConsultingGroup>().UpdateEntity(group);
                await UW.CommitAsync();
                return consultantGroup.GroupId;
            }

            return 0;
        }

        public async Task DeleteGroup(int groupId)
        {
            if (await IsGroupExist(groupId))
            {
                var group = await GetGroupById(groupId);
                CreateImage.DeleteImage(group.ImageName, "image\\ConsultantGroupImage", "Default.png");
                group.IsDelete = true;
                await UW.CommitAsync();
            }
        }

        public async Task<bool> IsGroupExist(int groupId) => await UW.Context.ConsultingGroups.AnyAsync(c => c.GroupId == groupId);

        public async Task<bool> IsGroupTitleExist(string groupTitle) => await UW.Context.ConsultingGroups.AnyAsync(c =>
        c.GroupTitle.TrimStart().TrimEnd() == groupTitle.TrimEnd().TrimStart());

        public async Task<IEnumerable<ConsultingGroup>> GetAllGroupForSite() => await UW.GetRepository<ConsultingGroup>().GetAllEntitiesAsync();
        
    }
}
