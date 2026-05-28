using Microsoft.EntityFrameworkCore;
using ThoughtFocus.DataAccess.Models.Audit; 
using System.Collections.Generic;
using aThoughtFocus.DataAccess.Models.Audit;
using Microsoft.Data.SqlClient;

namespace ThoughtFocus.DataAccess
{
    public class ApplicationAuditDBContext : DbContext
    {
        public ApplicationAuditDBContext(DbContextOptions options) : base(options)
        {
            AuditLogs = Set<AuditTrail>();
        }
        public virtual void SaveChanges(long? userId = null)
        {
            try
            {
                OnBeforeSaveChanges(userId);
                base.SaveChanges();
            }
            catch (SqlException ex)
            {
                throw new System.Exception("", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("", ex);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("", ex);
            }
        }
        public DbSet<AuditTrail> AuditLogs { get; set; }
        public DbSet<AuditSessionDetails> AuditSessionDetails { get; set; }

        private void OnBeforeSaveChanges(long? userId)
        {
            try
            {
                ChangeTracker.DetectChanges();
                var auditEntries = new List<AuditEntry>();
                foreach (var entry in ChangeTracker.Entries())
                {
                    if (entry.Entity is AuditTrail || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                        continue;
                    var auditEntry = new AuditEntry(entry);
                    auditEntry.TableName = entry.Entity.GetType().Name;
                    auditEntry.UserId = userId;
                    auditEntries.Add(auditEntry);
                    foreach (var property in entry.Properties)
                    {
                        string propertyName = property.Metadata.Name;
                        if (property.Metadata.IsPrimaryKey())
                        {
                            auditEntry.KeyValues[propertyName] = property.CurrentValue;
                            continue;
                        }

                        switch (entry.State)
                        {
                            case EntityState.Added:
                                auditEntry.AuditType = AuditType.Create;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                break;

                            case EntityState.Deleted:
                                auditEntry.AuditType = AuditType.Delete;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                break;

                            case EntityState.Modified:
                                if (property.IsModified)
                                {
                                    auditEntry.ChangedColumns.Add(propertyName);
                                    auditEntry.AuditType = AuditType.Update;
                                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                                }
                                break;
                        }
                    }
                }

                foreach (var auditEntry in auditEntries)
                {
                    AuditLogs.Add(auditEntry.ToAudit());
                }

            }

            catch (SqlException ex)
            {
                throw new System.Exception("", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("", ex);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("", ex);
            }
        }

    }
}


