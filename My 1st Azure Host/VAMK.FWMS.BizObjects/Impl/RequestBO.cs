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
    public class RequestBO : BizObjectBase<Request>, IRequest
    {
        #region Vars

        IRequestRepository requestRepository;

        #endregion

        public override Request Create()
        {
            return new Request();
        }

        #region Property Fileld

        public IRequestRepository Repository { get { return requestRepository; } }

        #endregion

        #region Costructor

        public RequestBO() : this(new RequestRepository()) { }

        public RequestBO(IRequestRepository repository)
            : base(repository)
        {
            requestRepository = repository;
        }

        #endregion

        #region Search

        /// <summary>
        /// Return list of Request for a given query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<Request> Search(Models.SearchQueries.RequestSearchQuery query)
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
