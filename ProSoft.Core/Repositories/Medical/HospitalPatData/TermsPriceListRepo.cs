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
        /* Ayman Saad  15-9-2024 */
        public async Task<List<PriceListDetail>> GetAllPriceListDetails(int id)
        {
              return await _Context.PriceListDetails
                .Where(obj => obj.PLId == id)
                .Include(p => p.Branch)
                .Include(p => p.Clinic)
                .Include(p => p.SClinic)
                .Include(p => p.Serv)
                .ToListAsync();
             
        }

        /* Ayman Saad  15-9-2024 */


        public async Task<List<TermsPriceListViewDTO>> GetAllTermsPriceList(int id)
        {
            List<TermsPriceListViewDTO> TermsPriceListDTO = await _Context.PriceListDetails.Where(obj => obj.PLId == id)
                .Select(obj => new TermsPriceListViewDTO()
                {
                    PLDtlId = Convert.ToInt32(obj.PLDtlId),
                    PLDetailCode=Convert.ToInt32(obj.PLDetailCode),
                    ClinicDesc = obj.Clinic.ClinicDesc,
                    SClinicDesc = obj.SClinic.SClinicDesc,
                    ServId =obj.Serv.ServId,
                    ServDesc =obj.Serv.ServDesc,
                    ServBefDesc =obj.ServBefDesc,
                    DiscoutComp=obj.DiscoutComp,
                    PlValue=obj.PlValue,
                    CompCovPercentage=obj.CompCovPercentage,
                    CompValue =obj.CompValue,
                    PlValue2 =obj.PlValue2,
                    PlValue3 =obj.PlValue3,
                    ExtraVal=obj.ExtraVal,
                    ExtraVal2 =obj.ExtraVal2,
                    Covered =obj.Covered,

                })
                .ToListAsync();
            return TermsPriceListDTO;
        }
        public async Task<int> GetNewIdSortAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.PLDetailCode);
                newID = (int)lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
        public async Task<TermsPriceListEditAddDTO> GetEmptyTermsPriceListAsync()
        {
            TermsPriceListEditAddDTO termsPriceListDTO = new TermsPriceListEditAddDTO();
            List<MainClinic> mainClinics = await _Context.MainClinics.ToListAsync();
            termsPriceListDTO.MainClinics = _mapper.Map<List<MainClinicViewDTO>>(mainClinics);

            return termsPriceListDTO;


        }

        public async Task AddTermPriceListAsync(int id, TermsPriceListEditAddDTO termsPriceListDTO)
        {
            termsPriceListDTO.PlValue = termsPriceListDTO.ServBefDesc - (termsPriceListDTO.ServBefDesc * (termsPriceListDTO.DiscoutComp / 100));
            //////////
            //termsPriceListDTO.CompCovPercentage = 100 - ((termsPriceListDTO.PlValue2 / termsPriceListDTO.PlValue) * 100) - ((termsPriceListDTO.PlValue3 / termsPriceListDTO.PlValue) * 100);
            //termsPriceListDTO.PlValue2 = termsPriceListDTO.PlValue -
            //       (termsPriceListDTO.PlValue * (termsPriceListDTO.CompCovPercentage / 100)) - termsPriceListDTO.PlValue3;
            //termsPriceListDTO.PlValue3 = termsPriceListDTO.PlValue -
            //       (termsPriceListDTO.PlValue * (termsPriceListDTO.CompCovPercentage / 100)) - termsPriceListDTO.PlValue2;
            /////////
            termsPriceListDTO.CompValue = termsPriceListDTO.PlValue * (termsPriceListDTO.CompCovPercentage / 100);
            ////////////////////////////////////

            #region EQUALS

            //if (termsPriceListDTO.PlValue2 == 0)
            //    termsPriceListDTO.CompCovPercentage = 100 - ((termsPriceListDTO.PlValue3 * termsPriceListDTO.ServBefDesc) / 100);
            //else
            //    termsPriceListDTO.CompCovPercentage = 100 - ((termsPriceListDTO.PlValue2 * termsPriceListDTO.ServBefDesc) /100) - ((termsPriceListDTO.PlValue3 * termsPriceListDTO.ServBefDesc) / 100);
            ////////////////////////////////////////
            //if (termsPriceListDTO.CompCovPercentage == 0)
            //    termsPriceListDTO.PlValue2 = termsPriceListDTO.ServBefDesc - termsPriceListDTO.PlValue3;
            //else
            //    termsPriceListDTO.PlValue2 = termsPriceListDTO.ServBefDesc - 
            //        (termsPriceListDTO.ServBefDesc * (termsPriceListDTO.CompCovPercentage / 100)) - termsPriceListDTO.PlValue3;
            ///////////////////////////////////////
            //if (termsPriceListDTO.PlValue3 == 0)
            //    termsPriceListDTO.PlValue2 = termsPriceListDTO.ServBefDesc - (termsPriceListDTO.ServBefDesc * (termsPriceListDTO.CompCovPercentage / 100));
            //else
            //    termsPriceListDTO.PlValue2 = termsPriceListDTO.ServBefDesc -
            //        (termsPriceListDTO.ServBefDesc * (termsPriceListDTO.CompCovPercentage / 100)) - termsPriceListDTO.PlValue3;

            #endregion
            termsPriceListDTO.PLId = id;
            PriceListDetail priceListDetail = _mapper.Map<PriceListDetail>(termsPriceListDTO);

            
            await _Context.AddAsync(priceListDetail);
            await _Context.SaveChangesAsync();
        }

        public async Task<TermsPriceListEditAddDTO> GetTermPriceListByIdAsync(int id)
        {
            PriceListDetail priceListDetail = await _Context.PriceListDetails
               .FirstOrDefaultAsync(obj => obj.PLDtlId == id);
            TermsPriceListEditAddDTO termsPriceListDTO = _mapper.Map<TermsPriceListEditAddDTO>(priceListDetail);

            List<MainClinic> mainClinics = await _Context.MainClinics.ToListAsync();
            List<SubClinic> subClinics = await _Context.SubClinics.ToListAsync();
            List<ServiceClinic> serviceClinics = await _Context.ServiceClinics.ToListAsync();

            termsPriceListDTO.MainClinics = _mapper.Map<List<MainClinicViewDTO>>(mainClinics);
            termsPriceListDTO.SubClinics = _mapper.Map<List<SubClinicViewDTO>>(subClinics);
            termsPriceListDTO.ServiceClinics = _mapper.Map<List<ServiceClinicViewDTO>>(serviceClinics);

            return termsPriceListDTO;
        }

        public async Task EditDoctorPercentAsync(int id, TermsPriceListEditAddDTO termsPriceListDTO)
        {
            PriceListDetail priceListDetail = await _Context.PriceListDetails.FirstOrDefaultAsync(obj => obj.PLDtlId == id);

            termsPriceListDTO.PlValue = termsPriceListDTO.ServBefDesc - (termsPriceListDTO.ServBefDesc * (termsPriceListDTO.DiscoutComp / 100));
            //////////
                //termsPriceListDTO.CompCovPercentage = 100 - ((termsPriceListDTO.PlValue2 / termsPriceListDTO.PlValue) * 100) - ((termsPriceListDTO.PlValue3 / termsPriceListDTO.PlValue) * 100);
                //termsPriceListDTO.PlValue2 = termsPriceListDTO.PlValue -
                //       (termsPriceListDTO.PlValue * (termsPriceListDTO.CompCovPercentage / 100)) - termsPriceListDTO.PlValue3;
                //termsPriceListDTO.PlValue3 = termsPriceListDTO.PlValue -
                //       (termsPriceListDTO.PlValue * (termsPriceListDTO.CompCovPercentage / 100)) - termsPriceListDTO.PlValue2;
            /////////
            termsPriceListDTO.CompValue = termsPriceListDTO.PlValue * (termsPriceListDTO.CompCovPercentage / 100);
            ////////////////////////////////////
            termsPriceListDTO.PLId = priceListDetail.PLId;
            _mapper.Map(termsPriceListDTO, priceListDetail);
            _Context.Update(priceListDetail);
            await _Context.SaveChangesAsync();
        }
    }
}
