using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class DepartmentBO : BizObjectBase<Department>, IDepartment
    {
        #region Vars

        IDepartmentRepository departmentRepository;

        #endregion

        public override Department Create()
        {
            return new Department();
        }

        #region Property Fileld

        public IDepartmentRepository Repository { get { return departmentRepository; } }

        #endregion

        #region Costructor

        public DepartmentBO() : this(new DepartmentRepository()) { }

        public DepartmentBO(IDepartmentRepository repository)
            : base(repository)
        {
            departmentRepository = repository;
        }

        #endregion

        #region Search

        /// <summary>
        /// Return list of Departments for a given query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<Department> Search(Models.SearchQueries.DepartmentSearchQuery query)
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
