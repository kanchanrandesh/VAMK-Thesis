using VAMK.FWMS.DataObjects.Context;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models.Interfaces;
using System.Data.Entity;

namespace VAMK.FWMS.DataObjects.Insfratructures
{
    public class RepositoryContext : IRepositoryContext
    {
        private const string OBJECT_CONTEXT_KEY = "FWMS.Data.ContextKey";

        public FWMSDbContext GetContext { get; private set; }

        public RepositoryContext()
        {
            GetContext = ContextManager.GetDbContext(OBJECT_CONTEXT_KEY);
        }

        public int SaveChanges()
        {
            return GetContext.SaveChanges();
        }

        public IDbSet<T> GetDbSet<T>() where T : class, IEntityBase
        {
            return GetContext.Set<T>();
        }
    }
}
