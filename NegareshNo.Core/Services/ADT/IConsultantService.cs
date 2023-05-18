using Microsoft.AspNetCore.Http;
using NegareshNo.Core.ViewModels.Consultant;
using NegareshNo.Data.Model.Access;
using NegareshNo.Data.Model.Consulting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NegareshNo.Core.Services.ADT
{
    public interface IConsultantService
    {
        Task<IEnumerable<ConsultantIndexAdminVM>> GetAllConsultantsIndexForAdminPanel();
        Task<IEnumerable<Consultant>> GetJustConsultant();
        Task<Consultant> GetConsultantById(int consultantId);
        Task<bool> IsExistConsultant(int counsultantId);
        Task<bool> IsConsultantUserNameExist(string userName);
        Task<string> GetConsultantUserName(int consultantId);
        Task<int> GetConsultantIdByUserName(string userName);
        Task<bool> CanUserLogin(string userName, string password);
        Task<Consultant> GetConsultantByUserName(string UserName);
        Task<string> GetConultantImageNameByUserName(string userName);
        IEnumerable<ConsultantSiteVM> GetAllConsultantsForSite();


        Task<int> CreateConsultant(Consultant consultant, IFormFile ImageName, int[] RolesId);
        Task<int> CreateConsultant(Consultant consultant, IFormFile ImageName, List<Role_Consultant> role_Consultants);
        Task<string[]> GetConsultantRolesTitle(int consultantId);
        Task<ConsultantEditVM> GetConsultantForEdit(int consultantId);
        Task<int> UpdateConsultant(ConsultantEditVM ViewModel, IFormFile ImageName);
        Task DeleteUser(int consultantId);
    }
}
