namespace VAMK.FWMS.Common.Envelop
{
    public class TransferObject<T>
    {
        public TransferObject() { this.StatusInfo = new StatusInfo(); }
        public TransferObject(T Tval, StatusInfo statusInfo, string hashID)
        {
            this.StatusInfo = statusInfo;
            this.ReturnValue = Tval;
            this.HashID = hashID;
        }
        public StatusInfo StatusInfo { get; set; }
        public T ReturnValue { get; set; }
        public string HashID { get; set; }
    }
}
