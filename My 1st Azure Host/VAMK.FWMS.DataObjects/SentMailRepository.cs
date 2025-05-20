using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;

namespace VAMK.FWMS.DataObjects
{
    public class SentMailRepository : RepositoryBase<SentMail>, ISentMailRepository
    {
        public SentMailRepository() : this(new RepositoryContext()) { }

        public SentMailRepository(IRepositoryContext context) : base(context) { }
    }
}
