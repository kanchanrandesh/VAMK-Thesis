using VAMK.FWMS.Common.Enums;

namespace VAMK.FWMS.Common
{
    public class StatusInfo
    {
        #region -Constructors

        public StatusInfo() : this(ServiceStatus.NotSet) { }
        public StatusInfo(ServiceStatus status) { this.Status = status; }

        #endregion

        #region -Members

        public ServiceStatus Status { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        /// 
        public string Message { get; set; }
        /// <summary>
        /// CodeBase
        /// </summary>
        /// 
        public string CodeBase { get; set; }
        /// <summary>
        /// StackTrace
        /// </summary>
        /// 
        public string StackTrace { get; set; }

        #endregion
    }
}
