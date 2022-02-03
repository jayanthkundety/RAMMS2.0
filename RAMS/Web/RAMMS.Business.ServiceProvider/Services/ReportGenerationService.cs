using RAMMS.Domain.Models;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.Linq;
using RAMMS.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using RAMMS.DTO;
using RAMMS.Repository.Interfaces;
using RAMMS.Business.ServiceProvider.Interfaces;

namespace RAMMS.Business.ServiceProvider
{

    //public interface IBridgeProvider
    //{
        

    //}
    public class ReportGenerationService : IReportGenerationService
    {
        readonly IRoadMasterRepository _roadrepo;
        readonly IRmAssetImgRepository rmAssetImgRepository;
        readonly IDDLookUpRepository _dDLookUp;
        public ReportGenerationService(IRoadMasterRepository _bridge, IRmAssetImgRepository _rmAssetRepo, IDDLookUpRepository dDLookUp)
        {
            _roadrepo = _bridge;
            rmAssetImgRepository = _rmAssetRepo;
            _dDLookUp = dDLookUp;
        }
        public List<SelectListItem> LoadProdData(RmRoadMaster rmroadmas)
        {
            List<SelectListItem> _ddLookupAsset;
            try
            {
                _ddLookupAsset = _roadrepo.GetLoadData(rmroadmas).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _ddLookupAsset;
        }
        public List<RmRoadMaster> LoadAllRMDataProd(RmRoadMaster _rmroadmas)
        {
              //  List<RmRoadMaster> _RmAllDataLookup;
            //    try
            //    {
            //        _RmAllDataLookup = _roadrepo.GetAllRMData(_rmroadmas).ToList();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }

            return null;
        }
        public List<RmRoadMaster> LoadRMLookupDataProd(RmRoadMaster _rmroadmas)
        {
            //List<RmRoadMaster> _RmAllDataLookup;
            //try
            //{
            //    _RmAllDataLookup = _roadrepo.GetRMDataLookup(_rmroadmas).ToList();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            return null;
        }

        //BridgeMainGridprov
        public List<RmAllassetInventory> GetBridgeGridProv()
        {
            List<RmAllassetInventory> _RmAssetData;
            _RmAssetData = _roadrepo.GetGridData().ToList();
            return _RmAssetData;
        }
        public List<SearchBridge> GetBridgeGrid()
        {
            string assetGroup = "BR";
            List<SearchBridge> SearchBridge = new List<SearchBridge>();
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"@assetgroup",assetGroup }

            };

