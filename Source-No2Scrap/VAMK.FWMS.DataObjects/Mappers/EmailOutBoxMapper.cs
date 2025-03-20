using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    public class EmailOutBoxMapper : EntityTypeConfiguration<EmailOutBox>
    {
        public EmailOutBoxMapper()
        {
            #region Properties

            this.HasKey(t => t.ID);
            this.ToTable("EmailOutboxes");

            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID").HasColumnType("int").IsOptional();
            this.Property(t => t.Sender).HasColumnName("Sender").HasColumnType("int");
            this.Property(t => t.SenderDisplayName).HasColumnName("SenderDisplayName").HasColumnType("nvarchar").HasMaxLength(200);
            this.Property(t => t.To).HasColumnName("Recipient").HasColumnType("nvarchar").HasMaxLength(200);
            this.Property(t => t.Bc).HasColumnName("Bc").HasColumnType("nvarchar").HasMaxLength(200);
            this.Property(t => t.Cc).HasColumnName("Cc").HasColumnType("nvarchar").HasMaxLength(200);
            this.Property(t => t.IsBodyHtml).HasColumnName("IsBodyHtml").HasColumnType("bit");
            this.Property(t => t.MailContent).HasColumnName("MailContent").HasColumnType("nvarchar(max)");
            this.Property(t => t.Subject).HasColumnName("Subject").HasColumnType("nvarchar").HasMaxLength(200);
            this.Property(t => t.EmailCreatedDate).HasColumnName("EmailCreatedDate").HasColumnType("datetime").IsOptional();
            this.Property(t => t.EmailStatus).HasColumnName("EmailStatus").HasColumnType("int");
            this.Property(t => t.Note).HasColumnName("Note").HasColumnType("nvarchar").HasMaxLength(500);
            this.Property(t => t.EmailSendAttempt).HasColumnName("EmailSendAttempt").HasColumnType("int");

            this.Property(t => t.User).HasColumnName("UserCreated").HasMaxLength(50);
            //this.Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            this.Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            this.Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.AuditReference);
            Ignore(t => t.State);

            #endregion

            #region Relations

            this.HasOptional(t => t.CreatedBy).WithMany().HasForeignKey(t => t.CreatedByID).WillCascadeOnDelete(false);

            #endregion
        }
    }
}