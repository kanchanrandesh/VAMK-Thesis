using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System;

namespace VAMK.FWMS.DataObjects.Mappers
{
    public class DonationMapper : EntityTypeConfiguration<Donation>
    {

        public DonationMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("Donations");

            Property(t => t.ManualRefNumber).HasColumnName("ManualRefNumber").HasColumnType("nvarchar").HasMaxLength(20);
            Property(t => t.TransacionNumber).HasColumnName("TransacionNumber").HasColumnType("nvarchar").HasMaxLength(20);
            Property(t => t.DonerID).HasColumnName("DonerID").HasColumnType("int");
            Property(t => t.Date).HasColumnName("Date").HasColumnType("datetime");
            Property(t => t.Description).HasColumnName("Description").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.DonationSatus).HasColumnName("DonationSatus").HasColumnType("int").IsOptional();
            Property(t => t.DateCollected).HasColumnName("DateCollected").HasColumnType("datetime").IsOptional();
            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion

            #region Relations
            HasOptional(t => t.Doner).WithMany().HasForeignKey(t => t.DonerID).WillCascadeOnDelete(true);

            HasMany(t => t.DonationItemList).WithOptional(l => l.Donation).HasForeignKey(t => t.DonationID).WillCascadeOnDelete(true);
            #endregion
        }
    }
}
