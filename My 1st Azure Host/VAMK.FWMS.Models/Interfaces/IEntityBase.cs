using System;

namespace VAMK.FWMS.Models.Interfaces
{
    public interface IEntityBase
    {
        int? ID { get; set; }
        string User { get; set; }
        byte[] TimeStamp { get; set; }
        State State { get; set; }
        //int? DomainID { get; set; }
        DateTime? DateCreated { get; set; }
        DateTime? DateModified { get; set; }
        string AuditReference { get; set; }
    }

    public enum State
    {
        Added,
        Unchanged,
        Modified,
        Deleted
    }
}
