using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class UnitMapper : EntityTypeConfiguration<Unit>
    {
        public UnitMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("UnitsOfMeasurement");

            Property(t => t.Code).HasColumnName("Code").HasColumnType("nvarchar").HasMaxLength(20);
            Property(t => t.UnitName).HasColumnName("UnitName").HasColumnType("nvarchar").HasMaxLength(200);
            
            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            //Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion           
        }
    }
}
