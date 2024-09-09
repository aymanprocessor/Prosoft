using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class EisSectionTypeRepo : Repository<EisSectionType, int>,IEisSectionTypeRepo
    {
        private readonly AppDbContext _context;

        public EisSectionTypeRepo(AppDbContext Context) : base(Context)
        {
            _context = Context;
        }

        public IEnumerable<SelectListItem> GetAllAsEnumerable()
        {
            return _context.EisSectionTypes.Select(e => new SelectListItem { Value = e.SecCode.ToString(), Text = e.SecName }).ToList();
        }
    }
}
