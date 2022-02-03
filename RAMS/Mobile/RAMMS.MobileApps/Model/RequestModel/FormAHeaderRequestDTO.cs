using System;
using System.Collections.Generic;

namespace RAMMS.DTO.RequestBO
{
    public class FormAHeaderRequestDTO
    {
        public FormAHeaderRequestDTO()
        {
            FormADetails = new List<FormADetailsRequestDTO>();
        }


        public string SmartInputValue { get; set; }


        public int No { get; set; }


        public string Id { get; set; }


        public string RoadCode { get; set; }


        public string Rmu { get; set; }


        public string RoadName { get; set; }


        public string ContNo { get; set; }


        public string AssetGroupCode { get; set; }


        public int? Month { get; set; }

        public int? Year { get; set; }

        public string Remarks { get; set; }


        public string SignPrp { get; set; }


        public int? UseridPrp { get; set; }


        public string UsernamePrp { get; set; }


        public string DesignationPrp { get; set; }


        public DateTime? DtPrp { get; set; }


        public string SignVer { get; set; }


        public int? UseridVer { get; set; }

        public string UsernameVer { get; set; }


        public string DesignationVer { get; set; }


        public DateTime? VerifiedDt { get; set; }


        public string ModBy { get; set; }


        public DateTime? ModDt { get; set; }


        public string CreatedBy { get; set; }


        public DateTime? CreatedDt { get; set; }


        public bool SubmitSts { get; set; }
       
        public bool? ActiveYn { get; set; }


        public string section { get; set; }
        public List<FormADetailsRequestDTO> FormADetails { get; set; }

        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }

    }


}
