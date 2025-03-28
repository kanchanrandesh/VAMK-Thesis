using VAMK.FWMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class ContactPersonMapper : EntityTypeConfiguration<ContactPerson>
    {
        public ContactPersonMapper()
        {
            #region Properties

            HasKey(t => t.ID);
            ToTable("ContactPersons");

            Property(t => t.DonerID).HasColumnName("DonerID").HasColumnType("int").IsOptional(); 
            Property(t => t.Code).HasColumnName("Code").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.Name).HasColumnName("Name").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.PhoneNumber).HasColumnName("PhoneNumber").HasColumnType("nvarchar").HasMaxLength(20);
            Property(t => t.Mobile).HasColumnName("Mobile").HasColumnType("nvarchar").HasMaxLength(20);

            Property(t => t.Email).HasColumnName("Email").HasColumnType("nvarchar").HasMaxLength(200);
            Property(t => t.IsDoner).HasColumnName("IsDoner").HasColumnType("bit"); 
            Property(t => t.RecipientID).HasColumnName("RecipientID").HasColumnType("int").IsOptional(); 

            Property(t => t.User).HasColumnName("UserCreated").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.DateCreated).HasColumnName("DateCreated").HasColumnType("datetime");
            Property(t => t.DateModified).HasColumnName("DateModified").HasColumnType("datetime");
            Ignore(t => t.AuditReference);
            Ignore(t => t.State);

            #endregion

            #region Relations
           
            #endregion
        }
    }
}
