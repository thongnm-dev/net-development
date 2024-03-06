namespace Net.Core
{
    public interface IAuditEntity
    {
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
    }
}