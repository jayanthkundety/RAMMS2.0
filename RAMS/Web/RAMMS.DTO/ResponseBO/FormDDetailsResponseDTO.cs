using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RAMMS.DTO.ResponseBO
{
    public class FormDDetailsResponseDTO
    {
        [MapTo("FddPkRefNo")]
        public int No { get; set; }

        [MapTo("FddFdhPkRefNo")]
        public int? FormDHeaderNo { get; set; }

        [MapTo("FddRoadCode")]
        public string RoadCode { get; set; }

        [MapTo("FddFrmCh")]
        public int? FrmCh { get; set; }

        [MapTo("FddFrmChDeci")]
        public int? FrmChDeci { get; set; }

        [MapTo("FddToCh")]
        public int? ToCh { get; set; }

        [MapTo("FddToChDeci")]
        public int? ToChDeci { get; set; }

        [MapTo("FddSiteRef")]
        public string SiteRef { get; set; }

        [MapTo("FddActCode")]
        public int? ActCode { get; set; }

        [MapTo("FddTimeArr")]
        public string TimeArr { get; set; }

        [MapTo("FddTimeDep")]
        public string TimeDep { get; set; }

        [MapTo("FddLength")]
        public decimal? Length { get; set; }

        [MapTo("FddWidth")]
        public decimal? Width { get; set; }

        [MapTo("FddHeight")]
        public decimal? Height { get; set; }

        [MapTo("FddUnit")]
        public string Unit { get; set; }

        [MapTo("FddProdQty")]
        public int? ProdQty { get; set; }

        [MapTo("FddProdUnit")]
        public string ProdUnit { get; set; }

        [MapTo("FddWorkSts")]
        public string WorkSts { get; set; }

        [MapTo("FddGenRemarks")]
        public string GenRemarks { get; set; }

        [MapTo("FddRemarks")]
        public string Remarks { get; set; }

        [MapTo("FddModBy")]
        public string ModifeidBy { get; set; }

        [MapTo("FddModDt")]
        public DateTime? ModifiedDate { get; set; }

        [MapTo("FddCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FddCrDt")]
        public DateTime? CreatedDate { get; set; }

        [MapTo("FddSubmitSts")]
        public bool SubmitSts { get; set; }

        [MapTo("FddActiveYn")]
        public bool? ActiveYn { get; set; }
        [MapTo("FddSourceType")]
        public string SourceType { get; set; }
        [MapTo("FddFxhPkRefNo")]
        public int? FormXPKRefNo { get; set; }
        [MapTo("FddRefId")]
        public string ReferenceID { get; set; }

        [MapTo("FddSourceRefId")]
        public string SourceRefID { get; set; }

        [MapTo("FddSrno")]
        public int? SrNo { get; set; }

        public FormDHeaderResponseDTO FormDHdr { get; set; }

        public AccUccImageDtlResponseDTO AccUccImageDtl { get; set; }

        public WarImageDtlResponseDTO WarImgageDtl { get; set; }

        /// <summary>
        /// /
        /// </summary>


    }
}
