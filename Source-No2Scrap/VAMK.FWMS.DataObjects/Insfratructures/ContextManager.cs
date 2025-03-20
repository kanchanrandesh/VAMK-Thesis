using System.Web;
using InteractivePreGeneratedViews;
using VAMK.FWMS.Common;
using VAMK.FWMS.DataObjects.Context;
using System.Data.Entity.Core.EntityClient;

namespace VAMK.FWMS.DataObjects.Insfratructures
{
    public class ContextManager
    {
        private static string _dataConnectionString = "name=" + ConfigSettings.DB_CONNECTION_STRING;
        public const string DbContexKey = "FWMS_DB_CONTEXT_KEY";

        private static FWMSDbContext _fWMSRepositoryContext;

        public static FWMSDbContext GetDbContext()
        {
            if (_fWMSRepositoryContext == null)
            {
                EntityConnectionStringBuilder ecsb = new EntityConnectionStringBuilder();
                //ecsb.Provider = "System.Data.SqlServerCe.4.0";
                ecsb.Provider = "System.Data.SqlClient";
                ecsb.ProviderConnectionString = _dataConnectionString;

                _fWMSRepositoryContext = new FWMSDbContext(ecsb.ToString());
            }
            return _fWMSRepositoryContext;
        }

        public static FWMSDbContext GetDbContext(string key)
        {
            try
            {
                ///Cache Entity Framework Context Handler in the Database
                using (var ctx = new FWMSDbContext())
                {
                    InteractiveViews
                        .SetViewCacheFactory(
                            ctx,
                            new SqlServerViewCacheFactory(ctx.Database.Connection.ConnectionString));
                }
            }
            catch (System.Exception)
            {

            }

            return new FWMSDbContext(_dataConnectionString);
        }

        public static T AsSingleton<T>() where T : FWMSDbContext, new()
        {
            HttpContext.Current.Items[DbContexKey] = (T)HttpContext.Current.Items[DbContexKey] ?? new T();
            return (T)HttpContext.Current.Items[DbContexKey];
        }
    }
}
