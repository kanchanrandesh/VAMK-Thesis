using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;

namespace VAMK.FWMS.DataObjects
{
    public class SequenceNumberRepository : RepositoryBase<SequenceNumber>, ISequenceNumberRepository
    {
        public SequenceNumberRepository() : this(new RepositoryContext()) { }

        public SequenceNumberRepository(IRepositoryContext context) : base(context) { }

        public SequenceNumber GetLastNumber(string type, string prefix)
        {
            return DbSet.FirstOrDefault(t => t.Type == type && t.Prefix == prefix);
        }
    }
}
