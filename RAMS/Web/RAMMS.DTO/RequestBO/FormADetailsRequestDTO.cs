using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RAMMS.DTO.RequestBO
{
    public class FormADetailsRequestDTO
    {
        [MapTo("FadPkRefNo")]
        public int No { get; set; }

        [MapTo("FadFahPkRefNo")]
        public int HeaderNo { get; set; }

        [MapTo("FadAssetId")]
        public string AssetId { get; set; }

        [MapTo("FadDt")]
        public string Dt { get; set; }

        [MapTo("FadSrno")]
        public int? Srno { get; set; }

        [MapTo("FadSiteRef")]
        public string SiteRef { get; set; }

        [MapTo("FadFrmCh")]
        public int? FromCh { get; set; }

        [MapTo("FadFrmChDeci")]
        public string FromChDeci { get; set; }

        [MapTo("FadToCh")]
        public int? ToCh { get; set; }

        [MapTo("FadToChDeci")]
        public string ToChDeci { get; set; }

        [MapTo("FadDefCode")]
        public string DefCode { get; set; }

        [MapTo("FadActCode")]
        public string ActCode { get; set; }

        [MapTo("FadUnit")]
        public string Unit { get; set; }

        [MapTo("FadLength")]
        public double? Length { get; set; }

        [MapTo("FadWidth")]
        public double? Width { get; set; }

        [MapTo("FadHeight")]
        public double? Height { get; set; }

        [MapTo("FadAdp")]
        public string Adp { get; set; }

        [MapTo("FadCdr")]
        public string Cdr { get; set; }

        [MapTo("FadPriority")]
        public string Priority { get; set; }

        [MapTo("FadWi")]
        public int? Wi { get; set; }

        [MapTo("FadWs")]
        public int? Ws { get; set; }

        [MapTo("FadWtc")]
        public int? Wtc { get; set; }

        [MapTo("FadWc")]
        public int? Wc { get; set; }

        [MapTo("FadSftPs")]
        public string SftPs { get; set; }

        [MapTo("FadSftWis")]
        public int? SftWis { get; set; }

        [MapTo("FadRt")]
        public int? Rt { get; set; }

        [MapTo("FadRemarks")]
        public string Remarks { get; set; }

        [MapTo("FadFormhApp")]
        public string FormhApp { get; set; }

        [MapTo("FadModBy")]
        public string ModBy { get; set; }

        [MapTo("FadModDt")]
        public string ModDt { get; set; }

        [MapTo("FadCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FadCrDt")]
        public string CreatedDt { get; set; }

        [MapTo("FadSubmitSts")]
        public bool SubmitSts { get; set; }

        [MapTo("FadActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("FadDesc")]
        public string Desc { get; set; }

        [MapTo("FadRefId")]
        public string FadRefNO { get; set; }

        public List<string> SiteRef_multiSelect { get; set; }

        public string CallFrom { get; set; }

        public string HeaderRefNo { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
    }
}
