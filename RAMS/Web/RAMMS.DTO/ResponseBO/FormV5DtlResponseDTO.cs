using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormV5DtlResponseDTO
    {

        public int Fv5dPkRefNo { get; set; }
        public int? Fv5dFf5hPkRefNo { get; set; }
        public string Fv5dFileNameFrm { get; set; }
        public string Fv5dFileNameTo { get; set; }
        public string Fv5dDesc { get; set; }
        public string Fv5dImageFilenameSys { get; set; }
        public string Fv5dImageFilenameUpload { get; set; }
        public string Fv5dImageUserFilePath { get; set; }
        public int? Fv5dModBy { get; set; }
        public DateTime? Fv5dModDt { get; set; }
        public int? Fv5dCrBy { get; set; }
        public DateTime? Fv5dCrDt { get; set; }
        public bool Fv5dSubmitSts { get; set; }
        public bool Fv5dActiveYn { get; set; }


    }
}
