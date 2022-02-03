using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace RAMMS.Common
{
    public class SqlDataset
    {
        public DataSet dataSet { get; set; }
        public Dictionary<string, object> outparam { get; set; }

    }
}
