using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class GroupMapper : EntityTypeConfiguration<Group>
    {
        public GroupMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("Groups");

            Property(t => t.Description).HasColumnName("Description").HasColumnType("nvarchar").HasMaxLength(200).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Groups_Description", 1) { IsUnique = true }));
            Property(t => t.SalesFullControl).HasColumnName("SalesFullControl").HasColumnType("bit");
            Property(t => t.IsActive).HasColumnName("IsActive").HasColumnType("bit");

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            //Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.AuditReference);
            Ignore(t => t.State);

            #endregion
        }
    }
}
