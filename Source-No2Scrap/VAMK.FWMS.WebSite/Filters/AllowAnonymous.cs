using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;

namespace VAMK.FWMS.WebSite.Filters
{
    /// <summary>
    /// Extenction for the authorizaton
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AllowAnonymous : Attribute { }

    /// <summary>
    /// Custom Attribute class implementation for Authorization Properties.
    /// </summary>
    ///  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class UserAccessRights : Attribute
    {
        public UserAccessRights() { }

        public UserAccessRights(bool IsAuthorizationrequired, string RuleCode, string RegularExpression, string Comment)
        {
            this._ruleCode = RuleCode;
            this._regularExpression = RegularExpression;
            this._comment = Comment;
            this._isAuthorizationrequired = IsAuthorizationrequired;
        }

        #region Named parameters properties

        /// <summary>
        /// IsAuthorization Required for the perticular Action.
        /// </summary>
        private bool _isAuthorizationrequired;
        public bool IsAuthorizationrequired
        {
            get { return _isAuthorizationrequired; }
        }

        /// <summary>
        /// Rule Code for the perticular Action.
        /// </summary>
        private string _ruleCode;
        public string RuleCode
        {
            get { return _ruleCode; }
        }

        /// <summary>
        /// Regular expression to be provided if required by the
        /// selection process.
        /// </summary>
        private string _regularExpression;
        public string RegularExpression
        {
            get { return _regularExpression; }
        }

        /// <summary>
        /// Comment to be passed to the security provider 
        /// for any logging reqirement.
        /// </summary>
        private string _comment;
        public string Comment
        {
            get { return _comment; }
        }

        #endregion
    }

}