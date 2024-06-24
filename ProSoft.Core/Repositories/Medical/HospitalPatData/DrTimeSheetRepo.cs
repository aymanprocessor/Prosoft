using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class DrTimeSheetRepo : Repository<Drtimsheet, int>,IDrTimeSheetRepo
    {
        private readonly IMapper _mapper;
        public DrTimeSheetRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<DrTimeSheetDTO>> GetAppointmentsForDrAsync(int DrId)
        {
            List<DrTimeSheetDTO> drtimsheetDTOs = await _Context.Drtimsheets.Where(obj => obj.DrId == DrId)
                .Select(obj => new DrTimeSheetDTO()
                {
                    DayNumber = obj.DayNumber,
                    ExPeriod = obj.ExPeriod,
                    Timfrom = obj.Timfrom,
                    Timto = obj.Timto,
                })
                .ToListAsync();
            return drtimsheetDTOs;
        }
    }
}
