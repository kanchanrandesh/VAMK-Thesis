using VAMK.FWMS.BizObjects.Impl;
using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class SentEmailBO : BizObjectBase<SentMail>, ISentEmail
    {
        #region Vars

        ISentMailRepository sentMailRepository;

        #endregion

        #region Property Fileld

        public ISentMailRepository Repository { get { return sentMailRepository; } }

        #endregion

        #region Costructor

        public SentEmailBO() : this(new SentMailRepository()) { }

        public SentEmailBO(ISentMailRepository repository)
            : base(repository)
        {
            sentMailRepository = repository;
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
