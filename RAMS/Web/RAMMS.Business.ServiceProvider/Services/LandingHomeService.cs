using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class LandingHomeService : ILandingHomeService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IDDLookUpRepository _repolookup;
        public LandingHomeService(IRepositoryUnit repoUnit, IDDLookUpRepository repoLookup)
        {
            _repoUnit = repoUnit;
            _repolookup = repoLookup;
        }        
        public async Task<int> getNCNActiveCount()
        {
            return await _repoUnit.FormN1Repository.GetActiveCount();
        }
        public async Task<List<UvwSearchData>> GlobalSearchData(string keyWord)
        {
            return await _repolookup.GlobalSearchData(keyWord);
        }
        public async Task<int> getNCRActiveCount()
        {
            return await _repoUnit.FormN2Repository.GetActiveCount();
        }


        // Temp
        public async Task<int> GetNodActiveCount(LandingHomeRequestDTO requestDTO)
        {
            int ActiveCount = 0;
           try
            {
                if(requestDTO.RMU != null || requestDTO.Section != null)
                {
                    if (requestDTO.Section != null)
                    {
                        int formACount = await _repoUnit.FormARepository.GetNodActiveSectionCount(requestDTO.Section);
                        ActiveCount = ActiveCount + formACount;

                        int formJCount = await _repoUnit.FormJRepository.GetActiveSectionCount(requestDTO.Section);
                        ActiveCount = ActiveCount + formACount;

                        int formHCount = await _repoUnit.FormHRepository.GetNodActiveSectionCount(requestDTO.Section);
                        ActiveCount = ActiveCount + formACount;
                    }
                    else
                    {
                        
                        foreach (var rmu in requestDTO.RMU)
                        {
                            int formACount = await _repoUnit.FormARepository.GetNodActiveRMUCount(rmu);
                            ActiveCount = ActiveCount + formACount;

                            int formJCount = await _repoUnit.FormJRepository.GetNodActiveRMUCount(rmu);
                            ActiveCount = ActiveCount + formACount;

                            int formHCount = await _repoUnit.FormHRepository.GetNodActiveRMUCount(rmu);
                            ActiveCount = ActiveCount + formACount;
                        }
                    }
                }
                else
                {
                    int formACount = await _repoUnit.FormARepository.GetActiveFormARecord();
                    ActiveCount = ActiveCount + formACount;

                    int formJCount = await _repoUnit.FormJRepository.GetActiveFormJRecord();
                    ActiveCount = ActiveCount + formACount;

                    int formHCount = await _repoUnit.FormHRepository.GetActiveFormHRecord();
                    ActiveCount = ActiveCount + formACount;
                }
                
                return ActiveCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<LandingHomeResponseDTO> GetHomeActiveCount(LandingHomeRequestDTO requestDTO)
        {
            var result = new LandingHomeResponseDTO();
            int NodActiveCount = 0;
            int NcnCount = 0;
            int NcrCount = 0;
            try
            {
                if (requestDTO.RMU != null || requestDTO.Section != null)
                {
                    if (requestDTO.Section != null)
                    {
                        int formACount = await _repoUnit.FormARepository.GetNodActiveSectionCount(requestDTO.Section);
                        NodActiveCount += formACount;

                        int formJCount = await _repoUnit.FormJRepository.GetActiveSectionCount(requestDTO.Section);
                        NodActiveCount += formJCount;

                        int formHCount = await _repoUnit.FormHRepository.GetNodActiveSectionCount(requestDTO.Section);
                        NodActiveCount += formHCount;

                       List<string> SectionBasedRoadCode = await _repoUnit.RoadmasterRepository.GetRdCodeBySection(requestDTO.Section);
                        if(SectionBasedRoadCode.Count != 0)
                        {
                             int NcN = await _repoUnit.FormN1Repository.GetActiveRdCodeCount(SectionBasedRoadCode);
                             NcnCount += NcN;
                             int Ncr = await _repoUnit.FormN2Repository.GetActiveRdCodeCount(SectionBasedRoadCode);
                             NcrCount += Ncr;
                        }


                    }
                    else
                    {
                        foreach (var rmu in requestDTO.RMU)
                        {
                            int formACount = await _repoUnit.FormARepository.GetNodActiveRMUCount(rmu);
                            NodActiveCount += formACount;

                            int formJCount = await _repoUnit.FormJRepository.GetNodActiveRMUCount(rmu);
                            NodActiveCount += formJCount;

                            int formHCount = await _repoUnit.FormHRepository.GetNodActiveRMUCount(rmu);
                            NodActiveCount += formHCount;

                            int NcN = await _repoUnit.FormN1Repository.GetActiveRmuBasedCount(rmu);
                            NcnCount += NcN;

                            int Ncr = await _repoUnit.FormN2Repository.GetActiveRmuBasedCount(rmu);
                            NcrCount += Ncr;
                        }
                    }

                }
                else
                {
                    int formACount = await _repoUnit.FormARepository.GetActiveFormARecord();
                    NodActiveCount += formACount;

                    int formJCount = await _repoUnit.FormJRepository.GetActiveFormJRecord();
                    NodActiveCount += formJCount;

                    int formHCount = await _repoUnit.FormHRepository.GetActiveFormHRecord();
                    NodActiveCount += formHCount;

                    int Ncn = await _repoUnit.FormN1Repository.GetActiveCount();
                    NcnCount += Ncn;

                    int Ncr = await _repoUnit.FormN2Repository.GetActiveCount();
                    NcrCount += Ncr;
                }

                result.LandingNodCount = NodActiveCount;
                result.landingNcnCount = NcnCount;
                result.LandingNcrCount = NcrCount;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetSectionByRMU(LandingHomeRequestDTO requestDTO)
        {
            try
            {
                RoadMasterRequestDTO ddLookUp = new RoadMasterRequestDTO();
                var selectListItems = new List<SelectListItem>();

                if (requestDTO.RMU != null)
                {
                    foreach (var rmu in requestDTO.RMU)
                    {
                        ddLookUp.RmuCode = rmu;
                        var listItems = await _repoUnit.RoadmasterRepository.GetSectionByRMU(ddLookUp);
                        foreach (var item in listItems)
                        {
                            selectListItems.Add(new SelectListItem
                            {
                                Value = item.ToString(),
                                Text = item.ToString()
                            });
                        }
                    }
                }
                else
                {
                    var listItems = await _repoUnit.RoadmasterRepository.GetSectionByRMU(ddLookUp);
                    foreach (var item in listItems)
                    {
                        selectListItems.Add(new SelectListItem
                        {
                            Value = item.ToString(),
                            Text = item.ToString()
                        });
                    }
                }
                return selectListItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
