using VAMK.FWMS.Models;
using System.Data.Entity.ModelConfiguration;

namespace VAMK.FWMS.DataObjects.Mappers
{
    internal class LogMapper : EntityTypeConfiguration<Log>
    {
        public LogMapper()
        {
            #region Properties

            HasKey(t => t.Id);
            ToTable("Logs");

            Property(t => t.Date).HasColumnName("Date").HasColumnType("datetime");
            Property(t => t.Thread).HasColumnName("Thread").HasColumnType("nvarchar").HasMaxLength(255);
            Property(t => t.Level).HasColumnName("Level").HasColumnType("nvarchar").HasMaxLength(50);
            Property(t => t.Logger).HasColumnName("Logger").HasColumnType("nvarchar").HasMaxLength(255);
            Property(t => t.Message).HasColumnName("Message").HasColumnType("nvarchar(max)").IsOptional();
            Property(t => t.Exception).HasColumnName("Exception").HasColumnType("nvarchar(max)").IsOptional();

            #endregion
        }
    }
}
