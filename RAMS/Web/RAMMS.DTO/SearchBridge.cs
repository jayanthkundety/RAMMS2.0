using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO
{
    public class SearchBridge
    {
        public string AI_Asset_ID { get; set; }

        public string AI_RMU_Code { get; set; }
        public string AI_Sec_Code { get; set; }
        public string AI_Sec_name { get; set; }
        public string AI_Rd_name { get; set; }
        public string AI_Rd_Code { get; set; }
        public string AI_Loc_CH_KM { get; set; }

        public string AI_Loc_CH_M { get; set; }
        public string AI_Bridge_Name { get; set; }

        public string AI_Grp_Type { get; set; }
        public string AI_Length { get; set; }
        public string AI_Bound { get; set; }
        public string AI_FRM_CH { get; set; }
        public string AI_To_CH { get; set; }
        public string AI_Asset_GRP_Code { get; set; }
        public string SearchInput { get; set; }
        public int AI_PK_Ref_No { get; set; }

    }
}
