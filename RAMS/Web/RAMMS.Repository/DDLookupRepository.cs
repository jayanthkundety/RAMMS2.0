using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RAMMS.Domain;
using RAMMS.Repository.Interfaces;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository;

namespace RAMS.Repository
{

    public class DDLookupRepository : RepositoryBase<RmDdLookup>, IDDLookUpRepository
    {
        public DDLookupRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }
        //DDLookUp
        public async Task<IEnumerable<RmDdLookup>> GetDdLookUp(DDLookUpDTO rmDdLookup)
        {
            if (rmDdLookup.TypeCode != null && rmDdLookup.TypeCode != "")
            {
                if (rmDdLookup.Type == "B1B2_Severity")
                {
                    return await _context.RmDdLookup.Where(x => x.DdlType == rmDdLookup.Type && x.DdlTypeCode == rmDdLookup.TypeCode && x.DdlActiveYn == true).OrderBy(a => Convert.ToInt32(a.DdlTypeValue)).ToListAsync();
                }
                else if (rmDdLookup.Type == "B1B2_Distress")
                {
                    return await _context.RmDdLookup.Where(x => x.DdlType == rmDdLookup.Type && x.DdlTypeCode == rmDdLookup.TypeCode && x.DdlActiveYn == true).OrderBy(a => a.DdlTypeValue).ToListAsync();
                }
                else
                {
                    return await _context.RmDdLookup.Where(x => x.DdlType == rmDdLookup.Type && x.DdlTypeCode == rmDdLookup.TypeCode && x.DdlActiveYn == true).ToListAsync();
                }

            }
            else
            {
                if (rmDdLookup.Type == "Year")
                {
                    return await _context.RmDdLookup.Where(a => a.DdlType == rmDdLookup.Type && a.DdlActiveYn == true).OrderBy(a => a.DdlTypeDesc).ToListAsync();
                }
                else
                {
                    return await _context.RmDdLookup.Where(a => a.DdlType == rmDdLookup.Type && a.DdlActiveYn == true).ToListAsync();
                }

            }
        }

        public async Task<IEnumerable<SelectListItem>> GetDefCode()
        {
            var defcode = await (from x in _context.RmAssetDefectCode
                                 select new SelectListItem
                                 {
                                     Text = x.AdcDefCode + " - " + x.AdcDefName,
                                     Value = x.AdcDefCode
                                 }).Distinct().ToListAsync();
            return defcode;
        }

        public async Task<string> GetDesc(DDLookUpDTO rmDdLookup)
        {
            if (rmDdLookup.TypeDesc != null && rmDdLookup.TypeCode != null)
            {
                return await _context.RmDdLookup.Where(x => x.DdlTypeCode == rmDdLookup.TypeCode && x.DdlTypeDesc == rmDdLookup.TypeDesc).Select(x => x.DdlTypeValue).FirstOrDefaultAsync();
            }
            else if (rmDdLookup.TypeCode != null)
            {
                return await _context.RmDdLookup.Where(x => x.DdlTypeCode == rmDdLookup.TypeCode).Select(x => x.DdlTypeDesc).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<IEnumerable<RmDdLookup>> GetRMUBasedSection(DDLookUpDTO dDLookUp)
        {
            if (dDLookUp.TypeValue != null)
            {
                return await _context.RmDdLookup.Where(x => x.DdlTypeValue == dDLookUp.TypeValue && x.DdlType == "Section Code").ToListAsync();
            }
            else
            {
                return await _context.RmDdLookup.Where(x => x.DdlType == "Section Code").ToListAsync();
            }
        }
        /// <summary>
        /// Get Form FC Asset Types (ELM,CLM and CW)
        /// </summary>
        /// <returns>IList<Dictionary<string, List<Dictionary<string, string>>>> --> Collection of key values, Key --> DdlTypeCode, values --> Collection of DdlPkRefNo, DdlTypeDesc and DdlTypeValue </returns>
        public FormAssetTypesDTO GetFormAssetTypes(string typeCode)
        {

            FormAssetTypesDTO result = new FormAssetTypesDTO();
            Dictionary<string, string> keyval = null;
            List<Dictionary<string, string>> refAssest = null;
            _context.RmDdLookup.Where(x => x.DdlType == "Asset Type" && (typeCode).Contains(x.DdlTypeCode)).ToList().ForEach((RmDdLookup lookup) =>
            {
                if (!result.Keys.Contains(lookup.DdlTypeCode)) { refAssest = new List<Dictionary<string, string>>(); result.Add(lookup.DdlTypeCode, refAssest); }
                else { refAssest = result[lookup.DdlTypeCode]; }
                keyval = new Dictionary<string, string>();
                keyval.Add("Id", lookup.DdlPkRefNo.ToString());
                keyval.Add("Desc", lookup.DdlTypeDesc);
                keyval.Add("Value", lookup.DdlTypeValue);
                refAssest.Add(keyval);
            });
            return result;
        }
        public async Task<List<UvwSearchData>> GlobalSearchData(string keyWord)
        {
            return await _context.UvwSearchData.Where(x => x.RefId.Contains(keyWord)).ToListAsync();
        }

        public string GetConcatenateDdlTypeDesc(DDLookUpDTO dto)
        {
            var arr = _context.RmDdLookup.Where(s => s.DdlType == dto.Type && s.DdlTypeCode == dto.TypeCode).Select(s => " " + s.DdlTypeDesc + " ").ToArray();
            if (arr != null)
            {
                return string.Join('/', arr);
            }
            else
            {
                return "";
            }
        }

        public string GetConcatenateDdlTypeValue(DDLookUpDTO dto)
        {
            var arr = _context.RmDdLookup.Where(s => s.DdlType == dto.Type && s.DdlTypeCode == dto.TypeCode).Select(s => " " + s.DdlTypeValue + " ").ToArray();
            if (arr != null)
            {
                return string.Join('/', arr);
            }
            else
            {
                return "";
            }
        }
    }
}



