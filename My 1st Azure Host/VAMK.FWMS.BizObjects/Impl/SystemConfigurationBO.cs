using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class SystemConfigurationBO : BizObjectBase<Models.SystemConfiguration>, ISystemConfiguration
    {
        #region Vars

        ISystemConfigurationRepository systemConfigurationRepository;

        #endregion

        public override Models.SystemConfiguration Create()
        {
            return new Models.SystemConfiguration();
        }

        #region Property Fileld

        public ISystemConfigurationRepository Repository { get { return systemConfigurationRepository; } }

        #endregion

        #region Costructor

        public SystemConfigurationBO() : this(new SystemConfigurationRepository()) { }

        public SystemConfigurationBO(ISystemConfigurationRepository repository)
            : base(repository)
        {
            systemConfigurationRepository = repository;
        }

        #endregion

        #region Object Type

        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        #endregion
    }
}
