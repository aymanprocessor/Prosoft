using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class TermsPriceListRepo : Repository<PriceListDetail, int>, ITermsPriceListRepo
    {
        private readonly IMapper _mapper;
        public TermsPriceListRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<TermsPriceListViewDTO>> GetAllTermsPriceList(int id)
        {
            List<TermsPriceListViewDTO> TermsPriceListDTO = await _Context.PriceListDetails.Where(obj => obj.PLId == id)
                .Select(obj => new TermsPriceListViewDTO()
                {
                    PLDetailCode=Convert.ToInt32(obj.PLDetailCode),
                    ClinicDesc = obj.Clinic.ClinicDesc,
                    SClinicDesc = obj.SClinic.SClinicDesc,
                    ServId =obj.Serv.ServId,
                    ServDesc =obj.Serv.ServDesc,
                })
                .ToListAsync();
            return TermsPriceListDTO;
        }
    }
}
