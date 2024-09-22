namespace ProjectsManagement.Models
{
    public class BlockedUser:BaseModel
    {
        public int BlockerID { get; set; } // ID للمستخدم اللي بيعمل Block
        public int BlockedID { get; set; }

        public User Blocker { get; set; } 
        public User Blocked { get; set; }  
    }
}
