using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System;
using System.Data.Entity.Infrastructure.Annotations;

namespace VAMK.FWMS.DataObjects.Mappers
{
    public class SystemUserMapper : EntityTypeConfiguration<SystemUser>
    {
        public SystemUserMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("SystemUsers");

            Property(t => t.UserName).HasColumnName("UserName").HasColumnType("nvarchar").HasMaxLength(20).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_SystemUsers_Code", 1) { IsUnique = true }));
            Property(t => t.FirstName).HasColumnName("FirstName").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.LastName).HasColumnName("LastName").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.Phone).HasColumnName("Phone").HasColumnType("nvarchar").HasMaxLength(20);           
            Property(t => t.Email).HasColumnName("Email").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.Password).HasColumnName("Password").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.Designation).HasColumnName("Designation").HasColumnType("nvarchar").HasMaxLength(200);           
            Property(t => t.SystemUserType).HasColumnName("SystemUserTypeID").HasColumnType("int");

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion


            #region Relations
                        

            #endregion
        }
    }
}
