using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;

namespace VAMK.FWMS.DataObjects
{
    public class EmailOutBoxRepository : RepositoryBase<EmailOutBox> , IEmailOutBoxRepository
    {
        public EmailOutBoxRepository() : this(new RepositoryContext()) { }

        public EmailOutBoxRepository(IRepositoryContext context) : base(context) { }

    }
}
