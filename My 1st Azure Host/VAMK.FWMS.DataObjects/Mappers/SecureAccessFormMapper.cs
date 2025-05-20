using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System;

namespace VAMK.FWMS.DataObjects.Mappers
{
    public class SecureAccessFormMapper : EntityTypeConfiguration<FormRule>
    {
        public SecureAccessFormMapper()
        {
            #region Properties
            HasKey(t => t.ID);
            ToTable("SecureAccessForms");

            Property(t => t.Code).HasColumnName("Code").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.Description).HasColumnName("Description").HasColumnType("nvarchar").HasMaxLength(200);            

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);
            #endregion           
        }
    }
    
}
