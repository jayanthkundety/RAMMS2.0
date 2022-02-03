using System;
using System.Collections.Generic;


namespace RAMMS.DTO.RequestBO
{
    public class FormDDetailsRequestDTO
    {
        
        public int No { get; set; }

        
        public int? FormDHeaderNo { get; set; }

        
        public string RoadCode { get; set; }

        
        public int? FrmCh { get; set; }

        
        public int? FrmChDeci { get; set; }

        
        public int? ToCh { get; set; }

        
        public int? ToChDeci { get; set; }

        
        public string SiteRef { get; set; }

        
        public int? ActCode { get; set; }

        
        public string TimeArr { get; set; }

        
        public string TimeDep { get; set; }

        
        public int? Length { get; set; }

        
        public int? Width { get; set; }

        
        public int? Height { get; set; }

        
        public string Unit { get; set; }

        
        public int? ProdQty { get; set; }

        
        public string ProdUnit { get; set; }

        
        public string WorkSts { get; set; }

        
        public string GenRemarks { get; set; }

        
        public string Remarks { get; set; }

        
        public string ModifeidBy { get; set; }

        
        public DateTime? ModifiedDate { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime? CreatedDate { get; set; }

        
        public bool SubmitSts { get; set; }

        
        public bool? ActiveYn { get; set; }
        
        public string SourceType { get; set; }
        
        public int? FormXPKRefNo { get; set; }
        
        public string ReferenceID { get; set; }

        
        public string SourceRefID { get; set; }

        public int? SrNo { get; set; }

        public FormDHeaderRequestDTO FormDHdr { get; set; }

        public AccUccImageDtlRequestDTO AccUccImageDtl { get; set; }

        public WarImageDtlRequestDTO WarImgageDtl { get; set; }

        /// <summary>
        /// /
        /// </summary>


    }
}