            var dataSet = _roadrepo.GetDataSet("proc_grid_allassets", System.Data.CommandType.StoredProcedure, param);
            if (dataSet.Tables != null && dataSet.Tables.Count > 0)
            {
                var res = dataSet.Tables[0].ToObject<SearchBridge>().ToList();
                // _RmAssetData = res;
                SearchBridge = dataSet.Tables[0].ToObject<SearchBridge>();
            }
            return SearchBridge;
        }
        public List<SearchBridge> SearchBridgeGridProv(string assetGroup, string InputValue)
        {
            //List<RmAllassetInventory> _RmAssetData = new List<RmAllassetInventory>();
            List<SearchBridge> SearchBridge = new List<SearchBridge>();
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"@assetgroup",assetGroup },
                {"@Inputvalue",InputValue }

            };

            var dataSet = _roadrepo.GetDataSet("proc_smart_search", System.Data.CommandType.StoredProcedure, param);
            if (dataSet.Tables != null && dataSet.Tables.Count > 0)
            {
                //var res = dataSet.Tables[0].ToObject<RmAllassetInventory>().ToList();
                // _RmAssetData = res;
                SearchBridge = dataSet.Tables[0].ToObject<SearchBridge>();
            }
            return SearchBridge;
        }
        public List<RmAllassetInventory> GetBridgeGridP()
        {
            List<RmAllassetInventory> _RmAssetData;
            _RmAssetData = _roadrepo.GetGridData().ToList();
            return _RmAssetData;
        }

        public List<AssetFieldDtl> GetAssetFieldDtls()
        {
            List<AssetFieldDtl> _assetFieldDtls;
            _assetFieldDtls = _roadrepo.GetAssetFieldDtls().ToList();
            return _assetFieldDtls;
        }

        public List<RmFormDownloadUse> GetAllRmFormDownloadUses()
        {
            List<RmFormDownloadUse> _RmFormDownloadUse;
            _RmFormDownloadUse = _roadrepo.GetAllAssetFormDownloadUse().ToList();
            return _RmFormDownloadUse;

            //GetAllAssetFormDownloadUse
        }

        public List<RmFormGenDtl> GetFormGenDtls()
        {
            List<RmFormGenDtl> _rmFormGenDtls;
            _rmFormGenDtls = _roadrepo.GetFormGenDtls().ToList();
            return _rmFormGenDtls;
        }

        public List<RAMMS.Domain.Models.FormDownloadHeader> GetDownloadHeaders(string Formtype,int id ,string Hdr_DTL)
        {
            List<RAMMS.Domain.Models.FormDownloadHeader> downloadHeaders = new List<RAMMS.Domain.Models.FormDownloadHeader>();

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"@Form_type", Formtype},
                {"@Form_key", id},
                {"@HDR_DTL", Hdr_DTL}
            };

            var permission = _roadrepo.GetDataSet("Proc_Download_Use_Forms", System.Data.CommandType.StoredProcedure, param);
            if (permission.Tables != null && permission.Tables.Count > 0)
            {
                var res = permission.Tables[0].ToObject<RAMMS.Domain.Models.FormDownloadHeader>().ToList();
                // Below code is only affecting FormX War print . It made the chainage to m(header6) field as empty. so that the print doesn't show the value
                //if (res.Count > 0)
                //{
                //    if (Formtype == "war" && Hdr_DTL == "Header")
                //    {
                //        for (int i = 0; i < res.Count; i++)
                //        {
                //            res[i].header6 = res[i].header6 = "";
                //        }
                //    }
                //}

                downloadHeaders = res;
                //downloadHeaders =  
            }

            return downloadHeaders;
        }

        public List<RAMMS.Domain.Models.FormDownloadHeader> GetDownloadDetails(string Formtype, int id, string Hdr_DTL)
        {
            List<RAMMS.Domain.Models.FormDownloadHeader> downloadHeaders = new List<RAMMS.Domain.Models.FormDownloadHeader>();

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"@Form_type", Formtype},
                {"@Form_key", id},
                {"@HDR_DTL", Hdr_DTL}
            };

            var permission = _roadrepo.GetDataSet("Proc_Download_Use_Forms", System.Data.CommandType.StoredProcedure, param);
            if (permission.Tables != null && permission.Tables.Count > 0)
            {
                var mulselectval = "";

                var res = permission.Tables[0].ToObject<RAMMS.Domain.Models.FormDownloadHeader>().ToList();
               
                //downloadHeaders =  
                if(res.Count > 0)
                {
                    if (Formtype == "formd" && Hdr_DTL == "footer")
                    {
                        for(int i = 0; i< res.Count; i++)
                        {
                            // Below commented lines for chainage from and to manual concadenation. 
                            //For uncomment this, we should change the FDU_Append_Overwrite column value from append to overwrite.
                            //res[i].header3 = $"{res[i].header2}+{res[i].header3}";
                            //res[i].header5 = $"{res[i].header4}+{res[i].header5}";
                            var multiselect = res[i].header6.Split(",").OfType<string>().ToList();
                            foreach(var multi in multiselect)
                            {
                                mulselectval+= _dDLookUp.GetAll().Where(x => x.DdlTypeDesc == multi && x.DdlType == "Site Ref").Select(x => x.DdlTypeValue).FirstOrDefault()+"/";
                                //mulselectval1 = string.Join("/", _dDLookUp.GetAll().Where(x => x.DdlTypeDesc == multi && x.DdlType == "Site Ref").Select(x => x.DdlTypeValue).FirstOrDefault());
                            }
                            res[i].header6 = res[i].header6 = mulselectval.Remove(mulselectval.Length - 1);
                            //res[i].header6 = res[i].header6 = _dDLookUp.GetAll().Where(x => x.DdlTypeDesc == res[i].header6 && x.DdlType == "Site Ref").Select(x => x.DdlTypeValue).FirstOrDefault();
                            res[i].header3 = res[i].header2 + "." + res[i].header3;
                            res[i].header5 = res[i].header4 + "." + res[i].header5;
                            mulselectval = "";
                        }
                    }
                }
                downloadHeaders = res;
            }

            return downloadHeaders;
        }

        public List<RmAssetImageDtl> SaveAssetImageDtlProvider(List<RmAssetImageDtl> rmAssetImageDtls)
        {
            //List<RmAssetImageDtl> rmAssetImageDtls = new List<RmAssetImageDtl>();
            return rmAssetImgRepository.SaveAssetImageDtl(rmAssetImageDtls);
        }

        public IEnumerable<RmAssetImageDtl> GetUploadedImageProvider()
        {
            return rmAssetImgRepository.GetUploadedImage().ToList();
        }
    }
    //public  class BridgeProvider: IBridgeProvider
    //  {

    //  }
}
