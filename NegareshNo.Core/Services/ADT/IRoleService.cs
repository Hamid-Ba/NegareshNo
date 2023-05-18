using NegareshNo.Core.ViewModels.Role;
using NegareshNo.Data.Model.Access;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NegareshNo.Core.Services.ADT
{
    public interface IRoleService
    {
        #region Permmision
        Task<IEnumerable<Permmision>> GetAllPermmisions();
        void AddPermmisionToRole(int[] permmisionsId, int roleId);
        IEnumerable<int> GetRolePermmisionsId(int roleId);
        #endregion

        #region Role
        Task<IEnumerable<Role>> GetAllRoles();
        bool IsUserHasPermmision(int permmisionId, string userName);
        Task<Role> GetRoleById(int roleId);
        Task<bool> IsThisRoleExist(string roleTitle);
        Task<int> AddRole(Role role);
        Task DeleteRole(Role role);
        RoleEditVM GetRoleForEdit(int roleId);
        Task UpdateRole(Role role);
        #endregion


    }
}
