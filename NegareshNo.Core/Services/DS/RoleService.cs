using Microsoft.EntityFrameworkCore;
using NegareshNo.Core.Services.ADT;
using NegareshNo.Core.ViewModels.Role;
using NegareshNo.Data.Model.Access;
using NegareshNo.Data.Model.Consulting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegareshNo.Core.Services.DS
{
    public class RoleService : IRoleService
    {
        private readonly UnitOfWork.UnitOfWork UW;
        private readonly IConsultantService consultantService;

        public RoleService(UnitOfWork.UnitOfWork uw, IConsultantService consultantService)
        {
            UW = uw;
            this.consultantService = consultantService;
        }

        public async Task<IEnumerable<Permmision>> GetAllPermmisions() => await UW.GetRepository<Permmision>().GetAllEntitiesAsync();

        public async Task<IEnumerable<Role>> GetAllRoles() => await UW.GetRepository<Role>().GetAllEntitiesAsync();

        public void AddPermmisionToRole(int[] permmisionsId, int roleId)
        {
            var role = UW.GetRepository<Role>().GetEntityById(roleId);
            if (UW.Context.Roles.Any(r => r.RoleId == roleId))
            {
                UW.GetRepository<Role_Permmision>().DeleteRangeOfEntities(UW.Context.Role_Permmisions.Where(pr => pr.RoleId == roleId).ToList());
                if (permmisionsId != null)
                {
                    UW.GetRepository<Role_Permmision>().AddRangeOfEntities(permmisionsId.Select(p => new Role_Permmision { PermmisionId = p, RoleId = roleId })
                        .ToList());

                    UW.Commit();
                }
            }
        }

        public IEnumerable<int> GetRolePermmisionsId(int roleId)
        {
            var role = UW.Context.Roles.Include(rp => rp.Role_Permmisions).SingleOrDefault(r => r.RoleId == roleId);

            if (role != null)
                return from r in role.Role_Permmisions select r.PermmisionId;

            return null;
        }

        public async Task<int> AddRole(Role role)
        {
            await UW.GetRepository<Role>().AddEntityAsync(role);
            await UW.CommitAsync();
            return role.RoleId;
        }

        public async Task DeleteRole(Role role)
        {
            UW.GetRepository<Role_Permmision>().DeleteRangeOfEntities(UW.Context.Role_Permmisions.Where(r => r.RoleId == role.RoleId).ToList());
            role.IsDelete = true;
            await UW.CommitAsync();
        }

        public async Task<Role> GetRoleById(int roleId) => await UW.GetRepository<Role>().GetEntityByIdAsync(roleId);

        public async Task<bool> IsThisRoleExist(string roleTitle) => await UW.Context.Roles.AnyAsync(r => r.RoleTitle.TrimStart().TrimEnd() ==
        roleTitle.TrimStart().TrimEnd());

        public RoleEditVM GetRoleForEdit(int roleId)
        {
            return UW.Context.Roles.Where(r => r.RoleId == roleId).Select(r => new RoleEditVM
            {
                RoleId = roleId,
                RoleTitle = r.RoleTitle,
                PermmisionsId = GetRolePermmisionsId(roleId).ToArray()
            }).SingleOrDefault();
        }

        public async Task UpdateRole(Role role)
        {
            if (role != null)
            {
                UW.GetRepository<Role>().UpdateEntity(role);
                await UW.CommitAsync();
            }
        }

        public bool IsUserHasPermmision(int permmisionId, string userName)
        {
            var userId = UW.Context.Consultants.Where(u => u.UserName == userName).SingleOrDefault().ConsultantId; /*consultantService.GetConsultantIdByUserName(userName);*/
            var userRoles = UW.Context.Role_Consultants.Where(r => r.ConsultantId == userId).Select(r => r.Role).ToList();

            foreach (var role in userRoles)
            {
                if (UW.Context.Role_Permmisions.Any(rp => rp.RoleId == role.RoleId && rp.PermmisionId == permmisionId)) return true;
            }

            return false;
        }
    }
}
