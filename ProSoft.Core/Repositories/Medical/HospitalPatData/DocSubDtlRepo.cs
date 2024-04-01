using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class DocSubDtlRepo : Repository<DocSubDtl, int>, IDocSubDtlRepo
    {
        public DocSubDtlRepo(AppDbContext Context) : base(Context)
        {
        }

        public Task<List<DoctorViewDTO>> GetAllDoctor()
        {
            throw new NotImplementedException();
        }
    }
}
