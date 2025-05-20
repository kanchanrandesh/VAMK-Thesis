using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace VAMK.FWMS.DataObjects
{
    public class GroupRuleRepository : RepositoryBase<GroupRule>, IGroupRuleRepository
    {
        public GroupRuleRepository() : this(new RepositoryContext()) { }

        public GroupRuleRepository(IRepositoryContext context) : base(context) { }

        public IList<GroupRule> GetAllFor(List<int> groupIds)
        {
            return DbSet.AsQueryable().Include(r => r.Rule).Where(t => groupIds.Any(i => t.GroupID == i)).ToList();
        }

        public System.Collections.Generic.IList<GroupRule> GetAllFor(Group groupObj)
        {
            return DbSet.Where(t => t.GroupID == groupObj.ID).ToList();
        }
    }
}
