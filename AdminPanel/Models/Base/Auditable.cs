namespace AdminPanel.Models.Base
{
    public abstract class Auditable
    {
        public DateTime? CreatedAt { get; set; }
    }
}
