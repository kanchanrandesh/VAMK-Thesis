using VAMK.FWMS.Models;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class SystemConfigurationMapper : EntityTypeConfiguration<SystemConfiguration>
    {
        public SystemConfigurationMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("SystemConfigurations");

            Property(t => t.RegistrationName).HasColumnName("RegistrationName").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.TimeZoneID).HasColumnName("TimeZoneID").HasColumnType("int").IsOptional();           
            Property(t => t.EmailServer).HasColumnName("EmailServer").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.EmailServerPort).HasColumnName("EmailServerPort").HasColumnType("int").IsOptional();
            Property(t => t.EmailSenderGeneral).HasColumnName("EmailSenderGeneral").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.EmailSenderGeneralPassword).HasColumnName("EmailSenderGeneralPassword").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.EmailSenderGeneralDisplayName).HasColumnName("EmailSenderGeneralDisplayName").HasColumnType("nvarchar").HasMaxLength(200);
            

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            //Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion

            #region Relations

            HasOptional(t => t.TimeZone).WithMany().HasForeignKey(t => t.TimeZoneID).WillCascadeOnDelete(false);           

            #endregion
        }
    }
}
