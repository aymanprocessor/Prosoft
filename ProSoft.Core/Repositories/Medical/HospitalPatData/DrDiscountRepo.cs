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
    public class DrDiscountRepo : Repository<DrDiscount, int>, IDrDiscountRepo
    {
        public DrDiscountRepo(AppDbContext Context) : base(Context)
        {

           
        }
        public async Task<List<DrDiscountViewDTO>> GetDoctorCommissions()
        {
            List<DrDiscountViewDTO> drDiscountViewDTOs = new();
            var drdiscounts = await _Context.DrDiscounts.ToListAsync();
            foreach (var item in drdiscounts)
            {
                var doctor = await _Context.Doctors.FirstOrDefaultAsync(d => d.DrId == item.DrId);
                if(doctor != null)
                {
                    DrDiscountViewDTO drDiscountViewDTO = new()
                    {
                        Id = item.Id,
                        DrId = item.DrId,
                        DoctorName = doctor.DrDesc,
                        DrPercentage = item.DrPercentage,
                        DrPercentageContract = item.DrPercentageContract,
                        FlagDisc = item.FlagDisc

                    };
                    drDiscountViewDTOs.Add(drDiscountViewDTO);
                }

            }
            return drDiscountViewDTOs;
        }
    }
}
