using VAMK.FWMS.Models.Interfaces;
using System.Data.Entity;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IRepositoryContext
    {
        IDbSet<T> GetDbSet<T>() where T : class, IEntityBase;
        int SaveChanges();
    }
}
