using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RAMMS.Repository.Interfaces;
using RAMMS.Common;

namespace RAMMS.Repository
{
    public class RAMMSContext : RAMSContext
    {
        private readonly IUserContext _context;
        public RAMMSContext(DbContextOptions<RAMSContext> options,IUserContext context) : base(options)
        {
            _context = context;
        }
        public RAMMSContext(DbContextOptions<RAMSContext> options) : base(options)
        {
            
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            List<AuditLog> pkAuditLog = null;
            var action = GetAuditAction();
            if (action != null)
            {
                pkAuditLog = OnBeforeSaveChanges(action);
                RmAuditLogAction.Add(action);
            }
            var result = await base.SaveChangesAsync();
            if (pkAuditLog != null)
            {
                await OnAfterSaveChanges(action, pkAuditLog);
            }

            return result;
        }
        private Domain.Models.RmAuditLogAction GetAuditAction()
        {
            if (!string.IsNullOrEmpty(_context.ActionMessage))
                return new Domain.Models.RmAuditLogAction()
                {
                    AlaActionName = _context.ActionMessage,
                    AlaCrBy = _context.UserID,
                    AlaRequester = _context.UserName,
                    AlaRequestIp = _context.IPAddress,
                    AlaCrDt = DateTime.UtcNow
                };
            else
                return null;
        }
        private List<AuditLog> OnBeforeSaveChanges(Domain.Models.RmAuditLogAction action)
        {
            ChangeTracker.DetectChanges();
            action.RmAuditLogTransaction = new List<RmAuditLogTransaction>();
            var auditEntries = new List<AuditLog>();
            var lstEntry = ChangeTracker.Entries().ToList();
            //foreach (var entry in ChangeTracker.Entries())
            for (int i = lstEntry.Count - 1; i >= 0; i--)
            {
                var entry = lstEntry[i];
                if (entry.Entity is RmAuditLogAction || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditLog(entry);
                auditEntries.Add(auditEntry);
                auditEntry.AltTableName = entry.Metadata.GetTableName();
                if (entry.State == EntityState.Added) { auditEntry.AltTransactionName = "Inserted " + auditEntry.AltTableName; }
                else if (entry.State == EntityState.Modified) { auditEntry.AltTransactionName = "Updated " + auditEntry.AltTableName; }
                else if (entry.State == EntityState.Deleted) { auditEntry.AltTransactionName = "Deleted " + auditEntry.AltTableName; }
                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues.Add(new Audit.AuditLogValues() { Col = propertyName, NV = property.CurrentValue });
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.LogValues.Add(new Audit.AuditLogValues() { Col = propertyName, OV = "", NV = property.CurrentValue }); //.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.LogValues.Add(new Audit.AuditLogValues() { Col = propertyName, OV = property.OriginalValue, NV = "" }); //[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.LogValues.Add(new Audit.AuditLogValues() { Col = propertyName, OV = property.OriginalValue, NV = property.CurrentValue });
                            }
                            break;
                    }
                }
            }

            if (!auditEntries.Where(_ => _.HasTemporaryProperties).Any())
            {
                // Save audit entities that have all the modifications
                foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties && _.LogValues != null && _.LogValues.Count > 0))
                {                    
                    action.RmAuditLogTransaction.Add(auditEntry.ToTransaction());
                }
                return null;
            }
            else
            {
                // keep a list of entries where the value of some properties are unknown at this step
                return auditEntries;
            }
        }

        private Task OnAfterSaveChanges(Domain.Models.RmAuditLogAction action, List<AuditLog> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditLog in auditEntries)
            {
                if (auditLog.HasTemporaryProperties)
                {
                    // Get the final value of the temporary properties
                    foreach (var prop in auditLog.TemporaryProperties)
                    {
                        if (prop.Metadata.IsPrimaryKey())
                        {
                            auditLog.LogValues.Insert(0, new Audit.AuditLogValues() { Col = prop.Metadata.Name, OV = "", NV = prop.CurrentValue });
                        }
                        else
                        {
                            auditLog.LogValues.Add(new Audit.AuditLogValues() { Col = prop.Metadata.Name, OV = "", NV = prop.CurrentValue });
                        }
                    }
                }
                action.RmAuditLogTransaction.Add(auditLog.ToTransaction());
            }

            return base.SaveChangesAsync();
        }
    }
}
