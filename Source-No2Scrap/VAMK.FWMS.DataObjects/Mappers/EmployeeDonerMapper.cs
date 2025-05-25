using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class UserDonerMapper : EntityTypeConfiguration<UserDoner>
    {
        public UserDonerMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("EmployeeDoners");
           
            Property(t => t.SystemUserID).HasColumnName("EmployeeID").HasColumnType("int");
            Property(t => t.DonerID).HasColumnName("DonerID").HasColumnType("int");

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            //Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.State);
            Ignore(t => t.AuditReference);

            #endregion


            #region Relations

            HasOptional(t => t.Doner).WithMany().HasForeignKey(t => t.DonerID).WillCascadeOnDelete(false);

            #endregion
        }
    }
}
