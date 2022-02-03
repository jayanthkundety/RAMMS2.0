using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa2Hdr
    {
        public RmFormQa2Hdr()
        {
            RmFormQa2Dtl = new HashSet<RmFormQa2Dtl>();
        }

        public int FqaiihPkRefNo { get; set; }
        public string FqaiihRefId { get; set; }
        public string FqaiihContNo { get; set; }
        public string FqaiihRoadCode { get; set; }
        public string FqaiihRmu { get; set; }
        public string FqaiihRoadName { get; set; }
        public int? FqaiihMonth { get; set; }
        public int? FqaiihYear { get; set; }
        public string FqaiihSection { get; set; }
        public string FqaiihCrewSup { get; set; }
        public string FqaiihComments { get; set; }
        public string FqaiihSignQaIni { get; set; }
        public int? FqaiihUseridQaIni { get; set; }
        public string FqaiihUsernameQaIni { get; set; }
        public string FqaiihDesignationQaIni { get; set; }
        public string FqaiihRemarksQaIni { get; set; }
        public string FqaiihSignQaI { get; set; }
        public int? FqaiihUseridQaI { get; set; }
        public string FqaiihUsernameQaI { get; set; }
        public string FqaiihDesignationQaI { get; set; }
        public string FqaiihRemarksQaI { get; set; }
        public string FqaiihSignQaIi { get; set; }
        public int? FqaiihUseridQaIi { get; set; }
        public string FqaiihUsernameQaIi { get; set; }
        public string FqaiihDesignationQaIi { get; set; }
        public string FqaiihRemarksQaIi { get; set; }
        public string FqaiihSignQaIii { get; set; }
        public int? FqaiihUseridQaIii { get; set; }
        public string FqaiihUsernameQaIii { get; set; }
        public string FqaiihDesignationQaIii { get; set; }
        public string FqaiihRemarksQaIii { get; set; }
        public string FqaiihSignQaIv { get; set; }
        public int? FqaiihUseridQaIv { get; set; }
        public string FqaiihUsernameQaIv { get; set; }
        public string FqaiihDesignationQaIv { get; set; }
        public string FqaiihRemarksQaIv { get; set; }
        public string FqaiihModBy { get; set; }
        public DateTime? FqaiihModDt { get; set; }
        public string FqaiihCrBy { get; set; }
        public DateTime? FqaiihCrDt { get; set; }
        public bool FqaiihSubmitSts { get; set; }
        public bool? FqaiihActiveYn { get; set; }
        public string FqaiihCrewSupName { get; set; }

        public virtual ICollection<RmFormQa2Dtl> RmFormQa2Dtl { get; set; }
    }
}
