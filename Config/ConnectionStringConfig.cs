namespace WebAutomation.Config
{
    public sealed class ConnectionStringConfig
    {
        public required string NetPayConnectionString { get; set; }
        public required string MainConnectionString { get; set; }
        public required string HRISConnectionString { get; set; }
        public required string PermissionConnectionString { get; set; }
        public required string IdentityServerConnectionString { get; set; }
        public required string NotificationConnectionString { get; set; }
        public required string WorkflowConnectionString { get; set; }
        public required string TaskConnectionString { get; set; }
    }
}
