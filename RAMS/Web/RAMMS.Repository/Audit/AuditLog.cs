using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RAMMS.Repository.Audit;
using RAMMS.Common;

namespace RAMMS.Repository
{
    public class AuditLog : RAMMS.Domain.Models.RmAuditLogTransaction
    {
        public AuditLog(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }            
        public IList<AuditLogValues> KeyValues { get; } = new List<AuditLogValues>();
        public IList<AuditLogValues> LogValues { get; } = new List<AuditLogValues>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();        
        public bool HasTemporaryProperties => TemporaryProperties.Any();      
        public RAMMS.Domain.Models.RmAuditLogTransaction ToTransaction()
        {
            if (LogValues.Count > 0)
            {
                this.AltTransactinDetails = Utility.JSerialize(LogValues);
                return this;
            }
            else
                return null;
        }
    }
}
