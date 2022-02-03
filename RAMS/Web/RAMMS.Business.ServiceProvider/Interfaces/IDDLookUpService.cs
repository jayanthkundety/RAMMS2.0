using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IDDLookUpService
    {
        Task<IEnumerable<SelectListItem>> GetDdLookup(DDLookUpDTO DdLookUp);
        Task<IEnumerable<SelectListItem>> GetDdTempLookup(DDLookUpDTO DdLookUp);
        Task<IEnumerable<SelectListItem>> GetLookUpCodeDesc(DDLookUpDTO DdLookUp);
        Task<IEnumerable<SelectListItem>> GetLookUpCodeText(DDLookUpDTO DdLookUp);
        Task<IEnumerable<SelectListItem>> GetDdDescValue(DDLookUpDTO DdLookUp);
        Task<string> GetDDLValueforTypeAndDesc(DDLookUpDTO DdLookUp);
        Task<IEnumerable<SelectListItem>> GetLookUpValueDesc(DDLookUpDTO DdLookUp);
        Task<IEnumerable<SelectListItem>> GetLookUpCodeTextConcat(DDLookUpDTO DdLookUp);
        Task<IEnumerable<SelectListItem>> GetLookUpTextDescConcat(DDLookUpDTO DdLookUp);
        Task<IEnumerable<SelectListItem>> GetLookUpTextConcat(DDLookUpDTO DdLookUp);
        Task<IEnumerable<SelectListItem>> GetLookUpCodeDescription(DDLookUpDTO DdLookUp);
        Task<IEnumerable<CSelectListItem>> GetDdLookup(params string[] type);
        Task<IEnumerable<SelectListItem>> GetAllDefectCode();
        Task<IEnumerable<SelectListItem>> GetLookUpIdDesc(DDLookUpDTO DdLookUp);
        Task<IEnumerable<SelectListItem>> GetLookUpIdValueDesc(DDLookUpDTO DdLookUp);
        Task<IEnumerable<SelectListItem>> GetDdLookupValue(DDLookUpDTO DdLookUp);
        Task<IEnumerable<RmDdLookup>> GetLookups(DDLookUpDTO DdLookUp);
        object GetDdlLookupByCode(string TypeCode);
    }
}
