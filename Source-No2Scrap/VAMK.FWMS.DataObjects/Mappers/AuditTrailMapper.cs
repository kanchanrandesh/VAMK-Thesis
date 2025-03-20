using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class AuditTrailMapper : EntityTypeConfiguration<AuditTrail>
    {
        public AuditTrailMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("AuditTrails");

            Property(t => t.EntityType).HasColumnName("EntityType").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.EntityID).HasColumnName("EntityID").HasColumnType("int").IsOptional();
            Property(t => t.EmployeeID).HasColumnName("EmployeeID").HasColumnType("int").IsOptional();
            Property(t => t.Date).HasColumnName("Date").HasColumnType("datetime").IsOptional();
            Property(t => t.Action).HasColumnName("Action").HasColumnType("int").IsOptional();
                        
            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            ////Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.AuditReference);
            Ignore(t => t.State);

            #endregion

            #region Relations

            HasMany(t => t.DetailList).WithOptional(l => l.AuditTrail).HasForeignKey(t => t.AuditTrailID).WillCascadeOnDelete(true);

            #endregion
        }
    }
}
