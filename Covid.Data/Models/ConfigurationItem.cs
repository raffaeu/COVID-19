namespace Covid.Data.Models
{
    public class ConfigurationItem
    {
        public string ConfirmedSource { get; set; }
        public string DeadSource { get; set; }
        public string RecoveredSource { get; set; }
        public string SqlConnection { get; set; }
    }
}