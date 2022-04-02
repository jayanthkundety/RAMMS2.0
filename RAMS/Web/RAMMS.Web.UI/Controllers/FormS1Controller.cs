using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static RAMMS.DTO.RequestBO.FormS1DetailDTO;

namespace RAMMS.Web.UI.Controllers
{
    [CAuthorize(ModuleName = ModuleNameList.Asset_Maintenance)]
    public class FormS1Controller : Models.BaseController
    {
        private IFormS1Service serFormS1;
        private ISecurity _security;
        private IWebHostEnvironment _environment;
        public FormS1Controller(IFormS1Service service, ISecurity security, IUserService userService, IWebHostEnvironment webhostenvironment)
        {
            serFormS1 = service;
            _security = security;
            _environment = webhostenvironment;
        }
        public IActionResult Index()
        {
            LoadLookupService("RMU", "Week No");

            var grid = new Models.CDataTable() { Name = "tblFS1HeaderGrid", APIURL = "/FormS1/HeaderList", LeftFixedColumn = 1 };
            grid.IsModify = _security.IsPCModify(ModuleNameList.Asset_Maintenance);
            grid.IsDelete = _security.IsPCDelete(ModuleNameList.Asset_Maintenance);
            grid.IsView = _security.IsPCView(ModuleNameList.Asset_Maintenance);
            grid.Columns.Add(new CDataColumns() { data = null, title = "Action", IsFreeze = true, sortable = false, render = "formS1.HeaderGrid.ActionRender" });
            grid.Columns.Add(new CDataColumns() { data = "RefID", title = "Reference No" });
            grid.Columns.Add(new CDataColumns() { data = "RMUCode", title = "RMU Abbreviation" });
            grid.Columns.Add(new CDataColumns() { data = "RMUDesc", title = "RMU Name" });
            grid.Columns.Add(new CDataColumns() { data = "DateOfEntry", title = "Date of Entry", render = "formS1.HeaderGrid.DateOfEntry" });
            grid.Columns.Add(new CDataColumns() { data = "WeekNo", title = "Week No" });
            grid.Columns.Add(new CDataColumns() { data = "PlanByName", title = "Planned By" });
            grid.Columns.Add(new CDataColumns() { data = "VetByName", title = "Vetted By" });
            grid.Columns.Add(new CDataColumns() { data = "AgrByName", title = "Agreed By" });
            grid.Columns.Add(new CDataColumns() { data = "ProcessStatus", title = "Status" });
            return View("~/Views/MAM/FormS1/FormS1.cshtml", grid);
        }
        public async Task<JsonResult> HeaderList(DataTableAjaxPostModel searchData)
        {
            return Json(await serFormS1.GetHeaderGrid(searchData), JsonOption());
        }
        public async Task<JsonResult> ListDetail(int id, DataTableAjaxPostModel searchData)
        {
            return Json(await serFormS1.GetDetailsGrid(id, searchData), JsonOption());
        }

