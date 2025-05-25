using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class SystemUserMapper : EntityTypeConfiguration<SystemUser>
    {
        public SystemUserMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("SystemUser");

            Property(t => t.Code).HasColumnName("Code").HasColumnType("nvarchar").HasMaxLength(20).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Users_Code", 1) { IsUnique = true }));
            Property(t => t.FirstName).HasColumnName("FirstName").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.LastName).HasColumnName("LastName").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.Phone).HasColumnName("Phone").HasColumnType("nvarchar").HasMaxLength(20);
            Property(t => t.Mobile).HasColumnName("Mobile").HasColumnType("nvarchar").HasMaxLength(20);
            Property(t => t.Email).HasColumnName("Email").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.UserName).HasColumnName("UserName").HasColumnType("nvarchar").HasMaxLength(200).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Users_UserName", 1) { IsUnique = true }));
            Property(t => t.Password).HasColumnName("Password").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.Designation).HasColumnName("Designation").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.DateOfBirth).HasColumnName("DateOfBirth").HasColumnType("datetime").IsOptional();
            Property(t => t.IsActive).HasColumnName("IsActive").HasColumnType("bit");

            Property(t => t.IsLocked).HasColumnName("IsLocked").HasColumnType("bit");
            Property(t => t.UnSuccessfulLoginAttempts).HasColumnName("UnSuccessfulLoginAttempts").HasColumnType("int").IsOptional();
            Property(t => t.PasswordResetDate).HasColumnName("PasswordResetDate").HasColumnType("datetime").IsOptional();
            Property(t => t.IsDoner).HasColumnName("IsDoner").HasColumnType("bit");
            Property(t => t.IsRecipient).HasColumnName("IsRecipient").HasColumnType("bit");

            Property(t => t.UserName).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            //Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion


            #region Relations          

            HasMany(t => t.UserDoners).WithOptional(l => l.SystemUser).HasForeignKey(t => t.SystemUserID).WillCascadeOnDelete(true);
            HasMany(t => t.UserRecipients).WithOptional(l => l.SystemUser).HasForeignKey(t => t.SystemUserID).WillCascadeOnDelete(true);
            #endregion
        }
    }
}
