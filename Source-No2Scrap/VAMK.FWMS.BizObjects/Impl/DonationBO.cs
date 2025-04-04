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
    public class DonationBO : BizObjectBase<Donation>, IDonation
    {
        #region Vars

        IDonationRepository donationRepository;

        #endregion

        public override Donation Create()
        {
            return new Donation();
        }

        #region Property Fileld

        public IDonationRepository Repository { get { return donationRepository; } }

        #endregion

        #region Costructor

        public DonationBO() : this(new DonationRepository()) { }

        public DonationBO(IDonationRepository repository)
            : base(repository)
        {
            donationRepository = repository;
        }

        #endregion

        #region Search

        /// <summary>
        /// Return list of Donation for a given query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<Donation> Search(Models.SearchQueries.DonationSearchQuery query)
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
