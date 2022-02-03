using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;

namespace RAMMS.DTO.ResponseBO
{
    public class FormQa2DtlHeaderResponseDTO
    {
        public FormQa2DtlHeaderResponseDTO()
        {
         
        }

        [MapTo("FqaiidPkRefNo")]
        public int No { get; set; }
        [MapTo("FqaiidFqaiihPkRefNo")]
        public int? FormQA2HeaderRefNo { get; set; }
        [MapTo("FqaiidSourceType")]
        public string SourceType { get; set; }
        [MapTo("FqaiidFsihPkRefNo")]
        public int? FsihPkRefNo { get; set; }
        [MapTo("FqaiidRefId")]
        public string RefId { get; set; }
        [MapTo("FqaiidSrno")]
        public int? Srno { get; set; }
        [MapTo("FqaiidSiteRef")]
        public string SiteRef { get; set; }
        [MapTo("FqaiidFrmCh")]
        public int? FrmCh { get; set; }
        [MapTo("FqaiidFrmChDeci")]
        public int? FrmChDeci { get; set; }
        [MapTo("FqaiidToCh")]
        public int? ToCh { get; set; }
        [MapTo("FqaiidToChDeci")]
        public int? ToChDeci { get; set; }
        [MapTo("FqaiidDefCode")]
        public string DefCode { get; set; }
        [MapTo("FqaiidWrkAct")]
        public string WrkAct { get; set; }
        [MapTo("FqaiidInitialCond")]
        public int? InitialCond { get; set; }
        [MapTo("FqaiidDtInitialCond")]
        public DateTime? DtInitialCond { get; set; }
        [MapTo("FqaiidQaI")]
        public int? QaI { get; set; }
        [MapTo("FqaiidDtQaI")]
        public DateTime? DtQaI { get; set; }
        [MapTo("FqaiidQaIi")]
        public int? QaIi { get; set; }
        [MapTo("FqaiidDtQaIi")]
        public DateTime? DtQaIi { get; set; }
        [MapTo("FqaiidQaIii")]
        public int? QaIii { get; set; }
        [MapTo("FqaiidDtQaIii")]
        public DateTime? DtQaIii { get; set; }
        [MapTo("FqaiidQaIv")]
        public int? QaIv { get; set; }
        [MapTo("FqaiidDtQaIv")]
        public DateTime? DtQaIv { get; set; }
        [MapTo("FqaiidDefDesc")]
        public string DefDesc { get; set; }
        [MapTo("FqaiidRwrkDim")]
        public string RwrkDim { get; set; }
        [MapTo("FqaiidWws13aFol")]
        public string Wws13aFol { get; set; }
        [MapTo("FqaiidRemarks")]
        public string Remarks { get; set; }
        [MapTo("FqaiidModBy")]
        public string ModBy { get; set; }
        [MapTo("FqaiidModDt")]
        public DateTime? ModDt { get; set; }
        [MapTo("FqaiidCrBy")]
        public string CrBy { get; set; }
        [MapTo("FqaiidCrDt")]
        public DateTime? CrDt { get; set; }
        [MapTo("FqaiidSubmitSts")]
        public bool SubmitSts { get; set; }
        [MapTo("FqaiidActiveYn")]
        public bool? ActiveYn { get; set; }
        [MapTo("FqaiidFqaiihPkRefNoNavigation")]
        public virtual FormQa2HeaderResponseDTO QA2Header { get; set; }
        [MapTo("FqaiidFsihPkRefNoNavigation")]
        public virtual FormQa2HeaderResponseDTO S1Header { get; set; }

        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }
       
    }
}
