namespace ProjectsManagement.Models
{
    public class Picture : BaseModel
    {
        public string Title { get; set; }
        public string URL { get; set; }

        public User User { get; set; }
    }
}
