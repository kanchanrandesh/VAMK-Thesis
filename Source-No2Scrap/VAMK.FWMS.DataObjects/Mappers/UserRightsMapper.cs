using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System;

namespace VAMK.FWMS.DataObjects.Mappers
{
    public class UserRightsMapper : EntityTypeConfiguration<UserRights>
    {
        public UserRightsMapper()
        {
            #region Properties
            HasKey(t => t.ID);
            ToTable("UserRights");
                        
            Property(t => t.SystemUserID).HasColumnName("SystemUserID").HasColumnType("int");
            Property(t => t.SecureAccessFormID).HasColumnName("SecureAccessFormID").HasColumnType("int");

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);
            #endregion           
        }
    }
}
