using System;

namespace AQuIP.Admin.Models
{
    public class CreateUserDTO
    {
        public string UserLogin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string Organization { get; set; }

    }

    public class CreateAssetViewerDTO
    {
        public string UserLogin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}