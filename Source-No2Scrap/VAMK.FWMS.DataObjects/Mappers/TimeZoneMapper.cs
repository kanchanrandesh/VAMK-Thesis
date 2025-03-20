using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class TimeZoneMapper : EntityTypeConfiguration<TimeZone>
    {
        public TimeZoneMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("TimeZones");

            Property(t => t.Key).HasColumnName("Key").HasColumnType("nvarchar").HasMaxLength(200).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_SampleDropDowns_Code", 1) { IsUnique = true }));
            Property(t => t.DisplayName).HasColumnName("DisplayName").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.HasDayLightSavingTime).HasColumnName("HasDayLightSavingTime").HasColumnType("bit");

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            //Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion

            #region Relations


            #endregion
        }
    }
}
