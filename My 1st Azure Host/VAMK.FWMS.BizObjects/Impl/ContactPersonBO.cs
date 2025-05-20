using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class ContactPersonBO : BizObjectBase<ContactPerson>, IContactPerson
    {
        #region Vars

        IContactPersonRepository contactRepository;

        #endregion

        public override ContactPerson Create()
        {
            return new ContactPerson();
        }

        #region Property Fileld

        public IContactPersonRepository Repository { get { return contactRepository; } }

        #endregion

        #region Costructor

        public ContactPersonBO() : this(new ContactPersonRepository()) { }

        public ContactPersonBO(IContactPersonRepository repository)
            : base(repository)
        {
            contactRepository = repository;
        }

        #endregion

        #region Search
                  

        /// <summary>
        /// Return list of Contacts for export
        /// </summary>
        /// <returns></returns>
        public IList<ContactPerson> GetAllForExport()
        {
            return Repository.GetAllForExport();
        }

        #endregion

        #region Get All For

        /// <summary>
        /// Return list of Contact Person for an Doners
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public IList<ContactPerson> GetAllFor(Doner doner)
        {
            return Repository.GetAllFor(doner);
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
