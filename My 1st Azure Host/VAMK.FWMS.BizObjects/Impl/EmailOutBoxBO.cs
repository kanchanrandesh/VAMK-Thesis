using VAMK.FWMS.BizObjects.Impl;
using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects
{
    public class EmailOutBoxBO : BizObjectBase<EmailOutBox>, IEmailOutBox
    {
        #region Vars

        IEmailOutBoxRepository emilOutBoxRepository;

        #endregion

        public override EmailOutBox Create()
        {
            return new EmailOutBox();
        }

        #region Property Fileld

        public IEmailOutBoxRepository Repository { get { return emilOutBoxRepository; } }

        #endregion

        #region Costructor

        public EmailOutBoxBO() : this(new EmailOutBoxRepository()) { }

        public EmailOutBoxBO(IEmailOutBoxRepository repository)
            : base(repository)
        {
            emilOutBoxRepository = repository;
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
