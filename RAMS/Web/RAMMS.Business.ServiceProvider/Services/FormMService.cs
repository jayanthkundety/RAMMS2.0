using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormMService:IFormMService
    {
        private readonly IFormMRepository _repo;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly IAssetsService _assetsService;
        private readonly IProcessService processService;
        private readonly ISecurity _security;
        public FormMService(IRepositoryUnit repoUnit, IFormMRepository repo,
            IAssetsService assetsService, IMapper mapper, IProcessService proService,
            ISecurity security)
        {
            _repo = repo;
            _mapper = mapper;
            _assetsService = assetsService;
            _repoUnit = repoUnit;
            processService = proService;
            _security = security;
        }

        public async Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData)
        {
            return await _repo.GetHeaderGrid(searchData);
        }

        public async Task<FormMDTO> Save(FormMDTO frmM, bool updateSubmit)
        {
            RmFormMHdr frmM_1 = this._mapper.Map<RmFormMHdr>((object)frmM);
            frmM_1 = UpdateStatus(frmM_1);

            RmFormMAuditDetails frmMAD = this._mapper.Map<RmFormMAuditDetails>(frmM.FormMAD);
            frmMAD.FmadPkRefNo = frmM.FormMAD.PkRefNo;
            frmMAD.FmadFmhPkRefNo = frmM.FormMAD.FmhPkRefNo;


            RmFormMHdr source = await this._repo.Save(frmM_1, updateSubmit);

            RmFormMAuditDetails sourceG2 = await this._repo.SaveAD(frmMAD, updateSubmit);


            frmM = this._mapper.Map<FormMDTO>((object)source);
            return frmM;
        }

        public RmFormMHdr UpdateStatus(RmFormMHdr form)
        {
            if (form.FmhPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormR1Repository._context.RmFormMHdr.Where(x => x.FmhPkRefNo == form.FmhPkRefNo).Select(x => new { Status = x.Fmhstatus, Log = x.FmhAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.FmhAuditLog = existsObj.Log;
                    form.Fmhstatus = existsObj.Status;
                }

            }


            if (form.FmhSubmitSts && (string.IsNullOrEmpty(form.Fmhstatus) || form.Fmhstatus == Common.StatusList.FormQA1Saved || form.Fmhstatus == Common.StatusList.FormQA1Rejected))
            {
                //form.Fg1hInspectedBy = _security.UserID;
                //form.Fg1hInspectedName = _security.UserName;
                //form.Fg1hInspectedDt = DateTime.Today;
                form.Fmhstatus = Common.StatusList.FormQA1Submitted;
                form.FmhAuditLog = Utility.ProcessLog(form.FmhAuditLog, "Submitted", "Submitted", form.FmhUsernameAudit, string.Empty, form.FmhDateAudit, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Executed By:" + form.FmhUsernameAudit + " - Form M (" + form.FmhPkRefNo + ")",//doubt
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/FormM/Edit/" + form.FmhPkRefNo.ToString() + "?view=1",
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }
            else if (string.IsNullOrEmpty(form.Fmhstatus) || form.Fmhstatus == "Initialize")
                form.Fmhstatus = Common.StatusList.FormR1R2Saved;

            return form;
        }

        public async Task<FormMDTO> FindByHeaderID(int headerId)
        {
            RmFormMHdr header = await _repo.FindByHeaderID(headerId);
            RmFormMAuditDetails frmR2 = new RmFormMAuditDetails();
            if (header.RmFormMAuditDetails != null)
            {
                frmR2 = header.RmFormMAuditDetails.FirstOrDefault(x => x.FmadFmhPkRefNo == headerId);
            }
            var frmR1 = _mapper.Map<FormMDTO>(header);

            frmR1.FormMAD = frmR2 != null ? _mapper.Map<FormMAuditDetailsDTO>(frmR2) : new FormMAuditDetailsDTO();
            return frmR1;
        }

        public async Task<FormMDTO> FindDetails(FormMDTO frmR1R2, int createdBy)
        {
            RmFormMHdr header = _mapper.Map<RmFormMHdr>(frmR1R2);
            header = await _repo.FindDetails(header);
            if (header != null)
            {
                frmR1R2 = _mapper.Map<FormMDTO>(header);
            }
            else
            {
                List<string> lstCVUNChar = Utility.GetAlphabets(1);
                frmR1R2.status = "Initialize";

                //frmR1R2.InspectedBy = _security.UserID;
                //frmR1R2.InspectedName = _security.UserName;
                //frmR1R2.InspectedDt = DateTime.Today;
                frmR1R2.CrBy = frmR1R2.ModBy = createdBy;
                frmR1R2.CrDt = frmR1R2.ModDt = DateTime.UtcNow;

                header = _mapper.Map<RmFormMHdr>(frmR1R2);
                header = await _repo.Save(header, false);
                frmR1R2 = _mapper.Map<FormMDTO>(header);
            }
            return frmR1R2;
        }

        public int Delete(int id)
        {
            if (id > 0 && !_repo.isF1Exist(id))
            {
                id = _repo.DeleteHeader(new RmFormMHdr() { FmhActiveYn = false, FmhPkRefNo = id });
            }
            else
            {
                return -1;
            }
            return id;
        }

        public List<FormMRpt> GetReportData(int headerid)
        {
            return _repo.GetReportData(headerid);
        }

        public byte[] FormDownload(string formname, int id, string basepath, string filepath)
        {
            string Oldfilename = "";
            string filename = "";
            string cachefile = "";
            basepath = $"{basepath}/Uploads";
            if (!filepath.Contains(".xlsx"))
            {
                Oldfilename = filepath + formname + ".xlsx";// formdetails.FgdFilePath+"\\" + formdetails.FgdFileName+ ".xlsx";
                filename = formname + DateTime.Now.ToString("yyyyMMddHHmmssfffffff").ToString();
                cachefile = filepath + filename + ".xlsx";
            }
            else
            {
                Oldfilename = filepath;
                filename = filepath.Replace(".xlsx", DateTime.Now.ToString("yyyyMMddHHmmssfffffff").ToString() + ".xlsx");
                cachefile = filename;
            }

            try
            {
                List<FormMRpt> _rpt = this.GetReportData(id);
                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var workbook = new XLWorkbook(cachefile))
                {
                    IXLWorksheet worksheet = workbook.Worksheet(1);

                    using (var book = new XLWorkbook(cachefile))
                    {
                        if (worksheet != null)
                        {
                            var rptCount = _rpt.Count;
                            var rpt = _rpt[rptCount - 1];
                            worksheet.Cell(4, 5).Value = rpt.Type;
                            worksheet.Cell(4, 16).Value = rpt.Method;
                            worksheet.Cell(4, 28).Value = rpt.Desc;
                            worksheet.Cell(5, 5).Value = rpt.RdCode;
                            worksheet.Cell(5, 15).Value = rpt.RdName;

                            worksheet.Cell(6, 5).Value = rpt.RmuName;
                            worksheet.Cell(6, 15).Value = rpt.DivCode;
                            worksheet.Cell(6, 25).Value = rpt.CrewSup;

                            worksheet.Cell(7, 5).Value = rpt.ActName;
                            worksheet.Cell(8, 5).Value = rpt.AuditTimeFrm;
                            worksheet.Cell(8, 8).Value = rpt.AuditTimeTo;
                            worksheet.Cell(8, 15).Value = rpt.AuditedDate;
                            worksheet.Cell(8, 24).Value = rpt.AuditedBy;
                            worksheet.Cell(9, 5).Value = rpt.AuditType;
                            //Signs
                            worksheet.Cell(12, 13).Value = rpt.A1tallyBox;
                            worksheet.Cell(13, 13).Value = rpt.A2tallyBox;
                            worksheet.Cell(14, 13).Value = rpt.A3tallyBox;
                            worksheet.Cell(15, 13).Value = rpt.A4tallyBox;
                            worksheet.Cell(16, 13).Value = rpt.A5tallyBox;
                            worksheet.Cell(17, 13).Value = rpt.A6tallyBox;
                            worksheet.Cell(18, 13).Value = rpt.A7tallyBox;
                            worksheet.Cell(19, 13).Value = rpt.A8tallyBox;

                            worksheet.Cell(12, 15).Value = rpt.A1total;
                            worksheet.Cell(13, 15).Value = rpt.A2total;
                            worksheet.Cell(14, 15).Value = rpt.A3total;
                            worksheet.Cell(15, 15).Value = rpt.A4total;
                            worksheet.Cell(16, 15).Value = rpt.A5total;
                            worksheet.Cell(17, 15).Value = rpt.A6total;
                            worksheet.Cell(18, 15).Value = rpt.A7total;
                            worksheet.Cell(19, 15).Value = rpt.A8total;

                            //Delineation
                            worksheet.Cell(21, 13).Value = rpt.B1tallyBox;
                            worksheet.Cell(22, 13).Value = rpt.B2tallyBox;
                            worksheet.Cell(23, 13).Value = rpt.B3tallyBox;
                            worksheet.Cell(24, 13).Value = rpt.B4tallyBox;
                            worksheet.Cell(25, 13).Value = rpt.B5tallyBox;
                            worksheet.Cell(26, 13).Value = rpt.B7tallyBox;
                            worksheet.Cell(27, 13).Value = rpt.B8tallyBox;
                            worksheet.Cell(28, 13).Value = rpt.B9tallyBox;

                            worksheet.Cell(21, 15).Value = rpt.B1total;
                            worksheet.Cell(22, 15).Value = rpt.B2total;
                            worksheet.Cell(23, 15).Value = rpt.B3total;
                            worksheet.Cell(24, 15).Value = rpt.B4total;
                            worksheet.Cell(25, 15).Value = rpt.B5total;
                            worksheet.Cell(26, 15).Value = rpt.B7total;
                            worksheet.Cell(27, 15).Value = rpt.B8total;
                            worksheet.Cell(28, 15).Value = rpt.B9total;

                            //PEDESTRIANS/CYCLISTS
                            worksheet.Cell(30, 13).Value = rpt.C1tallyBox;
                            worksheet.Cell(31, 13).Value = rpt.C2tallyBox;

                            worksheet.Cell(30, 15).Value = rpt.C1total;
                            worksheet.Cell(31, 15).Value = rpt.C2total;

                            //CREWS & EQUIPMENT
                            worksheet.Cell(33, 13).Value = rpt.D1tallyBox;
                            worksheet.Cell(34, 13).Value = rpt.D2tallyBox;
                            worksheet.Cell(35, 13).Value = rpt.D3tallyBox;
                            worksheet.Cell(36, 13).Value = rpt.D4tallyBox;
                            worksheet.Cell(37, 13).Value = rpt.D5tallyBox;
                            worksheet.Cell(38, 13).Value = rpt.D6tallyBox;
                            worksheet.Cell(39, 13).Value = rpt.D7tallyBox;
                            worksheet.Cell(40, 13).Value = rpt.D8tallyBox;

                            worksheet.Cell(33, 15).Value = rpt.D1total;
                            worksheet.Cell(34, 15).Value = rpt.D2total;
                            worksheet.Cell(35, 15).Value = rpt.D3total;
                            worksheet.Cell(36, 15).Value = rpt.D4total;
                            worksheet.Cell(37, 15).Value = rpt.D5total;
                            worksheet.Cell(38, 15).Value = rpt.D6total;
                            worksheet.Cell(39, 15).Value = rpt.D7total;
                            worksheet.Cell(40, 15).Value = rpt.D8total;

                            //CONE & BARRIERS
                            worksheet.Cell(12, 31).Value = rpt.E1tallyBox;
                            worksheet.Cell(13, 31).Value = rpt.E2tallyBox;

                            worksheet.Cell(12, 33).Value = rpt.E1total;
                            worksheet.Cell(13, 33).Value = rpt.E2total;

                            //TRUCK MOUNTED  ATTENUATOR
                            worksheet.Cell(15, 31).Value = rpt.F1tallyBox;
                            worksheet.Cell(16, 31).Value = rpt.F2tallyBox;
                            worksheet.Cell(17, 31).Value = rpt.F3tallyBox;
                            worksheet.Cell(18, 31).Value = rpt.F4tallyBox;
                            worksheet.Cell(19, 31).Value = rpt.F5tallyBox;
                            worksheet.Cell(20, 31).Value = rpt.F6tallyBox;
                            worksheet.Cell(21, 31).Value = rpt.F7tallyBox;

                            worksheet.Cell(15, 33).Value = rpt.F1total;
                            worksheet.Cell(16, 33).Value = rpt.F2total;
                            worksheet.Cell(17, 33).Value = rpt.F3total;
                            worksheet.Cell(18, 33).Value = rpt.F4total;
                            worksheet.Cell(19, 33).Value = rpt.F5total;
                            worksheet.Cell(20, 33).Value = rpt.F6total;
                            worksheet.Cell(21, 33).Value = rpt.F7total;

                            //MISCELLANEOUS
                            worksheet.Cell(23, 31).Value = rpt.G1tallyBox;
                            worksheet.Cell(24, 31).Value = rpt.G2tallyBox;
                            worksheet.Cell(25, 31).Value = rpt.G3tallyBox;
                            worksheet.Cell(26, 31).Value = rpt.G4tallyBox;
                            worksheet.Cell(27, 31).Value = rpt.G5tallyBox;
                            worksheet.Cell(28, 31).Value = rpt.G6tallyBox;
                            worksheet.Cell(29, 31).Value = rpt.G7tallyBox;
                            worksheet.Cell(30, 31).Value = rpt.G8tallyBox;
                            worksheet.Cell(31, 31).Value = rpt.G9tallyBox;
                            worksheet.Cell(32, 31).Value = rpt.G10tallyBox;

                            worksheet.Cell(23, 33).Value = rpt.G1total;
                            worksheet.Cell(24, 33).Value = rpt.G2total;
                            worksheet.Cell(25, 33).Value = rpt.G3total;
                            worksheet.Cell(26, 33).Value = rpt.G4total;
                            worksheet.Cell(27, 33).Value = rpt.G5total;
                            worksheet.Cell(28, 33).Value = rpt.G6total;
                            worksheet.Cell(29, 33).Value = rpt.G7total;
                            worksheet.Cell(30, 33).Value = rpt.G8total;
                            worksheet.Cell(31, 33).Value = rpt.G9total;
                            worksheet.Cell(32, 33).Value = rpt.G10total;

                            worksheet.Cell(34, 17).Value = rpt.Total;
                            worksheet.Cell(37, 17).Value = rpt.Findings;
                            worksheet.Cell(42, 1).Value = rpt.CorrectiveActions;

                            worksheet.Cell(52, 4).Value = rpt.UsernameAudit;
                            worksheet.Cell(53, 4).Value = rpt.DesignationAudit;
                            worksheet.Cell(54, 4).Value = rpt.DateAudit;

                            worksheet.Cell(52, 16).Value = rpt.UsernameWit;
                            worksheet.Cell(53, 16).Value = rpt.DesignationWit;
                            worksheet.Cell(54, 16).Value = rpt.DateWit;

                            worksheet.Cell(52, 27).Value = rpt.UsernameWitone;
                            worksheet.Cell(53, 27).Value = rpt.DesignationWitone;
                            worksheet.Cell(54, 27).Value = rpt.DateWitone;
                        }
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            System.IO.File.Delete(cachefile);
                            return content;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var workbook = new XLWorkbook(cachefile))
                {
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        System.IO.File.Delete(cachefile);
                        return content;
                    }
                }

            }
        }
    }
}
