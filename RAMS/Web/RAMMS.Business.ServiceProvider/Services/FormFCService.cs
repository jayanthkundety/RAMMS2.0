using AutoMapper;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using ClosedXML.Excel;
using System.IO;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormFCService : IFormFCService
    {
        private readonly IFormFCRepository _repo;
        private readonly IMapper _mapper;
        private readonly IAssetRepository _asset;
        private readonly IRoadMasterRepository _roadMaster;
        private readonly IDDLookUpRepository _lookup;
        public FormFCService(IFormFCRepository repo, IAssetRepository asset, IRoadMasterRepository roadMaster, IDDLookUpRepository lookup, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _asset = asset;
            _roadMaster = roadMaster;
            _lookup = lookup;
        }
        public async Task<FormFCDTO> FindByHeaderID(int headerId)
        {
            RmFormFcInsHdr header = await _repo.FindByHeaderID(headerId);
            return _mapper.Map<FormFCDTO>(header);
        }
        public async Task<FormFCDTO> FindDetails(FormFCDTO frmFC, int createdBy)
        {
            RmFormFcInsHdr header = _mapper.Map<RmFormFcInsHdr>(frmFC);
            header = await _repo.FindDetails(header);
            if (header != null)
            {
                frmFC = _mapper.Map<FormFCDTO>(header);
            }
            else
            {
                var rm = _roadMaster.GetById(frmFC.RoadId.Value); //.GetByRdCode(header.FcihRoadCode).Result;
                int iVal = Utility.ToInt(rm.RdmFrmChDeci.HasValue ? rm.RdmFrmChDeci.Value : 0);
                int iMol = iVal % 100;
                iVal = iMol > 0 ? iVal - iMol : iVal;
                frmFC.FrmCh = Utility.ToDecimal(Utility.ToString(rm.RdmFrmCh.HasValue ? rm.RdmFrmCh.Value : 0) + "." + Utility.ToString(iVal));
                iVal = Utility.ToInt(rm.RdmToChDeci.HasValue ? rm.RdmToChDeci.Value : 0);
                iMol = iVal % 100;
                iVal = iMol > 0 ? iVal + (100 - iMol) : iVal;
                frmFC.ToCh = Utility.ToDecimal(Utility.ToString(rm.RdmToCh.HasValue ? rm.RdmToCh.Value : 0) + "." + Utility.ToString(iVal));
                if (frmFC.FrmCh == frmFC.ToCh) { throw new Exception("There is no distance (from ch and to ch) for the selected road"); }
                string[] grpCodes = new string[] { "ELM", "RS", "CLM", "CW" };
                frmFC.InsDtl = await _asset.ListOfAssestByRoadCode(frmFC.RoadCode).Select(x => new FormFCDetailsDTO()
                {
                    AiFrmCh = x.AiFrmCh,
                    AiFrmChDeci = x.AiFrmChDeci,
                    AiToCh = x.AiToCh,
                    AiToChDeci = x.AiToChDeci,
                    SubmitSts = true,
                    ActiveYn = true,
                    AiAssetGrpCode = x.AiAssetGrpCode,
                    AiGrpType = x.AiGrpType,
                    AiBound = x.AiBound,
                    AiPkRefNo = x.AiPkRefNo,
                    DBFromCHKm = Convert.ToDecimal(x.AiFrmCh.Value.ToString() + "." + x.AiFrmChDeci),
                    Width = x.AiWidth,
                    Length = x.AiLength
                }).Where(x => grpCodes.Contains(x.AiAssetGrpCode)).OrderBy(x => x.DBFromCHKm).ToListAsync();
                if (frmFC.InsDtl.Count == 0) { throw new Exception("There is no assets for the selected road"); }
                frmFC.AssetTypes = Utility.JSerialize(_lookup.GetFormAssetTypes("ELM,CLM,CW,RS"));
                header = _mapper.Map<RmFormFcInsHdr>(frmFC);

                header = await _repo.Save(header, false);
                frmFC = _mapper.Map<FormFCDTO>(header);
            }
            return frmFC;
        }
        public async Task<bool> AssetsCheck(string roadCode)
        {
            string[] grpCodes = new string[] { "ELM", "RS", "CLM", "CW" };
            var InsDtl = await _asset.ListOfAssestByRoadCode(roadCode).Select(x => new FormFCDetailsDTO()
            {
                AiFrmCh = x.AiFrmCh,
                AiFrmChDeci = x.AiFrmChDeci,
                AiToCh = x.AiToCh,
                AiToChDeci = x.AiToChDeci,
                SubmitSts = true,
                ActiveYn = true,
                AiAssetGrpCode = x.AiAssetGrpCode,
                AiGrpType = x.AiGrpType,
                AiBound = x.AiBound,
                AiPkRefNo = x.AiPkRefNo,
                DBFromCHKm = Convert.ToDecimal(x.AiFrmCh.Value.ToString() + "." + x.AiFrmChDeci),
                Width = x.AiWidth,
                Length = x.AiLength
            }).Where(x => grpCodes.Contains(x.AiAssetGrpCode)).OrderBy(x => x.DBFromCHKm).ToListAsync();
            if (InsDtl.Count == 0) { return false; }

            return true;
        }

        public async Task<FormFCDTO> Save(FormFCDTO frmFC, bool updateSubmit)
        {
            RmFormFcInsHdr header = _mapper.Map<RmFormFcInsHdr>(frmFC);

            header = await _repo.Save(header, updateSubmit);
            frmFC = _mapper.Map<FormFCDTO>(header);
            return frmFC;
        }
        public async Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData)
        {
            return await _repo.GetHeaderGrid(searchData);
        }
        public int Delete(int id)
        {
            if (id > 0)
            {
                id = _repo.DeleteHeader(new RmFormFcInsHdr() { FcihActiveYn = false, FcihPkRefNo = id });
            }
            return id;
        }

        public FormFCRpt GetReportData(int headerid)
        {
            return _repo.GetReportData(headerid);
        }

        public byte[] FormDownload(string formname, int id, string filepath)
        {
            string Oldfilename = "";
            string filename = "";
            string cachefile = "";
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
                FormFCRpt rpt = this.GetReportData(id);
                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var workbook = new XLWorkbook(cachefile))
                {
                    int noofsheets = (rpt.Details.Count() / 20) + ((rpt.Details.Count() % 20) > 0 ? 1 : 1);
                    for (int sheet = 2; sheet <= noofsheets; sheet++)
                    {
                        using (var tempworkbook = new XLWorkbook(cachefile))
                        {
                            string sheetname = "sheet" + Convert.ToString(sheet);
                            IXLWorksheet copysheet = tempworkbook.Worksheet(1);
                            copysheet.Worksheet.Name = sheetname;
                            copysheet.Cell(8, 7).Value = rpt.Division;
                            copysheet.Cell(8, 16).Value = rpt.District;
                            copysheet.Cell(8, 42).Value = rpt.RMU;
                            copysheet.Cell(8, 70).Value = rpt.CrewLeader;
                            copysheet.Cell(9, 17).Value = rpt.RoadName;
                            copysheet.Cell(6, 26).Value = rpt.CrewLeader;
                            copysheet.Cell(7, 102).Value = rpt.InspectedByName;
                            copysheet.Cell(8, 102).Value = rpt.InspectedDate.HasValue ? rpt.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
                            copysheet.Cell(9, 105).Value = rpt.RoadLength;
                            copysheet.Cell(2, 73).Value = sheet;
                            copysheet.Cell(2, 80).Value = noofsheets;
                            workbook.AddWorksheet(copysheet);
                        }
                    }
                    for (int sheet = 1; sheet <= noofsheets; sheet++)
                    {
                        IXLWorksheet worksheet =
                        workbook.Worksheet(sheet);

                        if (worksheet != null)
                        {
                            worksheet.Cell(8, 7).Value = rpt.Division;
                            worksheet.Cell(8, 16).Value = rpt.District;
                            worksheet.Cell(8, 42).Value = rpt.RMU;
                            worksheet.Cell(8, 70).Value = rpt.CrewLeader;
                            worksheet.Cell(9, 17).Value = rpt.RoadName;
                            worksheet.Cell(9, 7).Value = rpt.RoadCode;
                            worksheet.Cell(6, 26).Value = rpt.CrewLeader;
                            worksheet.Cell(7, 102).Value = rpt.InspectedByName;
                            worksheet.Cell(8, 102).Value = rpt.InspectedDate.HasValue ? rpt.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
                            worksheet.Cell(9, 105).Value = rpt.RoadLength;
                            worksheet.Cell(4, 102).Value = sheet;
                            worksheet.Cell(4, 110).Value = noofsheets;
                            worksheet.Cell(32, 2).Value = rpt.Remarks;
                            worksheet.Cell(15, 11).Value = rpt.L_E_P;
                            worksheet.Cell(16, 11).Value = rpt.L_E_T;
                            worksheet.Cell(17, 11).Value = rpt.L_R;
                            worksheet.Cell(18, 11).Value = rpt.C_P_A;
                            worksheet.Cell(19, 11).Value = rpt.C_P_D;
                            worksheet.Cell(20, 11).Value = rpt.C_P_G;
                            worksheet.Cell(21, 11).Value = rpt.C_P_E;
                            worksheet.Cell(22, 11).Value = rpt.C_P_C;
                            worksheet.Cell(23, 11).Value = rpt.C_P_S;
                            worksheet.Cell(24, 11).Value = rpt.C_R;
                            worksheet.Cell(25, 11).Value = rpt.C_C_P;
                            worksheet.Cell(26, 11).Value = rpt.C_C_T;
                            worksheet.Cell(27, 11).Value = rpt.R_R;
                            worksheet.Cell(28, 11).Value = rpt.R_E_P;
                            worksheet.Cell(29, 11).Value = rpt.R_E_T;

                            int i = 15;
                            int title = 0;
                            var data = rpt.Details.Skip((sheet - 1) * 20).Take(20);

                            foreach (var r in data)
                            {
                                if (r.KMTitle.EndsWith("000") || r.KMTitle.EndsWith("500"))
                                {
                                    decimal temp = r.FromCh + (decimal)0.500;
                                    switch (title)
                                    {
                                        case 0:
                                            worksheet.Cell(13, 11).Value = r.KMTitle;
                                            worksheet.Cell(13, 30).Value = $" KM {temp.ToString().Replace(".", "+")}";
                                            break;
                                        case 1:
                                            worksheet.Cell(13, 30).Value = r.KMTitle;
                                            worksheet.Cell(13, 50).Value = $" KM {temp.ToString().Replace(".", "+")}";
                                            break;
                                        case 2:
                                            worksheet.Cell(13, 50).Value = r.KMTitle;
                                            worksheet.Cell(13, 70).Value = $" KM {temp.ToString().Replace(".", "+")}";
                                            break;
                                        case 3:
                                            worksheet.Cell(13, 70).Value = r.KMTitle;
                                            worksheet.Cell(13, 90).Value = $" KM {temp.ToString().Replace(".", "+")}";
                                            break;
                                        case 4:
                                            worksheet.Cell(13, 90).Value = r.KMTitle;
                                            break;
                                    }

                                    title++;
                                }

                                worksheet.Cell(15, i).Value = r.Left_EdgeLine_Paint;
                                worksheet.Cell(16, i).Value = r.Left_EdgeLine_Thermoplastic;
                                worksheet.Cell(17, i).Value = r.Left_RoadStuds;
                                worksheet.Cell(18, i).Value = r.CarriageWay_Pavment_Asphalt;
                                worksheet.Cell(19, i).Value = r.CarriageWay_Pavment_SurfaceDressed;
                                worksheet.Cell(20, i).Value = r.CarriageWay_Pavment_Gravel;
                                worksheet.Cell(21, i).Value = r.CarriageWay_Pavement_Earth;
                                worksheet.Cell(22, i).Value = r.CarriageWay_Pavment_Concrete;
                                worksheet.Cell(23, i).Value = r.CarriageWay_Pavment_Sand;
                                worksheet.Cell(24, i).Value = r.CarriageWay_CentreRoadStuds;
                                worksheet.Cell(25, i).Value = r.CarriageWay_CentreLine_Paint;
                                worksheet.Cell(26, i).Value = r.CarriageWay_CentreLine_Thermoplastic;
                                worksheet.Cell(27, i).Value = r.Right_RoadStuds;
                                worksheet.Cell(28, i).Value = r.Right_EdgeLine_Paint;
                                worksheet.Cell(29, i).Value = r.Right_Thermoplastic;
                                i += 4;
                            }
                        }
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
