using System;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using System.Reflection;

namespace RAMMS.Repository
{
    public class DivisonRepository : RepositoryBase<RmDivisionMaster>, IDivisonRepository
    {
        public DivisonRepository(RAMMSContext context) : base(context) { _context = context ?? throw new ArgumentNullException(nameof(context)); }
        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<DivisionRequestDTO> filterOptions)
        {
            return await (from s in _context.RmDivisionMaster where s.DivIsActive == true select s).LongCountAsync();
        }

        public async Task<List<SelectListItem>> DivisionList()
        {
            List<SelectListItem> selectListItemList = new List<SelectListItem>();
            return await _context.RmDivRmuSecMaster.Where(o => o.RdsmActiveYn == true)
                 .Select(s => new SelectListItem
                 {
                     Value = s.RdsmDivCode,
                     Text = s.RdsmDivision
                 }).Distinct().ToListAsync();
        }

        public List<SelectListItem> RMUListByDivCode(string divCode)
        {
            List<SelectListItem> selectListItemList = new List<SelectListItem>();
            return _context.RmDivRmuSecMaster.Where(o => o.RdsmDivCode == divCode && o.RdsmActiveYn == true)
            .Select(s => new SelectListItem
             {
                 Value = s.RdsmRmuCode,
                 Text = s.RdsmRmuName
             }).Distinct().ToList();
        }

        public async Task<List<DivisionRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<DivisionRequestDTO> filterOptions)
        {
            var query = (from s in _context.RmDivisionMaster where s.DivIsActive == true select s);
            if (!string.IsNullOrEmpty(filterOptions.Filters.Code))
            {
                query = query.Where(s =>
                s.DivCode.Contains(filterOptions.Filters.Code)
                || s.DivName.Contains(filterOptions.Filters.Name));
            }

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 0)
                { query = query.OrderByDescending(s => s.DivPkRefNo); }
                if (filterOptions.ColumnIndex == 1) { query = query.OrderBy(s => s.DivCode); }
                if (filterOptions.ColumnIndex == 2) { query = query.OrderBy(s => s.DivName); }
                if (filterOptions.ColumnIndex == 3) { query = query.OrderBy(s => s.DivIsActive); }
            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 0)
                { query = query.OrderByDescending(s => s.DivPkRefNo); }
                if (filterOptions.ColumnIndex == 1) { query = query.OrderByDescending(s => s.DivCode); }
                if (filterOptions.ColumnIndex == 2) { query = query.OrderByDescending(s => s.DivName); }
                if (filterOptions.ColumnIndex == 3) { query = query.OrderByDescending(s => s.DivIsActive); }
            }
            var lst = await query.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToListAsync();
            return lst.Select(s => new DivisionRequestDTO
            {
                PkRefNo = s.DivPkRefNo,
                Code = s.DivCode,
                Name = s.DivName,
                Isactive = s.DivIsActive
            }).ToList();
        }

        public async Task<DivisionRequestDTO> GetDivisions()
        {
            var divDTO = new DivisionRequestDTO();
            divDTO.Divisions = new List<Division>();

            var divList = await _context.RmIwWorksDeptMaster.ToListAsync();

            foreach (var dpt in divList)
            {
                var div = new Division();
                div.ID = dpt.FiwWrksDeptId;
                div.Code = dpt.FiwWrksDeptCode;
                div.Name = dpt.FiwWrksDeptName;
                div.Adress1 = dpt.FiwWrksDeptAddress1;
                div.Adress2 = dpt.FiwWrksDeptAddress2;
                div.Adress3 = dpt.FiwWrksDeptAddress3;
                div.Phone = dpt.FiwWrksDeptPhoneNo.ToString();
                div.Fax = dpt.FiwWrksDeptFaxNo.ToString();
                div.ZipCode = dpt.FiwWrksDeptZipcode.ToString();
                divDTO.Divisions.Add(div);
            }

            return divDTO;

        }

        public async Task<DivisionRequestDTO> GetServiceProviders()
        {
            var proDTO = new DivisionRequestDTO();
            proDTO.ServiceProviders = new List<ServiceProvider>();

            var proList = await _context.RmIwSrvProviderMaster.ToListAsync();

            foreach (var prov in proList)
            {
                var div = new ServiceProvider();
                div.Code = prov.FiwSrvProviderCode;
                div.Name = prov.FiwSrvProviderName;
                div.Adress1 = prov.FiwSrvProviderAddress1;
                div.Adress2 = prov.FiwSrvProviderAddress2;
                div.Adress3 = prov.FiwSrvProviderAddress3;
                div.Phone = prov.FiwSrvProviderPhoneNo.ToString();
                div.Fax = prov.FiwSrvProviderFaxNo.ToString();
                div.ZipCode = prov.FiwSrvProviderZipcode.ToString();
                proDTO.ServiceProviders.Add(div);
            }

            return proDTO;

        }


    }
}
