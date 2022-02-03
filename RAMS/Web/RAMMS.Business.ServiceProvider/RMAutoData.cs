using System;
using System.Collections.Generic;
using System.Text;
using RAMMS.Common;
///using RAMMS.Common.ServiceProvider;
using RAMMS.Domain.Models;
using System.Threading.Tasks;
using RAMS.Repository;
using Microsoft.Extensions.Logging;
using System.Linq;
namespace RAMMS.Business.ServiceProvider
{
    public class RMAutoData
    {
       // public RmRoadMaster _RMAutopopulate { set; get; }
        public string sDivision { set; get; }
        public string sRUMCode { set; get; }

        public string sSecCode { set; get; }
        public string sSecName { set; get; }

        public string sRoadName { set; get; }
        public string sRoadCode { set; get; }

        public string sChFrom { set; get; }
        public string sChFromDsl { set; get; }
        public string sAssetId { set; get; }
    }
}
