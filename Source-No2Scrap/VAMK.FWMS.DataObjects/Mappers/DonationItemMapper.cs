using System;
using System.Collections.Generic;
using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    public class DonationItemMapper : EntityTypeConfiguration<DonationItem>
    {

        public DonationItemMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("DonationItems");

            Property(t => t.DonationID).HasColumnName("DonationID").HasColumnType("int");
            Property(t => t.ItemID).HasColumnName("ItemID").HasColumnType("int");
            Property(t => t.Qty).HasColumnName("Qty").HasColumnType("decimal").HasPrecision(12, 2);
            Property(t => t.ProductCode).HasColumnName("ProductCode").HasColumnType("nvarchar").HasMaxLength(50);// ONLY FOR AUDI REFERENCE

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");

            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion

            #region Relations

            HasOptional(t => t.Donation).WithMany().HasForeignKey(t => t.DonationID).WillCascadeOnDelete(true);
            HasOptional(t => t.Item).WithMany().HasForeignKey(t => t.ItemID).WillCascadeOnDelete(true);

            #endregion
        }
    }
}

