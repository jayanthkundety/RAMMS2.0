using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IDDLookUpRepository : IRepositoryBase<RmDdLookup>
    {
        Task<IEnumerable<RmDdLookup>> GetDdLookUp(DDLookUpDTO rmDdLookup);
        Task<string> GetDesc(DDLookUpDTO rmDdLookup);
        Task<IEnumerable<RmDdLookup>> GetRMUBasedSection(DDLookUpDTO dDLookUp);
        Task<IEnumerable<SelectListItem>> GetDefCode();
        FormAssetTypesDTO GetFormAssetTypes(string typeCode);
        Task<List<UvwSearchData>> GlobalSearchData(string keyWord);
        string GetConcatenateDdlTypeDesc(DDLookUpDTO dto);
        string GetConcatenateDdlTypeValue(DDLookUpDTO dto);
    }
}
