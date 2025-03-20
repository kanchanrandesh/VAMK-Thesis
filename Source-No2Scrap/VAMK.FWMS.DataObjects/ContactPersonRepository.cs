using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace VAMK.FWMS.DataObjects
{
    public class ContactPersonRepository : RepositoryBase<ContactPerson>, IContactPersonRepository
    {
        public ContactPersonRepository() : this(new RepositoryContext()) { }

        public ContactPersonRepository(IRepositoryContext context) : base(context) { }

        //public IList<ContactPerson> Search(Models.SearchQueries.ContactSearchQuery query)
        //{
        //    if (query == null)
        //        return null;
        //    var queryble = GetQueryable();

        //    if (!string.IsNullOrEmpty(query.SearchText))
        //        queryble = queryble.Where(t => t.Name.Contains(query.SearchText) || t.LastName.Contains(query.SearchText) || t.Department.Contains(query.SearchText) || t.Phone.Contains(query.SearchText) || t.Mobile.Contains(query.SearchText) || t.Title.ToString().Contains(query.SearchText) || t.AccountManager.FirstName.Contains(query.SearchText) || t.AccountManager.LastName.Contains(query.SearchText));

        //    if (!string.IsNullOrEmpty(query.Name))
        //        queryble = queryble.Where(t => t.FirstName.Contains(query.Name) || t.LastName.Contains(query.Name));

        //    if (!string.IsNullOrEmpty(query.Email))
        //        queryble = queryble.Where(t => t.Email == query.Email);

        //    if (query.OrganizationID != null)
        //        queryble = queryble.Where(t => t.OrganizationID == query.OrganizationID);


        //    if (!string.IsNullOrEmpty(query.DesignationCategoryIDList))
        //    {
        //        string idString = query.DesignationCategoryIDList;
        //        if (idString.EndsWith(","))
        //        {
        //            idString = query.DesignationCategoryIDList.Substring(0, query.DesignationCategoryIDList.Length - 1);
        //        }
        //        int[] idArray = idString.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
        //        queryble = queryble.Where(t => idArray.Contains(t.DesignationCategoryID.Value));
        //    }
        //    if (!string.IsNullOrEmpty(query.JobRole))
        //    {
        //        queryble = queryble.Where(t => t.JobRole.Contains(query.JobRole));
        //    }
        //    if (query.AccountManagerID != null)
        //        queryble = queryble.Where(t => t.AccountManagerID == query.AccountManagerID);

        //    return queryble.ToList();
        //}

        // public IList<Contact> SearchNotInvited(Models.SearchQueries.ContactSearchQuery query)
        //{

        //var excludedIDs = new HashSet<int>(participants.Where(t => t.ContactID != null).Select(p => p.ContactID.Value));

        //if (query == null)
        //    return null;
        //var queryble = GetQueryable();

        //queryble = queryble.Where(p => !excludedIDs.Contains(p.ID.Value));

        //if (!string.IsNullOrEmpty(query.SearchText))
        //    queryble = queryble.Where(t => t.FirstName.Contains(query.SearchText) || t.LastName.Contains(query.SearchText) || t.Department.Contains(query.SearchText) || t.Phone.Contains(query.SearchText) || t.Mobile.Contains(query.SearchText) || t.Title.ToString().Contains(query.SearchText) || t.AccountManager.FirstName.Contains(query.SearchText) || t.AccountManager.LastName.Contains(query.SearchText));

        //if (!string.IsNullOrEmpty(query.Name))
        //    queryble = queryble.Where(t => t.FirstName.Contains(query.Name) || t.LastName.Contains(query.Name));

        //if (!string.IsNullOrEmpty(query.Email))
        //    queryble = queryble.Where(t => t.Email == query.Email);

        //if (query.OrganizationID != null)
        //    queryble = queryble.Where(t => t.OrganizationID == query.OrganizationID);

        //if (!string.IsNullOrEmpty(query.DesignationCategoryIDList))
        //{
        //    string idString = query.DesignationCategoryIDList;
        //    if (idString.EndsWith(","))
        //    {
        //        idString = query.DesignationCategoryIDList.Substring(0, query.DesignationCategoryIDList.Length - 1);
        //    }
        //    int[] idArray = idString.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
        //    queryble = queryble.Where(t => idArray.Contains(t.DesignationCategoryID.Value));
        //}

        //if (query.AccountManagerID != null)
        //    queryble = queryble.Where(t => t.AccountManagerID == query.AccountManagerID);


        //return queryble.Include(t => t.AccountManager).ToList();
        //}

        public IList<ContactPerson> GetAllForExport()
        {
            var returnVal = DbSet.Where(t => t.ID != null)                
                .OrderBy(t => t.Code)
                .ThenBy(t => t.Name)
                .ToList();

            return returnVal;
        }
    }
}
