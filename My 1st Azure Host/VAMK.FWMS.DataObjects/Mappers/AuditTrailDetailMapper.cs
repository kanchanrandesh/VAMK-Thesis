using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class AuditTrailDetailMapper : EntityTypeConfiguration<AuditTrailDetail>
    {
        public AuditTrailDetailMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("AuditTrailDetails");

            Property(t => t.AuditTrailID).HasColumnName("AuditTrailID").HasColumnType("int").IsOptional();
            Property(t => t.EntityType).HasColumnName("EntityType").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.EntityID).HasColumnName("EntityID").HasColumnType("int").IsOptional();
            Property(t => t.Property).HasColumnName("Property").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.PreviousValue).HasColumnName("PreviousValue").HasColumnType("nvarchar").HasMaxLength(500);
            Property(t => t.NewValue).HasColumnName("NewValue").HasColumnType("nvarchar").HasMaxLength(500);
            Property(t => t.Action).HasColumnName("Action").HasColumnType("int").IsOptional();

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.AuditReference);
            Ignore(t => t.State);

            #endregion

            #region Relations


            #endregion
        }
    }
}
