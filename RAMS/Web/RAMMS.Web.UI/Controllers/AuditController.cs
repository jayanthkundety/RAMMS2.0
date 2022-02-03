using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Web.UI.Models;

namespace RAMMS.Web.UI.Controllers
{
    public class AuditController : BaseController
    {
        private readonly IAuditActionService auditActionService;
        private readonly IAuditTransactionService auditTransactionService;
        public AuditController(IAuditActionService auditActionService, IAuditTransactionService auditTransactionService)
        {
            this.auditActionService = auditActionService;
            this.auditTransactionService = auditTransactionService;
        }

        public IActionResult Logs()
        {
            return View();
        }

        public async Task<IActionResult> GetAuditTransactionList(DataTableAjaxPostModel<AuditTransactionRequestDTO> searchData)
        {
            FilteredPagingDefinition<AuditTransactionRequestDTO> filteredPagingDefinition = new FilteredPagingDefinition<AuditTransactionRequestDTO>();
            filteredPagingDefinition.Filters = searchData.filterData ?? new AuditTransactionRequestDTO();
            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                filteredPagingDefinition.Filters.AlaPkRefNo = long.TryParse(Request.Form["columns[0][search][value]"].ToString(), out long a) ? a : 0;
            }
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await auditTransactionService.GetAuditTransactionList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        public async Task<IActionResult> GetAuditActionList(DataTableAjaxPostModel<AuditActionRequestDTO> searchData)
        {
            FilteredPagingDefinition<AuditActionRequestDTO> filteredPagingDefinition = new FilteredPagingDefinition<AuditActionRequestDTO>();
            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await auditActionService.GetAuditActionList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }
    }
}
