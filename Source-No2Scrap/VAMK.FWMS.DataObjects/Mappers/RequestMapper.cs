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

            Property(t => t.RecipientD).HasColumnName("RecipientD").HasColumnType("int");
            Property(t => t.Date).HasColumnName("Date").HasColumnType("datetime");
            Property(t => t.Description).HasColumnName("Description").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.RequestStatus).HasColumnName("RequestStatus").HasColumnType("int").IsOptional();

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion

            #region Relations
            HasMany(t => t.RequestItemList).WithOptional(l => l.Request).HasForeignKey(t => t.RequestID).WillCascadeOnDelete(true);
            #endregion
        }
    }
}
