using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class DonerBO : BizObjectBase<Doner>, IDoner
    {
        #region Vars

        IDonerRepository donerRepository;

        #endregion

        public override Doner Create()
        {
            return new Doner();
        }

        #region Property Fileld

        public IDonerRepository Repository { get { return donerRepository; } }

        #endregion

        #region Costructor

        public DonerBO() : this(new DonerRepository()) { }

        public DonerBO(IDonerRepository repository)
            : base(repository)
        {
            donerRepository = repository;
        }

        #endregion

        #region Search

        /// <summary>
        /// Return list of Doner for a given query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<Doner> Search(Models.SearchQueries.DonerSearchQuery query)
        {
            return Repository.Search(query);
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
