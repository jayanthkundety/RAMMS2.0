using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class RmIwSrvProviderMasterDTO
    {
        public int FiwSrvProviderId { get; set; }
        public string FiwSrvProviderCode { get; set; }
        public string FiwSrvProviderName { get; set; }
        public string FiwSrvProviderAddress1 { get; set; }
        public string FiwSrvProviderAddress2 { get; set; }
        public string FiwSrvProviderAddress3 { get; set; }
        public decimal? FiwSrvProviderPhoneNo { get; set; }
        public decimal? FiwSrvProviderFaxNo { get; set; }
        public decimal? FiwSrvProviderZipcode { get; set; }
        public int? FiwSrvProviderModBy { get; set; }
        public DateTime? FiwSrvProviderModDt { get; set; }
        public int? FiwSrvProviderCrBy { get; set; }
        public DateTime? FiwSrvProviderCrDt { get; set; }
        public bool? FiwSrvProviderActiveYn { get; set; }


    }
}
