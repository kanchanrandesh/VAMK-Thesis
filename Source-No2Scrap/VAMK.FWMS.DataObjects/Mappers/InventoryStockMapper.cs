using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    public class InventoryStockMapper : EntityTypeConfiguration<InventoryStock>
    {
        public InventoryStockMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("InventoryStocks");

            Property(t => t.ItemID).HasColumnName("ItemID").HasColumnType("int").IsOptional();
            Property(t => t.Quantity).HasColumnName("Quantity").HasColumnType("decimal").IsOptional();
            Property(t => t.AllocatedQuantity).HasColumnName("AllocatedQuantity").HasColumnType("decimal").IsOptional();

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
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
