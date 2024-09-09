using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Shared
{
    public class BranchRepo : Repository<Branch, int>, IBranchRepo
    {
        private readonly AppDbContext _context;

        public BranchRepo(AppDbContext Context) : base(Context)
        {
            _context = Context;
        }

        public IEnumerable<SelectListItem> GetAllBranchesAsEnumerable()
        {
            return _context.Branchs.Select(b => new SelectListItem { Value = b.BranchId.ToString(), Text = b.BranchDesc })
                .ToList();
        }
    }
}
