using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormIWSearchGridDTO
    {

        public string ReferenceNo { get; set; }

        public string ProjectTitle { get; set; }

        public string InitialPropDt { get; set; }

        public string RecommdDE { get; set; }

        public string FormType { get; set; }    
        public string W1dt { get; set; }

        public string Recommd { get; set; }

        public string Status { get; set; }

        public DateTime? TechnicalDt { get; set; }

        public DateTime? FinanceDt { get; set; }

        public string AgreedNego { get; set; }

        public string IssueW2Ref { get; set; }


        public DateTime? CommenDt { get; set; }

        public DateTime? CompDt { get; set; }

        public string ContractPeriod { get; set; }

        public string DlpPeriod { get; set; }

        public string FinalAmt { get; set; }

        public string SitePhy { get; set; }

        //Filter Section
        public string RoadCode { get; set; }

        public string RMU { get; set; }

        public string CommencementFrom { get; set; }

        public string CommencementTo { get; set; }

        public double? PercentageFrom { get; set; }

        public double? PercentageTo { get; set; }

        public int? Months { get; set; }

        public string IWRefNo { get; set; }

        public string PrjTitle { get; set; }

        public string TECMStatus { get; set; }

        public string FECMStatus { get; set; }

        public string SmartInputValue { get; set; }
        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }

    }
}
