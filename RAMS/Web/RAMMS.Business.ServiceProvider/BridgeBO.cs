using Microsoft.AspNetCore.Mvc.Rendering;
//using RAMMS.Common.ServiceProvider;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using RAMMS.DTO;
using RAMMS.Business.ServiceProvider.Interfaces;

namespace RAMMS.Business.ServiceProvider
{
    public interface IBridgeBO
    {
        // RmDdLookup ddLookup(RmDdLookup _RmDdLookup);
        List<SelectListItem> GetddLookup(RmRoadMaster _Rmroad);
        List<RmRoadMaster> GetRMAllData(RmRoadMaster _Rmroad);

        List<RmRoadMaster> GetRMLookupData(RmRoadMaster _Rmroad);

        //BridgeMainGridBO & Object
        List<RmAllassetInventory> GetBridgeGridBO();
        IEnumerable<RmAllassetInventory> bridgeObj { get; set; }



        public RmAllassetInventory _rmAllasset { get; set; }
        List<RmAllassetInventory> ExportBridgedata();
        IEnumerable<AssetFieldDtl> assetFieldDtlsobj { get; set; }
        List<AssetFieldDtl> GetAssetFieldDtls(string type);
        Byte[] GetFile(List<AssetFieldDtl> assetFields, IEnumerable<RmAllassetInventory> rmAllassetInventories);
        List<RmFormDownloadUse> getformfield(string formname);
        RmFormGenDtl GetRmFormGenDtl(string formname);
        List<RAMMS.Domain.Models.FormDownloadHeader> formheader(string Formtype, int id, string Hdr_DTL);
        List<RAMMS.Domain.Models.FormDownloadHeader> formDetails(string Formtype, int id, string Hdr_DTL);
        Byte[] formdownload(string formname,int id,string filepath);
        List<SearchBridge> GetBridgeGrid();
        List<SearchBridge> SearchBridgeGridBO(string assetGroup, string InputValue);
        List<RmAssetImageDtl> SaveAssetImageDtlBO(List<RmAssetImageDtl> rmAssetImageDtls);
        IEnumerable<RmAssetImageDtl> GetUploadedImageBO();
        Byte[] GetDownloadFileHdr(List<AssetFieldDtl> assetFields);

    }
    public class BridgeBO : IBridgeBO
    {
        //private readonly IBridgeProvider _bridgeprov;
        private readonly IReportGenerationService _reportGeneration;

        public BridgeBO(IReportGenerationService reportGeneration)
        {
            _reportGeneration = reportGeneration;
        }
        public List<SelectListItem> GetddLookup(RmRoadMaster _Rmroad)
        {
            if (_Rmroad.RdmDivCode == "MIRI")
            {
                return _reportGeneration.LoadProdData(_Rmroad);
            }
            return null;
        }

        //BridgeMainGridBO
        public IEnumerable<RmAllassetInventory> bridgeObj { get; set; }

        public List<RmAllassetInventory> GetBridgeGridBO()
        {
            var gridData = _reportGeneration.GetBridgeGridProv();

            return gridData;
        }

