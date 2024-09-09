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
    public class RegionRepo : Repository<Region, int> ,IRegionRepo
    {
        private readonly AppDbContext _context;

        public RegionRepo(AppDbContext Context) : base(Context)
        {
            _context = Context;
        }

        public IEnumerable<SelectListItem> GetAllAsEnumerable()
        {
            return _context.Regions.Select(e => new SelectListItem { Value = e.RegionCode.ToString(), Text = e.RegionDesc }).ToList();

        }
    }
}
