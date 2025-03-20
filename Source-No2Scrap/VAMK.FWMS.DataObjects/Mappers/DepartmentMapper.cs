using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class DepartmentMapper : EntityTypeConfiguration<Department>
    {
        public DepartmentMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("Departments");

            Property(t => t.Code).HasColumnName("Code").HasColumnType("nvarchar").HasMaxLength(20);
            Property(t => t.Name).HasColumnName("Name").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.AuthorizedOfficerID).HasColumnName("AuthorizedOfficerID").HasColumnType("int").IsOptional();

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion

            #region Relations

            HasOptional(t => t.AuthorizedOfficer).WithMany().HasForeignKey(t => t.AuthorizedOfficerID).WillCascadeOnDelete(false);

            #endregion
        }
    }
}
