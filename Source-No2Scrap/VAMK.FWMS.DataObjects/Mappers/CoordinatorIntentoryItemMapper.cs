using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    public class CoordinatorIntentoryItemMapper : EntityTypeConfiguration<CoordinatorIntentoryItem>
    {
        public CoordinatorIntentoryItemMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("CoordinatorIntentoryItems");

            Property(t => t.Date).HasColumnName("Number").HasColumnType("datetime").IsOptional();
            Property(t => t.InventoryEffectedby).HasColumnName("InventoryEffectedby").HasColumnType("int").IsOptional();
            Property(t => t.EffectedTransacionID).HasColumnName("EffectedTransacionID").HasColumnType("int").IsOptional();
            Property(t => t.ItemID).HasColumnName("ItemID").HasColumnType("int").IsOptional();
            Property(t => t.EffectedQty).HasColumnName("EffectedQty").HasColumnType("decimal").IsOptional();

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            //Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);
            #endregion

            #region Relations

            HasOptional(t => t.Item).WithMany().HasForeignKey(t => t.ItemID).WillCascadeOnDelete(false);
            #endregion

        }
    }
}
