using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class EmployeeRecipientMapper : EntityTypeConfiguration<RecipientUser>
    {
        public EmployeeRecipientMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("EmployeeRecipients");
           
            Property(t => t.EmployeeID).HasColumnName("EmployeeID").HasColumnType("int");
            Property(t => t.RecipientID).HasColumnName("RecipientID").HasColumnType("int");

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            //Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion


            #region Relations

            HasOptional(t => t.Recipient).WithMany().HasForeignKey(t => t.RecipientID).WillCascadeOnDelete(false);

            #endregion
        }
    }
}
