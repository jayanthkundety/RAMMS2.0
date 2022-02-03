using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class RoadMasterService : IRoadMasterService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        public RoadMasterService(IRepositoryUnit repoUnit, IMapper mapper)
        {
            _repoUnit = repoUnit;
            _mapper = mapper;
        }
        public async Task<RoadMasterResponseDTO> GetRMAllData(RoadMasterRequestDTO _Rmroad)
        {
            RoadMasterResponseDTO roadmaster = new RoadMasterResponseDTO();
            var CurrentItem = await _repoUnit.RoadmasterRepository.GetAllRMData(_Rmroad);
            roadmaster = _mapper.Map<RoadMasterResponseDTO>(CurrentItem);
            return roadmaster;
        }

        public async Task<List<RoadMasterResponseDTO>> GetRMLookupData(RoadMasterRequestDTO _Rmroad)
        {
            List<RoadMasterResponseDTO> roadmasterlist = new List<RoadMasterResponseDTO>();
            if (_Rmroad.DivisionCode == "MIRI")
            {
                var lists = await _repoUnit.RoadmasterRepository.GetRMDataLookup(_Rmroad);
                foreach (var listdata in lists)
                {
                    roadmasterlist.Add(_mapper.Map<RoadMasterResponseDTO>(listdata));
                }
                return roadmasterlist;
            }
            return null;
        }

        public async Task<List<RoadMasterResponseDTO>> GetRMUBasedData(RoadMasterRequestDTO _Rmroad)
        {
            List<RoadMasterResponseDTO> roadmasterlist = new List<RoadMasterResponseDTO>();

            var roadLists = await _repoUnit.RoadmasterRepository.GetRMUBasedData(_Rmroad);
            foreach (var listdata in roadLists)
            {
                roadmasterlist.Add(_mapper.Map<RoadMasterResponseDTO>(listdata));
            }
            return roadmasterlist;
        }
        public async Task<List<RoadMasterResponseDTO>> GetRM_RoadCode_Service(RoadMasterRequestDTO _Rmroad)
        {
            List<RoadMasterResponseDTO> roadmasterlist = new List<RoadMasterResponseDTO>();

            var lists = await _repoUnit.RoadmasterRepository.GetRM_RoadCode_Data(_Rmroad);
            foreach (var listdata in lists)
            {
                roadmasterlist.Add(_mapper.Map<RoadMasterResponseDTO>(listdata));
            }
            return roadmasterlist;

        }
        public async Task<RoadMasterResponseDTO> GetAllRoadCodeData(RoadMasterRequestDTO _Rmroad)
        {
            RoadMasterResponseDTO roadmaster = new RoadMasterResponseDTO();
            var CurrentItem = await _repoUnit.RoadmasterRepository.GetAllRoadCodeData(_Rmroad);
            roadmaster = _mapper.Map<RoadMasterResponseDTO>(CurrentItem);
            return roadmaster;
        }

        public async Task<RoadMasterResponseDTO> GetAllRoadCodeDataBySectionCode(RoadMasterRequestDTO _Rmroad)
        {
            RoadMasterResponseDTO roadmaster = new RoadMasterResponseDTO();
            var CurrentItem = await _repoUnit.RoadmasterRepository.GetAllRoadCodeDataBySectionCode(_Rmroad);
            roadmaster = _mapper.Map<RoadMasterResponseDTO>(CurrentItem);
            return roadmaster;
        }


        public async Task<AssetDDLResponseDTO> GetAssetDDL(AssetDDLRequestDTO roadMaster)
        {
            try
            {
                AssetDDLResponseDTO roadlist = await _repoUnit.RoadmasterRepository.GetFilteredList(roadMaster);

                return roadlist;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public async Task<IEnumerable<SelectListItem>> GetroadCodeValuByRMU(RoadMasterRequestDTO roadMaster)
        {
            var result = new List<SelectListItem>();
            try
            {
                var ddList = await _repoUnit.RoadmasterRepository.GetRMUBasedData(roadMaster);
                foreach (var list in ddList)
                {
                    result.Add(new SelectListItem
                    {
                        Value = list.RdmRdCode.ToString(),
                        Text = (list.RdmRdCode + "-" + list.RdmRdName).ToString()
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CSelectListItem>> GetAllRoadCodeAndName(bool IsPKIDValue = false)
        {
            var result = new List<SelectListItem>();
            try
            {
                if (IsPKIDValue)
                    return await _repoUnit.RoadmasterRepository.FindAsync(x => x.RdmActiveYn == true, x => new CSelectListItem() { Text = x.RdmRdCode + "-" + x.RdmRdName, Value = x.RdmPkRefNo.ToString(), CValue = x.RdmRmuCode, Item1 = x.RdmRdName, PKId = x.RdmPkRefNo, Code = x.RdmRdCode, Item2 = x.RdmSecCode.ToString(), Item3 = (x.RdmLengthPaved + x.RdmLengthUnpaved).ToString(), FromKm = (int)x.RdmFrmCh, FromM=x.RdmFrmChDeci.ToString(), ToKm=(int)x.RdmToCh, ToM=x.RdmToChDeci.ToString() });
                else
                    return await _repoUnit.RoadmasterRepository.FindAsync(x=>x.RdmActiveYn==true, x => new CSelectListItem() { Text = x.RdmRdCode + "-" + x.RdmRdName, Value = x.RdmRdCode, CValue = x.RdmRmuCode, Item1 = x.RdmRdName, PKId = x.RdmPkRefNo, Code = x.RdmRdCode, Item2 = x.RdmSecCode.ToString(), Item3 = (x.RdmLengthPaved + x.RdmLengthUnpaved).ToString(), FromKm = (int)x.RdmFrmCh, FromM = x.RdmFrmChDeci.ToString(), ToKm = (int)x.RdmToCh, ToM = x.RdmToChDeci.ToString() });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllRoadCodeAndNameTab()
        {
            var result = new List<SelectListItem>();
            var ddList = await _repoUnit.RoadmasterRepository.GetAllRoadCode();
            foreach (var list in ddList)
            {
                result.Add(new SelectListItem
                {
                    Value = list.RdmRdCode.ToString(),
                    Text = (list.RdmRdCode + "-" + list.RdmRdName).ToString()
                });
            }
            return result;
        }

        public async Task<RoadMasterResponseDTO> GetByRdCode(string roadCode)
        {

                RoadMasterResponseDTO roadmaster = new RoadMasterResponseDTO();
                var CurrentItem = await _repoUnit.RoadmasterRepository.GetByRdCode(roadCode);
                roadmaster = _mapper.Map<RoadMasterResponseDTO>(CurrentItem);
                return roadmaster;
        }
    }
}

