namespace ProjectsManagement.Models
{
    public class PasswordChangeRequest : BaseModel
    {
        public int UserID { get; set; }
        public string HashedToken { get; set; }
        public DateTime Time { get; set; }
    }
}
