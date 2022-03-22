using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormIWResponseDTO
    {
        public string W1RefNo { get; set; }
        public string W2RefNo { get; set; }

        public string WCRefNo { get; set; }
        public string WGRefNo { get; set; }
        public string WDRefNo { get; set; }
        public string WNRefNo { get; set; }

        public string W1Status { get; set; }

        public bool W1SubStatus { get; set; } //Submit Status
        public string W2Status { get; set; }

        public bool W2SubStatus { get; set; } //Submit Status

        public string WCStatus { get; set; }

        public bool WCSubStatus { get; set; } //Submit Status

        public string WGStatus { get; set; }

        public bool WGSubStatus { get; set; } //Submit Status
        public string WDStatus { get; set; }

        public bool WDSubStatus { get; set; } //Submit Status

        public string WNStatus { get; set; }
        public bool WNSubStatus { get; set; } //Submit Status




        //FormW1
        public string iWReferenceNo { get; set; }
        public string projectTitle { get; set; }

        public string initialPropDt { get; set; }
        public string overAllStatus { get; set; }

        public string recommdDEYN { get; set; }

        public string w1dt { get; set; }

        public string recommdYN { get; set; }

        public string estimatedCost { get; set; }

        //FormW2

        public string w2dt { get; set; }

        //FormW2-FECM
        public string tecmDt { get; set; }

        public string fecmDt { get; set; }

        public string agreedNegoYN { get; set; }

        public string agreedNegoPriceDt { get; set; }
        //FormW2
        public string issueW2Ref { get; set; }

        public string commenceDt { get; set; }

        public string compDt { get; set; }

        //FormWD

        public string wdDt { get; set; }

        public string newCompDt { get; set; }

        //FormWN

        public string wnDt { get; set; }

        //FormW2
        public string ContractPeriod { get; set; }

        //FormWC

        public string wcDt { get; set; }

        public string dlpPeriod { get; set; }

        public string finalAmt { get; set; }

        //FormW2 - FECM
        public string sitePhy { get; set; }

        public string wgDate { get; set; }

        public string RMU { get; set; }

        public string ProcessStatus { get; set; }
    }
}
