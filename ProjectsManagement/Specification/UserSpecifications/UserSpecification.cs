using ProjectsManagement.Models;

namespace ProjectsManagement.Specification.UserSpecifications
{
    public class UserSpecification:BaseSpecification<User>
    {
        public UserSpecification(string email)
        {
            Criteria = user => user.Email == email;
        }
        public UserSpecification(string email, string username)
        {
            Criteria = user => user.Email == email || user.UserName == username;
        }
    }
}
