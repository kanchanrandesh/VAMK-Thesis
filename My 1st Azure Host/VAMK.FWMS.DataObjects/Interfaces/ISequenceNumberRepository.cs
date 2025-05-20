using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMK.FWMS.Models;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface ISequenceNumberRepository : IRepository<SequenceNumber>
    {
        /// <summary>
        /// Return Last Number record for type and prefix
        /// </summary>
        /// <param name="type"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        SequenceNumber GetLastNumber(string type, string prefix);
    }
}

