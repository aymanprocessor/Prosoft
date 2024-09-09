using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IEisSectionTypeRepo:IRepository<EisSectionType,int>
    {
        public IEnumerable<SelectListItem> GetAllAsEnumerable();

    }
}
