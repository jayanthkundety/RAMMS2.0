using System;
using System.Collections.Generic;
using System.Text;


namespace RAMMS.DTO.RequestBO
{
    public class WarImageDtlRequestDTO
    {
        
        public int NO { get; set; }

        
        public int? HeaderId { get; set; }

        
        public int? FxhPkRefNo { get; set; }

        
        public string ImageTypeCode { get; set; }

        
        public int? ImageSrno { get; set; }

        
        public string ImageFilenameSys { get; set; }

        
        public string ImageFilenameUpload { get; set; }

        
        public string ModBy { get; set; }

        
        public DateTime? ModDt { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime? CreatedDt { get; set; }

        
        public bool SubmitSts { get; set; }

        
        public bool? ActiveYn { get; set; }

        
        public string ImageUserFilename { get; set; }
    }
}
