using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System;

namespace VAMK.FWMS.DataObjects.Mappers
{
    public class ItemMapper : EntityTypeConfiguration<Item>
    {
        public ItemMapper()
        {
            #region Properties
            HasKey(t => t.ID);
            ToTable("Items");

            Property(t => t.Code).HasColumnName("Description").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.Name).HasColumnName("Name").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.ItemCategory).HasColumnName("ItemCategory").HasColumnType("int").IsOptional();
            Property(t => t.UnitOfMeasurementID).HasColumnName("UnitOfMeasurementID").HasColumnType("int");

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);
            #endregion           
        }
    }
}
