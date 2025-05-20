using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class CompanyMapper : EntityTypeConfiguration<Company>
    {
        public CompanyMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("Companies");

            Property(t => t.Code).HasColumnName("Code").HasColumnType("nvarchar").HasMaxLength(20).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Companies_Code", 1) { IsUnique = true }));
            Property(t => t.PrefixCode).HasColumnName("PrefixCode").HasColumnType("nvarchar").HasMaxLength(2);
            Property(t => t.Name).HasColumnName("Name").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.AddressLine1).HasColumnName("AddressLine1").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.AddressLine2).HasColumnName("AddressLine2").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.AddressLine3).HasColumnName("AddressLine3").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.Phone1).HasColumnName("Phone1").HasColumnType("nvarchar").HasMaxLength(20);
            Property(t => t.Phone2).HasColumnName("Phone2").HasColumnType("nvarchar").HasMaxLength(20);
            Property(t => t.Email).HasColumnName("Email").HasColumnType("nvarchar").HasMaxLength(200);            

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.AuditReference);
            Ignore(t => t.State);
           

            #endregion

           
        }
    }
}
