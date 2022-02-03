using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormADetailResponseDTO
    {
       
        public int No { get; set; }

   
        public int HeaderNo { get; set; }
                

        public string AssetId { get; set; }

       
        public string Dt { get; set; }

        
        public int? Srno { get; set; }

       
        public string SiteRef { get; set; }

        
        public int? FromCh { get; set; }

        
        public int? FromChDeci { get; set; }

       
        public int? ToCh { get; set; }

       
        public int? ToChDeci { get; set; }

        

       
        public string DefCode { get; set; }

        
        public string ActCode { get; set; }

      
        public string Unit { get; set; }

        public double? Length { get; set; }

       
        public double? Width { get; set; }

        
        public double? Height { get; set; }

       
        public string Adp { get; set; }

       
        public string Cdr { get; set; }

       
        public string Priority { get; set; }

       
        public int? Wi { get; set; }

        
        public int? Ws { get; set; }

       
        public int? Wtc { get; set; }

      
        public int? Wc { get; set; }

        public string SftPs { get; set; }

        public int? SftWis { get; set; }

        
        public int? Rt { get; set; }

      
        public string Remarks { get; set; }

    
        public string FormhApp { get; set; }

     
        public string ModBy { get; set; }

       
        public string ModDt { get; set; }

      
        public string CreatedBy { get; set; }

      
        public string CreatedDt { get; set; }

        
        public bool SubmitSts { get; set; }

        
        public bool? ActiveYn { get; set; }

      
        public string Desc { get; set; }

        public string FadRefNO { get; set; }

        public List<string> SiteRef_multiSelect { get; set; }

        public string CallFrom { get; set; }

        public string HeaderRefNo { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
    }
}
