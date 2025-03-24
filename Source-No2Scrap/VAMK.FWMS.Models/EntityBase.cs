using VAMK.FWMS.Models.Interfaces;
using Newtonsoft.Json;
using System;

namespace VAMK.FWMS.Models
{
    public abstract class EntityBase
    {
        private State _state = State.Unchanged;

        public int? ID { get; set; }
        [JsonIgnore]
        public string User { get; set; }
        public byte[] TimeStamp { get; set; }
        [JsonIgnore]
        public State State { get { return _state; } set { _state = value; } }
        //[JsonIgnore]
        //public int? DomainID { get; set; }
        public DateTime? DateCreated { get; set; }
        [JsonIgnore]
        public DateTime? DateModified { get; set; }
        public string AuditReference { get; set; }
    }
}
