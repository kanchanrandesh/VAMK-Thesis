using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System;

namespace VAMK.FWMS.DataObjects.Mappers
{
    public class RequestMapper : EntityTypeConfiguration<Request>
    {
        public RequestMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("Requests");

            Property(t => t.ManualRefNumber).HasColumnName("ManualRefNumber").HasColumnType("nvarchar").HasMaxLength(20);
            Property(t => t.TransacionNumber).HasColumnName("TransacionNumber").HasColumnType("nvarchar").HasMaxLength(20);
            Property(t => t.RecipientID).HasColumnName("RecipientID").HasColumnType("int");
            Property(t => t.Date).HasColumnName("Date").HasColumnType("datetime");
            Property(t => t.Description).HasColumnName("Description").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.RequestStatus).HasColumnName("RequestStatus").HasColumnType("int").IsOptional();

            Property(t => t.DateIssued).HasColumnName("DateIssued").HasColumnType("datetime").IsOptional();
            Property(t => t.DateAccepted).HasColumnName("DateAccepted").HasColumnType("datetime").IsOptional();
            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion

            #region Relations
            HasOptional(t => t.Recipient).WithMany().HasForeignKey(t => t.RecipientID).WillCascadeOnDelete(false);

            HasMany(t => t.RequestItemList).WithOptional(l => l.Request).HasForeignKey(t => t.RequestID).WillCascadeOnDelete(true);
            #endregion
        }
    }
}
