namespace ProjectsManagement.ViewModels.Taskss
{
    public class TaskCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectID { get; set; }
        public List<int> UserIDs { get; set; }
    }
}