        public IActionResult Add()
        {
            ViewBag.IsEdit = true;
            return ViewRequest(0);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.IsEdit = true;
            return id > 0 ? ViewRequest(id) : RedirectToAction("404", "Error");
        }
        public IActionResult View(int id)
        {
            ViewBag.IsEdit = false;
            return id > 0 ? ViewRequest(id) : RedirectToAction("404", "Error");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id > 0) { return Ok(new { id = serFormS1.DeleteFormS1Hdr(id) }); }
            else { return BadRequest("Invalid Request!"); }

        }
        [HttpPost]
        public IActionResult DeleteDetail(int id)
        {
            if (id > 0) { return Ok(new { id = serFormS1.DeleteDetails(id) }); }
            else { return BadRequest("Invalid Request!"); }

        }
        private IActionResult ViewRequest(int id)
        {
            FormS1HeaderRequestDTO headerDetails = null;
            DateTime? dtWStart = null;
            if (id > 0)
            {
                headerDetails = serFormS1.FindHeaderByID(id);
                dtWStart = headerDetails.FromDt;
            }
            else
            {
                headerDetails = new FormS1HeaderRequestDTO();
                headerDetails.Status = "";
            }
            LoadLookupService("RMU", "Week No", "User", "FS1-StatusLegend");

            var grid = new Models.CDataTable() { Name = "tblFS1DetailGrid", APIURL = "/FormS1/ListDetail/" + id.ToString(), LeftFixedColumn = 1 };
            if (!ViewBag.IsEdit)
            {
                grid.IsModify = false;
                grid.IsDelete = false;
            }
            else
            {
                grid.IsModify = _security.IsPCModify(ModuleNameList.Asset_Maintenance);
                grid.IsDelete = _security.IsPCDelete(ModuleNameList.Asset_Maintenance);
            }
            grid.IsView = _security.IsPCView(ModuleNameList.Asset_Maintenance);
            grid.Columns.Add(new CDataColumns() { data = null, title = "Action", IsFreeze = true, sortable = false, render = "formS1.DetailGrid.ActionRender" });
            grid.Columns.Add(new CDataColumns() { data = "RefID", title = "Reference No", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "ACode", title = "Activity Code", ColumnGroup = "Road Details", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "RCode", title = "Code", ColumnGroup = "Road Details", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "RName", title = "Name", ColumnGroup = "Road Details", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "CHFrom", title = "From", ColumnGroup = "Chainage", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "CHTo", title = "To", ColumnGroup = "Chainage", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "SiteRef", title = "Site Ref.", ColumnGroup = "From Form A", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "Priority", title = "Pr.", ColumnGroup = "From Form A", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "WorkQty", title = "Work Qty", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "CDR", title = "CDR", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "CrewSupName", title = "Crew Supervisor", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "Mon", render = "formS1.DetailGrid.DaySchedule", title = "Mon(" + (dtWStart.HasValue ? dtWStart.Value.ToString("d-M") : "???") + ")", ColumnGroup = "Actual Day Scheduled", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "Tue", render = "formS1.DetailGrid.DaySchedule", title = "Tue(" + (dtWStart.HasValue ? dtWStart.Value.AddDays(1).ToString("d-M") : "???") + ")", ColumnGroup = "Actual Day Scheduled", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "Wed", render = "formS1.DetailGrid.DaySchedule", title = "Wed(" + (dtWStart.HasValue ? dtWStart.Value.AddDays(2).ToString("d-M") : "???") + ")", ColumnGroup = "Actual Day Scheduled", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "Thu", render = "formS1.DetailGrid.DaySchedule", title = "Thu(" + (dtWStart.HasValue ? dtWStart.Value.AddDays(3).ToString("d-M") : "???") + ")", ColumnGroup = "Actual Day Scheduled", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "Fri", render = "formS1.DetailGrid.DaySchedule", title = "Fri(" + (dtWStart.HasValue ? dtWStart.Value.AddDays(4).ToString("d-M") : "???") + ")", ColumnGroup = "Actual Day Scheduled", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "Sat", render = "formS1.DetailGrid.DaySchedule", title = "Sat(" + (dtWStart.HasValue ? dtWStart.Value.AddDays(5).ToString("d-M") : "???") + ")", ColumnGroup = "Actual Day Scheduled", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "Sun", render = "formS1.DetailGrid.DaySchedule", title = "Sun(" + (dtWStart.HasValue ? dtWStart.Value.AddDays(6).ToString("d-M") : "???") + ")", ColumnGroup = "Actual Day Scheduled", sortable = false });
            grid.Columns.Add(new CDataColumns() { data = "Remarks", title = "Remarks", sortable = false });

            return View("~/Views/MAM/FormS1/AddFormS1.cshtml", new Tuple<FormS1HeaderRequestDTO, CDataTable>(headerDetails, grid));
        }
        [HttpPost]
        public JsonResult Save(FormS1HeaderRequestDTO formS1)
        {
            return SaveAll(formS1, false);
        }
        [HttpPost]
        public JsonResult Submit(FormS1HeaderRequestDTO formS1)
        {
            formS1.SubmitSts = true;
            return SaveAll(formS1, true);
        }
        private JsonResult SaveAll(FormS1HeaderRequestDTO formS1, bool updateSubmit)
        {
            formS1.CrBy = _security.UserID;
            formS1.ModBy = _security.UserID;
            formS1.ModDt = DateTime.UtcNow;
            formS1.CrDt = DateTime.UtcNow;
            var result = serFormS1.SaveHeader(formS1, updateSubmit);
            return Json(new { RefNo = result.RefId, Id = result.PkRefNo }, JsonOption());
        }
        [HttpPost]
        public JsonResult FindDetails(FormS1HeaderRequestDTO header)
        {
            FormS1HeaderRequestDTO formS1 = serFormS1.FindDetails(header);
            if (formS1 == null || formS1.PkRefNo == 0)
            {
                header.CrBy = _security.UserID;
                header.ModBy = _security.UserID;
                header.ModDt = DateTime.UtcNow;
                header.CrDt = DateTime.UtcNow;
                formS1 = serFormS1.SaveHeader(header, false);
            }
            return Json(formS1, JsonOption());
        }
        [HttpPost]
        public async Task<IActionResult> LoadDetails(int id)
        {
            FormS1DetailDTO formS1Details = null;
            if (id > 0)
            {
                formS1Details = serFormS1.FindDetailsById(id);
                //var s1refno = formS1Details.RefId.Split('/');
                //List<ActWeekDtl> formD = await serFormS1.GetFormDDetails(formS1Details.RoadCode, formS1Details.ActCode, formS1Details.FrmChKm.ToString(), formS1Details.FrmChM, formS1Details.ToChKm.ToString(), formS1Details.ToChM, formS1Details.CrewSupervisor.ToString(), s1refno[4]);

                //formS1Details.actWeekDtls = new List<ActWeekDtl>();

                //foreach (var list in formD)
                //{
                //    formS1Details.actWeekDtls.Add(list);
                //}

                //foreach(var imp in formD)
                //{
                //    foreach(var wekdata in formS1Details.WkDtl)
                //    {
                //        if(imp.FormDFdhDay== "Monday" && wekdata.SchldDayOfWeek==1)
                //        {
                //            wekdata.FormDFddWorkSts = imp.FormDFddWorkSts;
                //            wekdata.FormDFdhDay = imp.FormDFdhDay;
                //            imp.updtWekstus = true;
                //        }
                //        if (imp.FormDFdhDay == "Tuesday" && wekdata.SchldDayOfWeek == 2)
                //        {
                //            wekdata.FormDFddWorkSts = imp.FormDFddWorkSts;
                //            wekdata.FormDFdhDay = imp.FormDFdhDay;
                //            imp.updtWekstus = true;
                //        }
                //        if (imp.FormDFdhDay == "Wednesday" && wekdata.SchldDayOfWeek == 3)
                //        {
                //            wekdata.FormDFddWorkSts = imp.FormDFddWorkSts;
                //            wekdata.FormDFdhDay = imp.FormDFdhDay;
                //            imp.updtWekstus = true;
                //        }
                //        if (imp.FormDFdhDay == "Thursday" && wekdata.SchldDayOfWeek == 4)
                //        {
                //            wekdata.FormDFddWorkSts = imp.FormDFddWorkSts;
                //            wekdata.FormDFdhDay = imp.FormDFdhDay;
                //            imp.updtWekstus = true;
                //        }
                //        if (imp.FormDFdhDay == "Friday" && wekdata.SchldDayOfWeek == 5)
                //        {
                //            wekdata.FormDFddWorkSts = imp.FormDFddWorkSts;
                //            wekdata.FormDFdhDay = imp.FormDFdhDay;
                //            imp.updtWekstus = true;
                //        }
                //        if (imp.FormDFdhDay == "Saturday" && wekdata.SchldDayOfWeek == 6)
                //        {
                //            wekdata.FormDFddWorkSts = imp.FormDFddWorkSts;
                //            wekdata.FormDFdhDay = imp.FormDFdhDay;
                //            imp.updtWekstus = true;
                //        }
                //        if (imp.FormDFdhDay == "Sunday" && wekdata.SchldDayOfWeek == 0)
                //        {
                //            wekdata.FormDFddWorkSts = imp.FormDFddWorkSts;
                //            wekdata.FormDFdhDay = imp.FormDFdhDay;
                //            imp.updtWekstus = true;
                //        }
                //    }

                //}
                //foreach(var updt in formD)
                //{
                //    if(updt.updtWekstus==false)
                //    {
                //        if(updt.FormDFdhDay== "Monday")
                //        {
                //            formS1Details.WkDtl.Add(new FormS1WkDtlDTO { Actual = null, Planned = null, FormDFddWorkSts = updt.FormDFddWorkSts, FormDFdhDay = updt.FormDFdhDay, SchldDayOfWeek=1 });
                //        }
                //        if (updt.FormDFdhDay == "Tuesday")
                //        {
                //            formS1Details.WkDtl.Add(new FormS1WkDtlDTO { Actual = null, Planned = null, FormDFddWorkSts = updt.FormDFddWorkSts, FormDFdhDay = updt.FormDFdhDay, SchldDayOfWeek = 2 });
                //        }
                //        if (updt.FormDFdhDay == "Wednesday")
                //        {
                //            formS1Details.WkDtl.Add(new FormS1WkDtlDTO { Actual = null, Planned = null, FormDFddWorkSts = updt.FormDFddWorkSts, FormDFdhDay = updt.FormDFdhDay, SchldDayOfWeek = 3 });
                //        }
                //        if (updt.FormDFdhDay == "Thursday")
                //        {
                //            formS1Details.WkDtl.Add(new FormS1WkDtlDTO { Actual = null, Planned = null, FormDFddWorkSts = updt.FormDFddWorkSts, FormDFdhDay = updt.FormDFdhDay, SchldDayOfWeek = 4 });
                //        }
                //        if (updt.FormDFdhDay == "Friday")
                //        {
                //            formS1Details.WkDtl.Add(new FormS1WkDtlDTO { Actual = null, Planned = null, FormDFddWorkSts = updt.FormDFddWorkSts, FormDFdhDay = updt.FormDFdhDay, SchldDayOfWeek = 5 });
                //        }
                //        if (updt.FormDFdhDay == "Saturday")
                //        {
                //            formS1Details.WkDtl.Add(new FormS1WkDtlDTO { Actual = null, Planned = null, FormDFddWorkSts = updt.FormDFddWorkSts, FormDFdhDay = updt.FormDFdhDay, SchldDayOfWeek = 6 });
                //        }
                //        if (updt.FormDFdhDay == "Sunday")
                //        {
                //            formS1Details.WkDtl.Add(new FormS1WkDtlDTO { Actual = null, Planned = null, FormDFddWorkSts = updt.FormDFddWorkSts, FormDFdhDay = updt.FormDFdhDay, SchldDayOfWeek = 0 });
                //        }

                //    }
                //}

            }
            LoadLookupService("Road_Master", "Act-FormS2", "User", GroupNameList.Supervisor, "FS1-StatusLegend");
            return PartialView("~/Views/MAM/FormS1/AddDetails.cshtml", formS1Details);
        }
        public IActionResult LoadDetailData(int id)
        {
            FormS1DetailDTO formS1Details = null;
            if (id > 0)
            {
                formS1Details = serFormS1.FindDetailsById(id);

                //if (formS1Details.RmFormS1WkDtl != null && formS1Details.RmFormS1WkDtl.Count() > 0)
                //{
                //    formS1Details.RmFormS1WkDtl.ToList().ForEach((RmFormS1WkDtl wk) =>
                //    {
                //        wk.FsiwdFsidPkRefNoNavigation = null;
                //    });
                //}
                return Json(formS1Details, JsonOption());
            }
            else
            {
                return BadRequest("Invalid Request");
            }

        }
        [HttpPost]
        public IActionResult SaveDetails(FormS1DetailDTO formS1Details)
        {
            formS1Details.CrBy = _security.UserID;
            formS1Details.ModBy = _security.UserID;
            formS1Details.ModDt = DateTime.UtcNow;
            formS1Details.CrDt = DateTime.UtcNow;
            /*if (formS1Details.RmFormS1WkDtl != null)
            {
                formS1Details.RmFormS1WkDtl.Where(x=> x.FsiwdPkRefNo==0).ToList().ForEach((wkdtl) => {
                    wkdtl.FsiwdCrBy = _security.UserID;
                    wkdtl.FsiwdCrDt = DateTime.UtcNow;
                });
            }*/

            var result = serFormS1.SaveDetails(formS1Details);
            //result.RmFormS1WkDtl.ToList().ForEach((wkdtl) => { wkdtl.FsiwdFsidPkRefNoNavigation = null; });
            return Json(new { Details = result }, JsonOption());
        }

        public IActionResult Print(int id)
        {
            var content1 = serFormS1.FormDownload("FORMS1", id, _environment.WebRootPath, _environment.WebRootPath + "/Templates/FORMS1.xlsx");
            string contentType1 = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(content1, contentType1, "FORMS1" + ".xlsx");
        }

        //public async Task<IActionResult> GetFormDDetails(string roadCode, string actCode, string frmCh, string frmchDeci, string toCh, string tochDeci, string CrewSupervisor, string weekNo)
        //{
        //    List<FormS1WkDtlDTO> formD = await serFormS1.GetFormDDetails(roadCode, actCode, frmCh, frmchDeci, toCh, tochDeci, CrewSupervisor, weekNo);
        //    return Json(new { Result = formD }, JsonOption());
        //}


    }
}
