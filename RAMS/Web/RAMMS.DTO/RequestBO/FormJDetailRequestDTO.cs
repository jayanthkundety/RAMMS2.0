using AutoMapper.Configuration.Conventions;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class FormJDetailsRequestDTO
    {
        [MapTo("FjdPkRefNo")]
        public int No { get; set; }

        [MapTo("FjdFjhPkRefNo")]
        public int HeaderNo { get; set; }


        [MapTo("FjdDt")]
        public string Dt { get; set; }

        [MapTo("FjdSrno")]
        public int? Srno { get; set; }

        [MapTo("FjdSiteRef")]
        public string SiteRef { get; set; }

        [MapTo("FjdFrmCh")]
        public int? FromCh { get; set; }

        [MapTo("FjdFrmChDeci")]
        public string FromChDeci { get; set; }

        [MapTo("FjdToCh")]
        public int? ToCh { get; set; }

        [MapTo("FjdToChDeci")]
        public string ToChDeci { get; set; }

        [MapTo("FjdDefCode")]
        public string DefCode { get; set; }

        [MapTo("FjdPrblmDesc")]
        public string Problem { get; set; }
        [MapTo("FjdWrkNeed")]
        public string WorkNeed { get; set; }

        [MapTo("FjdPriority")]
        public string Priority { get; set; }

        [MapTo("FjdActCode")]
        public string ActCode { get; set; }

        [MapTo("FjdLength")]
        public int? Length { get; set; }

        [MapTo("FjdWidth")]
        public int? Width { get; set; }

        [MapTo("FjdHeight")]
        public int? Height { get; set; }


        [MapTo("FjdWi")]
        public int? Wi { get; set; }

        [MapTo("FjdWs")]
        public int? Ws { get; set; }

        [MapTo("FjdWtc")]
        public int? Wtc { get; set; }

        [MapTo("FjdWc")]
        public int? Wc { get; set; }
        [MapTo("FjdRt")]
        public int? Rt { get; set; }

        [MapTo("FjdRemarks")]
        public string Remarks { get; set; }

        [MapTo("FjdFormhApp")]
        public string FormhApp { get; set; }

        [MapTo("FjdModBy")]
        public string ModBy { get; set; }

        [MapTo("FjdModDt")]
        public string ModDt { get; set; }

        [MapTo("FjdCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FjdCrDt")]
        public string CreatedDt { get; set; }

        [MapTo("FjdSubmitSts")]
        public bool SubmitSts { get; set; }

        [MapTo("FjdActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("FjdRefId")]
        public string FadRefNO { get; set; }

        public List<string> SiteRef_multiSelect { get; set; }

        public string CallFrom { get; set; }

        public string HeaderRefNo { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
    }
}
