using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class EmployeeMapper : EntityTypeConfiguration<Employee>
    {
        public EmployeeMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("Employees");

            Property(t => t.Title).HasColumnName("Title").HasColumnType("int").IsOptional();
            Property(t => t.Code).HasColumnName("Code").HasColumnType("nvarchar").HasMaxLength(20).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Employees_Code", 1) { IsUnique = true }));
            Property(t => t.FirstName).HasColumnName("FirstName").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.LastName).HasColumnName("LastName").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.Phone).HasColumnName("Phone").HasColumnType("nvarchar").HasMaxLength(20);
            Property(t => t.Mobile).HasColumnName("Mobile").HasColumnType("nvarchar").HasMaxLength(20);
            Property(t => t.Email).HasColumnName("Email").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.UserName).HasColumnName("UserName").HasColumnType("nvarchar").HasMaxLength(200).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Employees_UserName", 1) { IsUnique = true }));
            Property(t => t.Password).HasColumnName("Password").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.CompanyID).HasColumnName("CompanyID").HasColumnType("int").IsOptional();
            Property(t => t.Designation).HasColumnName("Designation").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.DateOfBirth).HasColumnName("DateOfBirth").HasColumnType("datetime").IsOptional();
            Property(t => t.IsActive).HasColumnName("IsActive").HasColumnType("bit");

            Property(t => t.IsLocked).HasColumnName("IsLocked").HasColumnType("bit");
            Property(t => t.UnSuccessfulLoginAttempts).HasColumnName("UnSuccessfulLoginAttempts").HasColumnType("int").IsOptional();
            Property(t => t.PasswordResetDate).HasColumnName("PasswordResetDate").HasColumnType("datetime").IsOptional();
            Property(t => t.IsDoner).HasColumnName("IsRelationshipManager").HasColumnType("bit");
            Property(t => t.IsRecipient).HasColumnName("IsSalesManager").HasColumnType("bit");
            Property(t => t.IsSalesEngineer).HasColumnName("IsSalesEngineer").HasColumnType("bit");
            Property(t => t.IsPreSaleEngineer).HasColumnName("IsPreSaleEngineer").HasColumnType("bit");
            Property(t => t.IsProjectManager).HasColumnName("IsProjectManager").HasColumnType("bit");
            Property(t => t.IsBizDeveloper).HasColumnName("IsBizDeveloper").HasColumnType("bit");
            Property(t => t.IsTechnicalPerson).HasColumnName("IsTechnicalPerson").HasColumnType("bit");
            Property(t => t.IsLeagalOfficer).HasColumnName("IsLeagalOfficer").HasColumnType("bit");

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            //Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion


            #region Relations

            HasOptional(t => t.Company).WithMany().HasForeignKey(t => t.CompanyID).WillCascadeOnDelete(false);

            #endregion
        }
    }
}
