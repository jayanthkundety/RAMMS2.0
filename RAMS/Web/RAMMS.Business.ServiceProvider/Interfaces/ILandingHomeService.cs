using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Services
{
    public interface ILandingHomeService
    {
        Task<int> GetNodActiveCount(LandingHomeRequestDTO requestDTO);
        Task<IEnumerable<SelectListItem>> GetSectionByRMU(LandingHomeRequestDTO requestDTO);
        Task<int> getNCNActiveCount();
        Task<int> getNCRActiveCount();
        Task<LandingHomeResponseDTO> GetHomeActiveCount(LandingHomeRequestDTO requestDTO);
        Task<List<UvwSearchData>> GlobalSearchData(string keyWord);
    }
}
