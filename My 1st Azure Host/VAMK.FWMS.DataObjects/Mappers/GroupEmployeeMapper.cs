using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class GroupEmployeeMapper : EntityTypeConfiguration<GroupUser>
    {
        public GroupEmployeeMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("GroupEmployees");

            Property(t => t.GroupID).HasColumnName("GroupID").HasColumnType("int").IsOptional();
            Property(t => t.EmployeeID).HasColumnName("EmployeeID").HasColumnType("int").IsOptional();

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            //Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.AuditReference);
            Ignore(t => t.State);

            #endregion

            #region Relations

            HasOptional(t => t.Group).WithMany().HasForeignKey(t => t.GroupID).WillCascadeOnDelete(false);
            HasOptional(t => t.Employee).WithMany().HasForeignKey(t => t.EmployeeID).WillCascadeOnDelete(false);

            #endregion
        }
    }
}
