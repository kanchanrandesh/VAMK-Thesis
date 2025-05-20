using VAMK.FWMS.Common.Utility;
using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class EmployeeBO : BizObjectBase<Employee>, IEmployee
    {
        #region Vars

        IEmployeeRepository employeeRepository;

        #endregion

        public override Employee Create()
        {
            return new Employee();
        }

        #region Property Fileld

        public IEmployeeRepository Repository { get { return employeeRepository; } }

        #endregion

        #region Costructor

        public EmployeeBO() : this(new EmployeeRepository()) { }

        public EmployeeBO(IEmployeeRepository repository)
            : base(repository)
        {
            employeeRepository = repository;
        }

        #endregion

        #region Search

        /// <summary>
        /// Return list of Employees for a given query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<Employee> Search(Models.SearchQueries.EmployeeSearchQuery query)
        {
            return Repository.Search(query);
        }
        public Employee ValidateLoginCredential(string username, string password)
        {
            CryptoProvider crypto = new CryptoProvider();
            var user = Repository.GetProxy(c => c.UserName == username);
            if(user != null && user.Password == crypto.GetHash(password) && user.IsActive)
            {
                return user;
            }
            else {
                return null;
            }

        }

        public Employee GetEmployeeByUserName(string username)
        {
            return Repository.GetEmployeeByUserName(username);
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
