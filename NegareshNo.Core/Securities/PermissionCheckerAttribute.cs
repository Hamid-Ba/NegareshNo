using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NegareshNo.Core.Services.ADT;
using System;
using System.Collections.Generic;
using System.Text;

namespace NegareshNo.Core.Securities
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IRoleService service;
        private int permissionId;

        public PermissionCheckerAttribute(int permissionId) => this.permissionId = permissionId;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            service = (IRoleService)context.HttpContext.RequestServices.GetService(typeof(IRoleService));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = context.HttpContext.User.Identity.Name;

                if (!service.IsUserHasPermmision(permissionId, userName)) { context.Result = new RedirectResult("/"); }
            }

            else context.Result = new RedirectResult("/");
        }

    }
}
