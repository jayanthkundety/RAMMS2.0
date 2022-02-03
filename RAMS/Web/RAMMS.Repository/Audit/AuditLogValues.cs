using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.Repository.Audit
{
    public class AuditLogValues
    {
        /// <summary>
        /// Column Name
        /// </summary>
        public string Col { get; set; }
        /// <summary>
        /// Old Values
        /// </summary>
        public object OV { get; set; }
        /// <summary>
        /// New Values
        /// </summary>
        public object NV { get; set; }
    }
}
