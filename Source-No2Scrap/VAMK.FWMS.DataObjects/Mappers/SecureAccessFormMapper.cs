using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System;

namespace VAMK.FWMS.DataObjects.Mappers
{
    public class SecureAccessFormMapper : EntityTypeConfiguration<SecureAccessForm>
    {
        public SecureAccessFormMapper()
        {
            #region Properties
            HasKey(t => t.ID);
            ToTable("SecureAccessForms");

            Property(t => t.FormCode).HasColumnName("FormCode").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.FormName).HasColumnName("FormName").HasColumnType("nvarchar").HasMaxLength(200);            

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);
            #endregion           
        }
    }
    
}