        //for bridge Add
        public RmAllassetInventory _rmAllasset { get; set; }
        public IEnumerable<AssetFieldDtl> assetFieldDtlsobj { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<RmRoadMaster> GetRMAllData(RmRoadMaster _Rmroad)
        {
            return _reportGeneration.LoadAllRMDataProd(_Rmroad);
        }
        public List<RmRoadMaster> GetRMLookupData(RmRoadMaster _Rmroad)
        {
            if (_Rmroad.RdmDivCode == "MIRI")
            {
                return _reportGeneration.LoadRMLookupDataProd(_Rmroad);
            }
            return null;
        }


        public List<RmAllassetInventory> ExportBridgedata()
        {
            List<RmAllassetInventory> gridData = _reportGeneration.GetBridgeGridProv().ToList();

            return gridData;
        }

        public List<AssetFieldDtl> GetAssetFieldDtls(string type)
        {
            List<AssetFieldDtl> assetFieldData = _reportGeneration.GetAssetFieldDtls().Where(x => x.AssetType == type).ToList();
            return assetFieldData;
        }

        public Byte[] GetFile(List<AssetFieldDtl> RmAllasset, IEnumerable<RmAllassetInventory> RmAllassetInventory)
        {
            var RmAllassetInventoryDTO = RmAllassetInventory.ToList();
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("sheet1");
                for (int i1 = 0; i1 < RmAllasset.Count; i1++)
                {
                    worksheet.Cell(1, i1 + 1).Value = RmAllasset[i1].HdrDisplayName;
                }

                Type _type = Type.GetType("RAMMS.Business.ServiceProvider.ExportExcel");
                PropertyInfo[] propertyInfos = _type.GetProperties();
                int rownumber = 1;
                for (int i = 0; i < RmAllassetInventoryDTO.Count; i++)
                {

                    if (i == 0)
                    {
                        rownumber = i + 1;
                    }

                    for (int j = 0; j < RmAllasset.Count; j++)
                    {
                        foreach (PropertyInfo _propertyInfo in propertyInfos)
                        {
                            if (_propertyInfo.Name.ToLower() == RmAllasset[j].FieldName.Replace("_", "").ToLower())
                            {
                                if (RmAllassetInventoryDTO[i].GetType().GetProperty(_propertyInfo.Name) != null && RmAllassetInventoryDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAllassetInventoryDTO[i], null) != null)
                                    worksheet.Cell(rownumber + 1, j + 1).Value = RmAllassetInventoryDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAllassetInventoryDTO[i], null).ToString();
                                else
                                    worksheet.Cell(rownumber + 1, j + 1).Value = "";
                            }
                        }
                    }
                    rownumber = rownumber + 1;
                }


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }
            }
        }

        public List<RmFormDownloadUse> getformfield(string formname)
        {
            List<RmFormDownloadUse> res = new List<RmFormDownloadUse>();
           try
            {
                res = _reportGeneration.GetAllRmFormDownloadUses().Where(x => x.FduFormType == formname).ToList();
                return res;
            }
            catch(Exception ex)
            {
                return null;
            }

        }
        public List<SearchBridge> GetBridgeGrid()
        {
            var gridData = _reportGeneration.GetBridgeGrid();

            return gridData;
        }
        public List<SearchBridge> SearchBridgeGridBO(string assetGroup, string InputValue)
        {
            List<SearchBridge> gridData = _reportGeneration.SearchBridgeGridProv(assetGroup, InputValue);

            return gridData;
        }

        public Byte[] formdownload(string formname, int id, string filepath)
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

            try {
            List<RAMMS.Domain.Models.FormDownloadHeader> RmAllassetInventoryDTO = formheader(formname.ToLower(), id, "Header");
            List<RmFormDownloadUse> RmAllasset = getformfield(formname);
            var formdetails = GetRmFormGenDtl(formname);
            List<RAMMS.Domain.Models.FormDownloadHeader> RmAlledtailsDTO = formDetails(formname.ToLower(), id, "detail");
                List<RAMMS.Domain.Models.FormDownloadHeader> RmAlledtailsDTO_det1 = formDetails(formname.ToLower(), id, "detail1");
                List<RAMMS.Domain.Models.FormDownloadHeader> RmAlledtailsDTO_det2 = formDetails(formname.ToLower(), id, "detail2");
                List<RAMMS.Domain.Models.FormDownloadHeader> RmAlledtailsFooterDTO = formDetails(formname.ToLower(), id, "footer");

                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var workbook = new XLWorkbook(cachefile))
                {
                    IXLWorksheet worksheet;
                    workbook.Worksheets.TryGetWorksheet("sheet1", out worksheet);

                    List<RmFormDownloadUse> RmAllassetDetails = RmAllasset.Where(x => x.FduTableTypeHdrDtl.ToLower() == "detail").ToList();
                    if (RmAlledtailsDTO != null && RmAlledtailsDTO.Count > 0)
                    {
                        decimal totalcount = Convert.ToDecimal(RmAlledtailsDTO.Count);
                        RmFormDownloadUse Index = RmAllasset.Where(x => x.FduTableTypeHdrDtl == "detail").FirstOrDefault();
                        if (Index != null)
                        {
                            decimal endvalues = Convert.ToDecimal(Index.Endindex + 1);
                            decimal startvalues = Convert.ToDecimal(Index.Startindex);
                            decimal pagecount = Math.Ceiling(totalcount / (endvalues - startvalues));
                            for (int sheet = 2; sheet <= Convert.ToInt32(pagecount); sheet++)
                            {
                                using (var tempworkbook = new XLWorkbook(cachefile))
                                {
                                    string sheetname = "sheet" + Convert.ToString(sheet);
                                    IXLWorksheet copysheet = tempworkbook.Worksheet(1);
                                    copysheet.Worksheet.Name = sheetname;
                                    workbook.AddWorksheet(copysheet);
                                }

                            }
                        }

                    }

                    
                    Type _type = Type.GetType("RAMMS.Business.ServiceProvider.FormDownloadHeader");
                    PropertyInfo[] propertyInfos = _type.GetProperties();

                    List<RmFormDownloadUse> RmAllassetHeader = RmAllasset.Where(x => x.FduTableTypeHdrDtl.ToLower() == "header").ToList();
                    if (RmAllassetInventoryDTO != null && RmAllassetInventoryDTO.Count > 0)
                    {
                        foreach (IXLWorksheet tworksheet in workbook.Worksheets)
                        {
                            for (int i = 0; i < RmAllassetInventoryDTO.Count; i++)
                            {
                                //string appenstring = "";
                                for (int j = 0; j < RmAllassetHeader.Count; j++)
                                {
                                    if (RmAllassetHeader[j].FduFormType == "FormX")
                                    {
                                        if (RmAllassetInventoryDTO[i].header7 == "PUBLIC")
                                        {
                                            //worksheet.get_Range("A1", "A14").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                                            tworksheet.Cells("J5").Value = "/";
                                        }
                                        if (RmAllassetInventoryDTO[i].header7 == "ENGINEERING CONSULTANT")
                                        {
                                            tworksheet.Cells("J6").Value = "/";
                                        }
                                        if (RmAllassetInventoryDTO[i].header7 == "S.O.s REPRESENTATIVE / S.O.")
                                        {
                                            tworksheet.Cells("J7").Value = "/";
                                        }
                                        if (RmAllassetInventoryDTO[i].header7 == "OTHERS")
                                        {
                                            tworksheet.Cells("J8").Value = "/";
                                        }
                                    }
                                    foreach (PropertyInfo _propertyInfo in propertyInfos)
                                    {
                                        if (_propertyInfo.Name.ToLower() == RmAllassetHeader[j].FduHeaderName.ToLower())
                                        {
                                            if (RmAllassetInventoryDTO[i].GetType().GetProperty(_propertyInfo.Name) != null && RmAllassetInventoryDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAllassetInventoryDTO[i], null) != null)
                                            {
                                                if (RmAllassetHeader[j].FduHeaderName.ToLower().Contains("header"))
                                                {                                                    
                                                    if (RmAllassetHeader[j].FduAppendOverwrite.ToLower() == "append")
                                                    {                                                       
                                                        //if (appenstring == "")
                                                        //    appenstring = string.Concat(tworksheet.Cell(RmAllassetHeader[j].FduExcelRowNo ?? 0, RmAllassetHeader[j].FduExcelColumnNo ?? 0).Value.ToString() , RmAllassetHeader[j].FduSeperator ?? "".ToString() , RmAllassetInventoryDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAllassetInventoryDTO[i], null).ToString());
                                                        //else
                                                        //    appenstring = string.Concat(appenstring , RmAllassetHeader[j].FduSeperator ?? "".ToString() , RmAllassetInventoryDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAllassetInventoryDTO[i], null).ToString());
                                                        if (RmAllassetHeader[j].FduSeperator == "-")
                                                        {
                                                            tworksheet.Cell(RmAllassetHeader[j].FduExcelRowNo ?? 0, RmAllassetHeader[j].FduExcelColumnNo ?? 0).SetValue<string>(string.Concat(tworksheet.Cell(RmAllassetHeader[j].FduExcelRowNo ?? 0, RmAllassetHeader[j].FduExcelColumnNo ?? 0).GetString(), RmAllassetHeader[j].FduSeperator ?? "".ToString(), RmAllassetInventoryDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAllassetInventoryDTO[i], null).ToString()));
                                                        }
                                                        else if(RmAllassetHeader[j].FduSeperator == ".")
                                                        {
                                                            tworksheet.Cell(RmAllassetHeader[j].FduExcelRowNo ?? 0, RmAllassetHeader[j].FduExcelColumnNo ?? 0).SetValue<string>(string.Concat(tworksheet.Cell(RmAllassetHeader[j].FduExcelRowNo ?? 0, RmAllassetHeader[j].FduExcelColumnNo ?? 0).GetString(), RmAllassetHeader[j].FduSeperator ?? "".ToString(), RmAllassetInventoryDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAllassetInventoryDTO[i], null).ToString()));
                                                        }
                                                        else
                                                        {
                                                            tworksheet.Cell(RmAllassetHeader[j].FduExcelRowNo ?? 0, RmAllassetHeader[j].FduExcelColumnNo ?? 0).Value = string.Concat(tworksheet.Cell(RmAllassetHeader[j].FduExcelRowNo ?? 0, RmAllassetHeader[j].FduExcelColumnNo ?? 0).GetString(), RmAllassetHeader[j].FduSeperator ?? "".ToString(), RmAllassetInventoryDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAllassetInventoryDTO[i], null).ToString());
                                                        }                                                        
                                                    }
                                                    else if(RmAllassetHeader[j].FduAppendOverwrite.ToLower() == "overwrite")
                                                    {
                                                        if (RmAllassetHeader[j].FduFormType == "FormX" && RmAllassetHeader[j].FduHeaderName == "header23")
                                                        {
                                                            if (RmAllassetInventoryDTO[i].header23 != "")
                                                            {
                                                                RmAllassetInventoryDTO[i].header23 = "Section " + RmAllassetInventoryDTO[i].header23;
                                                            }
                                                        }
                                                        tworksheet.Cell(RmAllassetHeader[j].FduExcelRowNo ?? 0, RmAllassetHeader[j].FduExcelColumnNo ?? 0).Value =  RmAllassetInventoryDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAllassetInventoryDTO[i], null).ToString();
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    int rownumber = 0;
                    RmAllassetDetails = new List<RmFormDownloadUse>();
                    RmAllassetDetails = RmAllasset.Where(x => x.FduTableTypeHdrDtl.ToLower() == "detail").ToList();
                    if (RmAlledtailsDTO != null && RmAlledtailsDTO.Count() > 0)
                    {
                        foreach (IXLWorksheet tworksheet in workbook.Worksheets)
                        {
                            int excelcolumn = 0;
                            for (int i = 0; i < RmAlledtailsDTO.Count; i++)
                            {
                                if (i == 0)
                                {
                                    excelcolumn = RmAllassetDetails[i].Startindex ?? 0;
                                    if (tworksheet.Name.ToLower() == "sheet1")
                                    {
                                        rownumber = 0;
                                    }
                                }

                                for (int j = 0; j < RmAllassetDetails.Count; j++)
                                {
                                    foreach (PropertyInfo _propertyInfo in propertyInfos)
                                    {
                                        if (_propertyInfo.Name.ToLower() == RmAllassetDetails[j].FduHeaderName.ToLower())
                                        {
                                            if (RmAlledtailsDTO[rownumber].GetType().GetProperty(_propertyInfo.Name) != null && RmAlledtailsDTO[rownumber].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAlledtailsDTO[rownumber], null) != null)
                                            {
                                                if (RmAllassetDetails[j].FduAppendOverwrite.ToLower() == "append")
                                                {
                                                    tworksheet.Cell(RmAllassetHeader[j].FduExcelRowNo ?? 0, RmAllassetHeader[j].FduExcelColumnNo ?? 0).Value = string.Concat(tworksheet.Cell(RmAllassetHeader[j].FduExcelRowNo ?? 0, RmAllassetHeader[j].FduExcelColumnNo ?? 0).Value.ToString(), RmAllassetHeader[j].FduSeperator ?? "".ToString(), RmAllassetInventoryDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAllassetInventoryDTO[i], null).ToString());
                                                }
                                                else if (RmAllassetDetails[j].FduAppendOverwrite.ToLower() == "overwrite")
                                                {
                                                    tworksheet.Cell(excelcolumn, RmAllassetDetails[j].FduExcelColumnNo ?? 0).Value = RmAlledtailsDTO[rownumber].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAlledtailsDTO[rownumber], null).ToString();
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (excelcolumn == (RmAllassetDetails[0].Endindex ?? 0) || rownumber == RmAlledtailsDTO.Count - 1)
                                {
                                    rownumber = rownumber + 1;
                                    break;
                                }

                                excelcolumn = excelcolumn + 1;
                                rownumber = rownumber + 1;
                            }

                        }
                    }

                    rownumber = 0;
                    RmAllassetDetails = new List<RmFormDownloadUse>();
                    RmAllassetDetails = RmAllasset.Where(x => x.FduTableTypeHdrDtl.ToLower() == "detail1").ToList();
                    if (RmAlledtailsDTO_det1 != null && RmAlledtailsDTO_det1.Count() > 0)
                    {
                        foreach (IXLWorksheet tworksheet in workbook.Worksheets)
                        {
                            int excelcolumn = 0;
                            for (int i = 0; i < RmAlledtailsDTO_det1.Count; i++)
                            {
                                if (i == 0)
                                {
                                    excelcolumn = RmAllassetDetails[i].Startindex ?? 0;
                                    if (tworksheet.Name.ToLower() == "sheet1")
                                    {
                                        rownumber = 0;
                                    }
                                }

                                for (int j = 0; j < RmAllassetDetails.Count; j++)
                                {
                                    foreach (PropertyInfo _propertyInfo in propertyInfos)
                                    {
                                        if (_propertyInfo.Name.ToLower() == RmAllassetDetails[j].FduHeaderName.ToLower())
                                        {
                                            if (RmAlledtailsDTO_det1[rownumber].GetType().GetProperty(_propertyInfo.Name) != null && RmAlledtailsDTO_det1[rownumber].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAlledtailsDTO_det1[rownumber], null) != null)
                                            {
                                                if (RmAllassetDetails[j].FduAppendOverwrite.ToLower() == "append")
                                                {
                                                    tworksheet.Cell(RmAllassetHeader[j].FduExcelRowNo ?? 0, RmAllassetHeader[j].FduExcelColumnNo ?? 0).Value = string.Concat(tworksheet.Cell(RmAllassetHeader[j].FduExcelRowNo ?? 0, RmAllassetHeader[j].FduExcelColumnNo ?? 0).Value.ToString(), RmAllassetHeader[j].FduSeperator ?? "".ToString(), RmAllassetInventoryDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAllassetInventoryDTO[i], null).ToString());
                                                }
                                                else if (RmAllassetDetails[j].FduAppendOverwrite.ToLower() == "overwrite")
                                                {
                                                    tworksheet.Cell(excelcolumn, RmAllassetDetails[j].FduExcelColumnNo ?? 0).Value = RmAlledtailsDTO_det1[rownumber].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAlledtailsDTO_det1[rownumber], null).ToString();
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (excelcolumn == (RmAllassetDetails[0].Endindex ?? 0) || rownumber == RmAlledtailsDTO_det1.Count - 1)
                                {
                                    rownumber = rownumber + 1;
                                    break;
                                }

                                excelcolumn = excelcolumn + 1;
                                rownumber = rownumber + 1;
                            }

                        }
                    }

                    rownumber = 0;
                    RmAllassetDetails = new List<RmFormDownloadUse>();
                    RmAllassetDetails = RmAllasset.Where(x => x.FduTableTypeHdrDtl.ToLower() == "detail2").ToList();
                    if (RmAlledtailsDTO_det2 != null && RmAlledtailsDTO_det2.Count() > 0)
                    {
                        foreach (IXLWorksheet tworksheet in workbook.Worksheets)
                        {
                            int excelcolumn = 0;
                            for (int i = 0; i < RmAlledtailsDTO_det2.Count; i++)
                            {
                                if (i == 0)
                                {
                                    excelcolumn = RmAllassetDetails[i].Startindex ?? 0;
                                    if (tworksheet.Name.ToLower() == "sheet1")
                                    {
                                        rownumber = 0;
                                    }
                                }

                                for (int j = 0; j < RmAllassetDetails.Count; j++)
                                {
                                    foreach (PropertyInfo _propertyInfo in propertyInfos)
                                    {
                                        if (_propertyInfo.Name.ToLower() == RmAllassetDetails[j].FduHeaderName.ToLower())
                                        {
                                            if (RmAlledtailsDTO_det2[rownumber].GetType().GetProperty(_propertyInfo.Name) != null && RmAlledtailsDTO_det2[rownumber].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAlledtailsDTO_det2[rownumber], null) != null)
                                            {
                                                if (RmAllassetDetails[j].FduAppendOverwrite.ToLower() == "append")
                                                {
                                                    tworksheet.Cell(RmAllassetHeader[j].FduExcelRowNo ?? 0, RmAllassetHeader[j].FduExcelColumnNo ?? 0).Value = string.Concat(tworksheet.Cell(RmAllassetHeader[j].FduExcelRowNo ?? 0, RmAllassetHeader[j].FduExcelColumnNo ?? 0).Value.ToString(), RmAllassetHeader[j].FduSeperator ?? "".ToString(), RmAllassetInventoryDTO[i].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAllassetInventoryDTO[i], null).ToString());
                                                }
                                                else if (RmAllassetDetails[j].FduAppendOverwrite.ToLower() == "overwrite")
                                                {
                                                    tworksheet.Cell(excelcolumn, RmAllassetDetails[j].FduExcelColumnNo ?? 0).Value = RmAlledtailsDTO_det2[rownumber].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAlledtailsDTO_det2[rownumber], null).ToString();
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (excelcolumn == (RmAllassetDetails[0].Endindex ?? 0) || rownumber == RmAlledtailsDTO_det2.Count - 1)
                                {
                                    rownumber = rownumber + 1;
                                    break;
                                }

                                excelcolumn = excelcolumn + 1;
                                rownumber = rownumber + 1;
                            }

                        }
                    }

                    rownumber = 0;
                    RmAllassetDetails = new List<RmFormDownloadUse>();
                    RmAllassetDetails = RmAllasset.Where(x => x.FduTableTypeHdrDtl.ToLower() == "footer").ToList();
                    if (RmAlledtailsFooterDTO != null && RmAlledtailsFooterDTO.Count() > 0)
                    {
                        foreach (IXLWorksheet tworksheet in workbook.Worksheets)
                        {
                            if (formname.ToLower() == "formd")
                            {
                                tworksheet.Cells("E26").Style.DateFormat.Format = "h:mm AM/PM";
                                tworksheet.Cells("E27").Style.DateFormat.Format = "h:mm AM/PM";

                                tworksheet.Cells("F26").Style.DateFormat.Format = "h:mm AM/PM";
                                tworksheet.Cells("F27").Style.DateFormat.Format = "h:mm AM/PM";

                                tworksheet.Cells("G26").Style.DateFormat.Format = "h:mm AM/PM";
                                tworksheet.Cells("G27").Style.DateFormat.Format = "h:mm AM/PM";

                                tworksheet.Cells("H26").Style.DateFormat.Format = "h:mm AM/PM";
                                tworksheet.Cells("H27").Style.DateFormat.Format = "h:mm AM/PM";

                                tworksheet.Cells("I26").Style.DateFormat.Format = "h:mm AM/PM";
                                tworksheet.Cells("I27").Style.DateFormat.Format = "h:mm AM/PM";

                                tworksheet.Cells("J26").Style.DateFormat.Format = "h:mm AM/PM";
                                tworksheet.Cells("J27").Style.DateFormat.Format = "h:mm AM/PM";

                                tworksheet.Cells("K26").Style.DateFormat.Format = "h:mm AM/PM";
                                tworksheet.Cells("K27").Style.DateFormat.Format = "h:mm AM/PM";

                                tworksheet.Cells("L26").Style.DateFormat.Format = "h:mm AM/PM";
                                tworksheet.Cells("L27").Style.DateFormat.Format = "h:mm AM/PM";

                                tworksheet.Cells("M26").Style.DateFormat.Format = "h:mm AM/PM";
                                tworksheet.Cells("M27").Style.DateFormat.Format = "h:mm AM/PM";

                                tworksheet.Cells("N26").Style.DateFormat.Format = "h:mm AM/PM";
                                tworksheet.Cells("N27").Style.DateFormat.Format = "h:mm AM/PM";
                            }
                           

                            int excelcolumn = 0;
                            int remarksexcelcolumn = 35;
                            for (int i = 0; i < RmAlledtailsFooterDTO.Count; i++)
                            {
                                if (i == 0)
                                {
                                    excelcolumn = RmAllassetDetails[i].Startindex ?? 0;
                                    if (tworksheet.Name.ToLower() == "sheet1")
                                    {
                                        rownumber = 0;
                                    }
                                }

                                for (int j = 0; j < RmAllassetDetails.Count; j++)
                                {
                                    foreach (PropertyInfo _propertyInfo in propertyInfos)
                                    {
                                        if (_propertyInfo.Name.ToLower() == RmAllassetDetails[j].FduHeaderName.ToLower())
                                        {
                                            if (RmAlledtailsFooterDTO[rownumber].GetType().GetProperty(_propertyInfo.Name) != null && RmAlledtailsFooterDTO[rownumber].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAlledtailsFooterDTO[rownumber], null) != null)
                                            {
                                                if (RmAllassetDetails[j].FduAppendOverwrite.ToLower() == "append")
                                                {
                                                    tworksheet.Cell(RmAllassetDetails[j].FduExcelRowNo ?? 0,excelcolumn).Value = tworksheet.Cell(excelcolumn, RmAllassetDetails[j].FduExcelRowNo ?? 0).Value + RmAllassetDetails[j].FduSeperator  + RmAlledtailsFooterDTO[rownumber].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAlledtailsFooterDTO[rownumber], null).ToString();
                                                }
                                                else if (RmAllassetDetails[j].FduAppendOverwrite.ToLower() == "overwrite")
                                                {
                                                    tworksheet.Cell( RmAllassetDetails[j].FduExcelRowNo ?? 0, excelcolumn).Value = RmAlledtailsFooterDTO[rownumber].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAlledtailsFooterDTO[rownumber], null).ToString();
                                                    if(formname.ToLower() == "formd" && RmAllassetDetails[j].FduTableFieldName == "FDD_Remarks")
                                                    {
                                                        tworksheet.Cells("C"+ remarksexcelcolumn).Value= RmAlledtailsFooterDTO[rownumber].GetType().GetProperty(_propertyInfo.Name).GetValue(RmAlledtailsFooterDTO[rownumber], null).ToString();
                                                        remarksexcelcolumn = remarksexcelcolumn+1;
                                                    }

                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (excelcolumn == (RmAllassetDetails[0].Endindex ?? 0) || rownumber == RmAlledtailsFooterDTO.Count - 1)
                                {
                                    rownumber = rownumber + 1;
                                    break;
                                }

                                excelcolumn = excelcolumn + 1;
                                rownumber = rownumber + 1;
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
            catch(Exception ex)
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

        public List<RmAssetImageDtl> SaveAssetImageDtlBO(List<RmAssetImageDtl> rmAssetImageDtls)
        {
            var imageDetail = _reportGeneration.SaveAssetImageDtlProvider(rmAssetImageDtls);
            return imageDetail;
        }

        public IEnumerable<RmAssetImageDtl> GetUploadedImageBO()
        {
            var data = _reportGeneration.GetUploadedImageProvider();
            return data.ToList();
        }

        public List<RAMMS.Domain.Models.FormDownloadHeader> formheader(string Formtype, int id, string Hdr_DTL)
        {
            List<RAMMS.Domain.Models.FormDownloadHeader> r1 = _reportGeneration.GetDownloadHeaders(Formtype, id, Hdr_DTL);
            return r1;

        }

        public Byte[] GetDownloadFileHdr(List<AssetFieldDtl> RmAllasset)
        {

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("sheet1");
                for (int i1 = 0; i1 < RmAllasset.Count; i1++)
                {
                    worksheet.Cell(1, i1 + 1).Value = RmAllasset[i1].HdrDisplayName;
                }


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }
            }
        }

        public List<RAMMS.Domain.Models.FormDownloadHeader> formDetails(string Formtype, int id, string Hdr_DTL)
        {
            List<RAMMS.Domain.Models.FormDownloadHeader> r1 = _reportGeneration.GetDownloadDetails(Formtype,id,Hdr_DTL);
            return r1;

        }

        public RmFormGenDtl GetRmFormGenDtl(string formname)
        {
            RmFormGenDtl res = new RmFormGenDtl();
            try
            {
                res = _reportGeneration.GetFormGenDtls().Where(x => x.FgdFileName== formname).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
