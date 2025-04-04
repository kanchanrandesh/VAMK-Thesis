using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class SequenceNumberBO : BizObjectBase<SequenceNumber>, ISequenceNumber
    {
        #region Vars

        ISequenceNumberRepository sequenceNumberRepository;

        #endregion

        public override SequenceNumber Create()
        {
            return new SequenceNumber();
        }

        #region Property Fileld

        public ISequenceNumberRepository Repository { get { return sequenceNumberRepository; } }

        #endregion

        #region Costructor

        public SequenceNumberBO() : this(new SequenceNumberRepository()) { }

        public SequenceNumberBO(ISequenceNumberRepository repository)
            : base(repository)
        {
            sequenceNumberRepository = repository;
        }

        #endregion

        #region Search

        /// <summary>
        /// Return Last Number record for type and prefix
        /// </summary>
        /// <param name="type"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public SequenceNumber GetLastNumber(string type, string prefix)
        {
            return Repository.GetLastNumber(type, prefix);
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
