﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class ModificationAccountCodesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
