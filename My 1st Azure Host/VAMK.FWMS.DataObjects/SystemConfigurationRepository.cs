using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System.Linq;

namespace VAMK.FWMS.DataObjects
{
    public class SystemConfigurationRepository : RepositoryBase<SystemConfiguration>, ISystemConfigurationRepository
    {
        public SystemConfigurationRepository() : this(new RepositoryContext()) { }

        public SystemConfigurationRepository(IRepositoryContext context) : base(context) { }

    }
}
