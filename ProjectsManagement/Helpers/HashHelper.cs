using Azure.Core;
using ProjectsManagement.Models;

namespace ProjectsManagement.Helpers
{
    public class HashHelper
    {
        public static string CreateHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool CheckHash(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
