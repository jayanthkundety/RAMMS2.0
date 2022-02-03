using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class AssertImportDescription
    {
        public int row_num { get; set; }
        public string asset_id { get; set; }
    }
    public class ProcessedDescription
    {
        public int is_success { get; set; }
        public int is_failure { get; set; }
        public int is_Inserted { get; set; }

        public int is_Updated { get; set; }
    }

    public class ProcessedIdentification
    {
        public string AI_Asset_ID { get; set; }
        public int AI_PK_Ref_No { get; set; }
    }


}
