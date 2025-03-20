using VAMK.FWMS.Models;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class DonerMapper : EntityTypeConfiguration<Doner>
    {
        public DonerMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("Doners");

            Property(t => t.Code).HasColumnName("Code").HasColumnType("nvarchar").HasMaxLength(20);
            Property(t => t.Name).HasColumnName("Name").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.Address).HasColumnName("Address").HasColumnType("nvarchar").HasMaxLength(500);
            Property(t => t.Location).HasColumnName("LocationCoordinates").HasColumnType("nvarchar").HasMaxLength(200);

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion
        }
    }
}
