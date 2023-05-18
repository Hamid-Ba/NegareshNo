using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NegareshNo.Core.Securities;

namespace NegareshNo.Pages
{
    [Authorize]
    [PermissionChecker(1)]
    public class AdminPanelModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}