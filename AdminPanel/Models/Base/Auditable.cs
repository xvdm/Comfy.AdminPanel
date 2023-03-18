namespace AdminPanel.Models.Base
{
    public abstract class Auditable
    {
        public DateTime? CreatedAt { get; set; }
        //public Guid CreatedBy { get; set; }
    }
}
