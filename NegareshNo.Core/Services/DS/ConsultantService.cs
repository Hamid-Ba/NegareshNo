using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NegareshNo.Core.Generators;
using NegareshNo.Core.Services.ADT;
using NegareshNo.Core.ViewModels.Consultant;
using NegareshNo.Data.Model.Access;
using NegareshNo.Data.Model.Consulting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegareshNo.Core.Services.DS
{
    public class ConsultantService : IConsultantService
    {
        private readonly UnitOfWork.UnitOfWork UW;

        public ConsultantService(UnitOfWork.UnitOfWork uw) => UW = uw;

        public async Task<IEnumerable<ConsultantIndexAdminVM>> GetAllConsultantsIndexForAdminPanel()
        {
            return await UW.Context.Consultants.Include(rc => rc.Role_Consultants).Select(c => new ConsultantIndexAdminVM
            {
                ConsultantId = c.ConsultantId,
                ConsultantFullName = c.ConsultantFullName,
                Job = c.Job,
                Birthday = c.Birthday,
                City = c.City,
                Email = c.Email,
                Status = c.Status,
                Website = c.Website,
                Skills = c.Skills,
                MobilePhone = c.MobilePhone,
                IsDelete = c.IsDelete,
                Roles = UW.Context.Role_Consultants.Where(r => r.ConsultantId == c.ConsultantId).Select(r => r.Role.RoleTitle).ToArray()
            }).ToListAsync();
        }

        public async Task<Consultant> GetConsultantById(int consultantId) => await UW.GetRepository<Consultant>().GetEntityByIdAsync(consultantId);

        public async Task<bool> IsExistConsultant(int consultantId) => await UW.Context.Consultants.AnyAsync(c => c.ConsultantId == consultantId);

        public async Task<int> CreateConsultant(Consultant consultant, IFormFile ImageName, int[] RolesId)
        {
            if (consultant != null)
            {
                consultant.Role_Consultants = RolesId.Select(r => new Data.Model.Access.Role_Consultant { RoleId = r }).ToList();
                consultant.ImageName = CreateImage.AddImageForCreateTime(ImageName, "\\image\\ConsultantImage", "NoImage.jpg");
                await UW.GetRepository<Consultant>().AddEntityAsync(consultant);
                await UW.CommitAsync();
            }

            return consultant.ConsultantId;
        }

        public async Task<int> CreateConsultant(Consultant consultant, IFormFile ImageName, List<Role_Consultant> role_Consultants)
        {
            if (consultant != null)
            {
                consultant.Role_Consultants = role_Consultants;
                consultant.ImageName = CreateImage.AddImageForCreateTime(ImageName, "\\image\\ConsultantImage", "NoImage.jpg");
                await UW.GetRepository<Consultant>().AddEntityAsync(consultant);
                await UW.CommitAsync();
            }

            return consultant.ConsultantId;
        }

        public async Task<string[]> GetConsultantRolesTitle(int consultantId) => await UW.Context.Role_Consultants.Where(r => r.ConsultantId == consultantId)
            .Select(r => r.Role.RoleTitle).ToArrayAsync();

        public async Task<ConsultantEditVM> GetConsultantForEdit(int consultantId)
        {
            if (await IsExistConsultant(consultantId))
            {
                return await UW.Context.Consultants.Where(c => c.ConsultantId == consultantId).Select(c => new ConsultantEditVM
                {
                    ConsultantId = c.ConsultantId,
                    ConsultantFullName = c.ConsultantFullName,
                    Job = c.Job,
                    Birthday = c.Birthday,
                    City = c.City,
                    Email = c.Email,
                    Status = c.Status,
                    Website = c.Website,
                    Skills = c.Skills,
                    MobilePhone = c.MobilePhone,
                    IsDelete = c.IsDelete,
                    RolesId = UW.Context.Role_Consultants.Where(r => r.ConsultantId == consultantId).Select(r => r.Role.RoleId).ToArray(),
                    CareerRecord = c.CareerRecord,
                    CareerSummary = c.CareerSummary,
                    EducationalRecord = c.EducationalRecord,
                    ImageName = c.ImageName,
                    LiceneRecord = c.LiceneRecord,
                    WorkRecord = c.WorkRecord,
                    Password = c.Password,
                    UserName = c.UserName
                }).SingleOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> UpdateConsultant(ConsultantEditVM ViewModel, IFormFile ImageName)
        {
            if (ViewModel != null)
            {
                var consultant = await GetConsultantById(ViewModel.ConsultantId);

                consultant.ConsultantFullName = ViewModel.ConsultantFullName;
                consultant.City = ViewModel.City;
                consultant.EducationalRecord = ViewModel.EducationalRecord;
                consultant.Email = ViewModel.Email;
                consultant.WorkRecord = ViewModel.WorkRecord;
                consultant.Website = ViewModel.Website;
                consultant.Status = ViewModel.Status;
                consultant.Skills = ViewModel.Skills;
                consultant.MobilePhone = ViewModel.MobilePhone;
                consultant.LiceneRecord = ViewModel.LiceneRecord;
                consultant.Job = ViewModel.Job;
                consultant.IsDelete = ViewModel.IsDelete;
                consultant.CareerSummary = ViewModel.CareerSummary;
                consultant.CareerRecord = ViewModel.CareerRecord;
                consultant.Birthday = ViewModel.Birthday;
                if(!String.IsNullOrEmpty(ViewModel.Password)) { consultant.Password = ViewModel.Password; }
                consultant.UserName = ViewModel.UserName;

                if (UW.Context.Role_Consultants.Any(rc => rc.ConsultantId == consultant.ConsultantId))
                {
                    UW.GetRepository<Role_Consultant>().DeleteRangeOfEntities(UW.Context.Role_Consultants.Where(r => r.ConsultantId == ViewModel.ConsultantId)
                        .ToList());
                }
                if (ViewModel.RolesId != null)
                {
                    consultant.Role_Consultants = ViewModel.RolesId.Select(r => new Role_Consultant { RoleId = r }).ToList();
                }

                consultant.ImageName = CreateImage.AddImages(ImageName, consultant.ImageName, "image\\ConsultantImage", "NoImage.jpg");

                UW.GetRepository<Consultant>().UpdateEntity(consultant);
                await UW.CommitAsync();
                return consultant.ConsultantId;
            }
            return 0;
        }

        public async Task DeleteUser(int consultantId)
        {
            var consultant = await GetConsultantById(consultantId);

            CreateImage.DeleteImage(consultant.ImageName, "image\\ConsultantImage", "NoImage.jpg");
            if (UW.Context.Role_Consultants.Any(rc => rc.ConsultantId == consultant.ConsultantId))
            {
                UW.GetRepository<Role_Consultant>().DeleteRangeOfEntities(UW.Context.Role_Consultants.Where(r => r.ConsultantId == consultantId)
                    .ToList());
            }

            if(UW.Context.Consultant_Groups.Any(cg => cg.ConsultantId == consultant.ConsultantId))
            {
                UW.GetRepository<Consultant_Group>().DeleteRangeOfEntities(UW.Context.Consultant_Groups.Where(r => r.ConsultantId == consultant.ConsultantId).ToList());
            }

            consultant.IsDelete = true;
            UW.GetRepository<Consultant>().UpdateEntity(consultant);
            await UW.CommitAsync();
        }

        public async Task<string> GetConsultantUserName(int consultantId)
        {
            if (await IsExistConsultant(consultantId))
            {
                var user = await GetConsultantById(consultantId);
                return user.UserName;
            }
            return null;
        }

        public async Task<bool> IsConsultantUserNameExist(string userName) => await UW.Context.Consultants.AnyAsync(c => c.UserName.TrimStart().TrimEnd()
        == userName.TrimEnd().TrimStart());

        public async Task<int> GetConsultantIdByUserName(string userName)
        {
            if (UW.Context.Consultants.Any(u => u.UserName == userName))
            {
                var user = await UW.Context.Consultants.Where(u => u.UserName.TrimStart().TrimEnd() ==
                            userName.TrimStart().TrimEnd()).SingleOrDefaultAsync();

                return user.ConsultantId;
            }
            return 0;
        }

        public async Task<bool> CanUserLogin(string userName, string password)
        {
            var user = await GetConsultantByUserName(userName);

            if (user != null)
                if (user.Password.TrimStart().TrimEnd() == password.TrimStart().TrimEnd()) return true;

            return false;
        }

        public async Task<Consultant> GetConsultantByUserName(string UserName)
        {
            if (await IsConsultantUserNameExist(UserName))
            {
                var userId = await GetConsultantIdByUserName(UserName);

                if (await IsExistConsultant(userId))
                {
                    var user = await GetConsultantById(userId);
                    return user;
                }
            }
            return null;
        }

        public async Task<string> GetConultantImageNameByUserName(string userName)
        {
            var user = await GetConsultantByUserName(userName);
            return user.ImageName;
        }

        public IEnumerable<ConsultantSiteVM> GetAllConsultantsForSite()
        {
            var role_consul = UW.Context.Role_Consultants.Include(r => r.Role).Include(c => c.Consultant).ToList();
            List<Consultant> consultants = new List<Consultant>();

            foreach (var item in role_consul)
            {
                if (item.Role.RoleTitle.Contains("مشاور"))
                {
                    consultants.Add(item.Consultant);
                }
            }

            var consultantSite =  consultants.Select(c => new ConsultantSiteVM
            {
                ImageName = c.ImageName,
                ConsultantFullName = c.ConsultantFullName,
                CareerSummary = c.CareerSummary,
                ConsultantId = c.ConsultantId
            }).ToList();

            return consultantSite;
        }

        public async Task<IEnumerable<Consultant>> GetJustConsultant()
        {
            var consultantRoleId = await UW.Context.Roles.Where(r => r.RoleTitle.Contains("مشاور")).Select(r => r.RoleId).FirstOrDefaultAsync();
            var consultants = await UW.Context.Role_Consultants.Include(c => c.Consultant).Where(r => r.RoleId == consultantRoleId).Select(c => c.Consultant).ToListAsync();

            return consultants;
        }
    }
}
