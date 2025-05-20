using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace VAMK.FWMS.DataObjects
{
    public class GroupEmployeeRepository : RepositoryBase<GroupUser>, IGroupEmployeeRepository
    {
        public GroupEmployeeRepository() : this(new RepositoryContext()) { }

        public GroupEmployeeRepository(IRepositoryContext context) : base(context) { }

        public System.Collections.Generic.IList<GroupUser> GetAllFor(Group groupObj)
        {
            return DbSet.AsQueryable().Include(g=>g.Group).Where(t => t.GroupID == groupObj.ID).ToList();
        }

        public IList<GroupUser> GetUserGroups(int employeeId)
        {
            return DbSet.AsQueryable().Include(g => g.Group).Where(t => t.EmployeeID == employeeId).ToList();
        }

        public GroupUser CheckSalesFullControl(int employeeId)
        {
            return DbSet.FirstOrDefault(t => t.EmployeeID == employeeId && t.Group.SalesFullControl);
        }
    }
}
