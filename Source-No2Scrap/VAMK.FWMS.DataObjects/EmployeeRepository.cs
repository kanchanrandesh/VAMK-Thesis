using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace VAMK.FWMS.DataObjects
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository() : this(new RepositoryContext()) { }

        public EmployeeRepository(IRepositoryContext context) : base(context) { }

        public IList<Employee> Search(Models.SearchQueries.EmployeeSearchQuery query)
        {
            if (query == null)
                return null;
            var queryble = GetQueryable();

            if (!string.IsNullOrEmpty(query.SearchText))
                queryble = queryble.Where(t => t.Code.Contains(query.SearchText) || t.FirstName.Contains(query.SearchText) || t.LastName.Contains(query.SearchText) || t.Phone.Contains(query.SearchText) || t.UserName.Contains(query.SearchText));

            if (!string.IsNullOrEmpty(query.Code))
                queryble = queryble.Where(t => t.Code == query.Code);

            if (!string.IsNullOrEmpty(query.Name))
                queryble = queryble.Where(t => t.FirstName.Contains(query.Name) || t.LastName.Contains(query.Name));

            if (!string.IsNullOrEmpty(query.UserName))
                queryble = queryble.Where(t => t.UserName == query.UserName);

            if (query.IsActive != null)
                queryble = queryble.Where(t => t.IsActive == query.IsActive);

            if (query.Company != null)
                queryble = queryble.Where(t => t.CompanyID == query.Company.ID);

            if (query.CompanyID != null)
                queryble = queryble.Where(t => t.CompanyID == query.CompanyID);

            return queryble.OrderBy(t => t.FirstName).ToList();
        }

        public override Employee GetSingle(System.Linq.Expressions.Expression<System.Func<Employee, bool>> whereCondition)
        {
            var returnVal = this.DbSet.Where(whereCondition)
                .Include(i => i.EmployeeDoners).Include(i => i.EmployeeRecipients)
                .FirstOrDefault();
            return returnVal;
        }

        public Employee GetEmployeeByUserName(string userName)
        {
            return this.DbSet.FirstOrDefault(t => t.UserName == userName);
        }
    }
}
