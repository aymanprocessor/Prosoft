﻿using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IClinicTransRepo
    {
        Task<IEnumerable<ClinicTran>> ClinicTransRangeFilter(string range);
        int ClinicTransCounts();
        int ClinicTransCountsDaily();
        int ClinicTransCountsWeekly();
        Task<List<ClinicTransViewDTO>> GetClinicTransByAdmissionAsync(int visitId, int flag);
        Task<ClinicTransEditAddDTO> GetClinicTransByIdAsync(int checkId);
        Task<ClinicTransEditAddDTO> GetEmptyClinicTransAsync();

        ///////////////////////////////////Get for Ajax//////////////////////////////////
        Task<List<SubClinicViewDTO>> GetSubClinic(int id);
        Task<List<SubItemViewDTO>> GetSubItem(int id);
        Task<List<ServiceClinicViewDTO>> GetServeClinic(int id);
        Task<TermsPriceListViewDTO> GetPricesDetails(int id, int clincID, int sClincID, int servID);
        Task<PatAdmissionEditAddDTO> GetPatAdmissionByIdAsync(int id);
        Task<decimal> GetPricesOfServices(int visitId, int flag);
        Task<decimal> GetAllDepositForVisit(int visitId);
        Task<ServiceClinicViewDTO> GetServiceClinicByIDs(int id, int sClincID, int servID);
        Task<DoctorPrecentViewDTO> GetDoctorPrices(int id, int sClincID, int servID);
        /////////////////////////////////////////////////////////////////////////////////
        Task AddClinicTransAsync(int visitId, int flag, ClinicTransEditAddDTO clinicTransDTO);
        Task AddClinicTransListAsync(int visitId, int flag, List<ClinicTransEditAddDTO> clinicTransDTOList);

        Task EditClinicTransAsync(int checkId, ClinicTransEditAddDTO clinicTransDTO);
        Task EditClinicTransBatchAsync( List<ClinicTransEditAddDTO> clinicTransDTO);
        Task DeleteClinicTransAsync(int id);
    }
}
