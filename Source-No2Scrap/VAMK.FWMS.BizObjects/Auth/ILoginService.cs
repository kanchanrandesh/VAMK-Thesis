using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.Models; 

namespace VAMK.FWMS.BizObjects.Auth
{
    public interface ILoginService
    {
        TransferObject<Models.SystemUser> IsUserValid(string user, string password);
        Common.Envelop.TransferObject<bool> IsRuleCodeAllowed(string User, string RuleCode);
    }
}
