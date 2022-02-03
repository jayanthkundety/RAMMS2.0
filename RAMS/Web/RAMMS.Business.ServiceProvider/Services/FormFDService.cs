using AutoMapper;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RAMMS.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RAMMS.DTO.Report;
using ClosedXML.Excel;
using System.IO;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormFDService : IFormFDService
    {
        private IFormFDRepository _repo;
        private readonly IMapper _mapper;
        private readonly IAssetRepository _asset;
        private readonly IRoadMasterRepository _roadMaster;
        private readonly IDDLookUpRepository _lookup;
        public FormFDService(IFormFDRepository formFDRepository, IAssetRepository asset, IRoadMasterRepository roadMaster, IDDLookUpRepository lookup, IMapper mapper)
        {
            _repo = formFDRepository;
            _mapper = mapper;
            _asset = asset;
            _roadMaster = roadMaster;
            _lookup = lookup;
        }
        public async Task<GridWrapper<object>> GetFormFDHeaderGrid(DataTableAjaxPostModel searchData)
        {
            return await _repo.GetFormFDGridHeader(searchData);
        }
        public async Task<FormFDDTO> FindDetails(FormFDDTO frmFd, int createdBy)
        {
            RmFormFdInsHdr header = _mapper.Map<RmFormFdInsHdr>(frmFd);
            header = await _repo.FindDetails(header);
            if (header != null)
            {
                frmFd = _mapper.Map<FormFDDTO>(header);
            }
            else
            {
                var rm = _roadMaster.GetById(frmFd.RoadId.Value); //.GetByRdCode(header.FcihRoadCode).Result;
                int iVal = Utility.ToInt(rm.RdmFrmChDeci.HasValue ? rm.RdmFrmChDeci.Value : 0);
                int iMol = iVal % 100;
                iVal = iMol > 0 ? iVal - iMol : iVal;
                frmFd.FrmCh = Utility.ToDecimal(Utility.ToString(rm.RdmFrmCh.HasValue ? rm.RdmFrmCh.Value : 0) + "." + Utility.ToString(iVal));
                iVal = Utility.ToInt(rm.RdmToChDeci.HasValue ? rm.RdmToChDeci.Value : 0);
                iMol = iVal % 100;
                iVal = iMol > 0 ? iVal + (100 - iMol) : iVal;
                frmFd.ToCh = Utility.ToDecimal(Utility.ToString(rm.RdmToCh.HasValue ? rm.RdmToCh.Value : 0) + "." + Utility.ToString(iVal));
                if (frmFd.FrmCh == frmFd.ToCh) { throw new Exception("There is no distance (from ch and to ch) for the selected road"); }
                string[] grpCodes = new string[] { "DI", "DR", "SH" };
                frmFd.InsDtl = await _asset.ListOfAssestByRoadCode(frmFd.RoadCode).Select(x => new FormFDDetailsDTO()
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
                }).Where(x => grpCodes.Contains(x.AiAssetGrpCode) && x.DBFromCHKm >= 0).OrderBy(x => x.DBFromCHKm).ToListAsync();
                if (frmFd.InsDtl.Count == 0) { throw new Exception("There is no assets for the selected road"); }
                frmFd.AssetTypes = Utility.JSerialize(_lookup.GetFormAssetTypes("DI,DR,SH"));
                header = _mapper.Map<RmFormFdInsHdr>(frmFd);
                header = await _repo.Save(header, false);
                frmFd = _mapper.Map<FormFDDTO>(header);
            }
            return frmFd;
        }
        public async Task<FormFDDTO> Save(FormFDDTO frmFD, bool updateSubmit)
        {
            RmFormFdInsHdr header = _mapper.Map<RmFormFdInsHdr>(frmFD);
            header = await _repo.Save(header, updateSubmit);
            frmFD = _mapper.Map<FormFDDTO>(header);
            return frmFD;
        }
        public async Task<FormFDDTO> FindByHeaderID(int headerId)
        {
            RmFormFdInsHdr header = await _repo.FindByHeaderID(headerId);
            return _mapper.Map<FormFDDTO>(header);
        }
        public int Delete(int id)
        {
            if (id > 0)
            {
                id = _repo.DeleteHeader(new RmFormFdInsHdr() { FdihActiveYn = false, FdihPkRefNo = id });
            }
            return id;
        }

        public FormFDRpt GetReportData(int headerid)
        {
            return _repo.GetReportData(headerid);
        }
        public async Task<bool> AssetsCheck(string roadCode)
        {
            string[] grpCodes = new string[] { "DI", "DR", "SH" };
            var InsDtl = await _asset.ListOfAssestByRoadCode(roadCode).Select(x => new FormFDDetailsDTO()
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
                FormFDRpt rpt = this.GetReportData(id);
                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var workbook = new XLWorkbook(cachefile))
                {
                    int noofsheets = (rpt.Details.Count() / 20) + ((rpt.Details.Count() % 20) > 0 ? 1 : 0);
                    for (int sheet = 2; sheet <= noofsheets; sheet++)
                    {
                        using (var tempworkbook = new XLWorkbook(cachefile))
                        {
                            string sheetname = "sheet" + Convert.ToString(sheet);
                            IXLWorksheet copysheet = tempworkbook.Worksheet(1);
                            copysheet.Worksheet.Name = sheetname;
                            copysheet.Cell(7, 7).Value = rpt.Division;
                            copysheet.Cell(7, 16).Value = rpt.District;
                            copysheet.Cell(7, 42).Value = rpt.RMU;
                            copysheet.Cell(7, 70).Value = rpt.CrewLeader;
                            copysheet.Cell(8, 7).Value = rpt.RoadCode;
                            copysheet.Cell(8, 17).Value = rpt.RoadName;
                            copysheet.Cell(6, 102).Value = rpt.InspectedByName;
                            copysheet.Cell(7, 102).Value = rpt.InspectedDate.HasValue ? rpt.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
                            copysheet.Cell(9, 105).Value = rpt.RoadLength;
                            copysheet.Cell(3, 102).Value = sheet;
                            copysheet.Cell(3, 111).Value = noofsheets;
                            workbook.AddWorksheet(copysheet);
                        }
                    }
                    int titleIteration = 0;
                    for (int sheet = 1; sheet <= noofsheets; sheet++)
                    {


                        IXLWorksheet worksheet =
                        workbook.Worksheet(sheet);

                        if (worksheet != null)
                        {
                            worksheet.Cell(7, 7).Value = rpt.Division;
                            worksheet.Cell(7, 16).Value = rpt.District;
                            worksheet.Cell(7, 42).Value = rpt.RMU;
                            worksheet.Cell(7, 70).Value = rpt.CrewLeader;
                            worksheet.Cell(8, 17).Value = rpt.RoadName;
                            worksheet.Cell(8, 7).Value = rpt.RoadCode;
                            worksheet.Cell(6, 26).Value = rpt.CrewLeader;
                            worksheet.Cell(6, 102).Value = rpt.InspectedByName;
                            worksheet.Cell(7, 102).Value = rpt.InspectedDate.HasValue ? rpt.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
                            worksheet.Cell(9, 105).Value = rpt.RoadLength;
                            worksheet.Cell(3, 102).Value = sheet;
                            worksheet.Cell(3, 111).Value = noofsheets;
                            worksheet.Cell(37, 3).Value = rpt.Remarks;
                            worksheet.Cell(13, 11).Value = rpt.L_DI_G;
                            worksheet.Cell(14, 11).Value = rpt.L_DR_E;
                            worksheet.Cell(15, 11).Value = rpt.L_DR_C;
                            worksheet.Cell(16, 11).Value = rpt.L_DR_B;
                            worksheet.Cell(17, 11).Value = rpt.L_SH_A;
                            worksheet.Cell(18, 11).Value = rpt.L_SH_C;
                            worksheet.Cell(19, 11).Value = rpt.L_SH_E;
                            worksheet.Cell(20, 11).Value = rpt.L_SH_G;
                            worksheet.Cell(21, 11).Value = rpt.L_SH_F;

                            worksheet.Cell(26, 11).Value = rpt.R_DI_G;
                            worksheet.Cell(27, 11).Value = rpt.R_DR_E;
                            worksheet.Cell(28, 11).Value = rpt.R_DR_C;
                            worksheet.Cell(29, 11).Value = rpt.R_DR_B;
                            worksheet.Cell(30, 11).Value = rpt.R_SH_A;
                            worksheet.Cell(31, 11).Value = rpt.R_SH_C;
                            worksheet.Cell(32, 11).Value = rpt.R_SH_E;
                            worksheet.Cell(33, 11).Value = rpt.R_SH_G;
                            worksheet.Cell(34, 11).Value = rpt.R_SH_F;

                            int i = 15;
                            int title = 0;
                            var data = rpt.Details.Skip((sheet - 1) * 20).Take(20);
                            foreach (var r in data)
                            {
                                if (r.FromCh.ToString().EndsWith("500") || r.FromCh.ToString().EndsWith("000"))
                                {
                                    decimal temp = r.FromCh + (decimal)0.500;
                                    switch (title)
                                    {
                                        case 0:
                                            worksheet.Cell(11, 11).Value = r.KMTitle;
                                            worksheet.Cell(24, 11).Value = r.KMTitle;
                                            worksheet.Cell(11, 30).Value = $" KM {temp.ToString().Replace(".", "+")}";
                                            worksheet.Cell(24, 30).Value = $" KM {temp.ToString().Replace(".", "+")}";
                                            break;
                                        case 1:
                                            worksheet.Cell(11, 30).Value = r.KMTitle;
                                            worksheet.Cell(24, 30).Value = r.KMTitle;
                                            worksheet.Cell(11, 50).Value = $" KM {temp.ToString().Replace(".", "+")}";
                                            worksheet.Cell(24, 50).Value = $" KM {temp.ToString().Replace(".", "+")}";
                                            break;
                                        case 2:
                                            worksheet.Cell(11, 50).Value = r.KMTitle;
                                            worksheet.Cell(24, 50).Value = r.KMTitle;
                                            worksheet.Cell(11, 70).Value = $" KM {temp.ToString().Replace(".", "+")}";
                                            worksheet.Cell(24, 70).Value = $" KM {temp.ToString().Replace(".", "+")}";
                                            break;
                                        case 3:
                                            worksheet.Cell(11, 70).Value = r.KMTitle;
                                            worksheet.Cell(24, 70).Value = r.KMTitle;
                                            worksheet.Cell(11, 90).Value = $" KM {temp.ToString().Replace(".", "+")}";
                                            worksheet.Cell(24, 90).Value = $" KM {temp.ToString().Replace(".", "+")}";
                                            break;
                                        case 4:
                                            worksheet.Cell(11, 90).Value = r.KMTitle;
                                            worksheet.Cell(24, 90).Value = r.KMTitle;
                                            break;
                                    }

                                    title++;
                                }
                                worksheet.Cell(13, i).Value = r.Left_Ditch_GravelSandEarth;
                                worksheet.Cell(14, i).Value = r.Left_Drain_Earth;
                                worksheet.Cell(15, i).Value = r.Left_Drain_Blockstone;
                                worksheet.Cell(16, i).Value = r.Left_Drain_Concrete;
                                worksheet.Cell(17, i).Value = r.Left_Shoulder_Asphalt;
                                worksheet.Cell(18, i).Value = r.Left_Shoulder_Concrete;
                                worksheet.Cell(19, i).Value = r.Left_Shoulder_Earth;
                                worksheet.Cell(20, i).Value = r.Left_Shoulder_Gravel;
                                worksheet.Cell(21, i).Value = r.Left_Shoulder_FootpathKerb;

                                worksheet.Cell(26, i).Value = r.Right_Ditch_GravelSandEarth;
                                worksheet.Cell(27, i).Value = r.Right_Drain_Earth;
                                worksheet.Cell(28, i).Value = r.Right_Drain_Blockstone;
                                worksheet.Cell(29, i).Value = r.Right_Drain_Concrete;
                                worksheet.Cell(30, i).Value = r.Right_Shoulder_Asphalt;
                                worksheet.Cell(31, i).Value = r.Right_Shoulder_Concrete;
                                worksheet.Cell(32, i).Value = r.Right_Shoulder_Earth;
                                worksheet.Cell(33, i).Value = r.Right_Shoulder_Gravel;
                                worksheet.Cell(34, i).Value = r.Right_Shoulder_FootpathKerb;

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
