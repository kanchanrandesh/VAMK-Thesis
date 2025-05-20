using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System;

namespace VAMK.FWMS.DataObjects.Mappers
{
    public class RequestItemMapper : EntityTypeConfiguration<RequestItem>
    {

        public RequestItemMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("RequestItems");

            Property(t => t.RequestID).HasColumnName("RequestID").HasColumnType("int");
            Property(t => t.ItemID).HasColumnName("ItemID").HasColumnType("int");
            Property(t => t.RequestedQty).HasColumnName("RequestQty").HasColumnType("decimal").HasPrecision(12, 2);
            Property(t => t.AllocatedQty).HasColumnName("AllocatedQty").HasColumnType("decimal").HasPrecision(12, 2);
            Property(t => t.IsFullfilled).HasColumnName("IsFullfilled").HasColumnType("bit");            

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion

            #region Relations

            HasOptional(t => t.Request).WithMany().HasForeignKey(t => t.RequestID).WillCascadeOnDelete(true);
            HasOptional(t => t.Item).WithMany().HasForeignKey(t => t.ItemID).WillCascadeOnDelete(true);
            #endregion
        }
    }   
}
