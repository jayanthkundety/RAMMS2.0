using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.Common
{
    public class StatusList
    {
        public const string Supervisor = "Supervisor";
        public const string Executive = "Executive";
        public const string HeadMaintenance = "HeadMaintenance";
        public const string JKRSSuperior = "JKRSSuperior";        
        public const string VerifiedJKRSSuperior = "Verified JKRSSuperior";
        public const string ProcessedJKRSSuperior = "Processed JKRSSuperior";
        public const string AgreedJKRSSuperior = "Agreed JKRSSuperior";
        public const string RegionManager = "Region Manager";
        public const string Completed = "Completed";

        public const string N1Init = "Initialize";
        public const string N1Issued = "Issued";
        public const string N1Received = "Received";
        public const string N1CorrectiveCompleted = "Corrective Action Completed";
        public const string N1CorrectiveAccepted = "Corrective Action Accepted";
        //public const string N1Verified = "Verified";

        public const string N2Init = "Initialize";
        public const string N2Issued = "Issued";
        public const string N2Received = "Received";
        public const string N2CorrectiveCompleted = "Corrective Action Completed";
        public const string N2CorrectiveAccepted = "Corrective Action Accepted";
        public const string N2PreventRecurrenceAccepted = "Prevent Recurrence Accepted";

        public const string S1Init = "Initialize";
        public const string S1Planned = "Planned";
        public const string S1Vetted = "Vetted";

        public const string S2Init = "Initialize";
        public const string S2Submitted = "Submitted";
        public const string S2Vetted = "Vetted";

        public const string FormXInit = "Initialize";
        public const string FormXWorkCompleted = "Work Completed";
        public const string FormXVerified= "Verified";
        public const string FormXVetted = "Vetted";

        public const string FormAInit = "Initialize";
        public const string FormAInspected = "Inspected";
        public const string FormAExecutiveApproved = "Executive Approved";
        public const string FormAHeadMaintenanceApproved = "Head Maintenance Approved";        
        public const string FormARegionManagerApproved = "Region Manager Approved";
        //public const string FormAJKRSSuperior = "JKRS Approved";

        public const string FormJInit = "Initialize";
        public const string FormJInspected = "Inspected";
        public const string FormJChecked = "Checked";
        public const string FormJHeadMaintenanceApproved = "Head Maintenance Approved";
        public const string FormJRegionManagerApproved = "Region Manager Approved";

        public const string FormHInit = "Initialize";
        public const string FormHReported = "Reported"; //Opp Executive
        public const string FormHVerified = "Verified"; //Head Maintenance       
        public const string FormHRegionManagerApproved = "Region Manager Approved";
        public const string FormHReceived = "Received";//JKRS
        public const string FormHVetted = "Vetted";//JKRS

        // Missing from old Code

        public const string FormC1C2Init = "Open";
        public const string FormC1C2Inspected = "Inspected";
        public const string FormC1C2ExecutiveApproved = "Executive";
        public const string FormC1C2HeadMaintenanceApproved = "Head Maintenance";
        public const string FormC1C2RegionManagerApproved = "Region Manager";

        public const string FormB1B2Init = "Open";
        public const string FormB1B2Inspected = "Inspected";
        public const string FormB1B2ExecutiveApproved = "Executive";
        public const string FormB1B2HeadMaintenanceApproved = "Head Maintenance";
        public const string FormB1B2RegionManagerApproved = "Region Manager";

        public const string FormF2Init = "Open";
        public const string FormF2Inspected = "Inspected";
        public const string FormF2ExecutiveApproved = "Executive";
        public const string FormF2HeadMaintenanceApproved = "Head Maintenance";
        public const string FormF2RegionManagerApproved = "Region Manager";

        public const string FormF4Init = "Open";
        public const string FormF4Inspected = "Inspected";
        public const string FormF4ExecutiveApproved = "Executive";
        public const string FormF4HeadMaintenanceApproved = "Head Maintenance";
        public const string FormF4RegionManagerApproved = "Region Manager";

        public const string FormF5Init = "Open";
        public const string FormF5Inspected = "Inspected";
        public const string FormF5ExecutiveApproved = "Executive";
        public const string FormF5HeadMaintenanceApproved = "Head Maintenance";
        public const string FormF5RegionManagerApproved = "Region Manager";

        public const string FormFCInit = "Open";
        public const string FormFCInspected = "Inspected";
        public const string FormFCExecutiveApproved = "Executive";
        public const string FormFCHeadMaintenanceApproved = "Head Maintenance";
        public const string FormFCRegionManagerApproved = "Region Manager";

        public const string FormFDInit = "Initialize";
        public const string FormFDInspected = "Inspected";
        public const string FormFDExecutiveApproved = "Executive";
        public const string FormFDHeadMaintenanceApproved = "Head Maintenance";
        public const string FormFDRegionManagerApproved = "Region Manager";

        public const string FormFSInit = "Open";
        public const string FormFSSummarized = "Summarized";
        public const string FormFSHeadMaintenanceApproved = "Executive";
        public const string FormFSRegionManagerApproved = "Region Manager";
    }
}
