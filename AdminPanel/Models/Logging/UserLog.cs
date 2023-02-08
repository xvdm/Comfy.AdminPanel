namespace AdminPanel.Models.Logging
{
    public class UserLog
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int LoggingActionId { get; set; }
        public Guid SubjectUserId { get; set; }

        public LoggingAction LoggingAction { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public ApplicationUser SubjectUser { get; set; } = null!;
    }
}
