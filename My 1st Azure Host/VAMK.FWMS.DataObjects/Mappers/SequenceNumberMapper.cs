using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using VAMK.FWMS.Models;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class SequenceNumberMapper : EntityTypeConfiguration<SequenceNumber>
    {
        public SequenceNumberMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("SequenceNumbers");

            Property(t => t.Type).HasColumnName("Type").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.Prefix).HasColumnName("Prefix").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.LastNumber).HasColumnName("LastNumber").HasColumnType("int");

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion
        }
    }
}
